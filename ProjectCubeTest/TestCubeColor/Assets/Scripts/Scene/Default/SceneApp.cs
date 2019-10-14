using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneApp : MonoBehaviour
{
    public Button btn;

    public void Start()
    {
        DontDestroyOnLoad(this);
        this.btn.onClick.AddListener(() =>
        {
            GameSceneManager.GetInstance().LoadScene(2);
        });
    }
}
