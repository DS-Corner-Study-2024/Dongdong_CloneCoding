using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어가 Area 벗어날 경우 타일 재사용 로직
public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return; // 매개변수의 태그가 Area 아니면 반환

        Vector3 playerPos = GameManager.instance.player.transform.position; // 플레이어 위치
        Vector3 myPos = transform.position; // 타일 맵 위치

        // x축, y축 거리 차
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.inputVec; // 플레이어 이동 방향
        // 이동 방향 (+ 대각선(1보다 작음(Normalized)) 처리)
        float dirX = playerDir.x < 0 ? -1: 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground": // 필드 재배치(무한 맵)
                if (diffX > diffY) // 거리 차가 X축이 더 크면
                    transform.Translate(Vector3.right * dirX * 40); // 필드 이동(X*방향*크기(필드 3))
                else if (diffX < diffY) // 거리 차가 Y축이 더 크면
                    transform.Translate(Vector3.up * dirY * 40); // 필드 이동(Y*방향*크기(필드 3))
                break;
            case "Enemy": // 몬스터 재배치(난이도 조율)
                if (coll.enabled) // 살아있는 몬스터
                {
                    //플레이어 이동 방향 * 20 + 랜덤 위치 (이동 방향 맞은 편 랜덤)
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
                }
                break;
        }
    }
}
