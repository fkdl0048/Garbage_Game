using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayBgm(EBGMSources.END);
    }

}
