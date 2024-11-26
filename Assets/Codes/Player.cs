using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed; // 이동 속도
    public Scanner scanner; //원거리 공격 구현
    public Hand[] hands;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>(); //스크립트도 컴포넌트와 동일하게 취급
        hands = GetComponentsInChildren<Hand>(true); // 비활성화 오브젝트 포함
    }

    void Update()
    {
        if (!GameManager.instance.isLive) return;
        //inputVec.x = Input.GetAxisRaw("Horizontal");
        //inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive) return;
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime; //나아갈 방향 크기
        //rigid.AddForce(inputVec); //힘
        //rigid.velocity = inputVec; //속도
        rigid.MovePosition(rigid.position + nextVec); //위치
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive) return;
        animator.SetFloat("Speed", inputVec.magnitude); //벡터의 순수한 크기 float 값

        if (inputVec.x != 0) spriteRenderer.flipX = inputVec.x < 0;
    }
}
