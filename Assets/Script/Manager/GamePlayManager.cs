using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{

    [SerializeField]
    private string GameMode = "";
    //설명

    [SerializeField]
    private float player_Type = 0;
    //설명

    [SerializeField]
    private float Enemy_Type = 0;
    //설명

    [SerializeField]
    private int player_Region_Type = 0;
    //설명

    [SerializeField]
    private int Enemy_Region_Type = 0;
    //설명

    [SerializeField]
    private List<CharacterSOMaker> SO;

    [SerializeField]
    private int map = 0;

    [SerializeField]
    private int Party = 0;
    [SerializeField]
    private int Race = 0;
    [SerializeField]
    private int Round = 0;
    [SerializeField]
    private int win = 0;
    [SerializeField]
    private int lose = 0;

    


    public void GameModeSetting(string s)
    {
        GameMode = s;
    }
    public float Player_Typecheck()
    {
        return player_Type;
    }
    public void Player_TypeSetting(float f)
    {
        player_Type = f;
    }
    public void Player_Region_TypeSetting(int i)
    {
        player_Region_Type = i;
    }
    public void Enemy_Region_TypeSetting(int i)
    {
        Enemy_Region_Type = i;
    }

    public float Enemy_Typecheck()
    {
        return Enemy_Type;
    }
    public void Enemy_TypeSetting(float f)
    {
        Enemy_Type = f;
    }

    public void SetWin()
    {
        win++;
    }
    public int GetWin()
    {
        return win;
    }

    public void SetRace()
    {
        Race++;
    }
    public int GetRace()
    {
        return Race;
    }
    public void SetParty()
    {
        Party++;
    }
    public int GetParty()
    {
        return Party;
    }

    public CharacterSOMaker SO_find(string s)
    {
        int i = 0;

        for (i = 0; i < SO.Count; i++) //  list는 Count로 전체 범위를 알수 있음
        {
            if (s.Equals("p") && SO[i].getSo_Character_type() == player_Type)
            {
                break;
            }
            else if (s.Equals("e") && SO[i].getSo_Character_type() == Enemy_Type)
            {
                break;
            }
        }

        return SO[i];
    }

    public int GetRound()
    {
        return Round;
    }

    public void SetRound()
    {
        Round++;
    }
    public void SetRound0()
    {
        Round = 0;
    }

    public void settingReset()
    {
        GameMode = "";

        player_Type = 0;

        Enemy_Type = 0;

        player_Region_Type = 0;

        Enemy_Region_Type = 0;

        map = 0;

        Party = 0;
        Race = 0;
        Round = 0;
        lose = 0;
    }

    public static GamePlayManager Instance = null;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
