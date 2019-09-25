using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public Rigidbody ri;
    public float vX = 5;//水平初速度
    public float vY = 0;//垂直初速度
    public int g = 10;//垂直加速度

    void Update()
    {
        GameObject cube = GameObject.Find("Cube");
        cube.transform.Translate(vX * Time.deltaTime* Vector3.right + 
            Vector3.down * vY * Time.deltaTime);
        vY += g * Time.deltaTime;
    }

    void Start()
    {
        /*方法1：transform.position
        GameObject cube = GameObject.Find("Cube");
        cube.transform.position += Vector3.right * vX * Time.deltaTime;
        cube.transform.position += Vector3.down * vY * Time.deltaTime;
        vY += g * Time.deltaTime;
        */

        /*方法2：rigid，需要添加给移动物体
        ri = this.GetComponent<Rigidbody>();
        ri.MovePosition(this.transform.position + Vector3.right * vX * Time.deltaTime
            + Vector3.down * vY * Time.deltaTime);
        vY += g * Time.deltaTime;
        */
    }
}
