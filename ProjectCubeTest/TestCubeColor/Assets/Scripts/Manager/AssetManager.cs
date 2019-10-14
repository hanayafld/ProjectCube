using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

//RawManager로부터 Dictionary, Data Load/Return 메소드 상속받아 사용함

public class AssetManager : RawManager
{
    private static AssetManager instance;
    private Dictionary<int, string> dicPrefabs = new Dictionary<int, string>();
    public SpriteAtlas atlas; //나중에 텍스쳐 아틀라스로 사용할 것

    private AssetManager()
    {
        this.atlas = Resources.Load<SpriteAtlas>("경로");
    }

    public static AssetManager GetInstance()
    {
        if (AssetManager.instance == null)
        {
            AssetManager.instance = new AssetManager();
        }
        return AssetManager.instance;
    }

    #region 데이터로드
    public override void LoadAllData()
    {
        this.LoadDataImpl<PrefabData>("경로");
    }
    #endregion
}
