using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed; // 이동 속도

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //inputVec.x = Input.GetAxisRaw("Horizontal");
        //inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
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
        animator.SetFloat("Speed", inputVec.magnitude); //벡터의 순수한 크기 float 값

        if (inputVec.x != 0) spriteRenderer.flipX = inputVec.x < 0;
    }
}
