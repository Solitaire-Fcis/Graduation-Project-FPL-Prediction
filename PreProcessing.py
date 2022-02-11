import numpy as np
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
from sklearn.preprocessing import LabelEncoder

# Dataset Reading/Cleaning
def Data_Reading(path):
    data = pd.read_csv(path).dropna()
    data_sus = data['total_points']
    data.pop(['bps', 'team_x', 'kickoff_time', 'total_point'])
    data['total_points'] = data_sus
    return data

# Dataset Encoding
def Data_Encoding(data, cols):
    return data

# Dataset Seasons Splitting
def Data_Seasons_Splitting():
    return Dict17, Dict18, Dict19, Dict20, Dict21, Dict22

# Dataset Initialization For Each Season
def Data_Init_Seasons():
    return Data17, Data18, Data19, Data20, Data21, Data22_

# Dataset Sorting Based On Players' Names
def Data_Sorting():
    return data



