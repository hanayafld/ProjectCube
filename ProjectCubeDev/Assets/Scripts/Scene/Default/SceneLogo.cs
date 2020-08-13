using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLogo : MonoBehaviour
{
    public Image imgLogo;

    public void Init(string name = "", int prefabId = 0)
    {
        StartCoroutine(this.FadeInOut());
    }

    public IEnumerator FadeInOut()
    {
        var color = this.imgLogo.color;
        float alpha = color.a;
        while (true)
        {
            alpha += 0.016f;
            color.a = alpha;
            this.imgLogo.color = color;

            if (alpha >= 1)
            {
                alpha = 1;
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        while (true)
        {
            alpha -= 0.016f;
            color.a = alpha;
            this.imgLogo.color = color;

            if (alpha <= 0)
            {
                alpha = 0;
                break;
            }
            yield return null;
        }

        GameSceneManager.GetInstance().LoadScene(3);
    }
}
