using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTime; // 실제로 흐르는 게임 시간
    public float maxGameTime = 2 * 10f; // 최대 플레이 시간

    public PoolManager pool;
    public Player player;

    public void Awake()
    {
        instance = this;
        // 아래 처럼 정적 클래스 변수로 접근 가능
        //GameManager.instance.player
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}
