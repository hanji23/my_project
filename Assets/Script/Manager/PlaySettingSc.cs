using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlaySettingSc : MonoBehaviour
{
    public RectTransform PlayerUI1, PlayerUI2, EnemyUI1, EnemyUI2;

    private void OnEnable()
    {
        StartCoroutine(start(0));
    }

    private void Start()
    {
        
    }

    public IEnumerator start(int type)
    {
        switch (type)
        {
            case 0:
                PlayerUI1.anchoredPosition = new Vector2(PlayerUI1.anchoredPosition.x, 85);
                PlayerUI2.anchoredPosition = new Vector2(PlayerUI2.anchoredPosition.x, -50);
                yield return new WaitForSeconds(0.05f);
                PlayerUI1.anchoredPosition = new Vector2(PlayerUI1.anchoredPosition.x, 40);
                PlayerUI2.anchoredPosition = new Vector2(PlayerUI2.anchoredPosition.x, -25);

                yield return new WaitForSeconds(0.05f);
                PlayerUI1.anchoredPosition = new Vector2(PlayerUI1.anchoredPosition.x, 10);
                PlayerUI2.anchoredPosition = new Vector2(PlayerUI2.anchoredPosition.x, -10);
                yield return new WaitForSeconds(0.05f);
                PlayerUI1.anchoredPosition = new Vector2(PlayerUI1.anchoredPosition.x, -25);
                PlayerUI2.anchoredPosition = new Vector2(PlayerUI2.anchoredPosition.x, 0);
                yield return new WaitForSeconds(0.025f);
                PlayerUI1.anchoredPosition = new Vector2(PlayerUI1.anchoredPosition.x, -45);
                PlayerUI2.anchoredPosition = new Vector2(PlayerUI2.anchoredPosition.x, 15);
                yield return new WaitForSeconds(0.025f);
                PlayerUI1.anchoredPosition = new Vector2(PlayerUI1.anchoredPosition.x, -52);
                PlayerUI2.anchoredPosition = new Vector2(PlayerUI2.anchoredPosition.x, 22);
                yield return new WaitForSeconds(0.025f);
                PlayerUI1.anchoredPosition = new Vector2(PlayerUI1.anchoredPosition.x, -50);
                PlayerUI2.anchoredPosition = new Vector2(PlayerUI2.anchoredPosition.x, 20);
                yield return new WaitForSeconds(0.025f);
                PlayerUI1.anchoredPosition = new Vector2(PlayerUI1.anchoredPosition.x, -48);
                PlayerUI2.anchoredPosition = new Vector2(PlayerUI2.anchoredPosition.x, 18);
                break;
            case 1:

                break;
        }
        
    }
}
