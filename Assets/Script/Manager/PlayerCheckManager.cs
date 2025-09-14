using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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
        //설명 (캐릭터 타입 넘버 짝수는 변종 스킨)

        [SerializeField]
        private int player_Region_Type = 0;
        //설명

        [SerializeField]
        private int win;

        [SerializeField]
        private CharacterSOMaker PlayerSo;

        [SerializeField]
        private bool isWin = false;

        [SerializeField]
        private int VsplayerNum = 0;

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

        public void WinCheck(bool b)
        {
            isWin = b;
        }

        public bool GetisWin()
        {
            return isWin;
        }

        public void SO_find()
        {
            int i = 0;

            for (i = 0; i < GamePlayManager.Instance.SO.Count(); i++) //  list는 Count로 전체 범위를 알수 있음
            {
                if (GamePlayManager.Instance.SO[i].getSo_Character_type() == player_Type)
                {
                    break;
                }
            }

            PlayerSo = GamePlayManager.Instance.SO[i];
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

        public void SetVsplayerNum(int i)
        {
            VsplayerNum = i;
        }

        public int GetVsplayerNum()
        {
            return VsplayerNum;
        }
    }

    [SerializeField]
    private List<PlayerList> Player = new List<PlayerList>();


    [SerializeField]
    private List<PlayerList> PlayerCopy = new List<PlayerList>();
    public void PlayerCopyStart()
    {
        for(int i = 0;i < Player.Count; i++)
        {
            PlayerCopy.Add(Player[i]);
        }
    }

    [SerializeField]
    private List<PlayerList> VersusPlayers = new List<PlayerList>();

    public void playerRandomList()
    {
        for(int i = 0; i < Player.Count; i++)
        {
            int r = UnityEngine.Random.Range(0, PlayerCopy.Count);
            VersusPlayers.Add(PlayerCopy[r]);
            PlayerCopy.RemoveAt(r);

            if (PlayerCopy.Count % 2 == 0)
            {
                VersusPlayers[i].SetVsplayerNum(VersusPlayers[i - 1].GetPlayerNum());
                VersusPlayers[i - 1].SetVsplayerNum(VersusPlayers[i].GetPlayerNum());
            }
        }
    }

    public void playerNextVsList()
    {
        VersusPlayers.Clear();

        List<PlayerList> winners = new List<PlayerList>();
        List<PlayerList> losers = new List<PlayerList>();

        // 승자/패자 분리
        foreach (var p in Player)
        {
            if (p.GetisWin())
                winners.Add(p);
            else
                losers.Add(p);
        }

        // 셔플
        Shuffle(winners);
        Shuffle(losers);

        // 섞인 순서대로 VersusPlayers에 추가
        VersusPlayers.AddRange(winners);
        VersusPlayers.AddRange(losers);

        for (int i = 0; i < VersusPlayers.Count; i++)
        {
            Player[i].WinCheck(false);

            if (i % 2 == 1)
            {
                VersusPlayers[i - 1].SetVsplayerNum(VersusPlayers[i].GetPlayerNum());
                VersusPlayers[i].SetVsplayerNum(VersusPlayers[i - 1].GetPlayerNum());
            }
        }
    }

    // 진짜 랜덤 셔플 함수 (Fisher-Yates)
    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    //public void playerNextVsList()
    //{
    //    VersusPlayers.Clear();

    //    List<int> f = new List<int>();

    //    for (int i = 0; i < Player.Count; i++)
    //    {
    //        if (Player[i].GetisWin())
    //        {
    //            //VersusPlayers.Add(Player[i]);
    //            f.Add(i);
    //        }
    //    }

    //    for (int i = f.Count; i > 0; i--)
    //    {
    //        int r = UnityEngine.Random.Range(0, f.Count);

    //        VersusPlayers.Add(Player[f[r]]);

    //        f.RemoveAt(r);
    //    }

    //    f.Clear();

    //    for (int i = 0; i < Player.Count; i++)
    //    {
    //        if (!Player[i].GetisWin())
    //        {
    //            //VersusPlayers.Add(Player[i]);
    //            f.Add(i);
    //        }
    //    }

    //    for (int i = f.Count; i > 0; i--)
    //    {
    //        int r = UnityEngine.Random.Range(0, f.Count);

    //        VersusPlayers.Add(Player[f[r]]);

    //        f.RemoveAt(r);
    //    }

    //    for (int i = 0; i < VersusPlayers.Count; i++)
    //    {
    //        Player[i].WinCheck(false);

    //        if (i % 2 == 1)
    //        {
    //            VersusPlayers[i - 1].SetVsplayerNum(VersusPlayers[i].GetPlayerNum());
    //            VersusPlayers[i].SetVsplayerNum(VersusPlayers[i - 1].GetPlayerNum());
    //        }
    //    }
    //}

    public void playerResultList()
    {
        int winner = 0;

        VersusPlayers.Sort((s1, s2) => s2.GetWin().CompareTo(s1.GetWin()));
    }

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
    public string GetPlayer(int i)
    {
        return Player[i].GetisPlayer();
    }
    public string GetResultPlayer(int i)
    {
        return VersusPlayers[i].GetisPlayer();
    }
    public string GetCharacter(int i)
    {
        return Player[i].Get_SO().getSo_Character_name();
    }

    public string GetResultCharacter(int i)
    {
        return VersusPlayers[i].Get_SO().getSo_Character_name();
    }

    public CharacterSOMaker PlayerSOCheck(int i)
    {
        return Player[i].Get_SO();
    }
    public int PlayerNumCheck(int i)
    {
        return Player[i].GetPlayerNum();
    }
    public int ResultPlayerNumCheck(int i)
    {
        return VersusPlayers[i].GetPlayerNum();
    }
    public int ResultPlayerWinCheck(int i)
    {
        return VersusPlayers[i].GetWin();
    }
    public int GetPlayerWin(int i)
    {
        return Player[i].GetWin();
    }
    public void SetPlayerWin(int i)
    {
        Player[i].SetWin();
    }
    public void IsPlayerWin(int i, bool b)
    {
        Player[i].WinCheck(b);
    }

    public void PlayerWinReset()
    {
        for (int i = 0; i < Player.Count; i++)
        {
            Player[i].WinCheck(false);
        }
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

    public int GetPlayerVs(int i)
    {
        return Player[i].GetVsplayerNum();
    }

    public void AiVsResult()
    {
        for (int i = 0; i < VersusPlayers.Count; i += 2)
        {
            if (VersusPlayers[i].GetisWin() == false && VersusPlayers[i + 1].GetisWin() == false)
            {
                   
                if (UnityEngine.Random.Range(0, 2) == 1)
                {
                    VersusPlayers[i].WinCheck(true);
                    VersusPlayers[i].SetWin();
                }
                else
                {
                    VersusPlayers[i + 1].WinCheck(true);
                    VersusPlayers[i + 1].SetWin();
                }
                    
            }
        }
    }

    public int ListCount()
    {
        return Player.Count;
    }

    public float ListCheck(int i)
    {
        return Player[i].Player_Typecheck();
    }

    public float ResultListCheck(int i)
    {
        return VersusPlayers[i].Player_Typecheck();
    }

    public void clearlist()
    {
        Player.Clear();
        PlayerCopy.Clear();
        VersusPlayers.Clear();
        PlayerList playerList = new PlayerList();
        playerList.SetCount(false);
    }

    public void clearCount()
    {
        PlayerList playerList = new PlayerList();
        playerList.SetCount(false);
    }


}
