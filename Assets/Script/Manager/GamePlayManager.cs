using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance = null;

    [SerializeField]
    private float timeScale = 1.0f;
    //테스트용 시간속도 변수

    public enum EGameMode 
    { 
        None,
        Training,
        AI,
        Online
    }

    public enum EMap
    {
        None
    }

    public int gameModeIndex = 0;
    //게임모드 정보가 들어가는 변수

    public int mapIndex = 0;
    // 게임 스테이지를 나타내는 변수

    public int currentRace = 0;
    //현재 레이스 변수 한 레이스당 3번의 라운드가 진행됨

    [SerializeField]
    public int totalRaceCount = 0;
    //한 게임당 몇번의 레이스를 진행할지 경정할때 쓰이는 변수

    public int currentRound = 0;
    //현재 라운드가 몇인지 기록하는 변수

    public void ResetGameSettings()
    {
        gameModeIndex = 0;
        mapIndex = 0;
        currentRace = 0;
        currentRound = 0;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentRound <= 3)
            Time.timeScale = timeScale;
    }
}
