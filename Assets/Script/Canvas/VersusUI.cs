using System;
using System.Collections;
using System.Collections.Generic;
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
    const string spriteArrayAddress = "character_Icon";

    [SerializeField]
    private Image[] icon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var mode = (GamePlayManager.EGameMode)GamePlayManager.Instance.gameModeIndex;

        if (GamePlayManager.Instance != null && 
            (mode == GamePlayManager.EGameMode.Training || mode == GamePlayManager.EGameMode.AI))
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
            Array.Sort(sprites, (a, b) => { return Util.ExtractNumber(a.name).CompareTo(Util.ExtractNumber(b.name)); });
            for (int i = 0; i < icon.Length; i++) 
            {
                icon[i].sprite = sprites[PlayerManager.Instance.allPlayers[i].characterNumber];
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

        while (PlayerManager.Instance.allPlayers.Count < 8)
        {
            int r = UnityEngine.Random.Range(1, 7);

            PlayerManager.Instance.AddNewPlayer((int)PlayerManager.EPlayerType.AI, r);
        }
        PlayerManager.Instance.ResetPlayerCount();

        for (int i = 0; i < icon.Length; i++)
        {
            if (PlayerManager.Instance.GetMainPlayerIndex() != -1 && PlayerManager.Instance.GetMainPlayerIndex() == i)
            { 
                icon[i].transform.parent.GetChild(0).GetComponent<Image>().color = new Color32(100, 200, 100, 255);
            }
        }
        
        handle = Addressables.LoadAssetAsync<Sprite[]>(spriteArrayAddress);
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

        PlayerManager.Instance.CopyPlayerList();
        PlayerManager.Instance.ShuffleAndMatchPlayers();

        InventoryManager.Instance.itemList.Clear();

        List<PlayerManager.PlayerInfo> p = PlayerManager.Instance.allPlayers;
        int pindex = PlayerManager.Instance.GetMainPlayerIndex();
        InventoryManager.Instance.itemList.Add(p[pindex].characterSO.SkillList[0]);
        InventoryManager.Instance.itemList.Add(PlayerManager.Instance.CharacterSOs[0].SkillList[0]);

        for (int i = 0; i < 2; i++)
            InventoryManager.Instance.skillList[i] = InventoryManager.Instance.itemList[i];


        SceneManager.LoadScene("BattleScene");
    }

    public void back()
    {
        if (GamePlayManager.Instance != null)
        {
            int gameMode_now = GamePlayManager.Instance.gameModeIndex;
            GamePlayManager.Instance.ResetGameSettings();
            GamePlayManager.Instance.gameModeIndex = gameMode_now;
        }

        if (PlayerManager.Instance != null)
            PlayerManager.Instance.ClearAllPlayerData();

        SceneManager.LoadScene("SelectScene");
    }

}
