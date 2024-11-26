using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VR;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

    Image icon;
    Text textLevel;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1]; //자식 오브젝트 필요(첫번째: 자기 자신, 두 번째(아이콘))
        icon.sprite = data.itemIcon; // 초기화

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0]; //텍스트 하나 뿐
    }

    private void LateUpdate()
    {
        textLevel.text = "Lv." + (level + 1);
    }

    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if(level == 0) {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>(); //게임 오브젝트에 Weapon 컴포넌트 추가
                    weapon.Init(data);
                } else {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level]; // 추가 데미지(기존 데미지*백분율)
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }
                level++;
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if (level == 0) {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>(); //게임 오브젝트에 Weapon 컴포넌트 추가
                    gear.Init(data);
                }
                else {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++;
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxhealth;
                break;
        }
        if (level == data.damages.Length) GetComponent<Button>().interactable = false;
    }
}
