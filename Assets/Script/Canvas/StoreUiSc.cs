using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreUiSc : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
