using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public GameObject body;
    private Cube cube;

    public float gameSpeed = 1;

    public Coroutine moveRoutine;


    void Awake()
    {
        this.cube = GetComponent<Cube>();
    }

    void Start()
    {
        this.moveRoutine = StartCoroutine(this.KeyInput());
    }

    private IEnumerator KeyInput()
    {
        while (true)
        {
            if (this.cube.isMoveWait)
            {
                if (Input.GetKey("up") || Input.GetKey("w"))
                {
                    if (this.cube.sensors[0].isMove)
                    {
                        this.SensorReset();
                        this.cube.isMoveWait = false;
                        StartCoroutine(this.Move("z", 1));
                        this.cube.moveCount++;
                    }
                }
                else if (Input.GetKey("down") || Input.GetKey("s"))
                {
                    if (this.cube.sensors[1].isMove)
                    {
                        this.SensorReset();
                        this.cube.isMoveWait = false;
                        StartCoroutine(this.Move("z", -1));
                        this.cube.moveCount++;
                    }
                }
                else if (Input.GetKey("left") || Input.GetKey("a"))
                {
                    if (this.cube.sensors[2].isMove)
                    {
                        this.SensorReset();
                        this.cube.isMoveWait = false;
                        StartCoroutine(this.Move("x", -1));
                        this.cube.moveCount++;
                    }
                }
                else if (Input.GetKey("right") || Input.GetKey("d"))
                {
                    if (this.cube.sensors[3].isMove)
                    {
                        this.SensorReset();
                        this.cube.isMoveWait = false;
                        StartCoroutine(this.Move("x", 1));
                        this.cube.moveCount++;
                    }
                }
            }
            yield return null;
        }
    }

    //센서가 벽에 만났을때 연달아 다음 벽이있을때를 위한 센서 초기화 메서드
    private void SensorReset()
    {
        for(int i=0;i<4;i++)
        {
            this.cube.sensors[i].isMove = true;
        }
    }

    #region 큐브 이동 자연스럽게
    private IEnumerator Move(string shaft, int dir)//shaft = 축(x, y, z), dir = 방향 (-1, 1)
    {
        for (int i = 0; i < 30 / this.gameSpeed; i++)//돌면서 y값 위로
        {
            this.MoveCube(shaft, dir);
            this.RotateCube(shaft, dir);
            this.NaturalRotate(1);
            yield return null;
        }

        for (int i = 0; i < 30 / this.gameSpeed; i++)//돌면서 y값 아래로
        {
            this.MoveCube(shaft, dir);
            this.RotateCube(shaft, dir);
            this.NaturalRotate(-1);
            yield return null;
        }
        //위의 for문으로 이동시 0.9999.. 로 이동함, 그래서 값 정렬해서 위치 재설정해서 정수단위이동 코드
        this.cube.transform.position = new Vector3((float)Math.Round(this.cube.transform.position.x), (float)Math.Round(this.cube.transform.position.y), (float)Math.Round(this.cube.transform.position.z));
        this.cube.isMoveWait = true;//다시 이동 가능
    }

    private void MoveCube(string shaft, int dir)
    {
        float x = 0, y = 0, z = 0;
        if (shaft == "x")
        {
            z = 1 / 60f * dir * this.gameSpeed;
        }
        else if (shaft == "z")
        {
            x = 1 / 60f * -dir * this.gameSpeed;
        }
        this.transform.position += (new Vector3(x, y, z));
    }

    private void RotateCube(string shaft, int dir)
    {
        float x = 0, y = 0, z = 0;
        if (shaft == "x")
        {
            x = 1.5f * dir * this.gameSpeed;
        }
        else if (shaft == "z")
        {
            z = 1.5f * dir * this.gameSpeed;
        }
        this.body.transform.Rotate(x, y, z, Space.World);
    }

    private void NaturalRotate(int dir)
    {
        this.body.transform.position += new Vector3(0, 0.007f * dir * this.gameSpeed, 0);
    }
    #endregion
}
