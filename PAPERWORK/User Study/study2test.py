import scipy.stats as stats
from scipy.stats import shapiro
import pandas as pd
import statsmodels.stats.multicomp as mc
from scipy.stats import levene

import matplotlib.pyplot as plt
import numpy as np

data1 = [33.59, 22.47, 24.67, 28.31, 41.23, 27.74, 25.92, 25.35, 26.04, 29.29, 30.19, 28.28]
data2 = [36.24, 31.68, 43.61, 32.05, 32.71, 34.39, 38.88, 35.21, 38.12, 34.32]
data3 = [24.13, 20.89, 28.57, 33.05, 26.48, 24.73, 29.24, 31.93, 22.51, 25.9, 31.04, 32.97]

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

stat, p = levene(data1, data2, data3)
print('Statistics=%.3f, p=%.3f' % (stat, p))
if p > 0.05:
    print("方差齐性检验通过")
else:
    print("方差齐性检验不通过")

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

plt.rcParams['font.family'] = ['Arial Unicode MS']
plt.figure(figsize=(10, 3))
plt.plot(data1, color='red', label='Our Method', marker='o')
plt.plot(data2, color='blue', label='PRISM', marker='s')
plt.plot(data3, color='black', label='Implicit Gaze', marker='^')
plt.axhline(y=sum(data1)/len(data1), color='red', linestyle='--')
plt.axhline(y=sum(data2)/len(data2), color='blue', linestyle='--')
plt.axhline(y=sum(data3)/len(data3), color='black', linestyle='--')
plt.grid()
plt.legend()
plt.show()