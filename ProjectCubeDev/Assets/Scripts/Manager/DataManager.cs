using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//RawManager로부터 Dictionary, Data Load/Return 메소드 상속받아 사용함

public class DataManager : RawManager
{
    private static DataManager instance;

    private DataManager()
    {

    }

    public static DataManager GetInstance()
    {
        if (DataManager.instance == null)
        {
            DataManager.instance = new DataManager();
        }
        return DataManager.instance;
    }

    #region 데이터로드
    public override void LoadAllData()
    {
        this.LoadDataImpl<PrefabData>("경로");
    }
    #endregion
}
