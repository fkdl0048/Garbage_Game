using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Cradit : MonoBehaviour
{
    [SerializeField] GameObject Text;
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject Button;
    [SerializeField] Image Endding;
    [SerializeField] float UpSpeed;
    void Start()
    {
        StartCoroutine(Fade(5));
        SoundManager.Instance.PlayBgm(EBGMSources.ENDING_BGM);
    }

    // Update is called once per frame
    void Update()
    {
        Text.transform.localPosition += Vector3.up * UpSpeed * Time.deltaTime;
        if (Text.transform.localPosition.y > 2100)
        {
            Panel.SetActive(false);
            Button.SetActive(true);
        }
        
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    private IEnumerator Fade(float time)
    {
        yield return new WaitForSeconds(time);
        Endding.DOFade(0, 1).OnComplete(() =>  Destroy(Endding));
    }
}
