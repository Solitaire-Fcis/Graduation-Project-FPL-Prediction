import numpy as np
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
from sklearn import preprocessing
from sklearn.preprocessing import LabelEncoder
from PreProcessing import *

# Data Reading
pdata, tdata = Data_Reading("./cleaned_merged_seasons.csv", "./master_team_list.csv") 

# Data Encoding For Columns: [position, was_home]
pdata = Data_Encoding(pdata, ['position', 'was_home'])

# Players' Data Splitting For Team Codes Management 
pdf17, pdf18, pdf19, pdf20, pdf21, pdf22 = Players_Data_Seasons_Splitting(pdata)

# Teams' Data Splitting For Team Codes Management
tdf17, tdf18, tdf19, tdf20, tdf21, tdf22 = Teams_Data_Seasons_Splitting(tdata)

# Teams Integration and Sorting According To Columns: [name, GW]
for pdf, tdf in zip([pdf17, pdf18, pdf19, pdf20, pdf21, pdf22], [tdf17, tdf18, tdf19, tdf20, tdf21, tdf22]):
    pdf = Teams_Data_Integration(pdf, tdf)
    pdf = Data_Sorting(pdf,['name', 'GW'])

# Data Concatenation and Tidiness For Extracting Ready To Use Datasets: pdf_encoded, tdf_encoded For Players and Teams 
pdf = pd.concat([pdf17, pdf18, pdf19, pdf20, pdf21, pdf22])
pdf_encoded, tdf_encoded =  Data_Tidiness(pdf)

# Dataset Correlation Determination
res = pdf['total_points']
corr = pdf.corr()
top = corr.index[abs(corr['total_points']>0.5)]
plt.subplots(figsize=(12,8))
top_corr = pdf[top].corr()
sns.heatmap(top_corr, annot=True)
plt.show()
