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

    public enum EPlayerType
    {
        None,
        Player,
        Enemy,
        Ai
    }

    [Serializable]
    public class PlayerList
    {

        public int PlayerType;

        static int count = 0;
        
        public int playerNum = 0;
        //플레이어 번호 방에 들어온 순서대로 값이 1씩 높아짐

        public int VsplayerNum = 0;
        //이번 레이스에 상대할 상대 플레이어 번호

        public int characterNum = 0;
        //고유 캐릭터 넘버(캐릭터 선택시 받아옴) (짝수는 변종 스킨)

        public int player_Region_Type = 0;
        //캐릭터 진영 (선택창에서 고른것이 여기로 넘어옴)

        public int win;
        //이긴 횟수

        public CharacterSOMaker PlayerSo;
        //캐릭터 스크랩터블 오브젝트 (선택한 캐릭터의 스크랩터블 오브젝트를 여기로 받음)

        public bool isWin = false;
        //해당 레이스를 이겻는지 확인하는 변수

        public PlayerList()
        {
            
        }
        public PlayerList(int p)
        {
            PlayerType = p;
        }

        //public void SO_find()
        //{
        //    int i = 0;

        //    for (i = 0; i < GamePlayManager.Instance.SO.Count(); i++) //  list는 Count로 전체 범위를 알수 있음
        //    {
        //        if (GamePlayManager.Instance.SO[i].getSo_Character_type() == characterNum)
        //        {
        //            break;
        //        }
        //    }

        //    PlayerSo = GamePlayManager.Instance.SO[i];
        //}

        public void SetCount(bool b) 
        {
            if (b)
                playerNum = ++count;
            else
                count = 0;
        }

    }

    public List<PlayerList> Player = new List<PlayerList>();

    [SerializeField]
    private List<PlayerList> PlayerCopy = new List<PlayerList>();
    public void PlayerCopyStart()
    {
        for (int i = 0; i < Player.Count; i++)
        {
            PlayerCopy.Add(Player[i]);
        }
    }

    public List<PlayerList> VersusPlayers = new List<PlayerList>();

    public void playerRandomList()
    {
        for(int i = 0; i < Player.Count; i++)
        {
            int r = UnityEngine.Random.Range(0, PlayerCopy.Count);
            VersusPlayers.Add(PlayerCopy[r]);
            PlayerCopy.RemoveAt(r);

            if (PlayerCopy.Count % 2 == 0)
            {
                VersusPlayers[i].VsplayerNum = VersusPlayers[i - 1].playerNum;
                VersusPlayers[i - 1].VsplayerNum = VersusPlayers[i].playerNum;
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
            if (p.isWin)
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
            Player[i].isWin = false;

            if (i % 2 == 1)
            {
                VersusPlayers[i - 1].VsplayerNum = VersusPlayers[i].playerNum;
                VersusPlayers[i].VsplayerNum = VersusPlayers[i - 1].playerNum;
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

    public void newPlayer(int i, int n)
    {
        PlayerList newP = new PlayerList(i);
        Player.Add(newP); // 리스트에 추가

        newP.characterNum = n;
        newP.PlayerSo = GamePlayManager.Instance.SO[newP.characterNum]; // 잘안되면 SO_find 참고
        newP.SetCount(true);

        if ((EPlayerType)newP.PlayerType == EPlayerType.Ai)
            newP.player_Region_Type = (UnityEngine.Random.Range(1, 5));
    }

    public void PlayerRegion(int i)
    {
        foreach (PlayerList p in Player)
        {
            if ((EPlayerType)p.PlayerType == EPlayerType.Player)
            {
                p.player_Region_Type = i;
            }
        }
    }

    public int PlayerCheck()
    {
        for (int i = 0; i < Player.Count; i++)
        {
            if ((EPlayerType)Player[i].PlayerType == EPlayerType.Player)
            {
                return i;
            }
        }
        return -1;
    }

    public void AiVsResult()
    {
        for (int i = 0; i < VersusPlayers.Count; i += 2)
        {
            if (VersusPlayers[i].isWin == false && VersusPlayers[i + 1].isWin == false)
            {
                   
                if (UnityEngine.Random.Range(0, 2) == 1)
                {
                    VersusPlayers[i].isWin = true;
                    VersusPlayers[i].win++;
                }
                else
                {
                    VersusPlayers[i + 1].isWin = true;
                    VersusPlayers[i + 1].win++;
                }
                    
            }
        }
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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
