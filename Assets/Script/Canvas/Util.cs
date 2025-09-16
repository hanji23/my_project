using System.Collections;
using TMPro;
using UnityEngine;
using static Util;

static class Util
{
    public enum ESetting
    {
        None,
        In,
        Out
    }
    public enum EType
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

    public static void EaseCubic(float elapsed, float duration, float startY, float endY, float frame, ESetting e1, EType e2, RectTransform Rect1 = null, RectTransform Rect2 = null, float? startX = null, float? endX = null)
    {
        float f, x, easedT = 0, y = 0;

        f = Mathf.Clamp01(elapsed / duration);

        switch (e1)
        {
            case ESetting.In:
                // EaseOutCubic: 느리게 시작해서 점점 빨라짐 (가속)
                easedT = Mathf.Pow(f, frame);
                break;
            case ESetting.Out:
                // EaseOutCubic: 빠르게 시작해서 점점 느려짐 (감속)
                easedT = 1f - Mathf.Pow(1f - f, frame);
                break;
        }

        y = Mathf.Lerp(startY, endY, easedT);

        if (e2 == EType.MoveYX && startX != null && endX != null)
        {
            x = Mathf.Lerp(startX.Value, endX.Value, easedT);
            if (Rect1 != null)
                EaseType(Rect1, e2, y, x);
            if (Rect2 != null)
                EaseType(Rect2, e2, y, x);
        }
        else
        {
            if (Rect1 != null)
                EaseType(Rect1, e2, y);
            if (Rect2 != null)
                EaseType(Rect2, e2, y);
        }
    }

    static void EaseType(RectTransform Rect, EType e, float y, float? x = null)
    {
        switch (e)
        {
            case EType.MoveY:
                Rect.anchoredPosition = new Vector2(Rect.anchoredPosition.x, y);
                break;
            case EType.MoveYX:
                Rect.anchoredPosition = new Vector2(x.Value, y);
                break;
            case EType.Text:
                Rect.localScale = new Vector3(Rect.localScale.x, y, Rect.localScale.z);
                break;
        }
    }

    public static IEnumerator FadeTransparency(TextMeshProUGUI t, ESetting e) {

        if (e == ESetting.In)
        {

        }
        else if (e == ESetting.Out)
        {
            for (byte colorA = 255; colorA > 0; colorA -= 15)
            {
                t.color = Util.Setcolor255A(colorA);
                yield return new WaitForSeconds(0.01f);
            }
            t.color = Util.Setcolor255A(0);
        }

        yield return new WaitForSeconds(0.01f);
    }
}
