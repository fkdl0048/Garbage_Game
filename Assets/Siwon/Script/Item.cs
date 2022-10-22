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

    public bool isFallen;

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
        if (isFallen == false)
        {
            SoundManager.Instance.PlaySound(ESoundSources.ETC_SLIME);
        }

        isFallen = true;

        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, 2f, 6);

        //Gizmos.DrawWireSphere(transform.position, 2f);

        for (int i = 0; i < colls.Length; i++)
        {
            Debug.Log("!");
            colls[i].gameObject.transform.SetParent(transform);
        }
    }

    private void Boom()
    {
        Vector2 explosionPos = transform.position;
        var collider2D = Physics2D.OverlapCircleAll(explosionPos, 2f);

        foreach (var VARIABLE in collider2D)
        {
            Rigidbody2D rb = VARIABLE.GetComponent<Rigidbody2D>();

            if (rb != null && rb.CompareTag("Garbage"))
            {
                AddExplosionForce2D(rb, transform.position, 900f, 6f);
            }
        }
        EffectManager.Instance.EffectSpawn(EEffectType.Boom, transform, 0.5f);
        Destroy(gameObject);
    }

    private void StaticBlock(GameObject obj)
    {
        if (obj.CompareTag("Ground") == false)
        {
            obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        rb.bodyType = RigidbodyType2D.Static;
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

    //�ð� �߰�
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

    public static void AddExplosionForce2D(Rigidbody2D rb, Vector2 explosionOrigin, float explosionForce, float explosionRadius)
    {
        Vector2 direction = (Vector2)rb.transform.position - explosionOrigin;
        float forceFalloff = 1 - (direction.magnitude / explosionRadius);
        rb.AddForce(direction.normalized * (forceFalloff <= 0 ? 0 : explosionForce) * forceFalloff);
    }
}