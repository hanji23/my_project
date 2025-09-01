using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class VersusUI : MonoBehaviour
{
    bool Search = true;
    float time;
    int nowtime;

    public TextMeshProUGUI text, timetext, timetext2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GamePlayManager.Instance != null && (GamePlayManager.Instance.GetGameMode().Equals("TraningMode") || GamePlayManager.Instance.GetGameMode().Equals("AiMode"))) 
        {
            time = 0;
        }
        else
        {
            time = 60;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Search) 
        {
            if (time >= 0)
            {
                time -= Time.deltaTime;
                nowtime = Mathf.CeilToInt(time);

                if (nowtime < 10)
                    timetext2.text = $"0{nowtime}";
                else
                    timetext2.text = $"{nowtime}";

                text.text = $"�÷��̾� Ž���� \n <size=8>�ش� �ð��� ������ Ai�� ��Ī�˴ϴ�</size>";
            }
            else
            {
                Search = false;
                StartCoroutine(UIstart());
            }

        }

    }

    IEnumerator UIstart()
    {

        float duration = 1;
        RectTransform parentRect = text.transform.parent.GetComponent<RectTransform>();
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);


            float easedT = Mathf.Pow(f, 1f);
            float x = Mathf.Lerp(parentRect.localScale.x, 1f, easedT);
            float y = Mathf.Lerp(parentRect.localScale.y, 0f, easedT);


            parentRect.localScale = new Vector3(x, y, parentRect.localScale.z);
            yield return null;
        }

        yield return null;

        text.rectTransform.localScale = new Vector3(1,1,1);


        text.text = $"������ ���� �Ϸ�!";
        timetext.text = "\n��Ƽ ����!";
        timetext2.text = "";

        elapsed = 0f;
       
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);

            // EaseOutCubic: ������ �����ؼ� ���� ������ (����)
            float easedT = 1f - Mathf.Pow(1f - f, 1f);
            float y = Mathf.Lerp(parentRect.localScale.y, 1f, easedT);

            parentRect.localScale = new Vector3(parentRect.localScale.x, y, parentRect.localScale.z);

            yield return null;
        }
        parentRect.localScale = new Vector3(1, 1, 1);
        yield return null;

        while (PlayerCheckManager.Instance.ListCount() < 8)
        {
            PlayerCheckManager.Instance.newPlayer("Ai");
        }

    }
}
