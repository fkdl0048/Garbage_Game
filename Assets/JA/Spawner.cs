using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Spawner : Singleton<Spawner>
{
    public bool isSpawn = false;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private float spawnRandominterval = 0.2f;
    
    [Header("Item")]
    [SerializeField] private GameObject[] itemList;
    [SerializeField] private float itemDelay = 10f;
    [SerializeField] private float itemRandominterval = 1f;
    private float curTime;
    private bool isItem = false;

    private Sprite[] sprites;
    private void Awake()
    {
        isSpawn = false;
        sprites = Resources.LoadAll<Sprite>("Sprites");
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        curTime += Time.deltaTime;

        if (curTime >= itemDelay + (Random.Range(-itemRandominterval, itemRandominterval)) && !isItem)
        {
            curTime = 0f;
            isItem = true;
        }
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            if (isSpawn)
            {
                Vector3 pos = new Vector3(transform.position.x - (transform.localScale.x / 2) + Random.Range(0, transform.localScale.x), transform.position.y, 0);
                if (isItem)
                {
                    isItem = false;
                    Instantiate(itemList[Random.Range(0, itemList.Length)], pos,
                        Quaternion.Euler(1, 1, Random.Range(0, 360)));
                }
                else
                {
                    GameObject go = new GameObject("Garbage");
                    go.layer = 6;
                    go.transform.position = pos;
                    go.transform.rotation = Quaternion.Euler(1, 1, Random.Range(0, 360));
                    go.AddComponent<Rigidbody2D>();
                    go.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
                    go.AddComponent<SpriteRenderer>();
                    go.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, (sprites.Length))];
                    go.AddComponent<PolygonCollider2D>();
                    go.AddComponent<Dust>();
                }
            }
            yield return new WaitForSeconds(spawnDelay + (Random.Range(-spawnRandominterval, spawnRandominterval)));
        }
    }
}
