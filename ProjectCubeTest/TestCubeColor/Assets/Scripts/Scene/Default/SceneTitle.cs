using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTitle : MonoBehaviour
{
    public Button btnNew;
    public Button btnLoad;
    public Button btnOption;
    public Button btnQuit;

    public void Init(string name = "", int prefabId = 0)
    {
        var dm = DataManager.GetInstance();
        dm.LoadAllData();
        var im = InfoManager.GetInstance();
        im.LoadInfo();

        Debug.Log("title 이닛됨");
        this.btnNew.onClick.AddListener(() =>
        {
            //새로하기
            GameSceneManager.GetInstance().LoadScene(4);
        });

        this.btnLoad.onClick.AddListener(() =>
        {
            //이어하기
            GameSceneManager.GetInstance().LoadScene(4);
        });

        this.btnOption.onClick.AddListener(() =>
        {
            //옵션설정 팝업창 띄우기
        });

        this.btnQuit.onClick.AddListener(() =>
        {
            //게임종료하시겠습니까? 팝업창 띄우기
        });
    }
}
