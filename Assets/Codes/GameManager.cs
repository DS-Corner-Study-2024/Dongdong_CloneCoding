using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public bool isLive; // 레벨업 시에 게임 정지
    public float gameTime; // 실제로 흐르는 게임 시간
    public float maxGameTime = 2 * 10f; // 최대 플레이 시간

    [Header("# Player Info")]
    public int health;
    public int maxhealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;

    public void Awake()
    {
        instance = this;
        // 아래 처럼 정적 클래스 변수로 접근 가능
        //GameManager.instance.player
    }

    private void Start()
    {
        health = maxhealth;

        //test
        uiLevelUp.Select(0);
    }

    void Update()
    {
        if (!isLive) return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;
        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)]) // 지정 레벨보다 높아질 경우 처리
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0; // 유니티 시간 속도(배율)
    }
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
