using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 근접, 아이템, 힐 등의 모든 아이템 관리
[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/ItemData")] // 스크립트블 오브젝트 생성 가능
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoe, Heal}

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile;
    public Sprite hand;
}
