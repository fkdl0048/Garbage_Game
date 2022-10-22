using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class Title : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    public void StartBtn()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1, 1);
        //¾À ÀüÈ¯
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
