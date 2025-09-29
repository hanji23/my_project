using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance = null;

    private List<SkillSOMaker> skillList = new List<SkillSOMaker>();

    [SerializeField]
    private List<SkillSOMaker> allSkills = new List<SkillSOMaker>();
    public List<SkillSOMaker> AllSkills => allSkills;

    public void SkillAdd()
    {
        if (skillList.Count == 0)
        {
            if (GamePlayManager.Instance != null)
            {
                for (int i = 1; i < PlayerManager.Instance.CharacterSOs[0].SkillList.Count; i++)
                {
                    skillList.Add(PlayerManager.Instance.CharacterSOs[0].SkillList[i]);
                }
            }

            if (PlayerManager.Instance != null)
            {
                List<PlayerManager.PlayerInfo> p = PlayerManager.Instance.allPlayers;
                int pindex = PlayerManager.Instance.GetMainPlayerIndex();

                for (int i = 1; i < p[pindex].characterSO.SkillList.Count; i++)
                {
                    skillList.Add(p[pindex].characterSO.SkillList[i]);
                }
            }
        }
        CapyList();
    }

    public void SkillRemove(SkillSOMaker item)
    {
        foreach (var skill in skillList)
        {
            if (skill == item)
            {
                skillList.Remove(skill);
                break;
            }
        }

        foreach (var skill in allSkills)
        {
            if (skill == item)
            {
                allSkills.Remove(skill);
                break;
            }
        }
    }

    public void CapyList(bool isShuffle = true)
    {
        allSkills.Clear();
        foreach (var skill in skillList)
        {
            allSkills.Add(skill);
        }

        if (allSkills.Count < 4)
            for (int i = 0; i < 4; i++)
            {
                allSkills.Add(PlayerManager.Instance.CharacterSOs[0].SkillList[PlayerManager.Instance.CharacterSOs[0].SkillList.Count - 1]);
            }
                
        if(isShuffle)
            Shuffle(allSkills);
    }


    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void ClearAllPlayerData()
    {
        skillList.Clear();
        allSkills.Clear();
    }
}
