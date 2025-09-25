using System;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class SkillSelectUI : MonoBehaviour
{
    [SerializeField]
    private StoreUI inventoryCanvas;

    [SerializeField]
    private SkillSOMaker[] skillList = new SkillSOMaker[4];

    [SerializeField]
    private TextMeshProUGUI[] skilltext;

    [SerializeField]
    private Image[] skillImage;

    int reRollCount = 0;
    int startIndex = 0;

    bool[] isSell = new bool[4];

    void Start()
    {
        SkillManager.Instance.SkillAdd();
        SellSkillList();
    }

    void SellSkillList()
    {
        for (int i = 0; i < 4; i++)
        {
            int idx = (startIndex + i) % SkillManager.Instance.AllSkills.Count;

            skillList[i] = SkillManager.Instance.AllSkills[idx];

            AsyncOperationHandle<Sprite[]> handle = Addressables.LoadAssetAsync<Sprite[]>($"Skill_Icon_{skillList[i].ChracterNumber}");

            int localIndex = i;
            handle.Completed += (op) => Handle_Completed(op, localIndex);

            skilltext[i].text = $"{skillList[i].SkillName}\n<size=6>{skillList[i].SkillText}</size>";
        }

    }

    public void Reroll()
    {
        reRollCount++;

        if(SkillManager.Instance.AllSkills.Count > 0)
        {
            startIndex = (reRollCount * 4) % SkillManager.Instance.AllSkills.Count;

            int totalSkills = SkillManager.Instance.AllSkills.Count;
            int totalPages = Mathf.CeilToInt(totalSkills / 4f); // 4개씩 나누기

            if (Array.Exists(isSell, b => b) || reRollCount % totalPages == 0)
            {
                reRollCount = 0;
                startIndex = 0;
            }
        }

        SkillManager.Instance.CapyList();

        SellSkillList();
        isSellReset();
    }

    public void SelectButton(int i)
    {
        if (isSell[i] == false)
        {
            if (InventoryManager.Instance.itemList.Count < 10)
            {
                InventoryManager.Instance.itemList.Add(skillList[i]);
                inventoryCanvas.Icon();
                skilltext[i].text = "구매 완료!";
                isSell[i] = true;
                SkillManager.Instance.SkillRemove(skillList[i]);
            }
            else
                Debug.Log("꽉참");
        }
    }

    void isSellReset()
    {
        for (int i = 0; i < isSell.Length; i++)
        {
            isSell[i] = false;
        }
    }

    private void Handle_Completed(AsyncOperationHandle<Sprite[]> handle, int i)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Sprite[] sprites = handle.Result;

            skillImage[i].sprite = sprites[skillList[i].SkillNumber];

        }
        else
        {
            Debug.LogError("Failed to load sprites.");
        }
        Addressables.Release(handle);
    }
}