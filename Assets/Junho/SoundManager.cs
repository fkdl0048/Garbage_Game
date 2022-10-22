using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ESoundType
{
    BGM,
    SFX
}
public enum ESoundSources
{
    P_WALK,
    P_JUMP,
    P_ACQUISITION,
    P_DEATH,
    G_FLIES,
    ETC_SLIME,
    ETC_TIME,
    UI_BUTTON,
    END
}
public enum EG_FALLS
{
    FALLS_1,
    FALLS_2,
    FALLS_3,
    FALLS_4,
    FALLS_5,
    FALLS_6,
    FALLS_7,
    FALLS_8,
    FALLS_9,
    FALLS_10,
    END
}
public enum EBGMSources
{
    MAIN_BGM,
    ENDING_BGM,
    END
}

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private List<AudioClip> audioSources = new List<AudioClip>();
    [SerializeField]
    private List<AudioClip> bgmSources = new List<AudioClip>();
    [SerializeField]
    private List<AudioClip> g_Falls = new List<AudioClip>();


    public float BGMVolum;
    public float SFXVolum;

    public AudioSource bgm;
    private void Awake()
    {
        for (int i = 0; i < ((int)ESoundSources.END); i++)
        {
            audioSources.Add(Resources.Load<AudioClip>("Audio/" + ((ESoundSources)i).ToString()));
        }
        for (int i = 0; i < ((int)EBGMSources.END); i++)
        {
            bgmSources.Add(Resources.Load<AudioClip>("Audio/" + ((EBGMSources)i).ToString()));
        }
        for (int i = 0; i < ((int)EG_FALLS.END); i++)
        {
            g_Falls.Add(Resources.Load<AudioClip>("Audio/" + ((EG_FALLS)i).ToString()));
        }

        PlayBgm(EBGMSources.MAIN_BGM);
    }

    public void PlayBgm(EBGMSources source)
    {

        GameObject go = new GameObject("bgm");

        AudioSource audio = go.AddComponent<AudioSource>();
        audio.clip = bgmSources[((int)source)];

        bgm = audio;
        audio.volume = BGMVolum;
        audio.loop = true;
        audio.Play();
    }
    public void PlayG_Falls()
    {
        GameObject go = new GameObject("sound");

        AudioSource audio = go.AddComponent<AudioSource>();
        audio.clip = g_Falls[Random.Range(0,((int)EG_FALLS.END))];

        audio.volume = SFXVolum;
        audio.Play();

        Destroy(go, audio.clip.length);
    }
    public void PlaySound(ESoundSources source)
    {

        GameObject go = new GameObject("sound");

        AudioSource audio = go.AddComponent<AudioSource>();
        audio.clip = audioSources[((int)source)];

        
        audio.volume = SFXVolum;
        audio.Play();

        Destroy(go, audio.clip.length);
    }

}
