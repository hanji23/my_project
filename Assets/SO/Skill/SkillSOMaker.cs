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
