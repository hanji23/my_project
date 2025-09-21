using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ready : MonoBehaviour
{
    TextMeshProUGUI t, t2;
    RectTransform topRect, bottomRect;

    [SerializeField]
    private GameObject ball;
    MainCamera mainCamera;

    void Awake()
    {
        t = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        t2 = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        StartCoroutine(UIstart());

        mainCamera = Camera.main.GetComponent<MainCamera>();
        topRect = t.rectTransform;
        bottomRect = t2.rectTransform;
    }

    public IEnumerator UIstart(float duration = 1f)
    {
        //TReset();
        yield return new WaitForSeconds(0.75f);
        //TReset();

        t.text = $"Race{GamePlayManager.Instance.currentRace}";

        float elapsed = 0f;
        float startY = 175f;
        float endY = 20f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Util.EaseCubic(elapsed, duration, startY, endY, 5f, Util.EEaseMode.Out, Util.EEaseType.MoveY, Rect1: topRect);
            yield return null;
        }

        // 마지막 위치 보정
        topRect.anchoredPosition = new Vector2(topRect.anchoredPosition.x, endY);
        bottomRect.anchoredPosition = new Vector2(bottomRect.anchoredPosition.x, -20);
        t2.text = "Ready!";

        t2.color = Util.Setcolor255A(255);
        elapsed = 0f;
        bottomRect.localScale = new Vector3(topRect.localScale.x, 0, topRect.localScale.z);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Util.EaseCubic(elapsed, duration, bottomRect.localScale.y, 1f, 1f, Util.EEaseMode.Out, Util.EEaseType.Text, Rect2: bottomRect);
            yield return null;
        }
        bottomRect.localScale = new Vector3(topRect.localScale.x, 1, bottomRect.localScale.z);

        yield return null;

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Util.EaseCubic(elapsed, duration, bottomRect.localScale.y, 0f, 1f, Util.EEaseMode.In, Util.EEaseType.Text, Rect1: topRect);
            Util.EaseCubic(elapsed, duration, bottomRect.localScale.y, 0f, 1f, Util.EEaseMode.In, Util.EEaseType.Text, Rect2: bottomRect);
            yield return null;
        }
        topRect.localScale = new Vector3(topRect.localScale.x, 0, topRect.localScale.z);
        bottomRect.localScale = new Vector3(topRect.localScale.x, 0, bottomRect.localScale.z);

        topRect.localScale = new Vector3(1, 1, 1);
        topRect.anchoredPosition = new Vector2(topRect.anchoredPosition.x, 0);

        t.text = "3";
        yield return StartCoroutine(Util.FadeTransparency(t, Util.EEaseMode.Out));
        yield return null;

        t.text = "2";
        yield return StartCoroutine(Util.FadeTransparency(t, Util.EEaseMode.Out));
        yield return null;

        t.text = "1";
        yield return StartCoroutine(Util.FadeTransparency(t, Util.EEaseMode.Out));
        yield return null;

        t.color = Util.Setcolor255A(255);
        t2.color = Util.Setcolor255A(255);

        GamePlayManager.Instance.currentRound++;

        t.text = "Let's";
        t2.text = "Party!";

        GameObject b = Instantiate(ball);
        b.GetComponent<BallAction>().player1Transform = mainCamera.player1;
        b.GetComponent<BallAction>().player2Transform = mainCamera.player2;
        b.GetComponent<BallAction>().ResetBall();
        b.name = b.name.Remove(b.name.Length - 7, 7);

        elapsed = 0f;
        startY = 175f;
        endY = 20f;
        bottomRect.localScale = new Vector3(bottomRect.localScale.x, 1, bottomRect.localScale.z);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Util.EaseCubic(elapsed, duration, startY, endY, 5f, Util.EEaseMode.Out, Util.EEaseType.MoveY, Rect1: topRect);
            Util.EaseCubic(elapsed, duration, -startY, -endY, 5f, Util.EEaseMode.Out, Util.EEaseType.MoveY, Rect2: bottomRect);

            yield return null;
        }
        // 마지막 위치 보정
        topRect.anchoredPosition = new Vector2(topRect.anchoredPosition.x, endY);
        bottomRect.anchoredPosition = new Vector2(bottomRect.anchoredPosition.x, -endY);

        yield return null;

        elapsed = 0f;
        startY = 20f;
        endY = 175f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Util.EaseCubic(elapsed, duration, startY, endY, 5f, Util.EEaseMode.In, Util.EEaseType.MoveYX, Rect1: topRect, startX: 0, endX: -250);
            Util.EaseCubic(elapsed, duration, -startY, -endY, 5f, Util.EEaseMode.In, Util.EEaseType.MoveYX, Rect2: bottomRect, startX: 0, endX: 250);

            yield return null;
        }

        t.color = Util.Setcolor255A(0);
        t2.color = Util.Setcolor255A(0);
        t.text = "";
        t2.text = "";
        topRect.anchoredPosition = new Vector3(0, 0);
        bottomRect.anchoredPosition = new Vector3(0, 0);

    }

     

    public IEnumerator UIdown()
    {
        TReset();
        t.text = "DOWN!";

        float elapsed = 0f;
        float startY = 175f;
        float endY = 0f;
        float duration = 1f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Util.EaseCubic(elapsed, duration, startY, endY, 5f, Util.EEaseMode.Out, Util.EEaseType.MoveY, Rect1: topRect);

            yield return null;
        }
        // 마지막 위치 보정
        topRect.anchoredPosition = new Vector2(topRect.anchoredPosition.x, endY);

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Util.EaseCubic(elapsed, duration, topRect.localScale.y, 0f, 1f, Util.EEaseMode.In, Util.EEaseType.Text, Rect1: topRect);

            yield return null;
        }
        topRect.localScale = new Vector3(topRect.localScale.x, 0, topRect.localScale.z);
        yield return null;

        t.text = $"Round {GamePlayManager.Instance.currentRound}!";
        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Util.EaseCubic(elapsed, duration, topRect.localScale.y, 1f, 1f, Util.EEaseMode.Out, Util.EEaseType.Text, Rect1: topRect);

            yield return null;
        }
        topRect.localScale = new Vector3(topRect.localScale.x, 1, topRect.localScale.z);
        yield return null;

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Util.EaseCubic(elapsed, duration, topRect.localScale.y, 0f, 1f, Util.EEaseMode.In, Util.EEaseType.Text, Rect1: topRect);

            yield return null;
        }

        topRect.localScale = new Vector3(topRect.localScale.x, 0, topRect.localScale.z);
        t.color = Util.Setcolor255A(0);

       yield return null;
    }

    public IEnumerator UIFINISH()
    {
        yield return StartCoroutine(Util.CameraShake());

        float elapsed = 0f;
        float startY = 175f;
        float endY = 0f;
        float duration = 1f;

        TReset();
        t.text = "FINISH!";

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Util.EaseCubic(elapsed, duration, startY, endY, 5f, Util.EEaseMode.Out, Util.EEaseType.MoveY, Rect1: topRect);

            yield return null;
        }
        // 마지막 위치 보정
        topRect.anchoredPosition = new Vector2(topRect.anchoredPosition.x, endY);

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Util.EaseCubic(elapsed, duration, topRect.localScale.y, 0f, 1f, Util.EEaseMode.In, Util.EEaseType.Text, Rect1: topRect);

            yield return null;
        }
        topRect.localScale = new Vector3(topRect.localScale.x, 0, topRect.localScale.z);

        Player Winner;

        if (mainCamera.player1.GetComponent<Player>().roundWins > mainCamera.player2.GetComponent<Player>().roundWins)
            Winner = GameObject.Find("Player1").GetComponent<Player>();
        else
            Winner = GameObject.Find("Player2").GetComponent<Player>();

        Winner.AddWinCount();
        t.text = $"{Winner.characterSO.Character_name} Win!";

        GameObject canvas = Winner.uiCanvas;
        canvas.transform.GetChild(1).Find("VictoryText").GetComponent<TextMeshProUGUI>().text = $"WIN_[ {Winner.GetWinCount()} ]";

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Util.EaseCubic(elapsed, duration, topRect.localScale.y, 1f, 1f, Util.EEaseMode.Out, Util.EEaseType.Text, Rect1: topRect);

            yield return null;
        }
        topRect.localScale = new Vector3(topRect.localScale.x, 1, topRect.localScale.z);

        yield return new WaitForSeconds(0.5f);

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Util.EaseCubic(elapsed, duration, topRect.localScale.y, 0f, 1f, Util.EEaseMode.In, Util.EEaseType.Text, Rect1: topRect);

            yield return null;
        }
        topRect.localScale = new Vector3(topRect.localScale.x, 0, topRect.localScale.z);

        for (byte colorA = 0; colorA < 255; colorA += 15)
        {
            transform.GetChild(2).GetComponent<Image>().color = Util.Setcolor0A(colorA);
           yield return null;
        }
        transform.GetChild(2).GetComponent<Image>().color = Util.Setcolor0A(255);

        yield return new WaitForSeconds(0.5f);

        PlayerManager.Instance.ResolveAiMatches();

        if (GamePlayManager.Instance.currentRace == GamePlayManager.Instance.totalRaceCount)
        {
            PlayerManager.Instance.matchedPlayers.Sort((s1, s2) => s2.winCount.CompareTo(s1.winCount));
            SceneManager.LoadScene("ResultScene");
        }
        else
        {
            PlayerManager.Instance.NextVersusRound();
            SceneManager.LoadScene("StoreScene");
        }
    }
           

    void TReset()
    {
        transform.GetChild(2).GetComponent<Image>().color = Util.Setcolor0A(0);
        t.color = Util.Setcolor255A(255);
        t2.color = Util.Setcolor255A(255);
        topRect.anchoredPosition = Vector3.zero;
        bottomRect.anchoredPosition = Vector3.zero;
        topRect.localScale = new Vector3(1, 1, 1);
        bottomRect.localScale = new Vector3(1, 1, 1);
        t.text = "";
        t2.text = "";
    }
}
