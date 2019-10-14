using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class InfoManager
{
    private static InfoManager instance;
    private string path = "/Info";

    //Info Dictionary
    private UserInfo userInfo;

    private InfoManager()
    {
        this.userInfo = new UserInfo();
    }

    public static InfoManager GetInstance()
    {
        if(InfoManager.instance == null)
        {
            InfoManager.instance = new InfoManager();
        }

        return InfoManager.instance;
    }

    #region 유저인포 로드
    public void LoadInfo()
    {
        this.LoadInfoImpl();
    }

    private void LoadInfoImpl()
    {
        var checkDirectory = Directory.Exists(Application.persistentDataPath + path);

        //신규유저
        if(checkDirectory == false)
        {
            Debug.Log("신규");
            Directory.CreateDirectory(Application.persistentDataPath + path);
            this.SetNewUserInfo();
        }
        //기존유저
        if(checkDirectory)
        {
            Debug.Log("기존");
            var text = File.ReadAllText(Application.persistentDataPath + path + "/userInfo.json");
            this.userInfo = JsonConvert.DeserializeObject<UserInfo>(text);
        }
    }
    #endregion

    #region 신규유저 인포 세팅
    private void SetNewUserInfo()
    {
        this.userInfo = new UserInfo();
        //새로하기 선택시 세팅값 설정
    }
    #endregion

    #region 유저인포 리턴
    public UserInfo GetUserInfo()
    {
        return this.userInfo;
    }
    #endregion

    #region 유저인포 저장
    public void SaveUserInfo()
    {
        var json = JsonConvert.SerializeObject(this.userInfo);
        File.WriteAllText(Application.persistentDataPath + path + "/userInfo.json", json, System.Text.Encoding.UTF8);
    }
    #endregion

    #region 인포 업데이트
    //인포업데이트
    public void UpdateUserInfo()
    {
        
    }
    #endregion
}
