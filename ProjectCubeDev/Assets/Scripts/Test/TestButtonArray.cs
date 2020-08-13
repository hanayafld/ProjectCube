using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButtonArray : MonoBehaviour
{
    public Button[] btns;

    private void Start()
    {
        for (int i = 0; i < this.btns.Length; i++)
        {
            int index = i;
            btns[index].onClick.AddListener(() => this.TaskOnClick(index));
        }
    }

    private void TaskOnClick(int index)
    {
        Debug.Log("니가 누른 버튼" + index, btns[index]);
    }
}
