using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneStage : MonoBehaviour
{
    public Button btnSave;
    public Button btnQuit;

    private int stageNum;

    public void InitStage(int stageNum)
    {
        this.stageNum = stageNum;
        //stageNum에 맞는 스테이지 불러와서 실행

        this.btnSave.onClick.AddListener(() =>
        {
            Debug.Log("저장됨");
        });

        this.btnQuit.onClick.AddListener(() =>
        {
            //종료할건지, 타이틀로 돌아갈건지 확인
            GameSceneManager.GetInstance().LoadScene(3);
        });
    }
}
