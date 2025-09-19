using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "스킬", menuName = "Skill/스킬")]
public class SkillSOMaker : ScriptableObject
{
    [SerializeField]
    private string skill_name;
    public string Skill_name
    {
        get { return skill_name; }
    }

    [SerializeField]
    private List<AnimationClip> ani;
    public AnimationClip Ani(int i)
    {
        return ani[i];
    }

    [SerializeField]
    private string skill_text;
    public string Skill_text
    {
        get { return skill_text; }
    }
}
