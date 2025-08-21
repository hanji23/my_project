using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class MainCanvas : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; //에디터 환경 ( EditorApplication.Exit(0)는 에디터를 완전 종료함)
#else
        Application.Quit(); //게임 환경
#endif
    }

    public void GameStart()
    {
        //씬 이동
        //유의사항 :  씬이 유니티 에디터에서 등록되어 있어야 합니다.
        SceneManager.LoadScene("SelectScene");
    }
}
