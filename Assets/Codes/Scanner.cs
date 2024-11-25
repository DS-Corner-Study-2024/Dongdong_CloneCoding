using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange; //스캔 범위
    public LayerMask targetLayer; // 스캔 대상 레이어
    public RaycastHit2D[] targets; // 스캔 결과 배열
    public Transform nearestTarget; // 가장 가까운 목표

    private void FixedUpdate()
    {
        //원형 캐스트(캐스팅 시작 위치, 원 반지름, 캐스팅 방향, 캐스팅 길이, 대상 레이어)
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100; // 거리 초기화

        foreach (RaycastHit2D target in targets) {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos); //해당 오브젝트와 타겟의 거리 차이

            if (curDiff < diff) { // 현재 거리 차보다 타겟 차이가 더 작으면 교체
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
}
