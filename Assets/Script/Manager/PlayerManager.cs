using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance = null;

    [SerializeField]
    private List<CharacterSOMaker> characterSOs;
    //캐릭터 SO 정보가 담긴 리스트
    public List<CharacterSOMaker> CharacterSOs => characterSOs;

    public enum EPlayerType
    {
        None,
        Player,
        Enemy,
        AI
    }

    [Serializable]
    public class PlayerInfo
    {

        public int playerType;

        static int _globalPlayerCount = 0;
        
        public int playerNumber = 0;
        //플레이어 번호 방에 들어온 순서대로 값이 1씩 높아짐

        public int opponentPlayerNumber = 0;
        //이번 레이스에 상대할 상대 플레이어 번호

        public int characterNumber = 0;
        //고유 캐릭터 넘버(캐릭터 선택시 받아옴) (짝수는 변종 스킨)

        public int playerRegionType = 0;
        //캐릭터 진영 (선택창에서 고른것이 여기로 넘어옴)

        public int winCount;
        //이긴 횟수

        public CharacterSOMaker characterSO;
        //캐릭터 스크랩터블 오브젝트 (선택한 캐릭터의 스크랩터블 오브젝트를 여기로 받음)

        public bool isWinner = false;
        //해당 레이스를 이겻는지 확인하는 변수

        public PlayerInfo()
        {
            
        }
        public PlayerInfo(int p)
        {
            playerType = p;
        }

        public void AssignPlayerNumber(bool b) 
        {
            if (b)
                playerNumber = ++_globalPlayerCount;
            else
                _globalPlayerCount = 0;
        }

    }

    public List<PlayerInfo> allPlayers = new List<PlayerInfo>();

    [SerializeField]
    private List<PlayerInfo> playerBackup = new List<PlayerInfo>();
    public void CopyPlayerList()
    {
        for (int i = 0; i < allPlayers.Count; i++)
        {
            playerBackup.Add(allPlayers[i]);
        }
    }

    public List<PlayerInfo> matchedPlayers = new List<PlayerInfo>();

    public void ShuffleAndMatchPlayers()
    {
        for(int i = 0; i < allPlayers.Count; i++)
        {
            int r = UnityEngine.Random.Range(0, playerBackup.Count);
            matchedPlayers.Add(playerBackup[r]);
            playerBackup.RemoveAt(r);

            if (playerBackup.Count % 2 == 0)
            {
                matchedPlayers[i].opponentPlayerNumber = matchedPlayers[i - 1].playerNumber;
                matchedPlayers[i - 1].opponentPlayerNumber = matchedPlayers[i].playerNumber;
            }
        }
    }

    public void NextVersusRound()
    {
        matchedPlayers.Clear();

        List<PlayerInfo> winners = new List<PlayerInfo>();
        List<PlayerInfo> losers = new List<PlayerInfo>();

        // 승자/패자 분리
        foreach (var p in allPlayers)
        {
            if (p.isWinner)
                winners.Add(p);
            else
                losers.Add(p);
        }

        // 셔플
        Shuffle(winners);
        Shuffle(losers);

        // 섞인 순서대로 VersusPlayers에 추가
        matchedPlayers.AddRange(winners);
        matchedPlayers.AddRange(losers);

        for (int i = 0; i < matchedPlayers.Count; i++)
        {
            allPlayers[i].isWinner = false;

            if (i % 2 == 1)
            {
                matchedPlayers[i - 1].opponentPlayerNumber = matchedPlayers[i].playerNumber;
                matchedPlayers[i].opponentPlayerNumber = matchedPlayers[i - 1].playerNumber;
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

    public void AddNewPlayer(int type, int characterNum)
    {
        PlayerInfo newP = new PlayerInfo(type);
        allPlayers.Add(newP); // 리스트에 추가

        newP.characterNumber = characterNum;
        newP.characterSO = CharacterSOs[newP.characterNumber];
        newP.AssignPlayerNumber(true);

        if ((EPlayerType)newP.playerType == EPlayerType.AI)
            newP.playerRegionType = (UnityEngine.Random.Range(1, 5));
    }

    public void SetPlayerRegion(int regionType)
    {
        foreach (PlayerInfo p in allPlayers)
        {
            if ((EPlayerType)p.playerType == EPlayerType.Player)
            {
                p.playerRegionType = regionType;
            }
        }
    }

    public int GetMainPlayerIndex()
    {
        for (int i = 0; i < allPlayers.Count; i++)
        {
            if ((EPlayerType)allPlayers[i].playerType == EPlayerType.Player)
            {
                return i;
            }
        }
        return -1;
    }

    public void ResolveAiMatches()
    {
        for (int i = 0; i < matchedPlayers.Count; i += 2)
        {
            if (matchedPlayers[i].isWinner == false && matchedPlayers[i + 1].isWinner == false)
            {
                   
                if (UnityEngine.Random.Range(0, 2) == 1)
                {
                    matchedPlayers[i].isWinner = true;
                    matchedPlayers[i].winCount++;
                }
                else
                {
                    matchedPlayers[i + 1].isWinner = true;
                    matchedPlayers[i + 1].winCount++;
                }
                    
            }
        }
    }

    public void ClearAllPlayerData()
    {
        allPlayers.Clear();
        playerBackup.Clear();
        matchedPlayers.Clear();
        PlayerInfo playerList = new PlayerInfo();
        playerList.AssignPlayerNumber(false);
    }

    public void ResetPlayerCount()
    {
        PlayerInfo tempPlayer = new PlayerInfo();
        tempPlayer.AssignPlayerNumber(false);
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
