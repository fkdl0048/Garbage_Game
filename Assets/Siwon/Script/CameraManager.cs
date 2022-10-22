using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public Camera _camera;

    private Transform cameraTransform;

    //ÀÌ°ä³Ä?
    public bool isWin;

    public bool isLose;


    private void Start()
    {
        cameraTransform = _camera.transform;
    }


    private void Update()
    {
        if(isWin == true)
        {

        }
    }


    private IEnumerator CaremaShake()
    {
        yield return null;
    }


}
