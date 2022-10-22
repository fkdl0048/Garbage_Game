using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class GameOver : Singleton<GameOver>
{
    [SerializeField] private Image fade;

    private void Start()
    {
        SoundManager.Instance.PlayBgm(EBGMSources.ENDING_BGM);
    }
    public void gameOver()
    {
        fade.gameObject.SetActive(true);
        fade.DOFade(1, 1);

        StartCoroutine(SceneChange());
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            StopCoroutine(SceneChange());
            Scene();
        }
    }

    private IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(4);
        Scene();
    }

    private void Scene()
    {
        Camera.main.transform.position = new Vector3(0,14,-10);
        Title.Instance.StartSet();
    }
}
