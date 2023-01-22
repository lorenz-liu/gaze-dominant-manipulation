# BUAA-Graduation-Project
Design and Implementation of a Head-eye Collaborative Object Manipulation Method in Virtual Reality. 

## Prerequisites

OS: Windows 10+

Please install by order in case config chaos:

* [INSTALL](https://unity.com/download) Unity Hub
* [INSTALL](https://dl.vive.com/vbspc/VIVEBusinessStreamingInstaller.exe) VIVE Business Streaming
* [INSTALL](https://cdn.akamai.steamstatic.com/client/installer/SteamSetup.exe) Steam
* [INSTALL](https://store.steampowered.com/app/250820/SteamVR/) SteamVR in Steam
* [INSTALL](unityhub://2021.3.16f) Unity Editor 2021.3.16f1
* [INSTALL](https://assetstore.unity.com/packages/tools/integration/steamvr-plugin-32647#reviews) SteamVR Assets in Unity Editor

## Documentation & Reference

[VIVE Wave UnityXR Tutorials](https://hub.vive.com/storage/docs/en-us/UnityXR/UnityXRTutorials.html)

## Terminology

**跳視 / Saccades / Saccadic Eye Movements**

![img](https://www.kyst.com.tw/image/data/knowledge/hum/Saccades_small.gif)

這是指雙眼非常快速且同步的在兩個凝視之間的移動。

通常是指雙眼同步在兩個凝視點之間快速移動， 

**凝視 / Fixations**

通常用來描述雙眼處理視覺訊息時停留相對穩定的一段時間，一般而言，凝視通常會是200~300 ms 之間，但也可能會是更長或更短的時間，端看刺激物的類型，像是在文字閱讀時，時間會較短，而觀看影像時，可能會時間長一點。

**微跳視 / Microsaccades / (Intra) Fixational Eye Movements**

設備提供的凝視點資訊，通常以圓圈呈現，這經常讓人誤以為凝視是一種眼睛完全靜止的狀態。

但實際上，眼睛從來不會是完全靜止的，在一個凝視點中，可能會有「漂移」（Drift：位置的微小變化）和微跳視的情況發生。
微跳視就是字面上的意思，跟一般跳視一樣，只是再更小一點。

到底要多小的跳視才算是微跳視呢？
這在研究領域中，也是經常討論的，不過只要小於一度視角，就算是一個合理的微跳視。 

**平滑追瞄移動 /** **Smooth Pursuit**

![img](https://www.kyst.com.tw/image/data/knowledge/hum/SR_Smooth_Pursuit.gif)

如果您正在注視感興趣的目標，並且該目標開始移動，您可能希望繼續注視該目標。當我們試圖用一系列小跳視以及凝視來追踪移動物體會減少可用於處理的視覺訊息量。該目標會反覆移出視網膜中心區域，需要我們跳視向新的位置，而在跳視期間我們基本上是失明的狀態。相反的，我們會以平滑追瞄的眼球運動進行。您可以把這個行為想像成一種移動的凝視狀態。如果您記錄人們觀看動態刺激（如電影）的話，就會看到平滑追瞄是很常見的眼球運動。不過在分析上來說可能會有點問題，因為雖然我們是注視在移動的物體上，但分析時如果將平滑追瞄視為「固定」的凝視點就不太合理了。

一般的凝視點通常會以一個XY來表示座標位置。但分析上來說，如果是繪製移動的凝視點的平均位置，這樣的資訊也不太對。平滑追瞄反而應該以時間為準來繪製凝視位置會比較好，以橫軸為時間，縱軸為X/Y位置。如果要量化平滑追瞄的眼球運動可能會有點複雜，不過研究上來說比較常用「velocity gain」（眼球追瞄速度與目標速度之間的比值）以及RMSE (Root Mean Square Error)

以下圖來說，可以將「velocity gain」視為紅線（gaze plot）跟黑線（target plot）的斜率比值。

![img](https://www.kyst.com.tw/image/data/knowledge/hum/SR_Pursuit_line.png)

其他常見的平滑追瞄指標包括追瞄的持續時間（這是指追瞄的花費時間，而不是跳視或非追瞄的凝視行為）和追瞄區間的數量。研究人員還可能對追瞄過程中發生的跳視類型感興趣，這些類型可以分為「Catch-Up」、「Back-up」、和「Anticipatory」。「Catch-Up」和「Back-up」跳視通常被認為是矯正性的，而「Anticipatory」的跳視則是侵入性的，在某些神經和神經精神疾病中可能會比較多。

**前庭眼反射 / Vestibular Ocular Reflex (VOR)**

當您凝視著一個目標的同時移動您的頭部，VOR系統會確保您的眼睛調整到適當的位置繼續凝視目標。因此當您直視前方並將頭部向右旋轉，您的眼睛會自動向左旋轉，以確保持續凝視著目標物（反之亦然）。即使是最小幅度的移動，VOR也是會發生。VOR 是人類最快速的反射動作之一，但這是一種無意識且低程度的反射行為。

這跟大部份的眼球追蹤研究人員，特別是使用桌上型眼動儀的使用者來說比較無關。 

**幅奏運動 / Vergence**

![img](https://www.kyst.com.tw/image/data/knowledge/hum/SR_Vergence.gif)

大部份的眼球運動是共軛的（意即眼睛是往同一個方向移動），但當我們將眼睛聚焦在很靠近的目標上時，我們的眼睛凝視會向內聚集（意即眼睛會同時向鼻側旋轉）。如果我們聚焦在遠處目標上的話，我們的眼睛凝視就會發散。以桌上型眼動儀的使用來說，幅奏運動通常不是問題，因為我們的刺激都是呈現在固定距離的螢幕上。以頭戴式或活動型的眼動儀而言，只要受測者可以自由凝視任何距離的目標，幅奏運動可以用來以z維度估計凝視資訊。

**視動震顫 / Nystagmus / OKN**

![img](https://www.kyst.com.tw/image/data/knowledge/hum/SR_Nystagmus.gif)

視動震顫是指眼球快速前後運動。該術語可以指醫療狀況，在這種情況下，前後運動可以採用多種不同的形式。通常有一個緩慢和快速的階段。慢速階段可以等同於平滑追瞄，而快速階段可以等同於跳視。視動性眼球震顫 (OKN) 發生在健康個體中，當大部分視野在同一方向上連續移動時。你可以很容易地通過觀察某人的眼睛來觀察 OKN，像是他們正在行駛的火車中凝視著窗外。

同樣，緩慢和快速階段始終存在——當隨著時間的推移繪製注視位置時，會產生“鋸齒”模式。關注醫學狀況或動眼神經控制的基本機制的研究人員通常會對這種眼球運動感興趣。

**眨眼 / Blinks**

技術上來說，眨眼當然不是眼睛本身的運動——但基於各種原因，這項行為在眼動研究中很重要。想當然爾，特別重要的是，如果看不到眼睛，就無法知道眼睛看哪邊了。首先，眨眼可以中斷凝視行為，這種情況下，在眼動數據中，單個凝視點將被視為兩個分開的凝視（只是在同一位置）。

在許多時候，這大概不會是個問題——因為重要的是凝視的持續時間和位置。複雜的是，眨眼和跳視通常是合在一起的事情，這表示眨眼會蓋掉實際的眼跳資訊。好在的是，SR Research 分析軟體 Data Viewer 提供了一系列處理眨眼的選項，包括去除或合併眨眼資訊前後的凝視行為。

## Last Updated

January 13, 2023
