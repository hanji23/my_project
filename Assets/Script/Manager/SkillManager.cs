using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    int count = 0;

    Dictionary<int, SkillSOMaker> skillList = new Dictionary<int, SkillSOMaker>();

    

    private void Start()
    {
        SkillAdd();
    }

    public void SkillAdd()
    {
        if(GamePlayManager.Instance != null && PlayerManager.Instance != null)
        {
            for (int i = 0; i < GamePlayManager.Instance.CharacterSOs[0].SkillList.Count; i++)
            {
                count++;
                skillList.Add(count, GamePlayManager.Instance.CharacterSOs[0].SkillList[i]);
            }

            List<PlayerManager.PlayerInfo> p = PlayerManager.Instance.allPlayers;
            int pindex = PlayerManager.Instance.GetMainPlayerIndex();

            for (int i = 0; i < p[pindex].characterSO.SkillList.Count; i++)
            {
                count++;
                skillList.Add(count, p[pindex].characterSO.SkillList[i]);
            }
        }
    }

    public void PrintAllSkills()
    {
        Debug.Log($"스킬 전체 갯수 : {skillList.Count}");

        foreach (var kvp in skillList)
        {
            Debug.Log($"Key: {kvp.Key}, Skill: {kvp.Value.name}");
        }

    }
}
