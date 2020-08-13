using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMove : MonoBehaviour
{
    public GameObject body;
    public Camera mainCamera;
    private Cube cube;

    public float cubeSpeed;
    public Coroutine moveRoutine;

    public Button btnUp;
    public Button btnDown;
    public Button btnLeft;
    public Button btnRight;

    void Awake()
    {
        this.cube = GetComponent<Cube>();
        this.cubeSpeed = this.cube.cubeSpeed;
        this.mainCamera = FindObjectOfType<Camera>();
    }


    void Start()
    {
        this.moveRoutine = StartCoroutine(this.KeyInput());

        #region 모바일 조작 Button(Main)
        this.btnUp.onClick.AddListener(() =>
        {
            if (this.cube.isMoveWait)
            {
                if (this.cube.sensors[0].isMove)
                {
                    this.cube.isMoveWait = false;
                    StartCoroutine(this.Move("z", 1));
                    this.MoveReset();
                }
            }
        });

        this.btnDown.onClick.AddListener(() =>
        {
            if (this.cube.isMoveWait)
            {
                if (this.cube.sensors[1].isMove)
                {
                    this.cube.isMoveWait = false;
                    StartCoroutine(this.Move("z", -1));
                    this.MoveReset();
                }
            }
        });

        this.btnLeft.onClick.AddListener(() =>
        {
            if (this.cube.isMoveWait)
            {
                if (this.cube.sensors[2].isMove)
                {
                    this.cube.isMoveWait = false;
                    StartCoroutine(this.Move("x", -1));
                    this.MoveReset();
                }
            }
        });

        this.btnRight.onClick.AddListener(() =>
        {
            if (this.cube.isMoveWait)
            {
                if (this.cube.sensors[3].isMove)
                {
                    this.cube.isMoveWait = false;
                    StartCoroutine(this.Move("x", 1));
                    this.MoveReset();
                }
            }
        });
        #endregion
    }

    #region 키보드 조작 ArrowKey && WASD
    private IEnumerator KeyInput()
    {
        while (true)
        {
            if (this.cube.isMoveWait)
            {
                #region 키보드 조작
                if (Input.GetKey("up") || Input.GetKey("w"))
                {
                    if (this.cube.sensors[0].isMove)
                    {
                        this.cube.isMoveWait = false;
                        StartCoroutine(this.Move("z", 1));
                        this.MoveReset();
                    }
                }
                else if (Input.GetKey("down") || Input.GetKey("s"))
                {
                    if (this.cube.sensors[1].isMove)
                    {
                        this.cube.isMoveWait = false;
                        StartCoroutine(this.Move("z", -1));
                        this.MoveReset();
                    }
                }
                else if (Input.GetKey("left") || Input.GetKey("a"))
                {
                    if (this.cube.sensors[2].isMove)
                    {
                        this.cube.isMoveWait = false;
                        StartCoroutine(this.Move("x", -1));
                        this.MoveReset();
                    }
                }
                else if (Input.GetKey("right") || Input.GetKey("d"))
                {
                    if (this.cube.sensors[3].isMove)
                    {
                        this.cube.isMoveWait = false;
                        StartCoroutine(this.Move("x", 1));
                        this.MoveReset();
                    }
                }
                #endregion
            }
            yield return null;
        }
    }
    #endregion

    private void MoveReset()//연달은 벽 통과할때 생기는 OnTriggerExit오류 방지 메서드
    {
        for (int i = 0; i < 4; i++)
        {
            this.cube.sensors[i].isMove = true;
        }
    }

    #region 큐브 이동 자연스럽게
    private IEnumerator Move(string shaft, int dir)//shaft = 축(x, y, z), dir = 방향 (-1, 1)
    {
        for (int i = 0; i < 30 / this.cubeSpeed; i++)//돌면서 y값 위로
        {
            this.MoveCube(shaft, dir);
            this.RotateCube(shaft, dir);
            this.NaturalRotate(1);
            yield return null;
        }

        for (int i = 0; i < 30 / this.cubeSpeed; i++)//돌면서 y값 아래로
        {
            this.MoveCube(shaft, dir);
            this.RotateCube(shaft, dir);
            this.NaturalRotate(-1);
            yield return null;
        }
        //위의 for문으로 이동시 0.9999.. 로 이동함, 그래서 정수단위 값으로 정렬해서 위치 재설정 코드
        this.cube.transform.position = new Vector3((float)Math.Round(this.cube.transform.position.x), (float)Math.Round(this.cube.transform.position.y), (float)Math.Round(this.cube.transform.position.z));
        this.cube.onMoveCount();//테스트(Test)시 주석처리
    }

    private void MoveCube(string shaft, int dir)
    {
        float x = 0, y = 0, z = 0;
        if (shaft == "x")
        {
            z = 1 / 60f * dir * this.cubeSpeed;
        }
        else if (shaft == "z")
        {
            x = 1 / 60f * -dir * this.cubeSpeed;
        }
        this.transform.position += (new Vector3(x, y, z));
        this.mainCamera.transform.position += (new Vector3(x, y, z));
    }

    private void RotateCube(string shaft, int dir)
    {
        float x = 0, y = 0, z = 0;
        if (shaft == "x")
        {
            x = 1.5f * dir * this.cubeSpeed;
        }
        else if (shaft == "z")
        {
            z = 1.5f * dir * this.cubeSpeed;
        }
        this.body.transform.Rotate(x, y, z, Space.World);
    }

    private void NaturalRotate(int dir)//자연스러운 회전을 위한 회전각 계수
    {
        this.body.transform.position += new Vector3(0, 0.007f * dir * this.cubeSpeed, 0);
    }
    #endregion
}
