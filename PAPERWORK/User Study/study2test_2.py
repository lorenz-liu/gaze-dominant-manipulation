import scipy.stats as stats
from scipy.stats import shapiro
import pandas as pd
import statsmodels.stats.multicomp as mc

data1 = [0.32]
data2 = [0.21, 0.52]
data3 = [0.28]

f_val, p_val = stats.f_oneway(data1, data2, data3)

print("One-Way ANOVA results:")
print("F-value:", f_val)
print("P-value:", p_val)

# 将数据转化成适合进行事后检验的形式
df = pd.DataFrame({'value': data1 + data2 + data3,
                   'group': ['data1']*len(data1) + ['data2']*len(data2) + ['data3']*len(data3)})
# 进行事后检验
tukey = mc.MultiComparison(df['value'], df['group']).tukeyhsd()

print(tukey)