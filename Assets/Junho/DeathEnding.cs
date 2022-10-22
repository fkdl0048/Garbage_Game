using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DeathEnding : MonoBehaviour
{
    [SerializeField] private Image fade;
    void Start()
    {
        StartCoroutine(Fade());
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            StopCoroutine(Fade());
            fade.DOFade(1, 1).OnComplete(() =>
            {
                SceneManager.LoadScene("Test");
            });
        }
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(4);
        fade.DOFade(1, 1).OnComplete(() =>
        {
            SceneManager.LoadScene("Test");
        });
    }
}
