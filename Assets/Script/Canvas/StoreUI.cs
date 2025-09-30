using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI win, namet, win_p, namet_p, roundt;

    [SerializeField]
    private Image I, I_p;

    [SerializeField]
    private int Player, Enemy;

    AsyncOperationHandle<Sprite[]> handle;
    const string spriteArrayAddress1 = "character_Icon";
    const string spriteArrayAddress2 = "Skill_Icon_";

    [SerializeField]
    private Image[] inventoryIcon;

    private void Handle_Completed(AsyncOperationHandle<Sprite[]> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Sprite[] sprites = handle.Result;
            Array.Sort(sprites, (a, b) => { return Util.ExtractNumber(a.name).CompareTo(Util.ExtractNumber(b.name)); });
            for (int i = 0; i < 8; i++)
            {
                if (PlayerManager.Instance.GetMainPlayerIndex() != -1 && PlayerManager.Instance.GetMainPlayerIndex() == i)
                {
                    Player = PlayerManager.Instance.allPlayers[i].playerNumber;
                    I_p.sprite = sprites[Mathf.FloorToInt(PlayerManager.Instance.allPlayers[Player - 1].characterNumber)];

                    namet_p.text =
                $"{(PlayerManager.EPlayerType)PlayerManager.Instance.allPlayers[Player - 1].playerType}" +
                $"{PlayerManager.Instance.allPlayers[Player - 1].playerNumber} \n {PlayerManager.Instance.allPlayers[Player - 1].characterSO.Character_name}";
                    win_p.text = $"win [ {PlayerManager.Instance.allPlayers[Player - 1].winCount} ]";

                    break;
                }
            }
            int emeny = PlayerManager.Instance.allPlayers[Player - 1].opponentPlayerNumber;

            Enemy = PlayerManager.Instance.allPlayers[emeny - 1].playerNumber;

            I.sprite = sprites[Mathf.FloorToInt(PlayerManager.Instance.allPlayers[Enemy - 1].characterNumber)];
            
            namet.text =
                $"{(PlayerManager.EPlayerType)PlayerManager.Instance.allPlayers[Enemy - 1].playerType}" +
                $"{PlayerManager.Instance.allPlayers[Enemy - 1].playerNumber} \n {PlayerManager.Instance.allPlayers[Enemy - 1].characterSO.Character_name}";
            win.text = $"win [ {PlayerManager.Instance.allPlayers[Enemy - 1].winCount} ]";

        }
        else
        {
            Debug.LogError("Failed to load sprites.");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        handle = Addressables.LoadAssetAsync<Sprite[]>(spriteArrayAddress1);
        handle.Completed += Handle_Completed;

        roundt.text = $"Race {GamePlayManager.Instance.currentRace + 1}\n<size=10>다음상대</size>";
        Icon();
    }

    public void NextBattle()
    {
        StartCoroutine(offui());
    }

    public IEnumerator offui()
    {
        if (handle.IsValid())
            Addressables.Release(handle);

        GameObject g = GameObject.Find("start").transform.GetChild(0).gameObject;
        g.SetActive(true);
        Image su = g.GetComponent<Image>();

        for (byte colorA = 0; colorA < 255; colorA += 15)
        {
            su.color = new Color32(0, 0, 0, colorA);
            yield return null;
        }
        su.color = new Color32(0, 0, 0, 255);
        yield return new WaitForSeconds(0.5f);

        GamePlayManager.Instance.currentRace++;
        GamePlayManager.Instance.currentRound = 0;
        //GamePlayManager.Instance.Enemy_TypeSetting(Random.Range(1,4));
        SceneManager.LoadScene("BattleScene");
    }

    public void Icon()
    {
        for (int i = 0; i < InventoryManager.Instance.itemList.Count; i++)
        {
            AsyncOperationHandle<Sprite[]> handle = Addressables.LoadAssetAsync<Sprite[]>($"{spriteArrayAddress2}{InventoryManager.Instance.itemList[i].ChracterNumber}");
            int localIndex = i;
            handle.Completed += (op) => Handle_IconCompleted(op, localIndex);
        }
    }

    private void Handle_IconCompleted(AsyncOperationHandle<Sprite[]> handle, int i)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Sprite[] sprites = handle.Result;
            Array.Sort(sprites, (a, b) => { return Util.ExtractNumber(a.name).CompareTo(Util.ExtractNumber(b.name)); });
            inventoryIcon[i].sprite = sprites[InventoryManager.Instance.itemList[i].SkillNumber];

        }
        else
        {
            Debug.LogError("Failed to load sprites.");
        }
    }
}
