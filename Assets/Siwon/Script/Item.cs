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

    private Color fadeColor = new Color(255, 255, 255, 0);

    private Rigidbody2D rb;


    private void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Garbage"))
        {
            switch (data.itemType)
            {
                case EItemType.Vine:
                    TieGarBage();
                    break;
                case EItemType.Boom:
                    Boom();
                    break;
                case EItemType.StaticBlock:
                    StaticBlock();
                    break;
            }
        }
    }

    private void TieGarBage()
    {
        spriterenderer.color = fadeColor;
        Collider[] colls = Physics.OverlapSphere(transform.position, 10f);
        gameObject.SetActive(false);
        for (int i = 0; i < colls.Length; i++)
        {
            colls[i].gameObject.transform.SetParent(transform);
        }
    }

    private void Boom()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, 10f);
        //Rigidbody2D[] rb = null;

        //for (int i = 0; i < colls.Length; i++)
        //{
        //    rb[i] = colls[i].GetComponent<Rigidbody2D>();
        //}

        spriterenderer.color = fadeColor;

        //for (int i = 0; i < colls.Length; i++)
        //{
        //    colls[i].GetComponent<Transform>().localScale
        //        = Vector2.Lerp(colls[i].transform.localScale, new Vector2(1.5f, 1.5f),0.1f);
        //}

        transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(1.5f, 1.5f), 0.1f);
        //¿Ã∆Â∆Æº“»Ø
    }

    private void StaticBlock()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }


    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2f);
        spriterenderer.color = Color.Lerp(spriterenderer.color, fadeColor, 2f);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}