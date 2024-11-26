using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 장비 설정 클래스
public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;
    
    public void Init(ItemData data)
    {
        // basic set
        name = "Gear" + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        // property set
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear(); // 생성되면 이 기어 기능 적용
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear(); // 레벨 업 시에 능력 향상
    }

    // 함수 호출 함수
    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    // 공격 속도(장갑)
    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();
        foreach (Weapon weapon in weapons)
        {
            switch (weapon.id)
            {
                case 0:
                    weapon.speed = 150 + (150 * rate);
                    break;
                default:
                    weapon.speed = 0.5f * (1f- rate);
                    break;
            }
        }
    }

    // 이동 속도(신발)
    void SpeedUp()
    {
        float speed = 3;
        GameManager.instance.player.speed = speed + speed * rate;
    }
}
