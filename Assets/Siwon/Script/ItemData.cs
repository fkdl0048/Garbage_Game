using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemType
{
    None,
    Slime,//µ¢±¼
    Boom,
    StaticBlock,
    Fire,
    Frozen,
    AddTime,
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Datas/ItemData", order = int.MinValue)]
public class ItemData : ScriptableObject
{
    public EItemType itemType;

    public Sprite itemSprite;

}
