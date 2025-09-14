using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private string type;
    [SerializeField]
    private int pnum;

    Transform attack0, attack1;
    Animator ani;
    Vector3 target = Vector3.zero;
    Vector3 vel = Vector3.zero;

    [SerializeField]
    private GameObject Canvas;


    [SerializeField]
    private int Win = 0;
    [SerializeField]
    private int RoundWin = 0;

    public CharacterSOMaker SO;

    bool startMove = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attack0 = transform.GetChild(0);
        attack1 = transform.GetChild(1);
        ani = GetComponent<Animator>();

        target = new Vector3(transform.position.x - (transform.position.x / Mathf.Abs(transform.position.x) * 3), transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(startMove)
        {
            //transform.position = Vector3.Lerp(gameObject.transform.position, target, 5 * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target, 1.5f * Time.deltaTime);
            ani.Play("run");

            if (transform.position.x == Mathf.Abs(3) * (transform.position.x / Mathf.Abs(transform.position.x)))
            {
                startMove = false;
                ani.Play("idle");

                PlaySetting.Instance.UIstart();

                Invoke("canvasCheck", 0.5f);
            }
                
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z) && ani.GetCurrentAnimatorStateInfo(0).IsName("idle")) //GetButtonDown 나중에 써보자
            {
                ani.Play($"attack{Random.Range(1, 4)}");
            }
            if (Input.GetKeyDown(KeyCode.X) && ani.GetCurrentAnimatorStateInfo(0).IsName("idle")) //GetButtonDown 나중에 써보자
            {
                ani.Play("attack4");
            }

            if (ani.GetCurrentAnimatorStateInfo(0).IsName("down"))
            {
                transform.position = Vector3.Lerp(gameObject.transform.position, target, 5 * Time.deltaTime);

            }
        }  
    }


    void BoxOn(int type)
    {
        if (!ani.GetCurrentAnimatorStateInfo(0).IsName("down"))
            transform.GetChild(type).gameObject.SetActive(true);
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
            target = new Vector3(transform.position.x + (transform.position.x / Mathf.Abs(transform.position.x) * 0.75f), transform.position.y, transform.position.z);

            StopAllCoroutines();
            ani.Play("down");
        }
    }

    public void setType(string s)
    {
        type = s;
    }

    public void canvasCheck()
    {
        if (type.Equals("p"))
            Canvas = GameObject.Find("PlayerCanvas");
        if (type.Equals("e"))
            Canvas = GameObject.Find("EnemyCanvas");

        Canvas.transform.GetChild(1).Find("VictoryText").GetComponent<TextMeshProUGUI>().text = $"WIN_[ {GetWin()} ]";
    }

    public GameObject Getcanvas()
    {
        return Canvas;
    }

    public void canvasWinCheck()
    {
        if(RoundWin < 3)
        {
            Canvas.transform.GetChild(1).transform.GetChild(1).transform.GetChild(RoundWin).GetComponent<Image>().color = Color.red;
            RoundWin++;
        }
        
    }

    public int GetWin()
    {
        return /*Win;*/PlayerCheckManager.Instance.GetPlayerWin(pnum);
    }
    public void SetWin()
    {
        PlayerCheckManager.Instance.SetPlayerWin(pnum);
        PlayerCheckManager.Instance.IsPlayerWin(pnum, true);
    }

    public int GetRoundWin()
    {
        return RoundWin;
    }

    public void Setpnum(int i)
    {
        pnum = i;
    }
}
