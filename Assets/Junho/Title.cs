using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Title : Singleton<Title>
{
    [SerializeField]
    private GameObject btnObj;
    [SerializeField]
    private GameObject titleWnd;
    public GameObject ingameWnd;
    public GameObject fadeImage;

    public bool isGameStart;

    private GameObject btn;
    public void StartBtn()
    {
        SoundManager.Instance.PlaySound(ESoundSources.UI_BUTTON);
        titleWnd.SetActive(false);
        fadeImage.gameObject.SetActive(true);
        fadeImage.GetComponent<SpriteRenderer>().DOFade(1, 1).OnComplete(() =>
        {
            GameObject go = Instantiate(btnObj);
            go.transform.position = new Vector3(0, 14, 0);
            Camera.main.gameObject.transform.position = new Vector3(0, -6, -10);
            btn = go;
        });
    }
    public void ExitBtn()
    {
        SoundManager.Instance.PlaySound(ESoundSources.UI_BUTTON);

        Application.Quit();
    }
}
