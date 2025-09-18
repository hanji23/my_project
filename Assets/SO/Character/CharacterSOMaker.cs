using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "캐릭터", menuName = "Character/캐릭터")]
public class CharacterSOMaker : ScriptableObject
{
    [SerializeField]
    private string character_name;
    public string Character_name
    {
        get { return character_name; }
    }

    [SerializeField]
    private int character_type;
    public int Character_Type
    {
        get { return character_type; }
    }

    [SerializeField]
    private float attack_power;
    public float Attack_Power
    {
        get { return attack_power; }
    }

    [SerializeField]
    private float stamina;
    public float Stamina
    {
        get { return stamina; }
    }

    [SerializeField]
    private float maxStamina;
    public float MaxStamina
    {
        get { return maxStamina; }
    }

    [SerializeField]
    private GameObject player;

    public Sprite mainI, loseI;
    [SerializeField]
    private Vector2 position;
    [SerializeField]
    private Vector2 size;

    [SerializeField]
    private Dictionary<int, Enum> skillList;

    public void spwan(int i, char c, int num, int x)
    {
        GameObject p = null;
        p = Instantiate(player, new Vector3(6 * x, 0, 0), Quaternion.identity);

        if (c == 'p')
        {
            p.transform.localScale = new Vector3(-1, 1, 1);
            p.transform.GetComponent<BoxCollider>().size = new Vector3(p.transform.GetComponent<BoxCollider>().size.x * -1, p.transform.GetComponent<BoxCollider>().size.y, p.transform.GetComponent<BoxCollider>().size.z);

            Camera.main.GetComponent<MainCamera>().player1 = p.transform;
        }
        else if(c == 'e')
        {
            Camera.main.GetComponent<MainCamera>().player2 = p.transform;
        }

        p.name = $"Player{num}";
        p.GetComponent<Player>().playerType = c;
        p.GetComponent<Player>().characterSO = this;
        p.GetComponent<Player>().playerIndex = i;
    }

    public Vector2 get_imageSet(string s)
    {
        if (s.Equals("size"))
            return size;
        else
            return position;
    }

    public Sprite get_Sprite(string s)
    {
        if (s.Equals("main"))
            return mainI;
        else
            return loseI;
    }

}
