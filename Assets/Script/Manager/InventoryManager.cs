using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance = null;

    public List<SkillSOMaker> itemList = new List<SkillSOMaker>();

    public SkillSOMaker[] skillList = new SkillSOMaker[5]; // 0~3 ÀÏ¹Ý ±â¼ú, 4 ±Ã±Ø±â

    int[] intList = new int[5];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
