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

    void Awake()
    {
        t = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        t2 = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        StartCoroutine(UIstart());

        topRect = t.rectTransform;
        bottomRect = t2.rectTransform;
    }

    public IEnumerator UIstart(float duration = 1f)
    {
        //TReset();
        yield return new WaitForSeconds(0.75f);
        //TReset();

        t.text = $"Race{GamePlayManager.Instance.Race}";

        float elapsed = 0f;
        float startY = 175f;
        float endY = 20f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Util.EaseCubic(elapsed, duration, startY, endY, 5f, Util.ESetting.Out, Util.EType.MoveY, Rect1: topRect);
            yield return new WaitForSeconds(0.01f);
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
            Util.EaseCubic(elapsed, duration, bottomRect.localScale.y, 1f, 1f, Util.ESetting.Out, Util.EType.Text, Rect2: bottomRect);
            yield return new WaitForSeconds(0.01f);
        }

       yield return new WaitForSeconds(0.01f);

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Util.EaseCubic(elapsed, duration, bottomRect.localScale.y, 0f, 1f, Util.ESetting.In, Util.EType.Text, Rect1: topRect);
            Util.EaseCubic(elapsed, duration, bottomRect.localScale.y, 0f, 1f, Util.ESetting.In, Util.EType.Text, Rect2: bottomRect);
            yield return new WaitForSeconds(0.01f);
        }

        topRect.localScale = new Vector3(1, 1, 1);

        topRect.anchoredPosition = new Vector2(topRect.anchoredPosition.x, 0);

        t.text = "3";

        yield return StartCoroutine(Util.FadeTransparency(t, Util.ESetting.Out));

       yield return new WaitForSeconds(0.01f);

        t.text = "2";

        yield return StartCoroutine(Util.FadeTransparency(t, Util.ESetting.Out));

        yield return new WaitForSeconds(0.01f);

        t.text = "1";

        yield return StartCoroutine(Util.FadeTransparency(t, Util.ESetting.Out));

        yield return new WaitForSeconds(0.01f);

        t.color = Util.Setcolor255A(255);
        t2.color = Util.Setcolor255A(255);

        GamePlayManager.Instance.Round++;

        t.text = "Let's";
        t2.text = "Party!";

        GameObject b = Instantiate(ball);
        b.name = b.name.Remove(b.name.Length - 7, 7);

        elapsed = 0f;
        startY = 175f;
        endY = 20f;
        bottomRect.localScale = new Vector3(bottomRect.localScale.x, 1, bottomRect.localScale.z);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Util.EaseCubic(elapsed, duration, startY, endY, 5f, Util.ESetting.Out, Util.EType.MoveY, Rect1: topRect);
            Util.EaseCubic(elapsed, duration, -startY, -endY, 5f, Util.ESetting.Out, Util.EType.MoveY, Rect2: bottomRect);

            yield return new WaitForSeconds(0.01f);
        }

        // 마지막 위치 보정
        topRect.anchoredPosition = new Vector2(topRect.anchoredPosition.x, endY);
        bottomRect.anchoredPosition = new Vector2(bottomRect.anchoredPosition.x, -endY);
        yield return new WaitForSeconds(0.03f);

        elapsed = 0f;
        startY = 20f;
        endY = 175f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Util.EaseCubic(elapsed, duration, startY, endY, 5f, Util.ESetting.In, Util.EType.MoveYX, Rect1: topRect, startX: 0, endX: -250);
            Util.EaseCubic(elapsed, duration, -startY, -endY, 5f, Util.ESetting.In, Util.EType.MoveYX, Rect2: bottomRect, startX: 0, endX: 250);

            yield return new WaitForSeconds(0.01f);
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
            Util.EaseCubic(elapsed, duration, startY, endY, 5f, Util.ESetting.Out, Util.EType.MoveY, Rect1: topRect);

            yield return new WaitForSeconds(0.01f);
        }

        // 마지막 위치 보정
        topRect.anchoredPosition = new Vector2(topRect.anchoredPosition.x, endY);

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);


            float easedT = Mathf.Pow(f, 1f);
            float x = Mathf.Lerp(topRect.localScale.x, 1f, easedT);
            float y = Mathf.Lerp(topRect.localScale.y, 0f, easedT);


            topRect.localScale = new Vector3(x, y , topRect.localScale.z);
           yield return new WaitForSeconds(0.01f);
        }

        topRect.localScale = new Vector3(1, 1, 1);
        //gameObject.SetActive(false);
        t.color = Util.Setcolor255A(0);
        t.text = "";
       yield return new WaitForSeconds(0.01f);

        TReset();
        t.text = $"Round {GamePlayManager.Instance.Round}!";

        t.color = Util.Setcolor255A(255);
        elapsed = 0f;
        topRect.localScale = new Vector3(topRect.localScale.x, 0, topRect.localScale.z);
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);

            // EaseOutCubic: 빠르게 시작해서 점점 느려짐 (감속)
            float easedT = 1f - Mathf.Pow(1f - f, 1f);
            float y = Mathf.Lerp(topRect.localScale.y, 1f, easedT);

            topRect.localScale = new Vector3(topRect.localScale.x, y, topRect.localScale.z);

           yield return new WaitForSeconds(0.01f);
        }

       yield return new WaitForSeconds(0.01f);

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);


            float easedT = Mathf.Pow(f, 1f);
            float x = Mathf.Lerp(topRect.localScale.x, 1f, easedT);
            float y = Mathf.Lerp(topRect.localScale.y, 0f, easedT);


            topRect.localScale = new Vector3(x, y, topRect.localScale.z);
           yield return new WaitForSeconds(0.01f);
        }

        topRect.localScale = new Vector3(1, 1, 1);
        //gameObject.SetActive(false);
        t.color = Util.Setcolor255A(0);

       yield return new WaitForSeconds(0.01f);
    }

    public IEnumerator UIFINISH()
    {
        Time.timeScale = 0f;
        float Ysave = Camera.main.transform.position.y;

        int i = 0;

        float elapsed = 0f;
        float startY = 1f;
        float endY = 0f;
        float duration = 1;

        while (elapsed < duration)
        {
            
            elapsed += Time.unscaledDeltaTime * 4;
            float f = Mathf.Clamp01(elapsed / duration);

            // EaseOutCubic: 빠르게 시작해서 점점 느려짐 (감속)
            float easedT = 1f - Mathf.Pow(1f - f, 3f);
            float y = Mathf.Lerp(startY, endY, easedT);

            if (i % 2 == 0)
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Ysave + y, Camera.main.transform.position.z);
            else
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Ysave - y, Camera.main.transform.position.z);
            i++;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Ysave, Camera.main.transform.position.z);
        Time.timeScale = 1f;

        elapsed = 0f;
        startY = 175f;
        endY = 0f;
        duration = 1f;

        TReset();
        t.text = "FINISH!";

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);

            // EaseOutCubic: 빠르게 시작해서 점점 느려짐 (감속)
            float easedT = 1f - Mathf.Pow(1f - f, 5f);
            float y = Mathf.Lerp(startY, endY, easedT);

            topRect.anchoredPosition = new Vector2(topRect.anchoredPosition.x, y);
           yield return new WaitForSeconds(0.01f);
        }

        // 마지막 위치 보정
        topRect.anchoredPosition = new Vector2(topRect.anchoredPosition.x, endY);

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);


            float easedT = Mathf.Pow(f, 1f);
            float x = Mathf.Lerp(topRect.localScale.x, 1f, easedT);
            float y = Mathf.Lerp(topRect.localScale.y, 0f, easedT);


            topRect.localScale = new Vector3(x, y, topRect.localScale.z);
           yield return new WaitForSeconds(0.01f);
        }

        topRect.localScale = new Vector3(1, 1, 1);
        //gameObject.SetActive(false);
        t.color = Util.Setcolor255A(0);
        t.text = "";

        Player Winner;
        if (GameObject.Find("Player1").GetComponent<Player>().RoundWin > GameObject.Find("Player2").GetComponent<Player>().RoundWin)
        {
            Winner = GameObject.Find("Player1").GetComponent<Player>();
            //GamePlayManager.Instance.SetWin();
        }
        else
        {
            Winner = GameObject.Find("Player2").GetComponent<Player>();
        }
        Winner.SetWin();
        t.text = $"{Winner.SO.Character_name} Win!";

        GameObject canvas = Winner.Canvas;
        canvas.transform.GetChild(1).Find("VictoryText").GetComponent<TextMeshProUGUI>().text = $"WIN_[ {Winner.GetWin()} ]";

        t.color = Util.Setcolor255A(255);
        elapsed = 0f;
        topRect.localScale = new Vector3(topRect.localScale.x, 0, topRect.localScale.z);
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);

            // EaseOutCubic: 빠르게 시작해서 점점 느려짐 (감속)
            float easedT = 1f - Mathf.Pow(1f - f, 1f);
            float y = Mathf.Lerp(topRect.localScale.y, 1f, easedT);

            topRect.localScale = new Vector3(topRect.localScale.x, y, topRect.localScale.z);

           yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);


            float easedT = Mathf.Pow(f, 1f);
            float x = Mathf.Lerp(topRect.localScale.x, 1f, easedT);
            float y = Mathf.Lerp(topRect.localScale.y, 0f, easedT);


            topRect.localScale = new Vector3(x, y, topRect.localScale.z);
           yield return new WaitForSeconds(0.01f);
        }

        for (byte colorA = 0; colorA < 255; colorA += 15)
        {
            transform.GetChild(2).GetComponent<Image>().color = Util.Setcolor0A(colorA);
           yield return new WaitForSeconds(0.01f);
        }
        transform.GetChild(2).GetComponent<Image>().color = Util.Setcolor0A(255);

        yield return new WaitForSeconds(0.5f);

        PlayerCheckManager.Instance.AiVsResult();

        if (GamePlayManager.Instance.Race == GamePlayManager.Instance.FinalRace)
        {
            PlayerCheckManager.Instance.VersusPlayers.Sort((s1, s2) => s2.win.CompareTo(s1.win));
            SceneManager.LoadScene("ResultScene");
        }
        else
        {
            PlayerCheckManager.Instance.playerNextVsList();
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
