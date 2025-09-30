using System;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameExitUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    AsyncOperationHandle<Sprite[]> handle;
    const string spriteArrayAddress = "character_Icon";

    [SerializeField]
    private Image[] icon;

    private void Handle_Completed(AsyncOperationHandle<Sprite[]> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Sprite[] sprites = handle.Result;
            Array.Sort(sprites, (a, b) => { return Util.ExtractNumber(a.name).CompareTo(Util.ExtractNumber(b.name)); });
            for (int i = 0; i < icon.Length; i++)
            {
                icon[i].sprite = sprites[PlayerManager.Instance.matchedPlayers[i].characterNumber];
                icon[i].transform.parent.transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>().text = 
                    $"{(PlayerManager.EPlayerType)PlayerManager.Instance.matchedPlayers[i].playerType}" +
                    $"{PlayerManager.Instance.matchedPlayers[i].playerNumber} \n " +
                    $"{PlayerManager.Instance.matchedPlayers[i].characterSO.Character_name}";
                icon[i].transform.parent.transform.parent.GetChild(3).GetComponent<TextMeshProUGUI>().text =
                    $"win [ {PlayerManager.Instance.matchedPlayers[i].winCount} ]";
            }
            Addressables.Release(handle);
        }
        else
        {
            Debug.LogError("Failed to load sprites.");
        }
    }

    private void Start()
    {
        handle = Addressables.LoadAssetAsync<Sprite[]>(spriteArrayAddress);
        handle.Completed += Handle_Completed;
    }

    public void Exit()
    {
        SceneManager.LoadScene("StartScene");
        GamePlayManager.Instance.ResetGameSettings();
        PlayerManager.Instance.ClearAllPlayerData();
        SkillManager.Instance.ClearAllPlayerData();
        InventoryManager.Instance.ClearAllPlayerData();
    }
}
