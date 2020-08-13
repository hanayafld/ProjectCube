using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTitle : MonoBehaviour
{
    //public Button btnStart;
    public Sprite[] spriteBtnBlues;
    public Sprite[] spriteBtnOffs;
    public Sprite[] spriteBtnReds;

    public Button[] btnStages;

    public void Init(string name = "", int prefabId = 0)
    {

        /*var dataManager = DataManager.GetInstance();
        dataManager.LoadAllData();*///아직은 데이터 필요없음

        #region Info
        var infoManager = InfoManager.GetInstance();
        infoManager.LoadInfo();
        var userInfo = infoManager.GetUserInfo();
        var userStageLevel = userInfo.stageLevel;
        #endregion
        
        this.SetBtnStage(userStageLevel);

        for (int i = 0; i < this.btnStages.Length; i++)
        {
            int btnIndex = i;
            btnStages[btnIndex].onClick.AddListener(() => this.LoadStageScene(btnIndex));
        }
    }

    private void LoadStageScene(int stageId)
    {
        GameSceneManager.GetInstance().LoadScene(4, stageId);
    }

    private void SetBtnStage(int userStageLevel)
    {
        var stageLevel = userStageLevel - 1;
        for (int i = 0; i < this.btnStages.Length; i++)
        {
            if (i == stageLevel)
            {
                this.btnStages[i].GetComponent<Image>().sprite = this.spriteBtnReds[i];
                this.btnStages[i].interactable = true;
            }
            else if (i < stageLevel)
            {
                this.btnStages[i].gameObject.GetComponent<Image>().sprite = this.spriteBtnBlues[i];
                this.btnStages[i].interactable = true;
            }
            else
            {
                this.btnStages[i].interactable = false;
            }
        }
    }
}
