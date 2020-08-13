using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

//RawManager로부터 Dictionary, Data Load/Return 메소드 상속받아 사용함

public class DataManager
{
    private static DataManager instance;

    private StageData stageData;
    public Dictionary<int, StageData> dicStageData = new Dictionary<int, StageData>();

    private DataManager()
    {
        this.stageData = new StageData();
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
    public void LoadStageData()
    {
        var ta = Resources.Load<TextAsset>("Json/Datas/stageData");
        var json = ta.text;
        var arrDatas = JsonConvert.DeserializeObject<StageData[]>(json);
        foreach (var data in arrDatas)
        {
            this.dicStageData.Add(data.id, data);
        }
    }
    #endregion
}
