using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneApp : MonoBehaviour
{
    public void Start()
    {
        DontDestroyOnLoad(this);

        var dataManager = DataManager.GetInstance();
        dataManager.LoadStageData();

        GameSceneManager.GetInstance().LoadScene(2);
    }
}
