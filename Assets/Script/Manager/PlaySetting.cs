using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class PlaySetting : MonoBehaviour
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

    public static PlaySetting Instance = null;

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
        for (int i = 0; i < 8; i++)
        {
            if (PlayerCheckManager.Instance.PlayerCheck() != -1 && PlayerCheckManager.Instance.PlayerCheck() == i)
            {
                Player = PlayerCheckManager.Instance.Player[i].playerNum;
                so_P = PlayerCheckManager.Instance.Player[i].PlayerSo;
                break;
            }
        }

        int emeny = PlayerCheckManager.Instance.Player[Player - 1].VsplayerNum;

        Enemy = PlayerCheckManager.Instance.Player[emeny - 1].playerNum;
        so_E = PlayerCheckManager.Instance.Player[emeny - 1].PlayerSo;

        so_P.spwan(Player - 1, 'p', 1, -1);
        so_E.spwan(Enemy - 1, 'e', 2, 1);

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
