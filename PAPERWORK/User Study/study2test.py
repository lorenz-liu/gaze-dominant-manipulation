import scipy.stats as stats
from scipy.stats import shapiro
import pandas as pd
import statsmodels.stats.multicomp as mc

data1 = [33.59, 22.47, 24.67, 28.31, 41.23, 27.74, 25.92, 25.35, 29.75]
data2 = [36.24, 31.68, 43.61, 32.05, 32.71, 34.39, 38.88, 33.79]
data3 = [24.13, 20.89, 28.57, 33.05, 26.48, 24.73, 29.24, 31.93, 22.51]

stat, p = shapiro(data1)
print('Statistics=%.3f, p=%.3f' % (stat, p))
if p > 0.05:
    print('数据符合正态分布')
else:
    print('数据不符合正态分布')

stat, p = shapiro(data2)
print('Statistics=%.3f, p=%.3f' % (stat, p))
if p > 0.05:
    print('数据符合正态分布')
else:
    print('数据不符合正态分布')

stat, p = shapiro(data3)
print('Statistics=%.3f, p=%.3f' % (stat, p))
if p > 0.05:
    print('数据符合正态分布')
else:
    print('数据不符合正态分布')

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