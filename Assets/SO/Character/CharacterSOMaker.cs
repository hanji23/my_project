using UnityEditor.Animations;
using UnityEngine;
[CreateAssetMenu(fileName = "ĳ����", menuName = "Character/ĳ����")]
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
    private GameObject player;

    public float getSo_attack_power()
    {
        return attack_power;
    }
}
