using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EEffectType
{
    Fire,
    Landing,//착지
    Boom,
    Slime,//끈끈이
    Frozen,//얼어있는
}
public class EffectManager : Singleton<EffectManager>
{
    public List<GameObject> effects = new List<GameObject>();


    /// <summary>
    /// 이펙트 소환 Effect
    /// </summary>
    /// <param name="type">소환할 이팩트의 종류</param>
    /// <param name="pos">소환할 위치</param>
    /// <param name="duration">이펙트의 지속시간 0 == 무한</param>
    public void EffectSpawn(EEffectType type, Vector3 pos, float duration)
    {
        GameObject effect = null;

        effect = Instantiate(effects[(int)type]);
        effect.transform.position = pos;

        if (duration != 0)
        {
            Destroy(effect, duration);
        }
    }
}



