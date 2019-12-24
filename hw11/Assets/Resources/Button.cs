using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;


public class Button : MonoBehaviour, IVirtualButtonEventHandler
{
    private GameObject cube;
    void Start()
    {
        VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; ++i)
        {
            vbs[i].RegisterEventHandler(this);
        }
        cube = transform.Find("Cube").gameObject;
    }
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        switch (vb.VirtualButtonName)
        {
            case "b1":
                cube.transform.position = new Vector3(1f, 0f, 0f);
                break;
            case "b2":
                cube.transform.position = new Vector3(-1f, 0f, 0f);
                break;
        }
        Debug.Log("OnButtonPressed: " + vb.VirtualButtonName);
    }
    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        switch (vb.VirtualButtonName)
        {
            case "b1":
                break;
            case "b2":
                break;
        }
        Debug.Log("OnButtonReleased: " + vb.VirtualButtonName);
    }
}
