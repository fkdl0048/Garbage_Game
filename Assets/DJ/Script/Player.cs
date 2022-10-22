using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private LineRenderer dragLine;

    private Vector3 mousePos;
    private Vector3 clickPos;

    private Rigidbody2D rigid;
    private Collider2D trashColider;
    private Rigidbody2D trashrigid;
    private LayerMask layerMaskGarbage;

    private bool isJump = false;
    private bool isC_Jump = false;
    private bool ismove = true;
    private GameObject getTrash;
    [SerializeField]private Slider pressBar;
    [Header("Animation")]
    [SerializeField] AnimationClip[] blink;
    private Animator anim;
    [Header("Stats")]
    [SerializeField] float Hp;
    [SerializeField] float MaxHp;
    [SerializeField] float throwPower;
    [SerializeField] float maxthrowPower;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float jumpdelay;
    [SerializeField] Vector2 pressRange;
    [SerializeField] Vector3 RangePos;
    [Header("Mark")]
    [SerializeField] GameObject markObject;
    [SerializeField] GameObject groupObject;
    [SerializeField] float markCount;
    [SerializeField] float markDis;
    private List<GameObject> markGroup = new List<GameObject>();
    private void Start()
    {
        layerMaskGarbage = LayerMask.GetMask("Garbage");
        dragLine = GetComponent<LineRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        for (int i = 0; i < markCount; i++)
        {
            markGroup.Add(Instantiate(markObject, transform.position, transform.rotation, groupObject.transform));
            markGroup[i].GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, i / markCount);
        }
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
        //                      �̼� ������
        if (isC_Jump != true && rigid.velocity.y < 0.1f && rigid.velocity.y > -0.1f)//�̼� ������
        {
            isJump = false;
            anim.SetBool("Jump", false);
        }
        else isJump = true;
        PressCheck();
        pressBar.value = Hp / MaxHp;
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
        }//����
    }
    private void PressCheck()
    {
        Collider2D[] blocks = Physics2D.OverlapBoxAll(transform.position + RangePos, pressRange, 0, layerMaskGarbage);
        if(blocks.Length >= 4) Hp -= Time.deltaTime;
        else Hp += Time.deltaTime;
        Hp = Mathf.Clamp(Hp, 0, MaxHp);
        if (Hp <= 0) { Debug.Log("a"); }
    }
    IEnumerator C_Jump()
    {
        anim.SetBool("Jump", true);
        ismove = false;
        yield return new WaitForSeconds(jumpdelay);
        rigid.velocity += Vector2.up * jumpPower;
        isC_Jump = false;
        isJump = true;
        ismove = true;
    }
    private void Inputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            groupObject.SetActive(true);
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
            for (int i = 0; i < markCount; i++)
                markGroup[i].transform.position = MarkRoute((float)i/ markDis);
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
        groupObject.SetActive(false);
        Vector2 movepower = Vector2.ClampMagnitude(clickPos - mousePos, maxthrowPower);
        transform.rotation = Quaternion.Euler(0, 180 * (movepower.x > 0 ? 0 : 1), 0);
        ismove = false;
        getTrash.tag = "Garbage";
        yield return new WaitForSeconds(0.3f);
        getTrash = null;
        ismove = true;
        trashColider.isTrigger = false;
        trashrigid.gravityScale = 1;
        trashrigid.AddForce(movepower * throwPower);
        anim.SetBool("Throw", false);
    }
    private Vector2 MarkRoute(float count)
    {
        return new Vector2(getTrash.transform.position.x, getTrash.transform.position.y) //�߻� ��ġ
            + (Vector2.ClampMagnitude(clickPos - mousePos, maxthrowPower)) * count//�߻� ���� / for�� idx
            + 0.5f * Physics2D.gravity * count * count;;//����
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag;
        if (collision.gameObject.tag != "Garbage" && collision.gameObject.tag != "Ground" && getTrash == null)
        {
            getTrash = collision.gameObject;
            trashColider = getTrash.GetComponent<Collider2D>();
            trashColider.isTrigger = true;
            trashrigid = trashrigid = getTrash.GetComponent<Rigidbody2D>();
            trashrigid.gravityScale = 0;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position + RangePos, pressRange);
    }
}
