using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private ItemData data;

    public ItemData Data 
    {
        get
        {
            return data;
        }
        set
        {
            data = value;
        }
    }

    private SpriteRenderer spriterenderer;

    private Color fadeColor = new Color(255,255,255,0);

    private void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FadeOut();
        }
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2f);
        spriterenderer.color = Color.Lerp(spriterenderer.color, fadeColor, 2f);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
