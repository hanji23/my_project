using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class MainCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject GPM;

    private void Start()
    {
        if (GameObject.Find("GamePlayManager") == null)
        {
            GameObject g = Instantiate(GPM);
            g.name.Replace("(Clone)", "");
            //g.name.Remove(g.name.Length - 8, 8);

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
}
