using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BeginningDirecting : MonoBehaviour
{
    private GameObject fade=> Title.Instance.fadeImage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag=="Player")
        {
            fade.GetComponent<SpriteRenderer>().DOFade(0, 1).OnComplete(() => {
                fade.gameObject.SetActive(false);
                Title.Instance.ingameWnd.SetActive(true);
                Spawner.Instance.isSpawn = true;
            });
        }
    }
}
