using TMPro;
using UnityEngine;

public class SkillSelectUI : MonoBehaviour
{
    [SerializeField]
    private SkillSOMaker[] skillList = new SkillSOMaker[4];

    [SerializeField]
    private GameObject[] skillButton;

    [SerializeField]
    private TextMeshProUGUI[] skilltext;


    int reRollCount = 0;
    int startIndex = 0;

    void Start()
    {
        SkillManager.Instance.SkillAdd();

        SellSkillList();

    }

    void SellSkillList()
    {
        
        if (SkillManager.Instance.AllSkills.Count < 4)
            for (int i = 0; i < 4; i++)
                SkillManager.Instance.AllSkills.Add(PlayerManager.Instance.CharacterSOs[0].SkillList[PlayerManager.Instance.CharacterSOs[0].SkillList.Count - 1]);

        for (int i = 0; i < 4; i++)
        {
            int idx = (startIndex + i) % SkillManager.Instance.AllSkills.Count;

            skillList[i] = SkillManager.Instance.AllSkills[idx];
            skillButton[i].GetComponent<SkillButton>().item = skillList[i];
            skilltext[i].text = $"{SkillManager.Instance.AllSkills[idx].SkillName}\n<size=6>{SkillManager.Instance.AllSkills[idx].SkillText}</size>";
        }
    }

    public void Reroll()
    {
        reRollCount++;

        startIndex = (reRollCount * 4) % SkillManager.Instance.AllSkills.Count;

        int totalSkills = SkillManager.Instance.AllSkills.Count;
        int totalPages = Mathf.CeilToInt(totalSkills / 4f); // 4개씩 나누기

        if (reRollCount % totalPages == 0)
            SkillManager.Instance.CapyList();
        SellSkillList();
    }
}
