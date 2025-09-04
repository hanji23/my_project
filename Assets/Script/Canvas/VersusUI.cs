using System.Collections;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VersusUI : MonoBehaviour
{
    bool Search = true;
    float time;
    int nowtime;

    public TextMeshProUGUI text, timetext, timetext2, backtext;

    AsyncOperationHandle<Sprite[]> handle;

    [SerializeField]
    private Image[] icon;

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

                text.text = $"플레이어 탐색중 \n <size=8>해당 시간이 지나면 Ai가 매칭됩니다</size>";
            }
            else
            {
                Search = false;
                StartCoroutine(UIstart());
            }

        }

    }

    // Instantiate the loaded prefab on complete
    private void Handle_Completed(AsyncOperationHandle<Sprite[]> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Sprite[] sprites = handle.Result;
            float f;
            for (int i = 0; i < icon.Length; i++)
            {
                f = PlayerCheckManager.Instance.ListCheck(i);

                icon[i].sprite = sprites[Mathf.FloorToInt(f) - 1];
            }
        }
        else
        {
            Debug.LogError("Failed to load sprites.");
        }
    }
    private void OnDestroy()
    {
        if (handle.IsValid())
            Addressables.Release(handle);
    }

    IEnumerator UIstart()
    {
        backtext.gameObject.SetActive(false);
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

        text.text = $"참가자 모집 완료!";
        timetext.text = "\n파티 시작!";
        timetext2.text = "";

        while (PlayerCheckManager.Instance.ListCount() < 8)
        {
            PlayerCheckManager.Instance.newPlayer("Ai", Random.Range(1, 4));
            //PlayerCheckManager.Instance.PlayerRegion(Random.Range(1, 5));
        }
        PlayerCheckManager.Instance.clearCount();

        for (int i = 0; i < icon.Length; i++)
        {
            if (PlayerCheckManager.Instance.PlayerCheck() != -1 && PlayerCheckManager.Instance.PlayerCheck() == i)
            { 
                icon[i].transform.parent.GetChild(0).GetComponent<Image>().color = new Color32(100, 200, 100, 255);
            }
        }

        handle = Addressables.LoadAssetAsync<Sprite[]>("character_Icon");
        handle.Completed += Handle_Completed;

        elapsed = 0f;
       
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float f = Mathf.Clamp01(elapsed / duration);

            // EaseOutCubic: 빠르게 시작해서 점점 느려짐 (감속)
            float easedT = 1f - Mathf.Pow(1f - f, 1f);
            float y = Mathf.Lerp(parentRect.localScale.y, 1f, easedT);

            parentRect.localScale = new Vector3(parentRect.localScale.x, y, parentRect.localScale.z);

            yield return null;
        }
        parentRect.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("BattleScene");
    }

    public void back()
    {
        if (GamePlayManager.Instance != null)
        {
            string text = GamePlayManager.Instance.GetGameMode();
            GamePlayManager.Instance.settingReset();
            GamePlayManager.Instance.GameModeSetting(text);
        }

        if (PlayerCheckManager.Instance != null)
            PlayerCheckManager.Instance.clearlist();

        SceneManager.LoadScene("SelectScene");
    }

}
