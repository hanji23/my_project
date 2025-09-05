using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameExitSc : MonoBehaviour
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
                f = PlayerCheckManager.Instance.ResultListCheck(i);

                icon[i].sprite = sprites[Mathf.FloorToInt(f) - 1];
                icon[i].transform.parent.transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>().text = 
                    $"{PlayerCheckManager.Instance.GetResultPlayer(i)}{PlayerCheckManager.Instance.ResultPlayerNumCheck(i)} \n {PlayerCheckManager.Instance.GetResultCharacter(i)}";
                icon[i].transform.parent.transform.parent.GetChild(3).GetComponent<TextMeshProUGUI>().text =
                    $"win [ {PlayerCheckManager.Instance.ResultPlayerWinCheck(i)} ]";
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
        GamePlayManager.Instance.settingReset();
        PlayerCheckManager.Instance.clearlist();
    }
}
