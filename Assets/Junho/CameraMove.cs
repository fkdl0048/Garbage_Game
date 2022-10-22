using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float maxY;
    [SerializeField]
    private float minY;

    public bool isMove = true;
    [SerializeField]
    private bool isTopPanel;
    private bool isEnter;

    Vector3 dir;

    private new GameObject camera => Camera.main.gameObject;
    public void OnPointerEnter(PointerEventData eventData)
    {
        isEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isEnter = false;
    }

    private void Start()
    {
        if (isTopPanel)
        {
            dir = Vector3.up;
        }
        else dir = Vector3.down;
    }
    private void FixedUpdate()
    {
        Check();
        if (isEnter && isMove)
        {
            camera.transform.position = Vector3.Lerp(
            camera.transform.position,
            camera.transform.position + dir,
            Time.deltaTime * 3);
        }
    }

    private void Check()
    {
        if (camera.transform.position.y > maxY)
        {
            isEnter = false;

            camera.transform.position = new Vector3(camera.transform.position.x, maxY, -10);

        }
        else if (camera.transform.position.y < minY)
        {
            isEnter = false;

            camera.transform.position = new Vector3(camera.transform.position.x, minY, -10);
        }
    }
}
