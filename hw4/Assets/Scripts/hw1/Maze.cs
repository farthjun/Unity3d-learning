using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    struct Button
    {
        public int id;//0~8
        public int x;
        public int y;
    }
    private Button[] buttons = new Button[9];
    private int buttonWidth = 50;
    private int buttonHeight = 50;

    void Display()
    {
        for (int i = 0; i < 9; ++i)
        {
            GUI.Button(new Rect(buttons[i].x, buttons[i].y, buttonWidth, buttonHeight), buttons[i].id.ToString());
        }
        Debug.Log(buttons[0].x);
        Debug.Log(buttons[0].y);
    }

    void OnGUI()
    {
        //Swap(7);
        Display();
        if (GUI.Button(new Rect(150, 100, 50, 50), "1")) {
            Debug.Log("hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh");
        }
        Debug.Log("===========================");
    }

    void Start()
    {
        int baseX = 150;
        int baseY = 100;
        for(int i = 0; i < 9; ++i)//初始坐标
        {
            buttons[i].id = (i != 8) ? i + 1 : 0;
            buttons[i].x = baseX + i % 3 * buttonWidth;
            buttons[i].y = baseY + i / 3 * buttonHeight;
        }
    }

    //编号为i的方块能否移动
    void Swap(int i)
    {
        Debug.Log("Begin!\n");
        if (i == 0) return;
        if(System.Math.Abs(buttons[i].x-buttons[8].x)+System.Math.Abs(buttons[8].x - buttons[8].x) == 50){
            Debug.Log("Yes!\n");
            //交换坐标
            int tempX = buttons[i].x;
            int tempY = buttons[i].y;
            buttons[i].x = buttons[8].x;
            buttons[i].y = buttons[8].y;
            buttons[8].x = tempX;
            buttons[8].y = tempY;
        }
    }
    
}
