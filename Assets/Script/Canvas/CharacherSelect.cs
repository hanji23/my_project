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


    private void Handle(string eventName, BaseEventData eventData)
    {
        //C# 패턴 매칭
        // 1. eventData is PointerEventData -> 객체가 PointerEventData인지 확인
        // 2. 맞으면 PointerData로 변환 해서 지역변수로 저장한다
        if (eventData is PointerEventData pointerData)
        {
            switch (eventName)
            {
                case "OnPointerEnter":
                    ani.Play(GetAniname());
                    i.rectTransform.sizeDelta = size;
                    i.rectTransform.anchoredPosition = position;
                    i.enabled = true;
                    t.text = Name;
                    back.color = new Color32(100, 200, 100, 255);
                    main.sprite = selectI;
                    break;
                case "OnPointerExit":
                    if (PlayerManager.Instance.allPlayers.Count == 0)
                    {
                        Setting();
                    }
                    break;
                case "OnPointerClick":
                    if (pointerData.button == PointerEventData.InputButton.Left)
                        PlayerManager.Instance.AddNewPlayer((int)(PlayerManager.EPlayerType)Enum.Parse(typeof(PlayerManager.EPlayerType), "Player"),player_Type);
                    else if (pointerData.button == PointerEventData.InputButton.Right)
                    {
                        GetComponent<Button>().onClick.Invoke();
                        PlayerManager.Instance.AddNewPlayer((int)(PlayerManager.EPlayerType)Enum.Parse(typeof(PlayerManager.EPlayerType), "Player"), player_Type + 1);
                    } 
                    break;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData) => Handle("OnPointerClick", eventData);

    public void OnPointerEnter(PointerEventData eventData) => Handle("OnPointerEnter", eventData);

    public void OnPointerExit(PointerEventData eventData) => Handle("OnPointerExit", eventData);

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
