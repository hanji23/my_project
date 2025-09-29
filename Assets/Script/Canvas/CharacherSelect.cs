using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacherSelect : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    IPointerClickHandler     //포인터 관련
{

    public Image i;
    Image main, back;
    public Sprite mainI, selectI;
    public TextMeshProUGUI t;

    [SerializeField]
    private string Name;
    [SerializeField]
    private int player_Type;
    [SerializeField]
    private string aniName;
    [SerializeField]
    private Vector2 position;
    [SerializeField]
    private Vector2 size;

    Animator ani;

    private void HandleEnter(BaseEventData eventData)
    {
        ani.Play(GetAniname());
        i.rectTransform.sizeDelta = size;
        i.rectTransform.anchoredPosition = position;
        i.enabled = true;
        t.text = Name;
        back.color = new Color32(100, 200, 100, 255);
        main.sprite = selectI;
    }
    private void HandleExit(BaseEventData eventData)
    {
        if (PlayerManager.Instance.allPlayers.Count == 0)
        {
            Setting();
        }
    }
    private void HandleClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            PlayerManager.Instance.AddNewPlayer((int)PlayerManager.EPlayerType.Player, player_Type);
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            GetComponent<Button>().onClick.Invoke();
            PlayerManager.Instance.AddNewPlayer((int)PlayerManager.EPlayerType.Player, player_Type + 1);
        }
    }

    public void OnPointerClick(PointerEventData eventData) => HandleClick(eventData);

    public void OnPointerEnter(PointerEventData eventData) => HandleEnter(eventData);

    public void OnPointerExit(PointerEventData eventData) => HandleExit(eventData);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ani = i.GetComponent<Animator>();
        back = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        main = transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();
    }

    public string GetAniname()
    {
        return aniName;
    }

    public void Setting()
    {
        ani.Play("None");
        i.enabled = false;
        t.text = "";
        back.color = new Color32(0, 0, 0, 130);
        main.sprite = mainI;
    }
}
