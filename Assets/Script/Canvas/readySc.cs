using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class readySc : MonoBehaviour
{
    TextMeshProUGUI t, t2;

    [SerializeField]
    private GameObject ball;

    private void OnEnable()
    {
        t = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        t2 = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        StartCoroutine(UIstart());
    }

    public IEnumerator UIstart(float duration = 1f)
    {
        TReset();
        yield return new WaitForSeconds(0.75f);
        TReset();
        //string s = "Ready!";
        //StringBuilder sb = new StringBuilder();

        //foreach (char c in s)
        //{
        //    sb.Append(c);
        //    t.text = sb.ToString();
        //    yield return new WaitForSeconds(0.01f);
        //}

        t.text = $"Race{GamePlayManager.Instance.GetRace()}";
        //t.text = "Ready!";
        //for (int i = 150; i >= 0; i -= 2)
        //{
        //    if (i > 100)
        //    {
        //        i -= 10;
        //    }
        //    if (i > 50)
        //    {
        //        i -= 8;
        //    }
        //    if (i > 20)
        //    {
        //        i -= 5;
        //    }
        //    if (i > 10)
        //    {
        //        i -= 2;
        //    }

        //    t.rectTransform.anchoredPosition = new Vector3(t.rectTransform.anchoredPosition.x, i);
        //    yield return new WaitForSeconds(0.01f);
        //}


        //EaseIn: 초반에 느리게 시작 (e.g. 일부로 천천히 시작하는 동작)

        //EaseOut: 끝으로 갈수록 부드럽게 감속

        //EaseInOut: 처음과 끝 모두 부드러운 움직임

        //EaseOutBack, EaseInElastic, EaseOutBounce



        float elapsed = 0f;
        float startY = 175f;
        float endY = 20f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);

            // EaseOutCubic: 빠르게 시작해서 점점 느려짐 (감속)
            float easedT = 1f - Mathf.Pow(1f - f, 5f);
            float y = Mathf.Lerp(startY, endY, easedT);

            t.rectTransform.anchoredPosition = new Vector2(t.rectTransform.anchoredPosition.x, y);
            yield return null;
        }

        // 마지막 위치 보정
        t.rectTransform.anchoredPosition = new Vector2(t.rectTransform.anchoredPosition.x, endY);
        t2.rectTransform.anchoredPosition = new Vector2(t2.rectTransform.anchoredPosition.x, -20);
        t2.text = "Ready!";

        t2.color = new Color32(255, 255, 255, 255);
        elapsed = 0f;
        t2.rectTransform.localScale = new Vector3(t.rectTransform.localScale.x, 0, t.rectTransform.localScale.z);
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);

            // EaseOutCubic: 빠르게 시작해서 점점 느려짐 (감속)
            float easedT = 1f - Mathf.Pow(1f - f, 1f);
            float y = Mathf.Lerp(t2.rectTransform.localScale.y, 1f, easedT);

            t2.rectTransform.localScale = new Vector3(t2.rectTransform.localScale.x, y, t2.rectTransform.localScale.z);

            yield return null;
        }

        yield return null;

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);


            float easedT = Mathf.Pow(f, 1f);
            float x = Mathf.Lerp(t.rectTransform.localScale.y, 0f, easedT);
            float y = Mathf.Lerp(t2.rectTransform.localScale.y, 0f, easedT);

            t.rectTransform.localScale = new Vector3(t.rectTransform.localScale.x, x, t.rectTransform.localScale.z);
            t2.rectTransform.localScale = new Vector3(t2.rectTransform.localScale.x, y, t2.rectTransform.localScale.z);
            yield return null;
        }

        //for (byte colorA = 255; colorA > 0; colorA -= 15)
        //{
        //    t.color = new Color32(255, 255, 255, colorA);
        //    yield return null;
        //}
        //t.color = new Color32(255, 255, 255, 0);
        //yield return null;

        t.rectTransform.localScale = new Vector3(1, 1, 1);

        t.rectTransform.anchoredPosition = new Vector2(t.rectTransform.anchoredPosition.x, 0);
        t.text = "3";
        for (byte colorA = 255; colorA > 0; colorA -= 15)
        {
            t.color = new Color32(255, 255, 255, colorA);
            yield return null;
        }
        t.color = new Color32(255, 255, 255, 0);
        yield return null;
        t.text = "2";
        for (byte colorA = 255; colorA > 0; colorA -= 15)
        {
            t.color = new Color32(255, 255, 255, colorA);
            yield return null;
        }
        t.color = new Color32(255, 255, 255, 0);
        yield return null;
        t.text = "1";
        for (byte colorA = 255; colorA > 0; colorA -= 15)
        {
            t.color = new Color32(255, 255, 255, colorA);
            yield return null;
        }
        t.color = new Color32(255, 255, 255, 0);
        yield return null;

        t.color = new Color32(255, 255, 255, 255);
        t2.color = new Color32(255, 255, 255, 255);

        GamePlayManager.Instance.SetRound();

        t.text = "Let's";
        t2.text = "Party!";

        //for (int i = 150; i >= 20; i -= 2)
        //{
        //    if (i > 100)
        //    {
        //        i -= 10;
        //    }
        //    if (i > 50)
        //    {
        //        i -= 8;
        //    }
        //    if (i > 20)
        //    {
        //        i -= 5;
        //    }
        //    if (i > 10)
        //    {
        //        i -= 2;
        //    }

        //    t.rectTransform.anchoredPosition = new Vector3(t.rectTransform.anchoredPosition.x, i);
        //    t2.rectTransform.anchoredPosition = new Vector3(t2.rectTransform.anchoredPosition.x, -i);
        //    yield return new WaitForSeconds(0.01f);
        //}

        GameObject b = Instantiate(ball);
        b.name = b.name.Remove(b.name.Length - 7, 7);

        elapsed = 0f;
        startY = 175f;
        endY = 20f;
        t2.rectTransform.localScale = new Vector3(t2.rectTransform.localScale.x, 1, t2.rectTransform.localScale.z);
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);

            // EaseOutCubic: 빠르게 시작해서 점점 느려짐 (감속)
            float easedT = 1f - Mathf.Pow(1f - f, 5f);
            float y = Mathf.Lerp(startY, endY, easedT);
            float y2 = Mathf.Lerp(-startY, -endY, easedT);

            t.rectTransform.anchoredPosition = new Vector2(t.rectTransform.anchoredPosition.x, y);
            t2.rectTransform.anchoredPosition = new Vector2(t2.rectTransform.anchoredPosition.x, y2);
            yield return null;
        }

        // 마지막 위치 보정
        t.rectTransform.anchoredPosition = new Vector2(t.rectTransform.anchoredPosition.x, endY);
        t2.rectTransform.anchoredPosition = new Vector2(t2.rectTransform.anchoredPosition.x, -endY);
        yield return null;

        //for (int i = 0; i <= 400; i += 20)
        //{
        //    t.rectTransform.anchoredPosition = new Vector3(-i, i);
        //    t2.rectTransform.anchoredPosition = new Vector3(i, -i);
        //    yield return new WaitForSeconds(0.01f);
        //}

        elapsed = 0f;
        startY = 20f;
        endY = 175f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);


            float easedT = Mathf.Pow(f, 5f);
            float y = Mathf.Lerp(startY, endY, easedT);
            float y2 = Mathf.Lerp(-startY, -endY, easedT);
            float x = Mathf.Lerp(0, -250, easedT);
            float x2 = Mathf.Lerp(0, 250, easedT);

            t.rectTransform.anchoredPosition = new Vector2(x, y);
            t2.rectTransform.anchoredPosition = new Vector2(x2, y2);
            yield return null;
        }

        t.color = new Color32(255, 255, 255, 0);
        t2.color = new Color32(255, 255, 255, 0);
        t.text = "";
        t2.text = "";
        t.rectTransform.anchoredPosition = new Vector3(0, 0);
        t2.rectTransform.anchoredPosition = new Vector3(0, 0);

        //gameObject.SetActive(false);
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
            float f = Mathf.Clamp01(elapsed / duration);

            // EaseOutCubic: 빠르게 시작해서 점점 느려짐 (감속)
            float easedT = 1f - Mathf.Pow(1f - f, 5f);
            float y = Mathf.Lerp(startY, endY, easedT);

            t.rectTransform.anchoredPosition = new Vector2(t.rectTransform.anchoredPosition.x, y);
            yield return null;
        }

        // 마지막 위치 보정
        t.rectTransform.anchoredPosition = new Vector2(t.rectTransform.anchoredPosition.x, endY);

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);


            float easedT = Mathf.Pow(f, 1f);
            float x = Mathf.Lerp(t.rectTransform.localScale.x, 1f, easedT);
            float y = Mathf.Lerp(t.rectTransform.localScale.y, 0f, easedT);


            t.rectTransform.localScale = new Vector3(x, y , t.rectTransform.localScale.z);
            yield return null;
        }

        t.rectTransform.localScale = new Vector3(1, 1, 1);
        //gameObject.SetActive(false);
        t.color = new Color32(255, 255, 255, 0);
        t.text = "";
        yield return null;

        TReset();
        t.text = $"Round {GamePlayManager.Instance.GetRound()}!";

        t.color = new Color32(255, 255, 255, 255);
        elapsed = 0f;
        t.rectTransform.localScale = new Vector3(t.rectTransform.localScale.x, 0, t.rectTransform.localScale.z);
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);

            // EaseOutCubic: 빠르게 시작해서 점점 느려짐 (감속)
            float easedT = 1f - Mathf.Pow(1f - f, 1f);
            float y = Mathf.Lerp(t.rectTransform.localScale.y, 1f, easedT);

            t.rectTransform.localScale = new Vector3(t.rectTransform.localScale.x, y, t.rectTransform.localScale.z);

            yield return null;
        }

        yield return null;

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);


            float easedT = Mathf.Pow(f, 1f);
            float x = Mathf.Lerp(t.rectTransform.localScale.x, 1f, easedT);
            float y = Mathf.Lerp(t.rectTransform.localScale.y, 0f, easedT);


            t.rectTransform.localScale = new Vector3(x, y, t.rectTransform.localScale.z);
            yield return null;
        }

        t.rectTransform.localScale = new Vector3(1, 1, 1);
        //gameObject.SetActive(false);
        t.color = new Color32(255, 255, 255, 0);

        yield return null;
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
            
            elapsed += Time.unscaledDeltaTime * 3;
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

            t.rectTransform.anchoredPosition = new Vector2(t.rectTransform.anchoredPosition.x, y);
            yield return null;
        }

        // 마지막 위치 보정
        t.rectTransform.anchoredPosition = new Vector2(t.rectTransform.anchoredPosition.x, endY);

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);


            float easedT = Mathf.Pow(f, 1f);
            float x = Mathf.Lerp(t.rectTransform.localScale.x, 1f, easedT);
            float y = Mathf.Lerp(t.rectTransform.localScale.y, 0f, easedT);


            t.rectTransform.localScale = new Vector3(x, y, t.rectTransform.localScale.z);
            yield return null;
        }

        t.rectTransform.localScale = new Vector3(1, 1, 1);
        //gameObject.SetActive(false);
        t.color = new Color32(255, 255, 255, 0);
        t.text = "";

        Player Winner;
        if (GameObject.Find("Player1").GetComponent<Player>().GetRoundWin() > GameObject.Find("Player2").GetComponent<Player>().GetRoundWin())
        {
            Winner = GameObject.Find("Player1").GetComponent<Player>();
            //GamePlayManager.Instance.SetWin();
        }
        else
        {
            Winner = GameObject.Find("Player2").GetComponent<Player>();
        }
        Winner.SetWin();
        t.text = $"{Winner.SO.getSo_Character_name()} Win!";

        GameObject canvas = Winner.Getcanvas();
        canvas.transform.GetChild(1).Find("VictoryText").GetComponent<TextMeshProUGUI>().text = $"WIN_[ {Winner.GetWin()} ]";

        t.color = new Color32(255, 255, 255, 255);
        elapsed = 0f;
        t.rectTransform.localScale = new Vector3(t.rectTransform.localScale.x, 0, t.rectTransform.localScale.z);
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);

            // EaseOutCubic: 빠르게 시작해서 점점 느려짐 (감속)
            float easedT = 1f - Mathf.Pow(1f - f, 1f);
            float y = Mathf.Lerp(t.rectTransform.localScale.y, 1f, easedT);

            t.rectTransform.localScale = new Vector3(t.rectTransform.localScale.x, y, t.rectTransform.localScale.z);

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);


            float easedT = Mathf.Pow(f, 1f);
            float x = Mathf.Lerp(t.rectTransform.localScale.x, 1f, easedT);
            float y = Mathf.Lerp(t.rectTransform.localScale.y, 0f, easedT);


            t.rectTransform.localScale = new Vector3(x, y, t.rectTransform.localScale.z);
            yield return null;
        }

        for (byte colorA = 0; colorA < 255; colorA += 15)
        {
            transform.GetChild(2).GetComponent<Image>().color = new Color32(0, 0, 0, colorA);
            yield return null;
        }
        transform.GetChild(2).GetComponent<Image>().color = new Color32(0, 0, 0, 255);

        yield return new WaitForSeconds(0.5f);

        PlayerCheckManager.Instance.AiVsResult();

        if (GamePlayManager.Instance.GetRace() == 8)
        {
            PlayerCheckManager.Instance.playerResultList();
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
        transform.GetChild(2).GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        t.color = new Color32(255, 255, 255, 255);
        t2.color = new Color32(255, 255, 255, 255);
        t.rectTransform.anchoredPosition = new Vector3(0, 0);
        t2.rectTransform.anchoredPosition = new Vector3(0, 0);
        t.rectTransform.localScale = new Vector3(1, 1, 1);
        t2.rectTransform.localScale = new Vector3(1, 1, 1);
        t.text = "";
        t2.text = "";
    }


}
