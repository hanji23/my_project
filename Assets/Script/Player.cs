using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public char playerType;

    public int playerIndex;

    Transform attackPoint0, attackPoint1;
    Animator ani;
    Vector3 targetPosition = Vector3.zero;

    public GameObject uiCanvas;

    public int roundWins = 0;

    public CharacterSOMaker characterSO;

    bool isMovingToStart = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackPoint0 = transform.GetChild(0);
        attackPoint1 = transform.GetChild(1);
        ani = GetComponent<Animator>();

        targetPosition = new Vector3(transform.position.x - (transform.position.x / Mathf.Abs(transform.position.x) * 3), transform.position.y, transform.position.z);

        StartCoroutine(MoveToStartPosition());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMovingToStart)
        {
            if (Input.GetKeyDown(KeyCode.Z) && ani.GetCurrentAnimatorStateInfo(0).IsName("idle")) //GetButtonDown 나중에 써보자
            {
                //ani.Play($"attack{Random.Range(1, 4)}");
            }
            if (Input.GetKeyDown(KeyCode.X) && ani.GetCurrentAnimatorStateInfo(0).IsName("idle")) //GetButtonDown 나중에 써보자
            {
                //ani.Play("attack4");
            }

            if (ani.GetCurrentAnimatorStateInfo(0).IsName("down"))
            {
                transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition, 5 * Time.deltaTime);

            }
        }  
    }


    void ActivateAttackBox(int index)
    {
        if (!ani.GetCurrentAnimatorStateInfo(0).IsName("down"))
            transform.GetChild(index).gameObject.SetActive(true);
    }
    IEnumerator ReturnIdle()
    {
        yield return new WaitForSeconds(0.25f);
        if (!ani.GetCurrentAnimatorStateInfo(0).IsName("down"))
            ani.Play("idle");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball") && !ani.GetCurrentAnimatorStateInfo(0).IsName("down"))
        {
            targetPosition = new Vector3(transform.position.x + (transform.position.x / Mathf.Abs(transform.position.x) * 0.75f), transform.position.y, transform.position.z);

            StopAllCoroutines();
            ani.Play("down");
        }
    }

    public void UpdateCanvasUI()
    {
        uiCanvas = playerType.Equals('p') ? uiCanvas = GameObject.Find("PlayerCanvas") : uiCanvas = GameObject.Find("EnemyCanvas");

        uiCanvas.transform.GetChild(1).Find("VictoryText").GetComponent<TextMeshProUGUI>().text = $"WIN_[ {GetWinCount()} ]";

        if (playerType.Equals('p'))
            SkillIcon();

    }

    public void SkillIcon()
    {
        for (int i = 0; i < InventoryManager.Instance.skillList.Length; i++)
        {
            AsyncOperationHandle<Sprite[]> handle = Addressables.LoadAssetAsync<Sprite[]>($"Skill_Icon_{InventoryManager.Instance.skillList[i].ChracterNumber}");
            int localIndex = i;
            handle.Completed += (op) => Handle_IconCompleted(op, localIndex);
        }
    }

    private void Handle_IconCompleted(AsyncOperationHandle<Sprite[]> handle, int i)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Sprite[] sprites = handle.Result;

            uiCanvas.transform.GetChild(0).GetChild(i).GetChild(0).GetChild(1).GetComponent<Image>().sprite = sprites[InventoryManager.Instance.skillList[i].SkillNumber];

        }
        else
        {
            Debug.LogError("Failed to load sprites.");
        }
        Addressables.Release(handle);
    }

    public void UpdateRoundWinUI()
    {
        if(roundWins < 3)
        {
            uiCanvas.transform.GetChild(1).transform.GetChild(1).transform.GetChild(roundWins).GetComponent<Image>().color = Color.red;
            roundWins++;
        }
    }

    public int GetWinCount()
    {
        return PlayerManager.Instance.allPlayers[playerIndex].winCount;
    }
    public void AddWinCount()
    {
        PlayerManager.Instance.allPlayers[playerIndex].winCount++;
        PlayerManager.Instance.allPlayers[playerIndex].isWinner = true;
    }

    IEnumerator MoveToStartPosition()
    {
        while (true) 
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 1.5f * Time.deltaTime);
            ani.Play("run");

            if (transform.position.x == Mathf.Abs(3) * (transform.position.x / Mathf.Abs(transform.position.x)))
            {
                isMovingToStart = false;
                ani.Play("idle");

                PlaySetting.Instance.UIstart();

                Invoke("UpdateCanvasUI", 0.5f);
                break;
            }
            yield return null;
        }
        
    }

}
