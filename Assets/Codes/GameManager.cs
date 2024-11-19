using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager pool;
    public Player player;

    public void Awake()
    {
        instance = this;
        // 아래 처럼 정적 클래스 변수로 접근 가능
        //GameManager.instance.player
    }
}
