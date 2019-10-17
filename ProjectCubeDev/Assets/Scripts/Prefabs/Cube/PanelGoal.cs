using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGoal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal")
        {
            Debug.Log("클리어!");
        }
    }
}
