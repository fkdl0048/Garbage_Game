using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cradit : MonoBehaviour
{
    [SerializeField] GameObject Text;
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject Button;
    [SerializeField] float UpSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text.transform.localPosition += Vector3.up * UpSpeed * Time.deltaTime;
        if (Text.transform.localPosition.y > 2100)
        {
            Panel.SetActive(false);
            Button.SetActive(true);
        }
        
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
