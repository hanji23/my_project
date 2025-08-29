using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(onui());
    }

    IEnumerator onui()
    {
        for (byte colorA = 255; colorA > 0; colorA -= 15)
        {
            transform.GetChild(0).GetComponent<Image>().color = new Color32(0, 0, 0, colorA);
            yield return null;
        }
        transform.GetChild(0).GetComponent<Image>().color = new Color32(0, 0, 0, 0);

        transform.GetChild(0).gameObject.SetActive(false);
    }

}
