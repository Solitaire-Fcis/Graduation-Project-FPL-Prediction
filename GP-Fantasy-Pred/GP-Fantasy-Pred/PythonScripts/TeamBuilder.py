# Select a team for a given gameweek
import pandas as pd
import pulp
import numpy as np
from pulp import PULP_CBC_CMD

'''
#  Runs selection over a range of gameweeks
def select_range(start_gw, end_gw, data_in):
    total_error = 0
    points = 0
    real_points = 0
    teams_list = []
    captains_list = []
    subs_list = []
    cal_points_list = []
    for gw in range(start_gw, end_gw):
        predicted_score, real_score, error, first_team, captain, subs, cal_points = select(gw, data_in, False)
        print('GW' + str(gw) + '---------------------------')
        print('Predicted Score : ' + str(round(predicted_score, 2)) + ' Points')
        print('Real Score      : ' + str(real_score) + '.00 Points')
        total_error += abs(error)
        points += predicted_score
        real_points += real_score
        teams_list.append(first_team)
        captains_list.append(captain)
        subs_list.append(subs)
        cal_points_list.append(cal_points)

    print('\nPredicted       ' + str(round(points, 2)))
    print('Real            ' + str(round(real_points, 2)))
    print('Total Error     ' + str(round(total_error, 2)))
    print('Average Error   ' + str(round(total_error / (end_gw - start_gw), 2)))
    return predicted_score, real_score, error, teams_list, captains_list, subs_list, cal_points_list
'''

def select(gw, budget, data_in, print_output=False):
    sub_factor = 0.1
    data_in = data_in[data_in.GW == gw]
    first_team, captain, subs, cal_points = select_team(data_in, budget, sub_factor)
    real_points_total = 0
    predicted_points_total = 0
    total_cost = 0
    team_list = []
    subs_list = []
    captain_name = ''
    for i in range(data_in.shape[0]):
        if captain[i].value() != 0:
            team_list.append(ret_player(data_in.iloc[i]))
            captain_name = data_in.iloc[i].name
            predicted_points_total += (data_in.iloc[i].predicted * 2)
            real_points_total += (data_in.iloc[i].total_points * 2)
            total_cost += data_in.iloc[i].value
        elif first_team[i].value() != 0:
            team_list.append(ret_player(data_in.iloc[i]))
            predicted_points_total += data_in.iloc[i].predicted
            real_points_total += data_in.iloc[i].total_points
            total_cost += data_in.iloc[i].value
    sub_points = 0
    for i in range(data_in.shape[0]):
        if subs[i].value() != 0:
            subs_list.append(ret_player(data_in.iloc[i]))
            total_cost += data_in.iloc[i].value
    error = abs(real_points_total - predicted_points_total)
    return predicted_points_total, real_points_total, total_cost, team_list, captain_name, subs_list, error

def ret_player(player):
    return player['name']+' '+str(int(player['position']))+' '+str(int(player['predicted']))

def select_team(player_data, budget, sub_factor):
    num_players = len(player_data)
    model = pulp.LpProblem("Constrained_value_maximisation", pulp.LpMaximize)
    # Array to store players selected for the starting team
    decisions = [pulp.LpVariable("x{}".format(i), lowBound=0, upBound=1, cat='Integer') for i in range(num_players)]
    # Array to captain decision
    captain_decisions = [pulp.LpVariable("y{}".format(i), lowBound=0, upBound=1, cat='Integer') for i in range(num_players)]
    # Array to store sub decisions
    sub_decisions = [pulp.LpVariable("z{}".format(i), lowBound=0, upBound=1, cat='Integer') for i in range(num_players)]
    # objective function
    model += sum((captain_decisions[i] + decisions[i] + sub_decisions[i] * sub_factor) * player_data.iloc[i].predicted for i in range(num_players)), "Objective"
    # cost constraint
    model += sum((decisions[i] + sub_decisions[i]) * (player_data.iloc[i].value) for i in range(num_players)) <= budget  # total cost
    # position constraints
    # 1 starting goalkeeper
    model += sum(decisions[i] for i in range(num_players) if player_data.iloc[i].position == 1) == 1
    # 2 total goalkeepers
    model += sum(decisions[i] + sub_decisions[i] for i in range(num_players) if player_data.iloc[i].position == 1) == 2
    # Select the starting defenders
    # Must be between 3 and 5 starting defenders
    model += sum(decisions[i] for i in range(num_players) if player_data.iloc[i].position == 2) >= 3
    model += sum(decisions[i] for i in range(num_players) if player_data.iloc[i].position == 2) <= 5
    # Select all defenders
    # Must be 5 defenders selected
    model += sum(decisions[i] + sub_decisions[i] for i in range(num_players) if player_data.iloc[i].position == 2) == 5
    # Select midfielders
    # Must be between 3 and 5 starting midfielders selected
    model += sum(decisions[i] for i in range(num_players) if player_data.iloc[i].position == 3) >= 3
    model += sum(decisions[i] for i in range(num_players) if player_data.iloc[i].position == 3) <= 5
    # 5 all midfielders
    # Must be 5 midfielders selected
    model += sum(decisions[i] + sub_decisions[i] for i in range(num_players) if player_data.iloc[i].position == 3) == 5
    # Select forwards
    # Must be between 1 and 3 starting forwards
    model += sum(decisions[i] for i in range(num_players) if player_data.iloc[i].position == 4) >= 1
    model += sum(decisions[i] for i in range(num_players) if player_data.iloc[i].position == 4) <= 3
    # Must be 3 forwards selected
    model += sum(decisions[i] + sub_decisions[i] for i in range(num_players) if player_data.iloc[i].position == 4) == 3
    # Only 3 players can be selected from a single club
    team_codes = np.unique(player_data.opponent_team)
    for team_id in np.unique(team_codes):
        model += sum(decisions[i] + sub_decisions[i] for i in range(num_players) if player_data.iloc[i].opponent_team == team_id) <= 3  # max 3 players
    # 11 starting players must be selected
    model += sum(decisions) == 11
    # 1 of the starting players must be selected as captain
    model += sum(captain_decisions) == 1  # 1 captain
    # Check player selections are valid
    for i in range(num_players):
        # Captain has to be present in starting team
        model += (decisions[i] - captain_decisions[i]) >= 0
        # Subs cannot be present in starting team
        model += (decisions[i] + sub_decisions[i]) <= 1
    model.solve(PULP_CBC_CMD(msg=0))
    return decisions, captain_decisions, sub_decisions, model.objective.value()