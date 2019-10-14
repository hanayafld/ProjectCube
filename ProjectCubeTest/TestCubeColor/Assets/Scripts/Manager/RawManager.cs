using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class RawManager
{
    protected Dictionary<GameEnums.dataType, Dictionary<int, RawData>> dicDatas = new Dictionary<GameEnums.dataType, Dictionary<int, RawData>>();

    #region 데이터 로드
    //path별로 각자 override해서 사용함
    public virtual void LoadAllData()
    {

    }

    protected void LoadDataImpl<T>(string path) where T : RawData
    {
        //왠지모르겠는데 24번라인에서 오류남 null missing 뜸 추후확인
        
        //Dictionary<int, RawData> dicTemp = new Dictionary<int, RawData>();
        //var textAsset = Resources.Load<TextAsset>(path);
        //var dataArr = JsonConvert.DeserializeObject<T[]>(textAsset.text);

        //foreach (var data in dataArr)
        //{
        //    dicTemp.Add(data.id, data);
        //}

        //var type = Enum.Parse(typeof(GameEnums.dataType), typeof(T).ToString());

        //this.dicDatas.Add((GameEnums.dataType)type, dicTemp);

        //Debug.LogFormat("{0}데이터 로드완료", (GameEnums.dataType)type);
    }
    #endregion

    #region 데이터 리턴
    //Dictionary 리턴
    public Dictionary<int, T> GetData<T>(GameEnums.dataType type) where T : RawData
    {
        Dictionary<int, T> dicTemp = new Dictionary<int, T>();
        var checkKey = this.dicDatas.ContainsKey(type);
        Debug.LogFormat("딕셔너리리턴 : {0}", type);

        if (checkKey == false)
        {
            Debug.LogFormat("<color=red> Key Error!!! </color>");
            return null;
        }
        else
        {
            foreach (var data in this.dicDatas[type])
            {
                dicTemp.Add(data.Key, (T)data.Value);
            }
        }

        return dicTemp;
    }

    //Data 리턴
    public T GetData<T>(GameEnums.dataType type, int index) where T : RawData
    {
        T data;
        Debug.LogFormat("데이터리턴 : {0}",type);
        var checkKey = dicDatas.ContainsKey(type);

        if (checkKey == false)
        {
            Debug.LogFormat("<color=red> Key Error!!! </color>");
            return null;
        }
        else
        {
            data = (T)this.dicDatas[type][index];
        }

        return data;
    }
    #endregion
}
