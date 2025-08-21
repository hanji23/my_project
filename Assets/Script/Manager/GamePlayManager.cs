using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField]
    private int GameMode = 0;
    //설명

    [SerializeField]
    private float player_Type = 0;
    //설명

    [SerializeField]
    private float Enemy_Type = 0;
    //설명

    [SerializeField]
    private List<CharacterSOMaker> SO;

    [SerializeField]
    private int map = 0;

    [SerializeField]
    private int race = 0;
    [SerializeField]
    private int round = 0;
    [SerializeField]
    private int win = 0;
    [SerializeField]
    private int lose = 0;

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
