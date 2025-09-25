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

    private enum SkillType 
    {
        None,           // �⺻��, ���� ���� ����
        Forward,        // ������ ������ ����
        UpTarget,       // ��ǥ�� ���� ������ ����
        UpPlayer,       // �÷��̾� �Ӹ� ���� �ø��� ����
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
        single,           // �ִϸ��̼� �ȿ� �ִϸ��̼� �ϳ�
        multi,            // �ִϸ��̼� �ȿ� �ִϸ��̼� �� �̻�
    };

    [SerializeField]
    private AniType aniType;

    [SerializeField]
    private float[] anistartFrame; //AniType�� multi ��� ����� �ִϸ��̼� ���� ������ ����

    //public Animator animator;
    //public float startTime;
    //private float clipFrameRate;
    //private float clipLength;
    //AnimationClip clip = animator.runtimeAnimatorController.animationClips[0];
    //clipFrameRate = clip.frameRate;
    //    clipLength = clip.length;

    //    // ������ �� �ð� ��ȯ
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
