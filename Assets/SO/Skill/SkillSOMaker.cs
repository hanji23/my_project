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
    public enum SkillTier 
    { 
        Normal,
        Ultimate 
    }
    [SerializeField]
    private SkillTier _skillTier;

    public SkillTier skillTier
    {
        get { return _skillTier; }
    }

    [SerializeField]
    private int chracterNumber;
    public int ChracterNumber
    {
        get { return chracterNumber; }
    }

    [SerializeField]
    private int skillNumber;
    public int SkillNumber
    {
        get { return skillNumber; }
    }

    private enum AniType
    {
        single,           // 애니메이션 안에 애니메이션 하나
        multi,            // 애니메이션 안에 애니메이션 둘 이상
    };

    [SerializeField]
    private AniType aniType;

    [SerializeField]
    private float[] anistartFrame; //AniType이 multi 경우 사용할 애니메이션 시작 프레임 변수

    //public Animator animator;
    //public float startTime;
    //private float clipFrameRate;
    //private float clipLength;
    //AnimationClip clip = animator.runtimeAnimatorController.animationClips[0];
    //clipFrameRate = clip.frameRate;
    //    clipLength = clip.length;

    //    // 프레임 → 시간 변환
    //    startTime = anistartFrame / clipFrameRate;

    [SerializeField]
    private string aniName;

    [SerializeField]
    private string skillText;
    public string SkillText
    {
        get { return skillText; }
    }
}
