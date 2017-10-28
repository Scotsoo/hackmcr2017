using System;
using System.Linq;
using UnityEngine;

public class MoanymonBehaviour: MonoBehaviour
{
    public GameObject Camera;

    public void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    //public bool DetectClick(string[] objects)
    public bool DetectClick()
    {
        // if left button pressed...
        if (!Input.GetMouseButtonDown(0))
        {
            return false;
        }

        Ray ray = Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        return hit.transform != null && hit.transform.gameObject != null && gameObject == hit.transform.gameObject;
    }
}

