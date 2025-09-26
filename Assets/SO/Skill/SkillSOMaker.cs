using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "��ų", menuName = "Skill/��ų")]
public class SkillSOMaker : ScriptableObject
{
    [SerializeField]
    private string skillName;
    public string SkillName
    {
        get { return skillName; }
    }

    public enum SkillType 
    {
        None,           // �⺻��, ���� ���� ����
        Forward,        // ������ ������ ����
        UpTarget,       // ��ǥ�� ���� ������ ����
        UpPlayer,       // �÷��̾� �Ӹ� ���� �ø��� ����
    };

    [SerializeField]
    private SkillType skillType;
    public SkillType _skillType
    {
        get { return skillType; }
    }

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

    public enum AniType
    {
        single,           // �ִϸ��̼� �ȿ� �ִϸ��̼� �ϳ�
        multi,            // �ִϸ��̼� �ȿ� �ִϸ��̼� �� �̻�
    };

    [SerializeField]
    private AniType _aniType;

    public AniType aniType
    {
        get { return _aniType; }
    }

    [SerializeField]
    private int[] anistartFrame; //AniType�� multi ��� ����� �ִϸ��̼� ���� ������ ����
    public int[] AnistartFrame
    {
        get { return anistartFrame; }
    }

    //public Animator animator;
    //public float startTime;
    //private float clipFrameRate;
    //private float clipLength;
    //AnimationClip clip = animator.runtimeAnimatorController.animationClips[0];
    //clipFrameRate = clip.frameRate;
    //    clipLength = clip.length;

    //    // ������ �� �ð� ��ȯ
    //    startTime = anistartFrame / clipFrameRate;

    //[SerializeField]
    //private string aniName;
    //public string AniName
    //{
    //    get { return aniName; }
    //}

    [SerializeField]
    private AnimationClip aniClip;
    public AnimationClip AniClip
{
        get { return aniClip; }
    }

    [SerializeField]
    private string skillText;
    public string SkillText
    {
        get { return skillText; }
    }
}
