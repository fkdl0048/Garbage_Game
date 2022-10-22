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
    P_AIMING,
    P_THROWING,
    P_DEATH,
    G_FLIES,
    G_FALLS,
    G_COLLAPSES,
    ETC_EARTHQUAKE,
    UI_BUTTON,
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
