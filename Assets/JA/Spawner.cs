using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private bool isSpawn = true;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private float spawnRandominterval = 0.2f;

    private Sprite[] sprites;
    private void Awake()
    { 
        sprites = Resources.LoadAll<Sprite>("Sprites");
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (isSpawn)
        {
            Vector3 pos = new Vector3(transform.position.x - (transform.localScale.x/2) + Random.Range(0, transform.localScale.x), transform.position.y, 0);
            GameObject go = new GameObject("Garbage");
            go.transform.position = pos;
            go.transform.rotation = Quaternion.Euler(1,1,Random.Range(0,360));
            go.AddComponent<Rigidbody2D>();
            go.AddComponent<SpriteRenderer>();
            go.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0,(sprites.Length))];
            go.AddComponent<PolygonCollider2D>();
            yield return new WaitForSeconds(spawnDelay + (Random.Range(-spawnRandominterval, spawnRandominterval)));
        }
    }
}
