using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLogo : MonoBehaviour
{
    public Button btn;

    public void Init(string name = "", int prefabId = 0)
    {
        Debug.Log("이닛됨");
        var a = SceneManager.GetActiveScene();
        Debug.LogFormat("Active Scene : {0}", a.ToString());
        this.btn.onClick.AddListener(() =>
        {
            GameSceneManager.GetInstance().LoadScene(3);
        });
    }
}
