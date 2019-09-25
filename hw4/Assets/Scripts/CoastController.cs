using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoastController
{
    readonly GameObject coast;
    readonly Vector3 from_pos = new Vector3(-16, -6, 10);
    readonly Vector3 to_pos = new Vector3(16, -6, 10);
    readonly Vector3[] positions;
    readonly int State;    // to->-1, from->1

    Character[] passengerPlaner;

    public CoastController(string _State)
    {
        positions = new Vector3[] {new Vector3(-21,-2.5F,10), new Vector3(-19,-2.5F,10), new Vector3(-17,-2.5F,10),
                new Vector3(-15,-2.5F,10), new Vector3(-13,-2.5F,10), new Vector3(-11,-2.5F,10)};

        passengerPlaner = new Character[6];

        if (_State == "from")
        {
            coast = Object.Instantiate(Resources.Load("Prefabs/land1", typeof(GameObject)), from_pos, Quaternion.identity, null) as GameObject;
            coast.name = "from";
            State = 1;
        }
        else
        {
            coast = Object.Instantiate(Resources.Load("Prefabs/land2", typeof(GameObject)), to_pos, Quaternion.identity, null) as GameObject;
            coast.name = "to";
            State = -1;
        }
    }

    public int getEmptyIndex()
    {
        for (int i = 0; i < passengerPlaner.Length; i++)
        {
            if (passengerPlaner[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    public Vector3 getEmptyPosition()
    {
        Vector3 pos = positions[getEmptyIndex()];
        pos.x *= State;
        return pos;
    }

    public void getOnCoast(Character ObjectControl)
    {
        int index = getEmptyIndex();
        passengerPlaner[index] = ObjectControl;
    }

    public Character getOffCoast(string passenger_name)
    {   // 0->priest, 1->devil
        for (int i = 0; i < passengerPlaner.Length; i++)
        {
            if (passengerPlaner[i] != null && passengerPlaner[i].getName() == passenger_name)
            {
                Character charactorCtrl = passengerPlaner[i];
                passengerPlaner[i] = null;
                return charactorCtrl;
            }
        }
        return null;
    }

    public int get_State()
    {
        return State;
    }

    public int[] GetobjectsNumber()
    {
        int[] count = { 0, 0 };
        for (int i = 0; i < passengerPlaner.Length; i++)
        {
            if (passengerPlaner[i] == null)
                continue;
            if (passengerPlaner[i].getType() == 0)
            {   // 0->priest, 1->devil
                count[0]++;
            }
            else
            {
                count[1]++;
            }
        }
        return count;
    }

    public void reset()
    {
        passengerPlaner = new Character[6];
    }

}