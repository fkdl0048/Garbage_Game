using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EEffectType
{
    Fire,
    Landing,//����
    Boom,
    Slime,//������
    Frozen,//����ִ�
}
public class EffectManager : Singleton<EffectManager>
{
    public List<GameObject> effects = new List<GameObject>();


    /// <summary>
    /// ����Ʈ ��ȯ Effect
    /// </summary>
    /// <param name="type">��ȯ�� ����Ʈ�� ����</param>
    /// <param name="pos">��ȯ�� ��ġ</param>
    /// <param name="duration">����Ʈ�� ���ӽð� 0 == ����</param>
    public void EffectSpawn(EEffectType type, Vector3 pos, float duration)
    {
        GameObject effect = null;
        
        if (effect == null)
            return;
        effect = Instantiate(effects[(int)type]);
        
        effect.transform.position = pos;

        if (duration != 0)
        {
            Destroy(effect, duration);
        }
    }
}



