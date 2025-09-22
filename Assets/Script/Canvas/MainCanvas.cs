using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject GPM, PCM, SKM;

    private void Start()
    {
        GameObject g;

        if (GamePlayManager.Instance == null)
        {
            g = Instantiate(GPM);
            g.name = g.name.Remove(g.name.Length - 7, 7);
        }

        if (PlayerManager.Instance == null)
        {
            g = Instantiate(PCM);
            g.name = g.name.Remove(g.name.Length - 7, 7);
        }

        if (SkillManager.Instance == null)
        {
            g = Instantiate(SKM);
            g.name = g.name.Remove(g.name.Length - 7, 7);
        }
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; //������ ȯ�� ( EditorApplication.Exit(0)�� �����͸� ���� ������)
#else
        Application.Quit(); //���� ȯ��
#endif
    }

    public void GameStart(string s)
    {
        //�� �̵�
        //���ǻ��� :  ���� ����Ƽ �����Ϳ��� ��ϵǾ� �־�� �մϴ�.
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
        //���߿� �ݺ��� ������
        GameObject.Find("Canvas").transform.Find("CharacterSelect").GetChild(0).GetComponent<CharacherSelect>().Setting();
        GameObject.Find("Canvas").transform.Find("CharacterSelect").GetChild(1).GetComponent<CharacherSelect>().Setting();
        GameObject.Find("Canvas").transform.Find("CharacterSelect").GetChild(2).GetComponent<CharacherSelect>().Setting();
    }
}
