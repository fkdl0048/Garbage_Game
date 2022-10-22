using System;
using UnityEngine;

public class Dust : MonoBehaviour
{
    private bool isGround = false;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Garbage")) && !isGround)
        {
            SoundManager.Instance.PlayG_Falls();
            //temp
            this.gameObject.tag = "Garbage";
            
            isGround = true;
            GameObject go = DustEffect.Instance.SetDustEffect();
            if (go == null)
                return;
            go.transform.position = col.contacts[0].point;
            //go.transform.rotation = transform.rotation;
            go.GetComponent<Animator>().Play(0);
        }
    }
}
