import pandas as pd

teams = pd.read_csv("./PreProcessed-Datasets/Teams_Dataset.csv")
TeamBuilder = pd.read_csv("../Players_Test.csv")

for i in range(len(teams)):
    TeamBuilder.loc[TeamBuilder['opponent_team'] == teams.iloc[i,2], 'opponent_team'] = teams.iloc[i,1]

TeamBuilder.to_csv('./PreProcessed-Datasets/Players_Finale.csv')