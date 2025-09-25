using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class SkillChangeUI : MonoBehaviour
{
    SkillSOMaker selectSO;

    public void OnSkillChangeUI(int listnumber)
    {
        if (listnumber >= InventoryManager.Instance.itemList.Count)
            return;

        selectSO = InventoryManager.Instance.itemList[listnumber];
        gameObject.SetActive(true);

        switch (InventoryManager.Instance.itemList[listnumber].skillTier)
        {
            case SkillSOMaker.SkillTier.Normal:
                transform.GetChild(1).gameObject.SetActive(true);
                NormalSkillIcon();
                break;
            case SkillSOMaker.SkillTier.Ultimate:
                transform.GetChild(2).gameObject.SetActive(true);
                UltimateSkillIcon();
                break;
        }
    }

    public void NormalSkillIcon()
    {
        for (int i = 0; i < 4; i++)
        {
            if (InventoryManager.Instance.skillList[i] == null)
                continue;
            AsyncOperationHandle<Sprite[]> handle = Addressables.LoadAssetAsync<Sprite[]>($"Skill_Icon_{InventoryManager.Instance.skillList[i].ChracterNumber}");
            int localIndex = i;
            handle.Completed += (op) => Handle_IconCompleted(op, localIndex);
        }
    }

    public void UltimateSkillIcon()
    {
        if (InventoryManager.Instance.skillList[4] != null)
        {
            AsyncOperationHandle<Sprite[]> handle = Addressables.LoadAssetAsync<Sprite[]>($"Skill_Icon_{InventoryManager.Instance.skillList[4].ChracterNumber}");
            handle.Completed += (op) => Handle_IconCompleted(op, 0);
        }

        AsyncOperationHandle<Sprite[]> handle2 = Addressables.LoadAssetAsync<Sprite[]>($"Skill_Icon_{selectSO.ChracterNumber}");
        handle2.Completed += (op) => Handle_IconCompleted(op, 1);

    }

    private void Handle_IconCompleted(AsyncOperationHandle<Sprite[]> handle, int i)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Sprite[] sprites = handle.Result;

            if (selectSO.skillTier == SkillSOMaker.SkillTier.Normal)
                transform.GetChild(1).GetChild(i).GetChild(0).GetChild(1).GetComponent<Image>().sprite = sprites[InventoryManager.Instance.skillList[i].SkillNumber];
            else
            {
                if (i == 0)
                    transform.GetChild(2).GetChild(i).GetChild(0).GetChild(1).GetComponent<Image>().sprite = sprites[InventoryManager.Instance.skillList[4].SkillNumber];
                else
                    transform.GetChild(2).GetChild(i).GetChild(0).GetChild(1).GetComponent<Image>().sprite = sprites[selectSO.SkillNumber];
            }
        }
        else
        {
            Debug.LogError("Failed to load sprites.");
        }
        Addressables.Release(handle);
    }

    public void IconSelect(int index)
    {
        for (int i = 0; i < InventoryManager.Instance.skillList.Length; i++)
        {
            if (InventoryManager.Instance.skillList[i] == selectSO)
            {
                InventoryManager.Instance.skillList[i] = null;
                if (selectSO.skillTier == SkillSOMaker.SkillTier.Normal)
                    transform.GetChild(1).GetChild(i).GetChild(0).GetChild(1).GetComponent<Image>().sprite = null;

                AsyncOperationHandle<Sprite[]> handle = Addressables.LoadAssetAsync<Sprite[]>("character_Icon");
                int localIndex = i;
                handle.Completed += (op) => Handle_IconNullCompleted(op, localIndex);

                //
            }
        }

        InventoryManager.Instance.skillList[index] = selectSO;
    }

    private void Handle_IconNullCompleted(AsyncOperationHandle<Sprite[]> handle, int i)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Sprite[] sprites = handle.Result;
            if (selectSO.skillTier == SkillSOMaker.SkillTier.Normal)
                transform.GetChild(1).GetChild(i).GetChild(0).GetChild(1).GetComponent<Image>().sprite = sprites[0];
        }
        else
        {
            Debug.LogError("Failed to load sprites.");
        }
        Addressables.Release(handle);
    }
}
