using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance = null;

    public SkillSOMaker[] itemList = new SkillSOMaker[10];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
