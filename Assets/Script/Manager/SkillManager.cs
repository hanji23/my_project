using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance = null;

    [SerializeField]
    private int count = 0;

    private Dictionary<int, SkillSOMaker> skillList = new Dictionary<int, SkillSOMaker>();

    [SerializeField]
    private List<SkillSOMaker> allSkills = new List<SkillSOMaker>();
    public List<SkillSOMaker> AllSkills => allSkills;

    public void SkillAdd()
    {
        if (count == 0)
        {
            if (GamePlayManager.Instance != null)
            {
                for (int i = 1; i < PlayerManager.Instance.CharacterSOs[0].SkillList.Count; i++)
                {
                    count++;
                    skillList.Add(count, PlayerManager.Instance.CharacterSOs[0].SkillList[i]);
                }
            }

            if (PlayerManager.Instance != null)
            {
                List<PlayerManager.PlayerInfo> p = PlayerManager.Instance.allPlayers;
                int pindex = PlayerManager.Instance.GetMainPlayerIndex();

                for (int i = 1; i < p[pindex].characterSO.SkillList.Count; i++)
                {
                    count++;
                    skillList.Add(count, p[pindex].characterSO.SkillList[i]);
                }
            }
        }
        CapyList();
    }

    public void SkillRemove(SkillSOMaker item)
    {
        int removeKey = int.MinValue;
        foreach (var skill in skillList)
        {
            if (skill.Value == item)
            {
                removeKey = skill.Key;
                break;
            }
        }

        if (removeKey != int.MinValue)
        {
            skillList.Remove(removeKey);
            count--;
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
        //List<SkillSOMaker> allSkills = skillList.Values.ToList();

        allSkills.Clear();
        foreach (var skill in skillList)
        {
            allSkills.Add(skill.Value);
        }

        if (allSkills.Count < 4)
            for (int i = 0; i < 4; i++)
            {
                allSkills.Add(PlayerManager.Instance.CharacterSOs[0].SkillList[PlayerManager.Instance.CharacterSOs[0].SkillList.Count - 1]);
                count = allSkills.Count;
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
}
