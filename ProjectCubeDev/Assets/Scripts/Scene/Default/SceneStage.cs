using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneStage : MonoBehaviour
{
    public Cube cube;

    public Button btnRegameKey;
    public Button btnHomeKey;
    public Button btnNextStage;
    public Button btnStart;

    public GameObject buttons;
    public GameObject clearMessage;
    public GameObject failedMessage;
    public GameObject objTxtMove;
    public GameObject objTxtTime;

    public Text txtCurrentMove;
    public Text txtMaxMove;
    public Text txtTimeLimit;

    private int currentMoveCount;
    private int maxMoveCount;
    private int timeLimit;

    private int stageId;
    private Camera mainCamera;

    private bool isClear = false;
    private bool isFaeiled = false;

    private void Awake()
    {
    }

    public void InitStage(int stageId)
    {
        this.stageId = stageId;
        Debug.LogFormat("불러온 스테이지 id : {0}", this.stageId);
        //stageNum에 맞는 스테이지 불러와서 실행

        #region Data
        var dataManager = DataManager.GetInstance();
        var stageData = dataManager.dicStageData[this.stageId];
        #endregion

        #region Info
        var infoManager = InfoManager.GetInstance();
        var userInfo = infoManager.GetUserInfo();
        var userStageLevel = userInfo.stageLevel;
        #endregion

        #region Stage Reset
        this.cube.transform.position = new Vector3(0, 1, 0);
        this.SetStageMap(stageData.path);
        this.SetCamara(stageData.cameraX, stageData.cameraY, stageData.cameraZ);

        this.btnStart.gameObject.SetActive(true);
        this.buttons.SetActive(false);
        this.cube.isMoveWait = false;

        this.SetDefeat(stageData.maxMove, stageData.timeLimit);
        #endregion Stage Reset

        this.cube.onMoveCount = () =>
        {
            this.MoveCounting();
        };
        this.cube.onGameClear = () =>
        {
            if(this.isFaeiled == false)
            {
                this.isClear = true;
                this.objTxtMove.SetActive(false);
                this.objTxtTime.SetActive(false);
                this.StageClear(userStageLevel, stageData.stageLevel);
                this.txtTimeLimit.gameObject.SetActive(false);
                this.txtCurrentMove.gameObject.SetActive(false);
                this.txtMaxMove.gameObject.SetActive(false);
            }
        };
        this.cube.onGameFailed = () =>
        {
            if(this.isClear == false)
            {
                this.isFaeiled = true;
                this.objTxtMove.SetActive(false);
                this.objTxtTime.SetActive(false);
                this.StageFailed(userStageLevel, stageData.stageLevel);
                this.txtTimeLimit.gameObject.SetActive(false);
                this.txtCurrentMove.gameObject.SetActive(false);
                this.txtMaxMove.gameObject.SetActive(false);
            }
        };

        #region Button
        this.btnStart.onClick.AddListener(() =>
        {
            this.btnStart.gameObject.SetActive(false);
            this.buttons.SetActive(true);
            this.cube.isMoveWait = true;
            StartCoroutine(this.MoveCamera());
            StartCoroutine(this.Timer());
        });

        this.btnRegameKey.onClick.AddListener(() =>
        {
            Debug.Log("다시하기");
            GameSceneManager.GetInstance().LoadScene(4, this.stageId);
        });

        this.btnHomeKey.onClick.AddListener(() =>
        {
            Debug.Log("난이도 선택");
            GameSceneManager.GetInstance().LoadScene(3);
        });

        this.btnNextStage.onClick.AddListener(() =>
        {
            Debug.LogFormat("다음 단계 : {0}", this.stageId + 1);
            GameSceneManager.GetInstance().LoadScene(4, this.stageId + 1);
        });
        #endregion
    }

    #region 스테이지 클리어 관련
    private IEnumerator Timer()
    {
        while (true)
        {
            if (this.timeLimit == 0)
            {
                Debug.Log("타임아웃");
                this.cube.onGameFailed();
                break;
            }
            yield return new WaitForSeconds(1.0f);
            this.timeLimit--;
            this.txtTimeLimit.text = this.timeLimit.ToString();
        }
        yield return null;
    }

    private void MoveCounting()
    {
        if (this.currentMoveCount == this.maxMoveCount)
        {
            Debug.Log("이동아웃");
            this.cube.onGameFailed();
        }
        this.currentMoveCount++;
        this.txtCurrentMove.text = this.currentMoveCount.ToString();
    }

    private void SetDefeat(int count, int time)
    {
        if (count == -1)
        {
            this.objTxtTime.SetActive(false);
            this.objTxtMove.SetActive(false);
            this.txtCurrentMove.gameObject.SetActive(false);
            this.txtMaxMove.gameObject.SetActive(false);
            this.currentMoveCount = count * 100;
        }
        else
        {
            this.txtCurrentMove.text = "0";
            this.txtMaxMove.text = "/" + count.ToString();
            this.currentMoveCount = 0;
            this.maxMoveCount = count;
        }

        if (time == -1)
        {
            this.txtTimeLimit.gameObject.SetActive(false);
            this.timeLimit = -100;
        }
        else
        {
            this.txtTimeLimit.text = time.ToString();
            this.timeLimit = time;
        }
    }

    private void StageClear(int userStageLevel, int currentStageLevel)
    {
        //내 레벨이 스테이지 레벨과 같을 경우 레벨업을 한다.
        if (userStageLevel == currentStageLevel)
        {
            var infoManager = InfoManager.GetInstance();
            infoManager.SaveUserInfo(true);
        }

        //클리어 메시지와 클리어 UI 설정
        var dataManager = DataManager.GetInstance();

        if (currentStageLevel == dataManager.dicStageData.Count)
        {
            var rtRegameKey = (RectTransform)this.btnRegameKey.transform;
            rtRegameKey.sizeDelta = new Vector3(180, 180);
            this.btnRegameKey.transform.localPosition = new Vector3(-120, -120, 0);
            var rtHomeKey = (RectTransform)this.btnHomeKey.transform;
            rtHomeKey.sizeDelta = new Vector3(180, 180);
            this.btnHomeKey.transform.localPosition = new Vector3(120, -120, 0);
        }
        else
        {
            this.btnNextStage.gameObject.SetActive(true);
            var rtRegameKey = (RectTransform)this.btnRegameKey.transform;
            rtRegameKey.sizeDelta = new Vector3(180, 180);
            this.btnRegameKey.transform.localPosition = new Vector3(-240, -120, 0);
            var rtHomeKey = (RectTransform)this.btnHomeKey.transform;
            rtHomeKey.sizeDelta = new Vector3(180, 180);
            this.btnHomeKey.transform.localPosition = new Vector3(0, -120, 0);
        }
        this.buttons.SetActive(false);
        this.clearMessage.SetActive(true);

    }

    private void StageFailed(int userStageLevel, int currentStageLevel)
    {
        //UI설정
        this.buttons.SetActive(false);
        this.failedMessage.SetActive(true);

        if (userStageLevel > currentStageLevel)
        {
            this.btnNextStage.gameObject.SetActive(true);

            var rtRegameKey = (RectTransform)this.btnRegameKey.transform;
            rtRegameKey.sizeDelta = new Vector3(180, 180);
            this.btnRegameKey.transform.localPosition = new Vector3(-240, -120, 0);
            var rtHomeKey = (RectTransform)this.btnHomeKey.transform;
            rtHomeKey.sizeDelta = new Vector3(180, 180);
            this.btnHomeKey.transform.localPosition = new Vector3(0, -120, 0);
        }
        else
        {
            var rtRegameKey = (RectTransform)this.btnRegameKey.transform;
            rtRegameKey.sizeDelta = new Vector3(180, 180);
            this.btnRegameKey.transform.localPosition = new Vector3(-120, -120, 0);
            var rtHomeKey = (RectTransform)this.btnHomeKey.transform;
            rtHomeKey.sizeDelta = new Vector3(180, 180);
            this.btnHomeKey.transform.localPosition = new Vector3(120, -120, 0);
        }
    }

    #endregion

    #region 스테이지 맵 세팅
    private void SetStageMap(string path)
    {
        Debug.LogFormat("셋 스테이지 맵 경로 : {0}", path);
        var stagePrefabs = Resources.Load<GameObject>(path);
        var stageMap = Instantiate<GameObject>(stagePrefabs);
    }
    #endregion

    #region 카메라 셋팅
    private void SetCamara(float x, float y, float z)
    {
        Debug.Log("카메라 셋팅");
        this.mainCamera = FindObjectOfType<Camera>();
        this.mainCamera.transform.position = new Vector3(x, y, z);
    }

    private IEnumerator MoveCamera()
    {
        Vector3 cameraPos = this.mainCamera.transform.position;
        Vector3 cameraPoint = new Vector3(4, 12, 0);

        var distancePos = new Vector3(cameraPos.x - cameraPoint.x, cameraPos.y - cameraPoint.y, cameraPos.z - cameraPoint.z);
        if (distancePos.x < 0)
        {
            while (true)
            {
                if (cameraPoint.x - this.mainCamera.transform.position.x < 0)
                {
                    this.mainCamera.transform.position = cameraPoint;//정렬
                    break;
                }
                this.mainCamera.transform.position += new Vector3(-distancePos.x / (Time.deltaTime * 2000), -distancePos.y / (Time.deltaTime * 2000), -distancePos.z / (Time.deltaTime * 2000));

                yield return null;
            }
        }
        else
        {
            while (true)
            {
                if (cameraPoint.x - this.mainCamera.transform.position.x > 0)
                {
                    this.mainCamera.transform.position = cameraPoint;//정렬
                    break;
                }
                this.mainCamera.transform.position += new Vector3(-distancePos.x / (Time.deltaTime * 2000), -distancePos.y / (Time.deltaTime * 2000), -distancePos.z / (Time.deltaTime * 2000));

                yield return null;
            }
        }
        yield return null;
    }
    #endregion
}
