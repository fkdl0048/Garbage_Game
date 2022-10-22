using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BeginningDirecting : MonoBehaviour
{
    [SerializeField] 
    private GameObject StartBtnObj;
    [SerializeField]
    private Image fade;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag=="Player")
        {
            fade.DOFade(0, 1).OnComplete(() => fade.gameObject.SetActive(false));
        }
    }
}
