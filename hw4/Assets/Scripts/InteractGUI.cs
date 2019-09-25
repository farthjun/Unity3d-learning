using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class InteractGUI : MonoBehaviour
{
    UserAction UserAcotionController;
    public int SetState { get { return GameState; } set { GameState = value; } }
    static int GameState = 0;

    // Use this for initialization
    void Start()
    {
        UserAcotionController = SSDirector.getInstance().currentScenceController as UserAction;
    }

    private void OnGUI()
    {
        if (GameState == 1)
        {
            GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2-50, 100, 50), "Gameover!");
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart"))
            {
                GameState = 0;
                UserAcotionController.Restart();
            }
        }
        else if (GameState == 2)
        {
            GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2-50, 100, 50), "You Win!");
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart"))
            {
                GameState = 0;
                UserAcotionController.Restart();
            }
        }
    }
}

public class ClickGUI : MonoBehaviour
{
    UserAction UserAcotionController;
    Character GameObjectsInScene;

    public void setController(Character characterCtrl)
    {
        GameObjectsInScene = characterCtrl;
    }

    void Start()
    {
        UserAcotionController = SSDirector.getInstance().currentScenceController as UserAction;
    }

    void OnMouseDown()
    {
        if (gameObject.name == "boat")
        {
            UserAcotionController.MoveBoat();
        }
        else
        {
            UserAcotionController.ObjectIsClicked(GameObjectsInScene);
        }
    }
}