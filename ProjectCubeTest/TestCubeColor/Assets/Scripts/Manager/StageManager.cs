using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager
{
    private static StageManager instance;

    private StageManager()
    {

    }

    public static StageManager GetInstance()
    {
        if(StageManager.instance == null)
        {
            StageManager.instance = new StageManager();
        }
        return StageManager.instance;
    }

    #region 스테이지 설정
    public void SetStage(int id)
    {
        this.SetStageImpl(id);
    }

    private void SetStageImpl(int id)
    {
        //스테이지 생성
    }
    #endregion
}
