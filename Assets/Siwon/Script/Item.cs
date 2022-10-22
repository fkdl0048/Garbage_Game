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

    public bool isThrowing;

    private void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        spriterenderer.sprite = data.itemSprite;
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Garbage") && isThrowing == true)
        {
            switch (data.itemType)
            {
                case EItemType.Slime:
                    TieGarBage();
                    break;
                case EItemType.Boom://Boom!!!!
                    Boom();
                    break;
                case EItemType.StaticBlock://못
                    StaticBlock();
                    break;
                case EItemType.Fire:
                    Fire();
                    break;
                case EItemType.Frozen:
                    Frozen();
                    break;
                case EItemType.AddTime:
                    AddTime();
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
            EffectManager.Instance.EffectSpawn(EEffectType.Slime, colls[i].transform.position, 0);
        }
    }

    private void Boom()
    {
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
        EffectManager.Instance.EffectSpawn(EEffectType.Boom, transform.position, 0.5f);
        gameObject.SetActive(false);
    }

    private void StaticBlock()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void Fire()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, 10f);

        for (int i = 0; i < colls.Length; i++)
        {
            EffectManager.Instance.EffectSpawn(EEffectType.Fire, colls[i].transform.position, 0.5f);
            Destroy(colls[i].gameObject, 0.5f);
        }
        Destroy(gameObject);
    }

    private void Frozen()
    {
        spriterenderer.color = fadeColor;
        Collider[] colls = Physics.OverlapSphere(transform.position, 10f);
        gameObject.SetActive(false);
        for (int i = 0; i < colls.Length; i++)
        {
            colls[i].gameObject.transform.SetParent(transform);
            EffectManager.Instance.EffectSpawn(EEffectType.Frozen, colls[i].transform.position, 0);
        }
    }
    
    //시간 추가
    private void AddTime()
    {
        TimeAttack.Instance.TimeValue -= 10f;
        Destroy(gameObject);
    }

    //private IEnumerator FadeOut()
    //{
    //    yield return new WaitForSeconds(2f);
    //    spriterenderer.color = Color.Lerp(spriterenderer.color, fadeColor, 2f);
    //    yield return new WaitForSeconds(2f);
    //    Destroy(gameObject);
    //}
}