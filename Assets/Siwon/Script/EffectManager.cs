using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EEffectType
{
    Fire,
    Landing,
    Boom,
    Slime,
    AddTime,
}
public class EffectManager : Singleton<EffectManager>
{
    public List<GameObject> effects = new List<GameObject>();


    /// <summary>
    /// ����Ʈ ��ȯ Effect
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    /// <param name="duration"></param>
    public void EffectSpawn(EEffectType type, Transform pos, float duration)
    {
        GameObject effect = null;

        if (effect == null)
            return;
        effect = Instantiate(effects[(int)type]);

        if (type == EEffectType.Slime)
        {
            effect.transform.SetParent(pos);
        }
        else if(type == EEffectType.AddTime)
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



