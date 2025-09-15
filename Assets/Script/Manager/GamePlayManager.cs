using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance = null;

    [SerializeField]
    private float timespeed = 1.0f;
    //테스트용 시간속도 변수

    public enum EGameMode 
    { 
        None,
        TraningMode,
        AiMode,
        OnLineMode
    }

    public enum EMap
    {
        None
    }

    public int GameMode = 0;

    //게임모드 정보가 들어가는 변수 (추후 enum으로 변경 예정)

    [SerializeField]
    private List<CharacterSOMaker> so;
    //캐릭터 SO 정보가 담긴 리스트
    public List<CharacterSOMaker> SO => so;

    public int map = 0;
    // 게임 스테이지를 나타내는 변수 (추후 enum으로 변경할 예정)

    public int Race = 0;
    //현재 레이스 변수 한 레이스당 3번의 라운드가 진행됨

    [SerializeField]
    public int FinalRace = 0;
    //한 게임당 몇번의 레이스를 진행할지 경정할때 쓰이는 변수

    public int Round = 0;
    //현재 라운드가 몇인지 기록하는 변수

    public void settingReset()
    {
        GameMode = 0;
        map = 0;
        Race = 0;
        Round = 0;
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
        if (Round <= 3)
            Time.timeScale = timespeed;
    }
}
