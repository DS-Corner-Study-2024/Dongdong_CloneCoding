using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed; // 이동 속도
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target; // 추격 물리 대상
    bool isLive; // 해당 몬스터의 생존 여부

    // 제어 변수 선언
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive) return;

        Vector2 dirVec = target.position - rigid.position; // 타겟 위치 - 몬스터 위치
        // 나아갈 다음 위치
        Vector2 nextVec = dirVec.normalized * speed * Time.deltaTime; // 방향(위치 차) * 속도 * 1/프레임
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero; // 속도 제거(플레이어와 몬스터가 부딪혔을 때 물리 충격 발생)
    }

    void LateUpdate()
    {
        if (!isLive) return;
        // 몬스터 바라보는 방향 반전
        spriter.flipX = target.position.x < rigid.position.x; // 목표 x < 몬스터 x
    }

    // 레벨에 따른 선언 및 초기화
    void OnEnable() // 스크립트 활성화 시 호출
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    // 레벨에 따른 선언 및 초기화
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) return;
        //health -= collision.GetComponent<Bullet>().damage;
    }
}
