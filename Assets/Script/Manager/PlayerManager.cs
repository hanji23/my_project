using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance = null;

    [SerializeField]
    private List<CharacterSOMaker> characterSOs;
    //ĳ���� SO ������ ��� ����Ʈ
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
        //�÷��̾� ��ȣ �濡 ���� ������� ���� 1�� ������

        public int opponentPlayerNumber = 0;
        //�̹� ���̽��� ����� ��� �÷��̾� ��ȣ

        public int characterNumber = 0;
        //���� ĳ���� �ѹ�(ĳ���� ���ý� �޾ƿ�) (¦���� ���� ��Ų)

        public int playerRegionType = 0;
        //ĳ���� ���� (����â���� ������ ����� �Ѿ��)

        public int winCount;
        //�̱� Ƚ��

        public CharacterSOMaker characterSO;
        //ĳ���� ��ũ���ͺ� ������Ʈ (������ ĳ������ ��ũ���ͺ� ������Ʈ�� ����� ����)

        public bool isWinner = false;
        //�ش� ���̽��� �̰���� Ȯ���ϴ� ����

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

        // ����/���� �и�
        foreach (var p in allPlayers)
        {
            if (p.isWinner)
                winners.Add(p);
            else
                losers.Add(p);
        }

        // ����
        Shuffle(winners);
        Shuffle(losers);

        // ���� ������� VersusPlayers�� �߰�
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

    // ��¥ ���� ���� �Լ� (Fisher-Yates)
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
        allPlayers.Add(newP); // ����Ʈ�� �߰�

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
