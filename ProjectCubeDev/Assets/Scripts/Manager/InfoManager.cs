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
        if (InfoManager.instance == null)
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

    //이부분 갈아엎어야함
    private void LoadInfoImpl()
    {
        var checkDirectory = Directory.Exists(Application.persistentDataPath + path);

        //신규유저
        if (checkDirectory == false)
        {
            Debug.Log("신규");
            Directory.CreateDirectory(Application.persistentDataPath + path);
            this.SetNewUserInfo();
            this.SaveUserInfo(false);
        }
        //기존유저
        if (checkDirectory)
        {
            var checkInfo = File.Exists(Application.persistentDataPath + path + "/userInfo.json");
            if (checkInfo)
            {
                Debug.Log("기존");
                Debug.Log(Application.persistentDataPath + path + "/userInfo.json");
                var text = File.ReadAllText(Application.persistentDataPath + path + "/userInfo.json");
                this.userInfo = JsonConvert.DeserializeObject<UserInfo>(text);
            }
            else
            {
                Debug.Log("신규");
                this.SetNewUserInfo();
                this.SaveUserInfo(false);
            }
        }
    }
    #endregion

    #region 신규유저 인포 세팅
    private void SetNewUserInfo()
    {
        this.userInfo = new UserInfo();
        //새로하기 선택시 세팅값 설정
        this.userInfo.stageLevel = 1;
    }
    #endregion

    #region 유저인포 리턴
    public UserInfo GetUserInfo()
    {
        return this.userInfo;
    }
    #endregion

    #region 유저인포 저장
    public void SaveUserInfo(bool isLevelUp)
    {
        if (isLevelUp)
        {
            Debug.Log("레벨업!");
            this.userInfo.stageLevel++;
        }
        var json = JsonConvert.SerializeObject(this.userInfo);
        File.WriteAllText(Application.persistentDataPath + path + "/userInfo.json", json, System.Text.Encoding.UTF8);
        Debug.Log("Info저장 완료");
    }
    #endregion
}
