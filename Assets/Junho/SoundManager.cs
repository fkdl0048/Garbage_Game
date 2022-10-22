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
    ETC_EARTHQUAKE,
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
    GAME_OVER_BGM,
    ENDING_BGM,
    END
}

public class SoundManager : Singleton<SoundManager>
{

    private List<AudioClip> audioSources = new List<AudioClip>();
    private List<AudioClip> bgmSources = new List<AudioClip>();
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
            bgmSources.Add(Resources.Load<AudioClip>("Audio/" + ((EG_FALLS)i).ToString()));
        }

        BGMVolum = PlayerPrefs.GetFloat("BGMVolum");
        SFXVolum = PlayerPrefs.GetFloat("SFXVolum");
        PlayBgm(EBGMSources.MAIN_BGM);
    }

    public void PlayBgm(EBGMSources source)
    {
        if (bgm != null)
        {
            Destroy(bgm);
        }

        GameObject go = new GameObject("bgm");

        AudioSource audio = go.AddComponent<AudioSource>();
        audio.clip = audioSources[((int)source)];

        bgm = audio;
        audio.volume = BGMVolum;
        audio.loop = true;
        audio.Play();
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

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("BGMVolum", BGMVolum);
        PlayerPrefs.SetFloat("SFXVolum", SFXVolum);
    }
}
