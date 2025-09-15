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

    private void Handle_Completed(AsyncOperationHandle<Sprite[]> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Sprite[] sprites = handle.Result;

            for (int i = 0; i < 8; i++)
            {
                if (PlayerCheckManager.Instance.PlayerCheck() != -1 && PlayerCheckManager.Instance.PlayerCheck() == i)
                {
                    Player = PlayerCheckManager.Instance.Player[i].playerNum;
                    I_p.sprite = sprites[Mathf.FloorToInt(PlayerCheckManager.Instance.Player[Player - 1].characterNum)];

                    namet_p.text =
                $"{(PlayerCheckManager.EPlayerType)PlayerCheckManager.Instance.Player[Player - 1].PlayerType}" +
                $"{PlayerCheckManager.Instance.Player[Player - 1].playerNum} \n {PlayerCheckManager.Instance.Player[Player - 1].PlayerSo.Character_name}";
                    win_p.text = $"win [ {PlayerCheckManager.Instance.Player[Player - 1].win} ]";

                    break;
                }
            }
            int emeny = PlayerCheckManager.Instance.Player[Player - 1].VsplayerNum;

            Enemy = PlayerCheckManager.Instance.Player[emeny - 1].playerNum;

            I.sprite = sprites[Mathf.FloorToInt(PlayerCheckManager.Instance.Player[Enemy - 1].characterNum)];
            
            namet.text =
                $"{(PlayerCheckManager.EPlayerType)PlayerCheckManager.Instance.Player[Enemy - 1].PlayerType}" +
                $"{PlayerCheckManager.Instance.Player[Enemy - 1].playerNum} \n {PlayerCheckManager.Instance.Player[Enemy - 1].PlayerSo.Character_name}";
            win.text = $"win [ {PlayerCheckManager.Instance.Player[Enemy - 1].win} ]";

        }
        else
        {
            Debug.LogError("Failed to load sprites.");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        handle = Addressables.LoadAssetAsync<Sprite[]>("character_Icon");
        handle.Completed += Handle_Completed;

        roundt.text = $"Race {GamePlayManager.Instance.Race + 1}\n<size=10>다음상대</size>";
    }

    public void NextBattle()
    {
        StartCoroutine(offui());
    }

    public IEnumerator offui()
    {
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

        GamePlayManager.Instance.Race++;
        GamePlayManager.Instance.Round = 0;
        //GamePlayManager.Instance.Enemy_TypeSetting(Random.Range(1,4));
        SceneManager.LoadScene("BattleScene");
    }
}
