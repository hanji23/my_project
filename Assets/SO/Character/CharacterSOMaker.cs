using UnityEditor.Animations;
using UnityEngine;
[CreateAssetMenu(fileName = "캐릭터", menuName = "Character/캐릭터")]
public class CharacterSOMaker : ScriptableObject
{
    [SerializeField]
    private string Character_name;
    [SerializeField]
    private float Character_type;
    [SerializeField]
    private float attack_power;
    [SerializeField]
    private float Stamina;
    [SerializeField]
    private float MaxStamina;
    [SerializeField]
    private GameObject player;

    public Sprite mainI, loseI;
    [SerializeField]
    private Vector2 position;
    [SerializeField]
    private Vector2 size;

    public float getSo_Character_type()
    {
        return Character_type;
    }
    public float getSo_attack_power()
    {
        return attack_power;
    }
    public float getSo_Stamina()
    {
        return Stamina;
    }

    public void spwan_P()
    {
        GameObject p = Instantiate(player, new Vector3(-6, 0 , 0), Quaternion.identity);
        p.transform.localScale = new Vector3(-1, 1, 1);
        p.transform.GetComponent<BoxCollider>().size = new Vector3(p.transform.GetComponent<BoxCollider>().size.x * -1, p.transform.GetComponent<BoxCollider>().size.y, p.transform.GetComponent<BoxCollider>().size.z);
        p.name = "Player1";
        p.GetComponent<Player>().setType("p");
        p.GetComponent<Player>().SO = this;
        Camera.main.GetComponent<CameraSc>().player1 = p.transform;
    }

    public void spwan_E()
    {
        GameObject e = Instantiate(player, new Vector3(6, 0, 0), Quaternion.identity);
        e.name = "Player2";
        e.GetComponent<Player>().setType("e");
        e.GetComponent<Player>().SO = this;
        Camera.main.GetComponent<CameraSc>().player2 = e.transform;
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
