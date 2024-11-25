using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    //원거리 무기
    float timer;
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>(); //부모 컴포넌트 가져오기
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        //test
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if(id == 0) Batch();
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            default: 
                speed = 0.3f; //연산속도, 적을수록 많이 발사
                break;
        }
    }

    void Batch()
    {
        for (int i = 0; i < count; i++) {
            Transform bullet;
            //= GameManager.instance.pool.Get(prefabId).transform; // 총알 생성 및 위치 변수 저장

            // 개수에 따른 배치 방법
            if (i < transform.childCount) bullet = transform.GetChild(i); // 주어진 개수 이하면 재활용
            else {
                bullet = GameManager.instance.pool.Get(prefabId).transform; // 넘으면 새로 생성
                bullet.parent = transform; // 생성된 총알이 weapon 아래에 속하도록
            }


            // 위치 초기화
            bullet.localPosition = Vector3.zero; // 플레이어 위치
            bullet.localRotation = Quaternion.identity;

            // 무기 위치
            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // 근접 무기(무한 관통)
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget) return; //가장 가까운 적 없다면 반환

        // 방향
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position; // 크기 포함 방향
        dir = dir.normalized; // 벡터 방향 유지하며 크기 1로 변환
        
        //총알 생성
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position; //위치
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); // 지정 축 중심으로 목표 향해 회전
        bullet.GetComponent<Bullet>().Init(damage, count, dir); //전달
    }
}
