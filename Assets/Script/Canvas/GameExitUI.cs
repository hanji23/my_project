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

    [SerializeField]
    private Image[] icon;

    private void Handle_Completed(AsyncOperationHandle<Sprite[]> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Sprite[] sprites = handle.Result;
            float f;
            for (int i = 0; i < icon.Length; i++)
            {
                f = PlayerManager.Instance.matchedPlayers[i].characterNumber;
                
                icon[i].sprite = sprites[Mathf.FloorToInt(f)];
                icon[i].transform.parent.transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>().text = 
                    $"{(PlayerManager.EPlayerType)PlayerManager.Instance.matchedPlayers[i].playerType}" +
                    $"{PlayerManager.Instance.matchedPlayers[i].playerNumber} \n " +
                    $"{PlayerManager.Instance.matchedPlayers[i].characterSO.Character_name}";
                icon[i].transform.parent.transform.parent.GetChild(3).GetComponent<TextMeshProUGUI>().text =
                    $"win [ {PlayerManager.Instance.matchedPlayers[i].winCount} ]";
            }
        }
        else
        {
            Debug.LogError("Failed to load sprites.");
        }
    }

    private void Start()
    {
        handle = Addressables.LoadAssetAsync<Sprite[]>("character_Icon");
        handle.Completed += Handle_Completed;
    }

    public void Exit()
    {
        SceneManager.LoadScene("StartScene");
        GamePlayManager.Instance.ResetGameSettings();
        PlayerManager.Instance.ClearAllPlayerData();
    }
}
