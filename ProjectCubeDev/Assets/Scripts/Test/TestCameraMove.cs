using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraMove : MonoBehaviour
{
    public Camera mainCamera;
    void Start()
    {
        StartCoroutine(this.MoveCamera());
    }

    private IEnumerator MoveCamera()
    {
        Vector3 cameraPos = this.mainCamera.transform.position;
        Vector3 cameraPoint = new Vector3(4, 8, 0);

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
}
