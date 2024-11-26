using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public bool isLive; // 레벨업 시에 게임 정지
    public float gameTime; // 실제로 흐르는 게임 시간
    public float maxGameTime = 2 * 10f; // 최대 플레이 시간

    [Header("# Player Info")]
    public float health;
    public float maxhealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult; // 게임 결과 저장
    public GameObject enemyCleaner; // 게임 클리어 시 적 초기화

    public void Awake()
    {
        instance = this;
        // 아래 처럼 정적 클래스 변수로 접근 가능
        //GameManager.instance.player
    }

    public void GameStart()
    {
        health = maxhealth;

        //test
        uiLevelUp.Select(0);
        Resume();
    }

    public void GameOver()
    {
        StartCoroutine("GameOverRoutine");
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f); // 묘 이미지 나올 수 있게끔 딜레이
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
    }

    public void GameVictroy()
    {
        StartCoroutine("GameVictroyRoutine");
    }

    IEnumerator GameVictroyRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true); // 적 초기화
        yield return new WaitForSeconds(0.5f); // 적 초기화할 수 있게끔 딜레이
        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0); //씬 이름 or 인덱스로 씬 불러오기
    }

    void Update()
    {
        if (!isLive) return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictroy();
        }
    }

    public void GetExp()
    {
        if (!isLive) return;
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
