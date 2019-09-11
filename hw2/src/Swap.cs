using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(delegate () {
            GameObject blank = GameObject.Find("Blank");
            double tempX = this.transform.position.x;
            double tempY = this.transform.position.y;
            //相邻
            if (System.Math.Abs(tempX-blank.transform.position.x)+ System.Math.Abs(tempY - blank.transform.position.y) == 50)
            {
                Vector3 pos = this.transform.position;
                this.transform.position = blank.transform.position;
                blank.transform.position = pos;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
