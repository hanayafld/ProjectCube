using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager
{
    private static GameSceneManager instance;
    private List<string> listBeforeScene;
    private string caller;

    private GameSceneManager()
    {
        this.listBeforeScene = new List<string>();
    }

    public static GameSceneManager GetInstance()
    {
        if(GameSceneManager.instance == null)
        {
            GameSceneManager.instance = new GameSceneManager();
        }
        return GameSceneManager.instance;
    }

    #region 로드씬
    /*
    public void LoadAdditiveScene(int id, string caller = "", int prefabId = 0)
    {
        this.listBeforeScene.Add(SceneManager.GetActiveScene().name);

        var sceneName = ((SceneEnums.sceneName)id).ToString();
        var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        
        operation.completed += (asyncOper) =>
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

            if(caller != "")
            {
                this.InitScene((SceneEnums.sceneName)id, caller, prefabId);
            }
            else
            {
                this.InitScene((SceneEnums.sceneName)id);
            }
        };
    }*/

    public void LoadScene(int id, int stageLevel = 0)
    {
        this.listBeforeScene = new List<string>();

        var sceneName = ((SceneEnums.sceneName)id).ToString();
        var operation = SceneManager.LoadSceneAsync(sceneName);

        Debug.LogFormat("불러온 씬 : {0}", sceneName);

        operation.completed += (asyncOper) =>
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            this.InitScene((SceneEnums.sceneName)id, "", stageLevel);
        };   
    }
    #endregion

    #region 언로드씬
    public void UnloadScene()
    {
        var operation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        operation.completed += (asyncOper) =>
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(this.listBeforeScene[this.listBeforeScene.Count-1]));
            this.listBeforeScene.RemoveAt(this.listBeforeScene.Count - 1);
        };
    }

    public void UnloadAllScene()
    {
        foreach(var name in this.listBeforeScene)
        {
            SceneManager.UnloadSceneAsync(name);
        }
    }
    #endregion

    #region 씬초기화
    //Scene을 초기화하는 과정이므로 Init메서드는 Scene의 대표 오브젝트 스크립트에서 정의
    //caller는 LoadScene 메소드를 호출한 Scene을 뜻함
    private void InitScene(SceneEnums.sceneName sceneName, string caller = "", int stageLevel = 0)
    {
        switch(sceneName)
        {
            case SceneEnums.sceneName.Logo:
                {
                    var go = GameObject.FindObjectOfType<SceneLogo>();
                    go.Init();
                    break;
                }
            case SceneEnums.sceneName.Title:
                {
                    var go = GameObject.FindObjectOfType<SceneTitle>();
                    go.Init();
                    break;
                }
            case SceneEnums.sceneName.Loading:
                {
                    var go = GameObject.FindObjectOfType<SceneLoading>();
                    go.Init();
                    break;
                }
            case SceneEnums.sceneName.Stage:
                {
                    var go = GameObject.FindObjectOfType<SceneStage>();
                    go.InitStage(stageLevel);
                    break;
                }
            default:
                break;
        }
    }
    #endregion
}
