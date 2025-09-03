using System;
using System.Collections.Generic;
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
    }

    [SerializeField]
    private List<PlayerList> Player = new List<PlayerList>();

    public void newPlayer(string s, float f)
    {
        PlayerList newP = new PlayerList(s);
        Player.Add(newP); // 리스트에 추가

        newP.Player_TypeSetting(f);
        newP.SO_find();
    }

    public void PlayerRegion(int i)
    {
        foreach (PlayerList p in Player)
        {
            if (p.GetisPlayer() == "Player")
            {
                p.Player_Region_TypeSetting(i);
            }
            else if(p.GetisPlayer() == "Ai")
            {
                p.Player_Region_TypeSetting(i);
            }
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

    public int ListCount()
    {
        return Player.Count;
    }

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void clearlist()
    {
        Player.Clear();
    }


}
