Unity小游戏——数字华容道

<img src="https://github.com/farthjun/Unity3d-learning/blob/master/hw2/img/reslut.png?raw=true" alt="reslut" style="zoom:67%;" />

数字华容道的玩法和拼图十分相似，要求玩家复原被打乱的数字。本次作业中，我使用Unity制作了一个简单的3*3的数字华容道小游戏。以下是制作过程。


## 1.创建初始九宫格

首先我们创建固定的九宫格。设想游戏的玩法如下：鼠标点击格子，如果该格子在空白格子旁边，则它们交换位置。采用Button就是一个很好的选择。

鼠标在Hierarchy 下方点击右键，选择UI-Button，就创建了一个Button对象。

在Inspector处设置该Button的名称以及Transform，设置为50*50大小。注意此处的坐标是相对坐标，相对于它的父类Canvas（画布）的：

![setButton](https://github.com/farthjun/Unity3d-learning/blob/master/hw2/img/setButton.png?raw=true)

复制设置好的Button，一共产生九个Button，并修改每个Button的相对坐标和Text内容，形成九宫格。做完这些，还可以对Button的字体、透明度做一些美化，结果显示如下：

![outlook](https://github.com/farthjun/Unity3d-learning/blob/master/hw2/img/outlook.png?raw=true)



## 2. 实现Button的移动

移动的条件很简单：被点击的Button必须与空白Button相邻，通过坐标交换来实现移动。可以给除了空白Button外的每一个Button都添加一个换位脚本(完整代码见src/Swap.cs)：

```
void Start()
    {
    	//监听鼠标的点击事件
        this.GetComponent<Button>().onClick.AddListener(delegate () {
            GameObject blank = GameObject.Find("Blank");
            double tempX = this.transform.position.x;
            double tempY = this.transform.position.y;
            //与空白Button相邻
            if (System.Math.Abs(tempX-blank.transform.position.x)+ System.Math.Abs(tempY - blank.transform.position.y) == 50)
            {
                Vector3 pos = this.transform.position;
                this.transform.position = blank.transform.position;
                blank.transform.position = pos;
            }
        });
    }


```



## 3. 打乱九宫格

按上面的方法再添加一个按钮“Start”，监听到鼠标点击，则执行打乱九宫格的代码。要做到打乱，首先记录各个Button的初始坐标，并将九个Button存入一个GameObject类型的数组中，以便管理(完整代码见src/Move.cs)：

```
//初始位置，用于判断游戏是否结束
    Vector3 canvas = new Vector3((float)244.5, (float)153.5, 0);//画布的全球坐标
    //以下为相对画布的坐标
    Vector3 pos1 = new Vector3(-50, 50, 0);
    Vector3 pos2 = new Vector3(0, 50, 0);
    Vector3 pos3 = new Vector3(50, 50, 0);
    Vector3 pos4 = new Vector3(-50, 0, 0);
    Vector3 pos5 = new Vector3(-0, 0, 0);
    Vector3 pos6 = new Vector3(50, 0, 0);
    Vector3 pos7 = new Vector3(-50, -50, 0);
    Vector3 pos8 = new Vector3(0, -50, 0);
    Vector3 blank = new Vector3(50, -50, 0);
    GameObject[] buttons = new GameObject[9];
```

通过对象名，找到Button对象并存入数组：

```
//按钮的命名为Btn1,...,Btn8
//空白块的命名为Blank
string obj_name = "Btn";
        for (int i = 0; i < 8; ++i)
        {
            string name = obj_name + (i + 1).ToString();
            buttons[i] = GameObject.Find(name);
        }
        buttons[8] = GameObject.Find("Blank");
```

随机选择两个Button，交换它们的位置，进行16次交换，以达到打乱的目的：

```
//随机打乱前8块的位置
        for (int i = 0; i < 16; ++i)
        {
            System.Random ran = new System.Random();
            int rand1 = GetRandomBySleep();
            int rand2 = GetRandomBySleep();
            GameObject btn1 = GameObject.Find("Btn" + rand1.ToString());
            GameObject btn2 = GameObject.Find("Btn" + rand2.ToString());
            Vector3 pos = btn1.transform.position;
            btn1.transform.position = btn2.transform.position;
            btn2.transform.position = pos;
        }
```

按钮“Start”监听鼠标点击的代码：

```
start.GetComponent<Button>().onClick.AddListener(delegate () {
            startFlag = true;
            GameStart();
});
```



## 4. 游戏胜利判断

判断也很简单：每个Buttons都归位，则说明游戏胜利。(见src/Move.cs)

    bool GameOver()
    {
        return (buttons[0].transform.position == pos1 + canvas) &&
            (buttons[1].transform.position == pos2 + canvas) &&
            (buttons[2].transform.position == pos3 + canvas) &&
            (buttons[3].transform.position == pos4 + canvas) &&
            (buttons[4].transform.position == pos5 + canvas) &&
            (buttons[5].transform.position == pos6 + canvas) &&
            (buttons[6].transform.position == pos7 + canvas) &&
            (buttons[7].transform.position == pos8 + canvas) &&
            (buttons[8].transform.position == blank + canvas) &&
            startFlag;
    }
GameObject.transform.position是一个三维向量，注意**相对坐标要加上画布的坐标才是该Button的全球坐标。**



## 5. 游戏的开始、结束控制

通过布尔变量来控制开始和结束是很重要的。比如，当游戏结束的时候我们希望输出“You Win!”，但是我们不希望在游戏开始前输出(但是此时满足游戏结束的条件)。这里引入一个布尔变量startFlag。

因此，我们的控制逻辑应该是：

游戏开始前，Button归位，startFlag = false，不输出“You Win!”

游戏开始，Button没有归位，startFlag = true，不输出“You Win!”

游戏结束，Button归位，startFlag = true，输出“You Win!”

    void OnGUI()
    {
        //You Win的样式
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.textColor = new Color(250, 240, 230);
    
        if (GameOver())
        {
            count = false;
            GUI.Label(new Rect(240, 240, 50, 50), "You Win!", fontStyle);
        }
        
    }


## 6. 显示计时器

利用FixedUpdate()每一帧固定为0.02s，我们可以显示游戏用时。游戏开始时，开始计时；游戏结束，停止计时并持续显示时间。要实现这些，我们还需要引入一个计时用的布尔变量count。

```
​```
bool count = false;//是否开始计时
private int flameCount = 0;
private int timeCount = 0;
​```
void TimeCount()
{
    if (count)
    {
        flameCount++;
        if (flameCount % 50 == 0)
        {
            timeCount++;
        }
    }
}
​```

void FixedUpdate()
{
	if(startFlag)
    	TimeCount();
}
```

在OnGUI()中，增加显示时间的代码：

```
//Time的样式
GUIStyle fontStyle2 = new GUIStyle();
fontStyle2.normal.textColor = new Color(255, 250,240);
//显示用时
GUI.Label(new Rect(50, 200, 50, 50), "Time: "+timeCount.ToString(), fontStyle2);
```



## 7. 可改进的地方

由于是随机打乱九宫格，会有一种情况是无法拼回原样的：

![nonSuccess](https://github.com/farthjun/Unity3d-learning/blob/master/hw2/img/nonSuccess.png?raw=true)

但是我暂时还没有对打乱算法进行改进。如果您拼到这一步了，不用再继续啦，直接点击start进行下一局吧。

关于游戏界面，我本想给每个按钮插入一张图片，做到类似下面这种效果：

<img src="https://github.com/farthjun/Unity3d-learning/blob/master/hw2/img/example.jpg?raw=true" style="zoom:20%;" width=300 />

尝试了一些方法都没有成功，以后更深入地学习Unity，或许可以进行改进。

最后，感谢您的阅读~
