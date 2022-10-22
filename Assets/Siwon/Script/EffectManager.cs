using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EEffectType
{
    Fire,
    Landing,
    Boom,
    AddTime,
}
public class EffectManager : Singleton<EffectManager>
{
    public List<GameObject> effects = new List<GameObject>();

    private Canvas canvas;
    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    /// <summary>
    /// Effect
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    /// <param name="duration"></param>
    public void EffectSpawn(EEffectType type, Transform pos, float duration)
    {
        GameObject effect = null;

        if (effect == null)
            return;
        if (type == EEffectType.AddTime)
        {
            effect = Instantiate(effects[(int)type], canvas.transform);
        }
        else
        {
            effect = Instantiate(effects[(int)type]);
        }

        if (type == EEffectType.AddTime)
        {
            effect.transform.position = new Vector2(850, -200);
        }
        else
        {
            effect.transform.position = pos.position;
        }

        if (duration != 0)
        {
            Destroy(effect, duration);
        }
    }
}



