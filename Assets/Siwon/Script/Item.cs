using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private SpriteRenderer spriterenderer;

    public EItemType itemType;

    private Color fadeColor = new Color(255, 255, 255, 0);

    private Rigidbody2D rb;

    public bool isThrowing;

    private void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;

        if (itemType == EItemType.StaticBlock && isThrowing)
        {
            StaticBlock(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Garbage") && isThrowing)
        {
            switch (itemType)
            {
                case EItemType.Slime:
                    TieGarBage();
                    break;
                case EItemType.Boom://Boom!!!!
                    Boom();
                    break;
                case EItemType.Fire:
                    Fire();
                    break;
                case EItemType.AddTime:
                    AddTime();
                    break;
            }
        }
    }

    private void TieGarBage()
    {
        SoundManager.Instance.PlaySound(ESoundSources.ETC_SLIME);

        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, 2f, LayerMask.GetMask("Garbage"));

        //Gizmos.DrawWireSphere(transform.position, 2f);

        print(colls.Length);

        for (int i = 0; i < colls.Length; i++)
        {
            colls[i].gameObject.transform.SetParent(transform);
        }
    }

    private void Boom()
    {
        spriterenderer.color = fadeColor;
        transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(1.5f, 1.5f), 0.1f);

        EffectManager.Instance.EffectSpawn(EEffectType.Boom, transform, 0.5f);
        Destroy(gameObject, 0.4f);
    }

    private void StaticBlock(GameObject obj)
    {
        rb.bodyType = RigidbodyType2D.Static;
        obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    private void Fire()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, 2f, LayerMask.GetMask("Garbage"));

        for (int i = 0; i < colls.Length; i++)
        {
            EffectManager.Instance.EffectSpawn(EEffectType.Fire, colls[i].transform, 0.5f);
            Destroy(colls[i].gameObject, 0.5f);
        }
        Destroy(gameObject);
    }

    private void Frozen()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, 2f, LayerMask.GetMask("Garbage"));
        gameObject.SetActive(false);
        for (int i = 0; i < colls.Length; i++)
        {
            colls[i].gameObject.transform.SetParent(transform);
            //EffectManager.Instance.EffectSpawn(EEffectType.Frozen, colls[i].transform, 0);
        }
    }

    //시간 추가
    private void AddTime()
    {
        TimeAttack.Instance.TimeValue -= 10f;
        EffectManager.Instance.EffectSpawn(EEffectType.AddTime, transform, 0.5f);
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