### 自动巡逻兵
这次的作业是自动巡逻兵游戏，作业要求是：
* 创建一个地图和若干巡逻兵(使用动画)；
* 每个巡逻兵走一个3~5个边的凸多边型，位置数据是相对地址。即每次确定下一个目标位置，用自己当前位置为原点计算；
* 巡逻兵碰撞到障碍物，则会自动选下一个点为目标；
* 巡逻兵在设定范围内感知到玩家，会自动追击玩家；
* 失去玩家目标后，继续巡逻；
* 计分：玩家每次甩掉一个巡逻兵计一分，与巡逻兵碰撞游戏结束；<br>
首先显示完成后的效果：<br>
![](https://github.com/flashowner/sixth3DHomework/blob/master/%E6%88%AA%E5%9B%BE/%E5%AE%89.PNG)<br>
刚开始思考如何做巡逻兵游戏的时候是希望能够用动画来做，因为老师前两节课讲的内容就是和动画有关<br>
然而当我开始做的时候才发现下载预制自带的动画使用的时候总会有各种各样的问题，要不就是没有反应<br>
要不就是设置不了Animator里的参数，后来我决定使用自己做的“动画”，也就是让人物移动得好像动画<br>
一样，可以通过改变人物的位置和旋转来达到这个效果。接下来我将介绍我做这个游戏的步骤：<br>
首先先从网上下载巡逻兵，地形和玩家的预制，拖到资源里：<br>
![](https://github.com/flashowner/sixth3DHomework/blob/master/%E6%88%AA%E5%9B%BE/%E6%8D%95%E8%8E%B7.PNG)<br>
接着确定好各个资源初始化的位置，加载预制，在这里用工厂模式生产巡逻兵，给每一个巡逻兵添加一个<br>
初始的位置，并且给它们的姓名属性赋值方便接下来的操作：<br>
![](https://github.com/flashowner/sixth3DHomework/blob/master/%E6%88%AA%E5%9B%BE/%E6%8D%95%E8%8E%B71.PNG)<br>
考虑到巡逻兵和玩家的动作完全不同，所以考虑将玩家和巡逻兵的动作分开，玩家的动作主要是接收键盘<br>
的输入以及判定当前所在区域的位置，以便对应区域的巡逻兵做出相应，下面是玩家判定区域的函数：<br>
![](https://github.com/flashowner/sixth3DHomework/blob/master/%E6%88%AA%E5%9B%BE/%E6%8D%95%E8%8E%B72.PNG)<br>
其中的 FenchLocation是一个用来判定区域位置的类：
![](https://github.com/flashowner/sixth3DHomework/blob/master/%E6%88%AA%E5%9B%BE/%E6%8D%95%E8%8E%B73.PNG)<br>
以绝对坐标z轴12.42的值为界可以分为上下两大区域，以绝对坐标x轴-3和3为界可以分为左中右三大区域<br>
这样合起来一共有六大区域。相较于玩家，巡逻兵的动作就更复杂一些了，首先在没有检测到玩家时巡逻兵<br>
需要按栅栏照一定的路线移动，相当于巡逻，在碰到障碍物的时候能够转弯，在这里我设置巡逻兵的移动方向<br>
依次是下左上右，但是在碰到栅栏转弯的时候会有一点bug就是转弯的时候巡逻兵侧着身体还是会碰到栅栏<br>
所以巡逻兵会再多转一次弯，而且如果碰到栅栏的时候位置不对，因为栅栏的碰撞器表面不是平滑的，就会<br>
造成巡逻兵在一定范围里闪动。在检测人物是否出现在制定范围的时候，一开始我是想用两个碰撞器，一个<br>
胶囊碰撞器用来与玩家的碰撞进行检测，一个盒触发器用来检测玩家是否出现在指定范围，但是在使用的<br>
的时候发现无法区分到底是哪一个碰撞器被触发，于是我就改用了另一种方法，那就是获得玩家所在的位置<br>
，如果玩家的区域和巡逻兵的区域是在同一位置的话，那么巡逻兵就会去追玩家：<br>
![](https://github.com/flashowner/sixth3DHomework/blob/master/%E6%88%AA%E5%9B%BE/%E6%8D%95%E8%8E%B74.PNG)<br>
![](https://github.com/flashowner/sixth3DHomework/blob/master/%E6%88%AA%E5%9B%BE/%E6%8D%95%E8%8E%B75.PNG)<br>
isCatching用来判断巡逻兵当前的状态是否是追捕玩家的状态。在追赶的时候巡逻兵的速度会比原来的状态<br>
更快一些，为了保证巡逻兵不跑到别的地方去还得确定它的移动范围：<br>
![](https://github.com/flashowner/sixth3DHomework/blob/master/%E6%88%AA%E5%9B%BE/%E6%8D%95%E8%8E%B76.PNG)<br>
![](https://github.com/flashowner/sixth3DHomework/blob/master/%E6%88%AA%E5%9B%BE/%E6%8D%95%E8%8E%B77.PNG)<br>
到此一个基本的巡逻兵游戏可以算是完成的，只需要根据之前的mvc架构将动作进行分离，增加场控，以及使用<br>
工场模式，之前的记分员这次交由订阅与发布模式来实现。<br>
订阅与发布模式：<br>
我们知道在unity3D开发中通过GetComponent就可以获得某个模块的实例，进而引用这个实例完成相关任务的调用。<br>
可是显然这种方法，就像我们随身带着现金去和不同的人进行交易，每次交易的时候都需要我们考虑现金的支入和支<br>
出问题，从安全性和耦合度两个方面进行考虑，这种方法在面对复杂的系统设计的时候，非常容易造成模块间的相互<br>
依赖，即会增加不同模块间的耦合度。<br>
为了解决这个问题，大家开始考虑单例模式，因为单例模式能够保证在全局内有一个唯一的实例，所以这种方式可以<br>
有效地降低模块间的直接引用。单例模式就像是我们在银行内办理了一个唯一的账户，这样我们在交易的时候只需要<br>
通过这个账户来进行控制资金的流向就可以了。单例模式确保了各个模块间的独立性，可是单例模式更多的是一种主<br>
动行为，即我们在需要的时候主动去调用这个模块，单例模式存在的问题是无法解决被调用方的反馈问题，除非被调<br>
用方主动地去调用调用方的模块实例。<br>
说到这里我们好像看到了一种新的模式，这就是我们下面要提到的事件机制。<br>
首先这里要提到一种称为“订阅者模式”的设计模式，该模式定义了一种一对多的依赖关系，让多个观察者对象同时监<br>
听某一个主题对象。这个对象在状态发生变化时会通知所有观察者对象，使它们能够自动更新自己。<br>
在这里我通过GameEventManager发布分数和游戏结束信息。GameStatusText订阅信息，GameStatusText不需要了解<br>
分数和游戏结束是如何实现的只需要从GameEventManager获得信息，剩余的交给GameEventManager考虑就行了，具体<br>
实现代码如下：<br>
GameStatusText:<br>
![](https://github.com/flashowner/sixth3DHomework/blob/master/%E6%88%AA%E5%9B%BE/%E6%8D%95%E8%8E%B78.PNG)<br>
![](https://github.com/flashowner/sixth3DHomework/blob/master/%E6%88%AA%E5%9B%BE/%E6%8D%95%E8%8E%B79.PNG)<br>
GameEventManager:<br>
![](https://github.com/flashowner/sixth3DHomework/blob/master/%E6%88%AA%E5%9B%BE/%E6%8D%95%E8%8E%B710.PNG)<br>
视频链接：<br>
http://v.youku.com/v_show/id_XMzU5OTc0MTkyMA==.html?spm=a2hzp.8253869.0.0
