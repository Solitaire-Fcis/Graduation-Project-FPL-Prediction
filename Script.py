import numpy as np
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
from sklearn import preprocessing
from sklearn.preprocessing import LabelEncoder
from PreProcessing import *

# Data Reading
#pdata, tdata = Data_Reading("./cleaned_merged_seasons.csv", "./master_team_list.csv") 
players, teams = Data_Reading('./PreProcessed_Datasets/Players_Dataset.csv', './PreProcessed_Datasets/Teams_Dataset.csv')

# Data Encoding For Columns: [position, was_home]
#pdata = Data_Encoding(pdata, ['position', 'was_home'])

# Players' Data Splitting For Team Codes Management 
#pdf17, pdf18, pdf19, pdf20, pdf21, pdf22 = Players_Data_Seasons_Splitting(pdata)

# Teams' Data Splitting For Team Codes Management
#tdf17, tdf18, tdf19, tdf20, tdf21, tdf22 = Teams_Data_Seasons_Splitting(tdata)

# Teams Integration and Sorting According To Columns: [name, GW]
#for pdf, tdf in zip([pdf17, pdf18, pdf19, pdf20, pdf21, pdf22], [tdf17, tdf18, tdf19, tdf20, tdf21, tdf22]):
   # pdf = Teams_Data_Integration(pdf, tdf)
    #pdf = Data_Sorting(pdf,['name', 'GW'])

# Data Concatenation and Tidiness For Extracting Ready To Use Datasets: pdf_encoded, tdf_encoded For Players and Teams 
#pdf = pd.concat([pdf17, pdf18, pdf19, pdf20, pdf21, pdf22])
#pdf_encoded, tdf_encoded =  Data_Tidiness(pdf)

# Exporting Datasets To External CSVs
#pdf_encoded.to_csv('./PreProcessed_Datasets/Players_Dataset.csv')
#tdf_encoded.to_csv('./PreProcessed_Datasets/Teams_Dataset.csv')


pdf17, pdf18, pdf19, pdf20, pdf21, pdf22 = Players_Data_Seasons_Splitting(players)

for pdf, ind in zip([pdf17, pdf18, pdf19, pdf20, pdf21, pdf22],[17,18,19,20,21,22]):
    pdf = Data_Encoding(pdf, ['name'])
    #pdf.to_csv('./Seasoned_Datasets/pdf'+str(ind)+'.csv')
    #pdf = ren_pdf(pdf, ind)

features_unwanted = ['goals_conceded','influence','assists', 'bonus', 'clean_sheets', 'creativity', 'element', 'goals_scored', 'ict_index', 'minutes', 'opp_team_name', 'own_goals', 'penalties_missed', 'penalties_saved', 'red_cards', 'saves', 'team_a_score', 'team_h_score', 'threat', 'transfers_balance', 'transfers_in','transfers_out', 'yellow_cards']
for feat in features_unwanted:
    players.drop(feat, axis=1,inplace=True)

pdf21 = players.loc[players['season_x'] == '2020-21']
players.drop(players.index[players['season_x'] == '2020-21'], inplace=True)

players = Data_Encoding(players, ['name', 'season_x'])
pdf21 = Data_Encoding(pdf21, ['name', 'season_x'])

print(players['total_points'])


#pdf18 = Points_Concat(pdf17, pdf18, 18)
#pdf19 = Points_Concat(pdf18, pdf19, 19)
#pdf20 = Points_Concat(pdf19, pdf20, 20)
#pdf21 = Points_Concat(pdf20, pdf21, 21)


# Dataset Correlation Determination
features = ['total_points','season_x', 'name', 'position', 'fixture', 'opponent_team', 'round', 'was_home', 'GW']
corr = players.corr()
top = corr.index[abs(corr['total_points'] >= 0)]
plt.subplots(figsize=(12,8))
top_corr = players.corr()
sns.heatmap(top_corr, annot=True)
plt.show()


#sns.set()
#sns.pairplot(players[features], height = 2.5)
#plt.show()

#pdf17.set_index(keys=['name'], drop=False, inplace=True)
#names=pdf17['name'].unique().tolist()
#AaronCresswell = pdf17.loc[pdf17.name == 'Aaron Cresswell']
