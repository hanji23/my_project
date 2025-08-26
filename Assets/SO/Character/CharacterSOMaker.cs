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
    private float region_type;
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

    public float getSo_attack_power()
    {
        return attack_power;
    }
    public float getSo_Stamina()
    {
        return Stamina;
    }
}
