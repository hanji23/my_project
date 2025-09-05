using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class PlaySettingSc : MonoBehaviour
{
    [SerializeField]
    private GameObject GPM;

    public RectTransform PlayerUI1, PlayerUI2, EnemyUI1, EnemyUI2;
    public Image playerImage, enemyImage;
    public Canvas ready;
    int uicheck = 0;

    [SerializeField]
    private CharacterSOMaker so_P, so_E;

    [SerializeField]
    private int Player, Enemy;

    public static PlaySettingSc Instance = null;

    private void Awake()
    {
        if (GamePlayManager.Instance == null)
        {
            GameObject g = Instantiate(GPM);
            //g.name.Replace("(Clone)", "");
            g.name = g.name.Remove(g.name.Length - 7, 7);
        }

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        //테스트용 나중에 삭제
        //PlayerUI1.transform.parent.gameObject.SetActive(false);
        //EnemyUI1.transform.parent.gameObject.SetActive(false);
        //Destroy(GameObject.Find("Player1"));
        //Destroy(GameObject.Find("Player2"));
        //Destroy(GameObject.Find("AttackBall"));
        //ready.GetComponent<readySc>().StopAllCoroutines();
        //ready.gameObject.SetActive(false);
        //여기까지

        //if (GamePlayManager.Instance.Player_Typecheck() == 0)
        //    GamePlayManager.Instance.Player_TypeSetting(Random.Range(1, 4));

        //Player = GamePlayManager.Instance.Player_Typecheck();
        //so_P = GamePlayManager.Instance.SO_find("p");

        for (int i = 0; i < 8; i++)
        {
            if (PlayerCheckManager.Instance.PlayerCheck() != -1 && PlayerCheckManager.Instance.PlayerCheck() == i)
            {
                Player = PlayerCheckManager.Instance.PlayerNumCheck(i);
                so_P = PlayerCheckManager.Instance.PlayerSOCheck(i);
                break;
            }
        }

        //if (GamePlayManager.Instance.Enemy_Typecheck() == 0)
        //    GamePlayManager.Instance.Enemy_TypeSetting(Random.Range(1,4));

        //Enemy = GamePlayManager.Instance.Enemy_Typecheck();
        //so_E = GamePlayManager.Instance.SO_find("e");

        //int emeny = Random.Range(1, 8);

        int emeny = PlayerCheckManager.Instance.GetPlayerVs(Player - 1);

        Enemy = PlayerCheckManager.Instance.PlayerNumCheck(emeny - 1);
        so_E = PlayerCheckManager.Instance.PlayerSOCheck(emeny - 1);

        so_P.spwan_P(Player - 1);
        so_E.spwan_E(Enemy - 1);

        playerImage.sprite = so_P.get_Sprite("main");
        playerImage.rectTransform.sizeDelta = so_P.get_imageSet("size");
        playerImage.rectTransform.anchoredPosition = so_P.get_imageSet("position");

        enemyImage.sprite = so_E.get_Sprite("main");
        enemyImage.rectTransform.sizeDelta = so_E.get_imageSet("size");
        enemyImage.rectTransform.anchoredPosition = so_E.get_imageSet("position");
    }

    private void Start()
    {
        
    }

    public IEnumerator UIstart(RectTransform r, RectTransform r2)
    {
        r.transform.parent.gameObject.SetActive(true);

        r.anchoredPosition = new Vector2(r.anchoredPosition.x, 85);
        r2.anchoredPosition = new Vector2(r2.anchoredPosition.x, -50);
        yield return new WaitForSeconds(0.05f);
        r.anchoredPosition = new Vector2(r.anchoredPosition.x, 40);
        r2.anchoredPosition = new Vector2(r2.anchoredPosition.x, -25);

        yield return new WaitForSeconds(0.05f);
        r.anchoredPosition = new Vector2(r.anchoredPosition.x, 10);
        r2.anchoredPosition = new Vector2(r2.anchoredPosition.x, -10);
        yield return new WaitForSeconds(0.05f);
        r.anchoredPosition = new Vector2(r.anchoredPosition.x, -25);
        r2.anchoredPosition = new Vector2(r2.anchoredPosition.x, 0);
        yield return new WaitForSeconds(0.025f);
        r.anchoredPosition = new Vector2(r.anchoredPosition.x, -45);
        r2.anchoredPosition = new Vector2(r2.anchoredPosition.x, 15);
        yield return new WaitForSeconds(0.025f);
        r.anchoredPosition = new Vector2(r.anchoredPosition.x, -52);
        r2.anchoredPosition = new Vector2(r2.anchoredPosition.x, 22);
        yield return new WaitForSeconds(0.025f);
        r.anchoredPosition = new Vector2(r.anchoredPosition.x, -50);
        r2.anchoredPosition = new Vector2(r2.anchoredPosition.x, 20);
        yield return new WaitForSeconds(0.025f);
        r.anchoredPosition = new Vector2(r.anchoredPosition.x, -48);
        r2.anchoredPosition = new Vector2(r2.anchoredPosition.x, 18);
    }

    public void UIstart()
    {
        uicheck++;

        if (uicheck % 2  == 0)
        {
            StartCoroutine(UIstart(PlayerUI1, PlayerUI2));
            StartCoroutine(UIstart(EnemyUI1, EnemyUI2));
            
            UIready();
            
        }
        
    }

    public void UIready()
    {
        ready.gameObject.SetActive(true);
    }
}
