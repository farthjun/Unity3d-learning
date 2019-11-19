using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Patrols;

public class HeroStatus : MonoBehaviour {
    public int standOnArea = -1;
    //GameObject camera;


    void Start () {
        //camera = GameObject.Find("Main Camera");
    }
	
	void Update () {
        modifyStandOnArea();
        //camera.transform.position = new Vector3(this.transform.position.x, 10, this.transform.position.z);
	}

    void modifyStandOnArea() {
        float posX = this.gameObject.transform.position.x;
        float posZ = this.gameObject.transform.position.z;
        if (posZ >= FenchLocation.FenchHori) {
            if (posX < FenchLocation.FenchVertLeft)
                standOnArea = 0;
            else if (posX > FenchLocation.FenchVertRight)
                standOnArea = 2;
            else
                standOnArea = 1;
        }
        else {
            if (posX < FenchLocation.FenchVertLeft)
                standOnArea = 3;
            else if (posX > FenchLocation.FenchVertRight)
                standOnArea = 5;
            else
                standOnArea = 4;
        }
    }
}
