using System;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
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

        if (PlayerCheckManager.Instance == null)
        {
            GameObject g = Instantiate(PCM);
            //g.name.Replace("(Clone)", "");
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
        GamePlayManager.Instance.GameMode = (int)(GamePlayManager.GameModeList)Enum.Parse(typeof(GamePlayManager.GameModeList), s);
        GamePlayManager.Instance.FinalRace = 16;
        SceneManager.LoadScene("SelectScene");
    }

    public void SelectExit()
    {
        GamePlayManager.Instance.GameMode = 0;
        SceneManager.LoadScene("StartScene");
    }

    public void RegionExit()
    {
        //GamePlayManager.Instance.Player_TypeSetting(0);
        //GamePlayManager.Instance.Player_Region_TypeSetting(0);
        PlayerCheckManager.Instance.clearlist();
    }

    public void CharacterExit()
    {
        //���߿� �ݺ��� ������
        GameObject.Find("Canvas").transform.Find("CharacterSelect").GetChild(0).GetComponent<CharacherSelect>().Setting();
        GameObject.Find("Canvas").transform.Find("CharacterSelect").GetChild(1).GetComponent<CharacherSelect>().Setting();
        GameObject.Find("Canvas").transform.Find("CharacterSelect").GetChild(2).GetComponent<CharacherSelect>().Setting();
    }
}
