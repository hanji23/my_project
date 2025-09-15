using UnityEngine;

static class Util
{
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

    public static void EaseOutCubic(float elapsed, float duration, float startY, float endY, RectTransform topRect, RectTransform bottomRect)
    {
        float f = Mathf.Clamp01(elapsed / duration);

        // EaseOutCubic: 빠르게 시작해서 점점 느려짐 (감속)
        float easedT = 1f - Mathf.Pow(1f - f, 5f);
        float y = Mathf.Lerp(startY, endY, easedT);

        topRect.anchoredPosition = new Vector2(topRect.anchoredPosition.x, y);
    }
}
