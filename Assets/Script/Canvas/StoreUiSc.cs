using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreUiSc : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI win, namet;

    [SerializeField]
    private Image I;

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
                    Player = PlayerCheckManager.Instance.PlayerNumCheck(i);
                    break;
                }
            }
            int emeny = PlayerCheckManager.Instance.GetPlayerVs(Player - 1);

            Enemy = PlayerCheckManager.Instance.PlayerNumCheck(emeny - 1);

            I.sprite = sprites[Mathf.FloorToInt(PlayerCheckManager.Instance.ListCheck(Enemy - 2))];

            namet.text =
                $"{PlayerCheckManager.Instance.GetPlayer(Enemy - 1)}{PlayerCheckManager.Instance.PlayerNumCheck(Enemy - 1)} \n {PlayerCheckManager.Instance.GetCharacter(Enemy - 1)}";
            win.text = $"win [ {PlayerCheckManager.Instance.ResultPlayerWinCheck(Enemy - 1)} ]";

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

    }

    // Update is called once per frame
    void Update()
    {
        
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

        GamePlayManager.Instance.SetRace();
        GamePlayManager.Instance.SetRound0();
        //GamePlayManager.Instance.Enemy_TypeSetting(Random.Range(1,4));
        SceneManager.LoadScene("BattleScene");
    }
}
