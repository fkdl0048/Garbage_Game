using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private LineRenderer dragLine;

    private Vector3 mousePos;
    private Vector3 clickPos;

    private Rigidbody2D rigid;
    private Collider2D trashColider;
    private Rigidbody2D trashrigid;

    private bool isJump = false;
    private bool isC_Jump = false;
    private bool ismove = true;
    private GameObject getTrash;
    [Header("Animation")]
    [SerializeField] Animation[] blink;
    private Animator anim;
    [Header("Stats")]
    [SerializeField] float throwPower;
    [SerializeField] float maxthrowPower;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;
    [Header("Mark")]
    [SerializeField] GameObject markObject;
    [SerializeField] GameObject groupObject;
    [SerializeField] int markcount;
    private List<GameObject> markGroup = new List<GameObject>();
    private void Start()
    {
        dragLine = GetComponent<LineRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        for (int i = 0; i < markcount; i++)
            markGroup.Add(Instantiate(markObject, transform.position, transform.rotation, groupObject.transform));
    }
    void Update()
    {
        if (getTrash != null)
        {
            getTrash.transform.position = transform.position + Vector3.up;
            Inputs();
        }
        if(ismove) Walk();
        if(ismove) Jump();
        //                      미세 움직임
        if (isC_Jump != true && rigid.velocity.y < 0.001f && rigid.velocity.y > -0.001f)//미세 움직임
        {
            isJump = false;
            anim.SetBool("Jump", false);
        }
        else isJump = true;
      
    }
    private void Walk()
    {
        float X = Input.GetAxisRaw("Horizontal");
        transform.position += Vector3.left * -X * moveSpeed * Time.deltaTime;
        if(X != 0)
        {
            transform.rotation = Quaternion.Euler(0, 180 * (X == 1 ? 0 : 1),0);
            anim.SetBool("Walk", true);
        }
        else anim.SetBool("Walk", false);
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJump == false && isC_Jump == false)
        {
            isC_Jump = true;
            StartCoroutine(C_Jump());
        }//점프
    }

    IEnumerator C_Jump()
    {
        anim.SetBool("Jump", true);
        ismove = false;
        yield return new WaitForSeconds(0.5f);
        rigid.velocity += Vector2.up * jumpPower;
        isC_Jump = false;
        isJump = true;
        ismove = true;
    }
    private void Inputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos.z = 0;
            dragLine.enabled = true;
            dragLine.SetPosition(0, clickPos);
        }
        else if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            dragLine.SetPosition(1, mousePos);
            for (int i = 0; i < markcount; i++)
                markGroup[i].transform.position = MarkRoute(i);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            dragLine.enabled = false;
            StartCoroutine(Shottrash());
        }
    }
    IEnumerator Shottrash()
    {
        anim.SetBool("Throw", true);
        getTrash.tag = "DropTrash";
        Vector2 movepower = Vector2.ClampMagnitude(clickPos - mousePos,maxthrowPower);
        transform.rotation = Quaternion.Euler(0, 180 * (movepower.x > 0 ? 0 : 1), 0);
        ismove = false;
        getTrash = null;
        yield return new WaitForSeconds(0.3f);
        ismove = true;
        trashColider.isTrigger = false;
        trashrigid.AddForce(movepower * throwPower);
        trashrigid.gravityScale = 1;
        anim.SetBool("Throw", false);
    }
    private Vector2 MarkRoute(float time)
    {
        return Vector2.ClampMagnitude(clickPos - mousePos, maxthrowPower) * time + 0.5f * Physics2D.gravity * time * time;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag;
        if (collision.gameObject.tag != "DropTrash" && collision.gameObject.tag != "Floor" && getTrash == null)
        {
            getTrash = collision.gameObject;
            trashColider = getTrash.GetComponent<Collider2D>();
            trashColider.isTrigger = true;
            trashrigid = trashrigid = getTrash.GetComponent<Rigidbody2D>();
            trashrigid.gravityScale = 0;
        }
    }
}
