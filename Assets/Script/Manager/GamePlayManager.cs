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
    //�׽�Ʈ�� �ð��ӵ� ����

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

    //���Ӹ�� ������ ���� ���� (���� enum���� ���� ����)

    [SerializeField]
    private List<CharacterSOMaker> so;
    //ĳ���� SO ������ ��� ����Ʈ
    public List<CharacterSOMaker> SO => so;

    public int map = 0;
    // ���� ���������� ��Ÿ���� ���� (���� enum���� ������ ����)

    public int Race = 0;
    //���� ���̽� ���� �� ���̽��� 3���� ���尡 �����

    [SerializeField]
    public int FinalRace = 0;
    //�� ���Ӵ� ����� ���̽��� �������� �����Ҷ� ���̴� ����

    public int Round = 0;
    //���� ���尡 ������ ����ϴ� ����

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
