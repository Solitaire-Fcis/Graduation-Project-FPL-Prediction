import numpy as np
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
from sklearn.preprocessing import LabelEncoder
pd.options.mode.chained_assignment = None

# Dataset Reading/Cleaning
def Data_Reading(ppath, tpath):
    Players_Data = pd.read_csv(ppath, dtype={"name": str, "position": str, "team_x": str})
    Teams_Data = pd.read_csv(tpath)
    #data_sus = Players_Data['total_points']
    #Players_Data.pop('total_points')
    #Players_Data = Players_Data.drop(['bps', 'team_x', 'kickoff_time'], axis=1)
    #Players_Data['total_points'] = data_sus
    return Players_Data, Teams_Data

# Dataset Encoding
def Data_Encoding(data, cols):
    data_tmp = data
    for col in cols:
        lbl = LabelEncoder()
        lbl.fit(data_tmp[col].values)
        data_tmp[col] = lbl.transform(list(data_tmp[col].values))
    return data_tmp

# Dataset Seasons Splitting For Players Dataset
def Players_Data_Seasons_Splitting(pdf):
    pdf17 = pdf.loc[pdf['season_x'] == '2016-17']
    pdf18 = pdf.loc[pdf['season_x'] == '2017-18']
    pdf19 = pdf.loc[pdf['season_x'] == '2018-19']
    pdf20 = pdf.loc[pdf['season_x'] == '2019-20']
    pdf21 = pdf.loc[pdf['season_x'] == '2020-21']
    pdf22 = pdf.loc[pdf['season_x'] == '2021-22']
    return pdf17, pdf18, pdf19, pdf20, pdf21, pdf22

# Dataset Seasons Splitting For Teams Dataset
def Teams_Data_Seasons_Splitting(tdf):
    tdf17 = tdf.loc[tdf['season'] == '2016-17']
    tdf18 = tdf.loc[tdf['season'] == '2017-18']
    tdf19 = tdf.loc[tdf['season'] == '2018-19']
    tdf20 = tdf.loc[tdf['season'] == '2019-20']
    tdf21 = tdf.loc[tdf['season'] == '2020-21']
    tdf22 = tdf.loc[tdf['season'] == '2021-22']
    return tdf17, tdf18, tdf19, tdf20, tdf21, tdf22

# Teams Integration In Players Dataset
def Teams_Data_Integration(pdf, tdf):
    team = dict()
    for i in range(len(tdf)):
        team[tdf.iloc[i,1]] = tdf.iloc[i,2]
    for i in team:
        pdf.loc[pdf['opponent_team'] == i, 'opponent_team'] = team[i] 
    return pdf

# Dataset Sorting Based On Players' Names
def Data_Sorting(pdf, cols):
    pdf = pdf.sort_values(cols, ascending=True)
    return pdf

# Dataset Tidiness For Extracting Players/Teams Datasets Ready To Use After [Encoding Column opponent_team To Match All Seasons]
def Data_Tidiness(pdf):
    teams_names = pd.DataFrame()
    teams_names = pdf['opponent_team']
    pdf_encoded = Data_Encoding(pdf, ['opponent_team'])
    tdf_encoded = pd.concat([pdf_encoded['opponent_team'], teams_names], axis=1).drop_duplicates().reset_index().drop('index', axis=1)
    tdf_encoded.columns.values[0] = 'code'
    tdf_encoded.columns.values[1] = 'team_name'
    tdf_encoded = Data_Sorting(tdf_encoded, ['code']).reset_index().drop('index', axis=1)
    return pdf_encoded, tdf_encoded
'''
def Points_Concat(pdf, pdfNext, index):
    tot_poin_next = pdfNext['total_points'+str(index)]
    pdf['total_points'+str(index)] = tot_poin_next
    print(pdf)
    return pdf

def ren_pdf(pdf, index):
    df = pdf['total_points']
    pdf['total_points'+str(index)] = df
    pdf.pop('total_points')
    print(pdf)    
    return pdf
'''
'''
def rename(pdf17,pdf18,pdf19,pdf20,pdf21):
    df=pdf17['total_points']
    pdf17['total_points17']=df
    pdf17.pop('total_points')
    df=pdf18['total_points']
    pdf18['total_points18']=df
    pdf18.pop('total_points')
    df=pdf19['total_points']
    pdf19['total_points19']=df
    pdf19.pop('total_points')
    df=pdf20['total_points']
    pdf20['total_points20']=df
    pdf20.pop('total_points')
    df=pdf21['total_points']
    pdf21['total_points21']=df
    pdf21.pop('total_points')
'''