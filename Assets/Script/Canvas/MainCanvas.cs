using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject GPM;

    private void Start()
    {
        if (GamePlayManager.Instance == null)
        {
            GameObject g = Instantiate(GPM);
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
        GamePlayManager.Instance.GameModeSetting(s);
        SceneManager.LoadScene("SelectScene");
    }

    public void SelectExit()
    {
        GamePlayManager.Instance.GameModeSetting(null);
        SceneManager.LoadScene("StartScene");
    }

    public void RegionExit()
    {
        GamePlayManager.Instance.Player_TypeSetting(0);
        GamePlayManager.Instance.Region_TypeSetting(0);
    }

    public void CharacterExit()
    {
        GameObject.Find("Canvas").transform.Find("CharacterSelect").GetChild(Mathf.FloorToInt(GamePlayManager.Instance.Player_Typecheck()) - 1).GetComponent<CharacherSelect>().Setting();
    }
}
