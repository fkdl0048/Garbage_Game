using System;
using UnityEngine;

public class Dust : MonoBehaviour
{
    private bool isGround = false;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Garbage")) && !isGround)
        {
            isGround = true;
            GameObject go = DustEffect.Instance.SetDustEffect();
            go.transform.position = col.contacts[0].point;
            //go.transform.rotation = transform.rotation;
            go.GetComponent<Animator>().Play(0);
        }
    }
}
