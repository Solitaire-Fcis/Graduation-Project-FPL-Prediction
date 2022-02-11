import numpy as np
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
from sklearn import preprocessing
from sklearn.preprocessing import LabelEncoder
from PreProcessing import *

data = DataReading("./cleaned_merged_seasons.csv") 


# Dataset Correlation Determination
'''
features = pd.concat([features1, features2], axis=1)
res = data["total_points"]

features.drop('kickoff_time', axis=1, inplace=True)

corr = data.corr()
top = corr.index[abs(corr['total_points']>0.1)]

plt.subplots(figsize=(12,8))
top_corr = data[top].corr()
sns.heatmap(top_corr, annot=True)
plt.show()
'''
