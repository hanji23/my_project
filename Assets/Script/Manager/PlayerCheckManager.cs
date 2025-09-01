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
        private int win;

        public PlayerList(string p)
        {
            isPlayer = p;
        }
    }

    [SerializeField]
    private List<PlayerList> Player = new List<PlayerList>();

    public void newPlayer(string s)
    {
        PlayerList newP = new PlayerList(s);
        Player.Add(newP); // 리스트에 추가
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
}
