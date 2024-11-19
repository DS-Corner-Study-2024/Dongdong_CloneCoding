using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    int level;
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length-1);

        if (timer > spawnData[level].spawnTime) {
            Spawn();
            timer = 0;
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; // 자기자신(Spawner 위치(인덱스 0)) 제외
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable] // 직렬화(inspector에서 수정하기 위함)
public class SpawnData
{
    public float spawnTime; //소환시간
    public int spriteType; //몬스터 종류
    public int health; // 체력
    public float speed; // 속도
}