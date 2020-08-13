using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

//Dictionary, 데이터 로드, 리턴 메소드 RawManager를 상속받아 사용함

public class SoundManager : RawManager
{
    //사운드매니저인데, 사용하는 건 작성해야함
    private static SoundManager instance;

    private SoundManager()
    {

    }
    
    public static SoundManager GetInstance()
    {
        if(SoundManager.instance == null)
        {
            SoundManager.instance = new SoundManager();
        }
        return SoundManager.instance;
    }

    #region 데이터 로드
    public override void LoadStageData()
    {

    }
    #endregion
}
