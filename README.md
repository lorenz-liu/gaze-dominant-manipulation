# BUAA-Graduation-Project
**虚拟现实头眼协同对象操纵方法设计与实现**

Design and Implementation of a Head-eye Collaborative Object Manipulation Method in Virtual Reality. 

## Prerequisites

OS: Windows 10+

HMD: VIVE Focus 3 with its exclusive Eye Tracker (**LETHALLY IMPORTANT: Please make sure the HMD's version is up-to-date!**)

Please install by order in case config chaos:

* [INSTALL](https://unity.com/download) Unity Hub
* [INSTALL](https://cdn.akamai.steamstatic.com/client/installer/SteamSetup.exe) Steam
* [INSTALL](https://store.steampowered.com/app/250820/SteamVR/) SteamVR in Steam
* [INSTALL](unityhub://2021.3.16f) Unity Editor 2021.3.16f1
* [INSTALL](https://assetstore.unity.com/packages/tools/integration/steamvr-plugin-32647#reviews) SteamVR Assets in Unity Editor
* [INSTALL](https://dl.vive.com/vbspc/VIVEBusinessStreamingInstaller.exe) VIVE Business Streaming

## Documentation & Reference

[VIVE OpenXR Tutorials](https://developer.vive.com/resources/openxr/openxr-pcvr/tutorials/unity/integrate-facial-tracking-your-avatar/)

## Appendix

论文编辑器：TexMaker

#### 摘要

本论文提出了一种在虚拟现实中基于头眼协同的对象操作方法。该方法是凝视主导且完全无手干预的，实现了包括空间平移、旋转和等比例缩放的完全6DOF操作。我们还提出了一个完整的方法流程和一个基于3D用户界面的“四叶草”模式选择菜单，让用户只需用眼动信号即可轻松切换不同的操作模式，实现了在虚拟空间中对多对象的连续交互操作。为应对眼动追踪数据的高噪音问题，我们引入了过滤算法和线性优化过程来增强用户体验，确保交互自然流畅。这种新型交互系统使身体有障碍的人或手部被占用或限制的场景中的物体操作变得更加容易。通过消除对手部的需求，这种方法为与虚拟现实环境的交互提供了一种新的方式，扩大了沉浸式体验的可用性。

This paper proposes a head­-eye collaborative object manipulation in virtual reality. Our eye­-dominant and hands­free method enables spatial translation, rotation, and proportional scal­ing with a complete 6DOF manipulation. We also realize an entire pipeline with a mode­ switching 3D user interface menu called “Clover,” allowing users to effortlessly switch be­ tween different manipulation modes with sole eye movement. The pipeline enables sequential and incessant interaction actions with multiple objects in a virtual environment. To address the challenges posed by noisy eye-­tracking data, we introduce a filtering algorithm and a linear op­timization process to enhance the user experience, ensuring that the interaction is both natural and fluent. This novel interaction system makes object manipulation accessible to individuals with physical disabilities or in scenarios where hands are either occupied or restrained. By elim­inating the need for hands, this approach provides a new way of interacting with virtual reality environments, expanding the usability of immersive experiences.

#### 关键词

人机交互；虚拟现实；三维对象操纵；头眼协同；无手操纵；多模态接口

#### 贡献

1. 提出了一个基于头眼协同的、完全无手干预的对象操纵方法，实现了空间位移、空间定轴旋转和空间等比例缩放以及各操纵模式之间的快速切换，并且显著优于当前基于头眼协同的最优对象操纵方法；
2. 提出了一个“四叶草”模式选择菜单，解决了对象操纵研究的模式间切换问题，构建了一个完整的闭环操纵流程；
3. 提出了一种眼动信号的滤波算法和一种头眼协同的凝视驻留点计算优化；
4. 提出了一种全面测试对象操纵的“积木”用户实验，有助于相关研究对其方法效果的定量测试。

## Last Updated

May 28, 2023

