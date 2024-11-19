using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리펩 보관 변수
    public GameObject[] prefabs;

    // 풀 담당 리스트
    List<GameObject>[] pools;

    private void Awake()
    {
        // 풀 배열 초기화
        pools = new List<GameObject>[prefabs.Length];
        for (int i = 0; i < pools.Length; i++) {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index) // 매개변수: 가져올 인덱스 번호
    {
        GameObject select = null;

        // 선택한 풀의 비활성화 게임 오브젝트 접근
        foreach (GameObject item in pools[index]) {
            if (!item.activeSelf) { // 비활성화 상태
                select = item; // 변수 할당
                select.SetActive(true); // 활성화
                break;
            }
        }
        // 비활성화 게임 오브젝트가 없다면
        if (!select) {
            // 새로 생성 후 할당(transform: hierarchy창에서 부모에 속하도록)
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
