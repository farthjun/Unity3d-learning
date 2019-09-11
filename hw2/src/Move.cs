using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
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
    bool startFlag = false;
    bool count = false;//是否开始计时
    private int flameCount = 0;
    private int timeCount = 0;

    //计时
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

    //产生随机数
    static int GetRandomBySleep()
    {
        int rand = 0;
        System.Random random = new System.Random();
        Thread.Sleep(10);
        rand = random.Next(1, 9);
        return rand;
    }

    void GameStart()
    {
        flameCount = 0;
        timeCount = 0;
        count = true;
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
        if (buttons[8].transform.position != blank+canvas)
        {
            for(int i = 0; i < 8; ++i)
            {
                //确保空方块在右下角
                if (buttons[i].transform.position == blank+canvas)
                {
                    Debug.Log("Yes");
                    GameObject obj = GameObject.Find("Blank");
                    Vector3 pos = buttons[i].transform.position;
                    buttons[i].transform.position = obj.transform.position;
                    obj.transform.position = pos;
                    break;
                }
            }
        }

    }

    void OnGUI()
    {
        //You Win的样式
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.textColor = new Color(250, 240, 230);

        //Time的样式
        GUIStyle fontStyle2 = new GUIStyle();
        fontStyle2.normal.textColor = new Color(255, 250,240);
        //显示用时
        GUI.Label(new Rect(50, 200, 50, 50), "Time: "+timeCount.ToString(), fontStyle2);

        if (GameOver())
        {
            count = false;
            GUI.Label(new Rect(240, 240, 50, 50), "You Win!", fontStyle);
        }
        
    }

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

    // Start is called before the first frame update
    void Start()
    {
        string obj_name = "Btn";
        for (int i = 0; i < 8; ++i)
        {
            string name = obj_name + (i + 1).ToString();
            buttons[i] = GameObject.Find(name);
        }
        buttons[8] = GameObject.Find("Blank");
        GameObject start = GameObject.Find("Start");
        start.GetComponent<Button>().onClick.AddListener(delegate () {
            startFlag = true;
            GameStart();
        });
    }

    void FixedUpdate()
    {
        if(startFlag)
            TimeCount();
    }
}
