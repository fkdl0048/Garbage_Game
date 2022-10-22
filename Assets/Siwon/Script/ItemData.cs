using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemType
{
    None,
    Vine,//µ¢±¼
    Boom,
    StaticBlock,
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Datas/ItemData", order = int.MinValue)]
public class ItemData : ScriptableObject
{
    public EItemType itemType;

    public Sprite itemSprite;

    public string name;

    public float force;

    
}
