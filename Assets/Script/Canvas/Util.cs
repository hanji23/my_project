using System.Collections;
using TMPro;
using UnityEngine;

static class Util
{
    public enum EEaseMode
    {
        None,
        In,
        Out
    }
    public enum EEaseType
    {
        None,
        MoveY,
        MoveYX,
        Text
    }

    public static Color Setcolor255A(byte A)
    {
        Color color = new Color32(255, 255, 255, A);
        return color;
    }
    public static Color Setcolor0A(byte A)
    {
        Color color = new Color32(0, 0, 0, A);
        return color;
    }

    public static void EaseCubic(float elapsed, float duration, float startY, float endY, float frame, EEaseMode e1, EEaseType e2, RectTransform Rect1 = null, RectTransform Rect2 = null, float? startX = null, float? endX = null)
    {
        float f, x, easedT = 0, y = 0;

        f = Mathf.Clamp01(elapsed / duration);

        switch (e1)
        {
            case EEaseMode.In:
                // EaseOutCubic: 느리게 시작해서 점점 빨라짐 (가속)
                easedT = Mathf.Pow(f, frame);
                break;
            case EEaseMode.Out:
                // EaseOutCubic: 빠르게 시작해서 점점 느려짐 (감속)
                easedT = 1f - Mathf.Pow(1f - f, frame);
                break;
        }

        y = Mathf.Lerp(startY, endY, easedT);

        switch (e2)
        {
            case EEaseType.Text:
            case EEaseType.MoveY:
                if (Rect1 != null)
                    EaseType(Rect1, e2, y);
                if (Rect2 != null)
                    EaseType(Rect2, e2, y);
                break;

            case EEaseType.MoveYX:
                x = Mathf.Lerp(startX.Value, endX.Value, easedT);
                if (Rect1 != null)
                    EaseType(Rect1, e2, y, x);
                if (Rect2 != null)
                    EaseType(Rect2, e2, y, x);
                break;
        }
    }

    static void EaseType(RectTransform Rect, EEaseType e, float y, float? x = null)
    {
        switch (e)
        {
            case EEaseType.MoveY:
                Rect.anchoredPosition = new Vector2(Rect.anchoredPosition.x, y);
                break;
            case EEaseType.MoveYX:
                Rect.anchoredPosition = new Vector2(x.Value, y);
                break;
            case EEaseType.Text:
                Rect.localScale = new Vector3(Rect.localScale.x, y, Rect.localScale.z);
                break;
        }
    }

    public static IEnumerator FadeTransparency(TextMeshProUGUI t, EEaseMode e) {

        if (e == EEaseMode.In)
        {

        }
        else if (e == EEaseMode.Out)
        {
            for (byte colorA = 255; colorA > 0; colorA -= 15)
            {
                t.color = Setcolor255A(colorA);
                yield return null;
            }
            t.color = Setcolor255A(0);
        }

        yield return null;
    }

    public static IEnumerator CameraShake(float elapsed = 0f, float startY = 1f, float endY = 0f, float duration = 1)
    {
        Time.timeScale = 0f;
        int i = 0;
        float Ysave = Camera.main.transform.position.y;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime * 10;
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
    }
}
