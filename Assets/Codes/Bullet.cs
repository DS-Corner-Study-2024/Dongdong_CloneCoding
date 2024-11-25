using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigid; //원거리 공격

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir) // dir: 방향
    {
        this.damage = damage;
        this.per = per;

        if (per > -1) { // 근접 무기가 아니면 속도 적용
            rigid.velocity = dir * 15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적과 충돌한 것이 아니라면, 근접무기라면 반환
        if (!collision.CompareTag("Enemy") || per == -1 ) return;

        per--; // 충돌했기에 관통력 감소
        if (per == -1) { // 원거리 공격 역할 종료
            rigid.velocity = Vector2.zero; // 재사용을 위한 속도 초기화
            gameObject.SetActive(false); // 오브젝트 관리 중이므로 Destroy 사용 X
        }
    }
}
