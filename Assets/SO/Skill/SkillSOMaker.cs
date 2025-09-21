using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "스킬", menuName = "Skill/스킬")]
public class SkillSOMaker : ScriptableObject
{
    [SerializeField]
    private string skillName;
    public string SkillName
    {
        get { return skillName; }
    }

    private enum SkillType 
    {
        None,           // 기본값, 공격 없음 상태
        Forward,        // 앞으로 던지는 공격
        UpTarget,       // 목표를 향해 포물선 공격
        UpPlayer,       // 플레이어 머리 위로 올리는 공격
    };

    [SerializeField]
    private SkillType skillType;

    [SerializeField]
    private string skillNumber;
    public string SkillNumber
    {
        get { return skillNumber; }
    }

    [SerializeField]
    private List<AnimationClip> ani;
    public AnimationClip Ani(int i)
    {
        return ani[i];
    }

    [SerializeField]
    private string skillText;
    public string SkillText
    {
        get { return skillText; }
    }
}
