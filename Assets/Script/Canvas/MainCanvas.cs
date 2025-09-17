using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject GPM, PCM;

    private void Start()
    {
        if (GamePlayManager.Instance == null)
        {
            GameObject g = Instantiate(GPM);
            //g.name.Replace("(Clone)", "");
            g.name = g.name.Remove(g.name.Length - 7, 7);
        }

        if (PlayerManager.Instance == null)
        {
            GameObject g = Instantiate(PCM);
            //g.name.Replace("(Clone)", "");
            g.name = g.name.Remove(g.name.Length - 7, 7);
        }
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; //에디터 환경 ( EditorApplication.Exit(0)는 에디터를 완전 종료함)
#else
        Application.Quit(); //게임 환경
#endif
    }

    public void GameStart(string s)
    {
        //씬 이동
        //유의사항 :  씬이 유니티 에디터에서 등록되어 있어야 합니다.
        GamePlayManager.Instance.gameModeIndex = (int)(GamePlayManager.EGameMode)Enum.Parse(typeof(GamePlayManager.EGameMode), s);
        GamePlayManager.Instance.totalRaceCount = 16;
        SceneManager.LoadScene("SelectScene");
    }

    public void SelectExit()
    {
        GamePlayManager.Instance.gameModeIndex = 0;
        SceneManager.LoadScene("StartScene");
    }

    public void RegionExit()
    {
        //GamePlayManager.Instance.Player_TypeSetting(0);
        //GamePlayManager.Instance.Player_Region_TypeSetting(0);
        PlayerManager.Instance.ClearAllPlayerData();
    }

    public void CharacterExit()
    {
        //나중에 반복문 돌리기
        GameObject.Find("Canvas").transform.Find("CharacterSelect").GetChild(0).GetComponent<CharacherSelect>().Setting();
        GameObject.Find("Canvas").transform.Find("CharacterSelect").GetChild(1).GetComponent<CharacherSelect>().Setting();
        GameObject.Find("Canvas").transform.Find("CharacterSelect").GetChild(2).GetComponent<CharacherSelect>().Setting();
    }
}
