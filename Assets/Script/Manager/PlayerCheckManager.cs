using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCheckManager : MonoBehaviour
{
    public static PlayerCheckManager Instance = null;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (Instance == null)
        {
            Instance = this;
        }
    }

    [Serializable]
    private class PlayerList
    {
        [SerializeField]
        private string isPlayer;

        static int count = 0;
        
        [SerializeField]
        private int playerNum = 0;

        [SerializeField]
        private float player_Type = 0;
        //설명

        [SerializeField]
        private int player_Region_Type = 0;
        //설명

        [SerializeField]
        private int win;

        [SerializeField]
        private CharacterSOMaker PlayerSo;

        public PlayerList()
        {
            
        }
        public PlayerList(string p)
        {
            isPlayer = p;
        }

        public string GetisPlayer()
        {
            return isPlayer;
        }

        // 변수 읽기 수정 부분
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

        public void SetWin()
        {
            win++;
        }
        public int GetWin()
        {
            return win;
        }

        public void SO_find()
        {
            int i = 0;

            for (i = 0; i < GamePlayManager.Instance.SOLC(); i++) //  list는 Count로 전체 범위를 알수 있음
            {
                if (GamePlayManager.Instance.SOList(i).getSo_Character_type() == player_Type)
                {
                    break;
                }
            }

            PlayerSo = GamePlayManager.Instance.SOList(i);
        }

        public CharacterSOMaker Get_SO()
        {
            return PlayerSo;
        }

        public void SetCount(bool b) 
        {
            if (b)
            {
                count++;
                playerNum = count;
            }
            else
                count = 0;
            
        }
        public int GetPlayerNum()
        {
            return playerNum;
        }
    }

    [SerializeField]
    private List<PlayerList> Player = new List<PlayerList>();

    public void newPlayer(string s, float f)
    {
        PlayerList newP = new PlayerList(s);
        Player.Add(newP); // 리스트에 추가

        newP.Player_TypeSetting(f);
        newP.SO_find();
        newP.SetCount(true);

        if (newP.GetisPlayer() == "Ai")
        {
            newP.Player_Region_TypeSetting(UnityEngine.Random.Range(1, 5));
        }

        //Debug.Log(newP.GetPlayerNum());
    }

    public void PlayerRegion(int i)
    {
        foreach (PlayerList p in Player)
        {
            if (p.GetisPlayer() == "Player")
            {
                p.Player_Region_TypeSetting(i);
            }
        }
    }

    public int PlayerCheck()
    {
        for (int i = 0; i < Player.Count; i++)
        {
            if (Player[i].GetisPlayer() == "Player")
            {
                return i;
            }
        }
        return -1;
    }
    public float PlayerTypeCheck(int i)
    {
        return Player[i].Player_Typecheck();
    }
    public CharacterSOMaker PlayerSOCheck(int i)
    {
        return Player[i].Get_SO();
    }
    public int PlayerNumCheck(int i)
    {
        return Player[i].GetPlayerNum();
    }
    public int GetPlayerWin(int i)
    {
        return Player[i].GetWin();
    }
    public void SetPlayerWin(int i)
    {
        Player[i].SetWin();
    }

    public float GetPlayerType()
    {
        foreach (PlayerList p in Player)
        {
            if (p.GetisPlayer() == "Player")
            {
                return p.Player_Typecheck();
            }
        }
        return 0;
    }

    public int ListCount()
    {
        return Player.Count;
    }

    public float ListCheck(int i)
    {
        return Player[i].Player_Typecheck();
    }

    public void clearlist()
    {
        Player.Clear();
        PlayerList playerList = new PlayerList();
        playerList.SetCount(false);
    }

    public void clearCount()
    {
        PlayerList playerList = new PlayerList();
        playerList.SetCount(false);
    }
}
