using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    [HideInInspector]
    public bool isMoveWait = false;//이게 true면, 이동조작가능, false면 이동조작불가
    public Sensor[] sensors;// 센서들 순서대로 0~4, 위 아래 왼 오 바닥센서(납작한 박스콜라이더로 되어있음)
    public GameObject cubeBody;
    public PanelGoal panelGoal;

    public int cubeSpeed = 1;
    private CubeMove cubeMove;
    public System.Action onGameClear;
    public System.Action onGameFailed;
    public System.Action onMoveCount;//이동횟수 알려줌

    void Awake()
    {
        this.cubeMove = GetComponent<CubeMove>();
        this.panelGoal.onGameClear = () =>
        {
            StartCoroutine(this.GameClear());
        }; 
    }

    #region 특수 타일 감지
    private void OnTriggerEnter(Collider other)//큐브가 올라가는 순간 타일을 읽음
    {
        if (other.tag == "Tile")
        {
            StartCoroutine(this.Tile());
        }
        else if (other.tag == "Goal")
        {
            StartCoroutine(this.Tile());
        }
        else if (other.tag == "OutLine")
        {
            Debug.Log("OutLine");
            this.onGameFailed();
        }
        else if (other.tag == "Trap")
        {
            Debug.Log("Trap");
            this.onGameFailed();
        }
        else if (other.tag == "RotateLeft")
        {
            Debug.Log("RotateLeft");
            StartCoroutine(this.TileRotate(-1));
        }
        else if (other.tag == "RotateRight")
        {
            Debug.Log("RotateRight");
            StartCoroutine(this.TileRotate(1)); ;
        }
        else if (other.tag == "Slide")
        {
            Debug.Log("Slide");
            StartCoroutine(this.TileSlide(other.transform.eulerAngles.y));
        }
        else if (other.tag == "In")
        {
            Debug.Log("In");
            StartCoroutine(this.TileInOut(other.transform.Find("Tile(Out)").transform.position));
        }
    }
    #endregion

    #region 특수 타일 효과 모음
    private IEnumerator Tile()
    {
        yield return new WaitForSeconds(1.5f / cubeSpeed);
        this.isMoveWait = true;
    }

    private IEnumerator TileInOut(Vector3 tileOut)
    {
        yield return new WaitForSeconds(1.5f / cubeSpeed);

        //내려감
        Vector3 dir = new Vector3(0, 1, 0);
        for (int i = 0; i < 30 / this.cubeSpeed; i++)
        {
            this.transform.position += 1 / 30f * -dir * this.cubeSpeed;
            yield return null;
        }

        //이동
        this.transform.position = tileOut;
        yield return new WaitForSeconds(1.5f / cubeSpeed);

        //올라옴
        for (int i = 0; i < 30 / this.cubeSpeed; i++)
        {
            this.transform.position += 1 / 30f * dir * this.cubeSpeed;
            yield return null;
        }

        this.isMoveWait = true;
    }

    private IEnumerator TileSlide(float tileDir)//타일의 화살표 위치를 받음
    {
        yield return new WaitForSeconds(1.5f / cubeSpeed);
        Debug.Log(tileDir);
        Vector3 dir = new Vector3(0, 0, 0);//큐브가 이동 할 방향

        if (tileDir == 0)
        {
            //위
            dir.x = -1;
        }
        else if (tileDir == 90)
        {
            //오른
            dir.z = 1;
        }
        else if (tileDir == 180)
        {
            //아래
            dir.x = 1;
        }
        else if (tileDir == 270)
        {
            //왼
            dir.z = -1;
        }

        Debug.Log(dir);
        for (int i = 0; i < 30 / this.cubeSpeed; i++)
        {
            this.transform.position += 1 / 30f * dir * this.cubeSpeed;
            this.cubeMove.mainCamera.transform.position += 1 / 30f * dir * this.cubeSpeed;
            yield return null;
        }
    }

    private IEnumerator TileRotate(int dir)
    {
        yield return new WaitForSeconds(1.5f / cubeSpeed);

        for (int i = 0; i < 45 / this.cubeSpeed; i++)
        {
            this.cubeBody.transform.Rotate(0, dir * this.cubeSpeed * 2, 0, Space.World);
            yield return null;
        }

        this.isMoveWait = true;
    }
    #endregion
    
    #region 게임 클리어와 실패 시
    private IEnumerator GameClear()
    {
        this.onGameClear();
        yield return new WaitForSeconds(1.5f / cubeSpeed);

        float pointA = this.transform.position.y;

        while (true)
        {
            if (this.transform.position.y <= pointA - 1.1f)
            {
                break;
            }
            this.transform.Translate(new Vector3(0, -0.03f, 0));
            yield return null;
        }
        yield return null;
    }
    #endregion
}
