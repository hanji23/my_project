using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class readySc : MonoBehaviour
{
    TextMeshProUGUI t, t2;

    private void OnEnable()
    {
        t = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        t2 = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        StartCoroutine(UIstart());
    }

    public IEnumerator UIstart(float duration = 1f)
    {
        yield return new WaitForSeconds(0.75f);
        string s = "Ready!";
        StringBuilder sb = new StringBuilder();
        t.color = new Color32(255, 255, 255, 255);
        t2.color = new Color32(255, 255, 255, 255);
        t.rectTransform.anchoredPosition = new Vector3(0, 0);
        t2.rectTransform.anchoredPosition = new Vector3(0, 0);
        //foreach (char c in s)
        //{
        //    sb.Append(c);
        //    t.text = sb.ToString();
        //    yield return new WaitForSeconds(0.01f);
        //}

        t.text = "Ready!";
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
        float startY = 150f;
        float endY = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);

            // EaseOutCubic: 빠르게 시작해서 점점 느려짐 (감속)
            float easedT = 1f - Mathf.Pow(1f - f, 3f);
            float y = Mathf.Lerp(startY, endY, easedT);

            t.rectTransform.anchoredPosition = new Vector2(t.rectTransform.anchoredPosition.x, y);
            yield return null;
        }

        // 마지막 위치 보정
        t.rectTransform.anchoredPosition = new Vector2(t.rectTransform.anchoredPosition.x, endY);


        yield return new WaitForSeconds(0.39f);

        for (byte colorA = 255; colorA > 0; colorA -= 5)
        {
            t.color = new Color32(255, 255, 255, colorA);
            yield return new WaitForSeconds(0.01f);
        }
        t.color = new Color32(255, 255, 255, 0);
        yield return new WaitForSeconds(0.01f);

        t.text = "3";
        for (byte colorA = 255; colorA > 0; colorA -= 5)
        {
            t.color = new Color32(255, 255, 255, colorA);
            yield return new WaitForSeconds(0.01f);
        }
        t.color = new Color32(255, 255, 255, 0);
        yield return new WaitForSeconds(0.01f);
        t.text = "2";
        for (byte colorA = 255; colorA > 0; colorA -= 5)
        {
            t.color = new Color32(255, 255, 255, colorA);
            yield return new WaitForSeconds(0.01f);
        }
        t.color = new Color32(255, 255, 255, 0);
        yield return new WaitForSeconds(0.01f);
        t.text = "1";
        for (byte colorA = 255; colorA > 0; colorA -= 5)
        {
            t.color = new Color32(255, 255, 255, colorA);
            yield return new WaitForSeconds(0.01f);
        }
        t.color = new Color32(255, 255, 255, 0);
        yield return new WaitForSeconds(0.01f);

        t.color = new Color32(255, 255, 255, 255);
        t2.color = new Color32(255, 255, 255, 255);
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

        elapsed = 0f;
        startY = 150f;
        endY = 20f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);

            // EaseOutCubic: 빠르게 시작해서 점점 느려짐 (감속)
            float easedT = 1f - Mathf.Pow(1f - f, 3f);
            float y = Mathf.Lerp(startY, endY, easedT);
            float y2 = Mathf.Lerp(-startY, -endY, easedT);

            t.rectTransform.anchoredPosition = new Vector2(t.rectTransform.anchoredPosition.x, y);
            t2.rectTransform.anchoredPosition = new Vector2(t2.rectTransform.anchoredPosition.x, y2);
            yield return null;
        }

        // 마지막 위치 보정
        t.rectTransform.anchoredPosition = new Vector2(t.rectTransform.anchoredPosition.x, endY);

        yield return new WaitForSeconds(0.29f);

        //for (int i = 0; i <= 400; i += 20)
        //{
        //    t.rectTransform.anchoredPosition = new Vector3(-i, i);
        //    t2.rectTransform.anchoredPosition = new Vector3(i, -i);
        //    yield return new WaitForSeconds(0.01f);
        //}

        elapsed = 0f;
        startY = 20f;
        endY = 150f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);

            // EaseOutCubic: 빠르게 시작해서 점점 느려짐 (감속)
            float easedT = Mathf.Pow(f, 3f);
            float y = Mathf.Lerp(startY, endY, easedT);
            float y2 = Mathf.Lerp(-startY, -endY, easedT);

            t.rectTransform.anchoredPosition = new Vector2(t.rectTransform.anchoredPosition.x, y);
            t.rectTransform.anchoredPosition = new Vector2(-y * 2, t.rectTransform.anchoredPosition.y);
            t2.rectTransform.anchoredPosition = new Vector2(t2.rectTransform.anchoredPosition.x, y2);
            t2.rectTransform.anchoredPosition = new Vector2(y * 2, t2.rectTransform.anchoredPosition.y);
            yield return null;
        }

        t.color = new Color32(255, 255, 255, 0);
        t2.color = new Color32(255, 255, 255, 0);
        t.text = "";
        t2.text = "";
        gameObject.SetActive(false);
    }
}
