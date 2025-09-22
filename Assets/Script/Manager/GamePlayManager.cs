using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance = null;

    [SerializeField]
    private float timeScale = 1.0f;
    //�׽�Ʈ�� �ð��ӵ� ����

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
    //���Ӹ�� ������ ���� ����

    public int mapIndex = 0;
    // ���� ���������� ��Ÿ���� ����

    public int currentRace = 0;
    //���� ���̽� ���� �� ���̽��� 3���� ���尡 �����

    [SerializeField]
    public int totalRaceCount = 0;
    //�� ���Ӵ� ����� ���̽��� �������� �����Ҷ� ���̴� ����

    public int currentRound = 0;
    //���� ���尡 ������ ����ϴ� ����

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
