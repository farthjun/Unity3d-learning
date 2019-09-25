using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class FirstController : MonoBehaviour, ISceneController, UserAction
{
    public InteractGUI UserGUI;
    public CoastController fromCoast;
    public CoastController toCoast;
    public BoatController boat;
    //新增的裁判类
    public Judge judge;
    private Character[] Character;
    private FirstSceneActionManager FSAmanager;

    void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.currentScenceController = this;
        UserGUI = gameObject.AddComponent<InteractGUI>() as InteractGUI;
        Character = new Character[6];
        LoadResources();
    }

    void Start()
    {
        FSAmanager = GetComponent<FirstSceneActionManager>();
    }

    public void LoadResources()
    {
        fromCoast = new CoastController("from");
        toCoast = new CoastController("to");
        boat = new BoatController();
        judge = new Judge(fromCoast, toCoast, boat);
        GameObject river = Instantiate(Resources.Load("Prefabs/river", typeof(GameObject)), new Vector3(0, -7, 10), Quaternion.identity, null) as GameObject;
        river.name = "river";
        for (int i = 0; i < 3; i++)
        {
            Character p = new Character("priest");
            p.setName("priest" + i);
            p.setPosition(fromCoast.getEmptyPosition());
            p.getOnCoast(fromCoast);
            fromCoast.getOnCoast(p);
            Character[i] = p;
        }

        for (int i = 0; i < 3; i++)
        {
            Character d = new Character("devil");
            d.setName("devil" + i);
            d.setPosition(fromCoast.getEmptyPosition());
            d.getOnCoast(fromCoast);
            fromCoast.getOnCoast(d);
            Character[i + 3] = d;
        }
    }

    public void ObjectIsClicked(Character Objects)
    {
        if (FSAmanager.Complete == SSActionEventType.Started) return;
        if (Objects.isOnBoat())
        {
            CoastController whichCoast;
            if (boat.get_State() == -1)
            { 
                whichCoast = toCoast;
            }
            else
            {
                whichCoast = fromCoast;
            }
            boat.GetOffBoat(Objects.getName());
            FSAmanager.CharacterMove(Objects, whichCoast.getEmptyPosition());
            Objects.getOnCoast(whichCoast);
            whichCoast.getOnCoast(Objects);

        }
        else
        {
            CoastController whichCoast = Objects.getCoastController(); 

            if (boat.getEmptyIndex() == -1)
            {
                return;
            }

            if (whichCoast.get_State() != boat.get_State())  
                return;

            whichCoast.getOffCoast(Objects.getName());
            FSAmanager.CharacterMove(Objects, boat.getEmptyPosition());
            Objects.getOnBoat(boat);
            boat.GetOnBoat(Objects);
        }
        //通知controller游戏结束，显示win或lose
        UserGUI.SetState = judge.Check();
    }

    public void MoveBoat()
    {
        if (FSAmanager.Complete == SSActionEventType.Started || boat.isEmpty()) return;
        FSAmanager.BoatMove(boat);
        //新增的裁判类
        UserGUI.SetState = judge.Check();
    }

    public void Restart()
    {
        fromCoast.reset();
        toCoast.reset();
        foreach (Character gameobject in Character)
        {
            gameobject.reset();
        }
        boat.reset();
    }
}