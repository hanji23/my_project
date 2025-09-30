using System;
using System.Collections;
using System.Linq;
using TMPro;
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

    AsyncOperationHandle<Sprite[]> handle;
    const string spriteArrayAddress = "Skill_Icon_";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackPoint0 = transform.GetChild(0);
        attackPoint1 = transform.GetChild(1);
        ani = GetComponent<Animator>();

        targetPosition = new Vector3(transform.position.x - (transform.position.x / Mathf.Abs(transform.position.x) * 3), transform.position.y, transform.position.z);

        StartCoroutine(MoveToStartPosition());

        if (PlayerManager.Instance.allPlayers[playerIndex].playerType == (int)PlayerManager.EPlayerType.AI)
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMovingToStart)
        {
            if (playerType.Equals('p')) 
            {
                if (Input.GetKeyDown(KeyCode.Z) && ani.GetCurrentAnimatorStateInfo(0).IsName("idle")) //GetButtonDown 나중에 써보자
                {
                    Onbutton(0);
                }
                if (Input.GetKeyDown(KeyCode.X) && ani.GetCurrentAnimatorStateInfo(0).IsName("idle")) //GetButtonDown 나중에 써보자
                {
                    Onbutton(1);
                }
                if (Input.GetKeyDown(KeyCode.C) && ani.GetCurrentAnimatorStateInfo(0).IsName("idle")) //GetButtonDown 나중에 써보자
                {
                    Onbutton(2);
                }
                if (Input.GetKeyDown(KeyCode.V) && ani.GetCurrentAnimatorStateInfo(0).IsName("idle")) //GetButtonDown 나중에 써보자
                {
                    Onbutton(3);
                }
                if (Input.GetKeyDown(KeyCode.Space) && ani.GetCurrentAnimatorStateInfo(0).IsName("idle")) //GetButtonDown 나중에 써보자
                {
                    Onbutton(4);
                }
            }
            

            if (ani.GetCurrentAnimatorStateInfo(0).IsName("down"))
            {
                transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition, 5 * Time.deltaTime);

            }
        }  
    }

    void Onbutton(int i)
    {
        SkillSOMaker SO = InventoryManager.Instance.skillList[i];
        AnimationClip targetClip = null;
        if (SO.AnistartFrame.Length > 0)
            targetClip = ani.runtimeAnimatorController.animationClips.FirstOrDefault(c => c.name == SO.AniClip.name);
        else
        {
            ani.Play("jump");
            return;
        }

        if (targetClip != null)
        {
            float clipFrameRate = targetClip.frameRate;
            int r;

            if (SO.aniType == SkillSOMaker.AniType.multi)
                r = SO.AnistartFrame[UnityEngine.Random.Range(0, SO.AnistartFrame.Length)];
            else
                r = SO.AnistartFrame[0];

            float startTime = r / clipFrameRate;
            ani.Play(SO.AniClip.name, 0, startTime / targetClip.length); // normalizedTime 사용
        }
        
    }

    void ActivateAttackBox(int index)
    {
        if (!ani.GetCurrentAnimatorStateInfo(0).IsName("down"))
            transform.GetChild(index).gameObject.SetActive(true);
    }
    IEnumerator ReturnIdle()
    {
        yield return new WaitForSeconds(0.1f);
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
            if (InventoryManager.Instance.skillList[i] == null)
                continue;
            handle = Addressables.LoadAssetAsync<Sprite[]>($"{spriteArrayAddress}{InventoryManager.Instance.skillList[i].ChracterNumber}");
            int localIndex = i;
            handle.Completed += (op) => Handle_IconCompleted(op, localIndex);
        }
        Addressables.Release(handle);
    }

    private void Handle_IconCompleted(AsyncOperationHandle<Sprite[]> handle, int i)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Sprite[] sprites = handle.Result;
            Array.Sort(sprites, (a, b) => { return Util.ExtractNumber(a.name).CompareTo(Util.ExtractNumber(b.name)); });
            uiCanvas.transform.GetChild(0).GetChild(i).GetChild(0).GetChild(1).GetComponent<Image>().sprite = sprites[InventoryManager.Instance.skillList[i].SkillNumber];

        }
        else
        {
            Debug.LogError("Failed to load sprites.");
        }
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

    public void AIAttack()
    {
        SkillSOMaker SO = characterSO.SkillList[UnityEngine.Random.Range(0, characterSO.SkillList.Count)];
        AnimationClip targetClip = null;
        if (SO.AnistartFrame.Length > 0)
            targetClip = ani.runtimeAnimatorController.animationClips.FirstOrDefault(c => c.name == SO.AniClip.name);
        if (targetClip != null)
        {
            float clipFrameRate = targetClip.frameRate;
            int r;

            if (SO.aniType == SkillSOMaker.AniType.multi)
                r = SO.AnistartFrame[UnityEngine.Random.Range(0, SO.AnistartFrame.Length)];
            else
                r = SO.AnistartFrame[0];

            float startTime = r / clipFrameRate;
            ani.Play(SO.AniClip.name, 0, startTime / targetClip.length); // normalizedTime 사용
        }
    }
}
