using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGoal : MonoBehaviour
{
    public System.Action onGameClear;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal")
        {
            this.onGameClear();
        }
    }
}
