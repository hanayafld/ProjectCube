using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [HideInInspector]
    public bool isMoveWait = true;//이게 true면, 이동가능, false면 이동 불가
    public int moveCount;//이동횟수 알려줌
    public Sensor[] sensors;// 센서들 순서대로 0~3, 위 아래 왼 오(납작한 박스콜라이더로 되어있음)

    private CubeMove cubeMove;

    void Awake()
    {
        this.cubeMove = GetComponent<CubeMove>();
    }

    #region 특수 타일 감지
    private void OnTriggerEnter(Collider other)//큐브가 올라가는 순간 타일을 읽음
    {
        if (other.tag == "OutLine")
        {
            Debug.Log("떨어져유");
            StartCoroutine(this.Fall());
        }
        else if (other.tag == "Trap")
        {
            Debug.Log("함정이에유");

        }
    }
    #endregion

    #region 특수 타일 효과 모음
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(0.25f);

        while (true)
        {
            this.isMoveWait = false;
            this.transform.position += new Vector3(0, -0.1f, 0);
            yield return null;
        }
    }
    #endregion
}
