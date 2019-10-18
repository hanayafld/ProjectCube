using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public bool isMove = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Debug.Log("막힘");
            this.isMove = false;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Wall")
    //    {
    //        this.isMove = true;
    //    }
    //}
}