using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BallSc : MonoBehaviour
{
    Rigidbody rb;
    Transform p1, p2;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private string AttackType;

    [SerializeField]
    private float attackspeed;

    bool gameEnd;

    float time;
    int nowtime;
    string downplayer;

    TextMeshProUGUI t1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        p1 = GameObject.Find("Player1").transform;
        p2 = GameObject.Find("Player2").transform;

        t1 = GameObject.Find("Canvas").transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        BallReset();
    }

    private void FixedUpdate()
    {
        switch (AttackType)
        {
            case "forward":
  
                break;
            case "up_target":
                
                break;
            case "up_player":

                break;
            case "PlayerHit":
                
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (AttackType.Equals("PlayerHit"))
        {
            if (time <= 0)
            {
                attackStart();
                BallReset();
            }
            else
            {
                time -= Time.deltaTime;
                nowtime = Mathf.CeilToInt(time);
                t1.text = $"{downplayer} 다운!\n재시작 시간 : {nowtime}";
            }
            
        }
    }
    void OnTriggerEnter(Collider hit)
    {
        if (!gameEnd)
        {
            if (hit.CompareTag("attack_forward"))//전방 발사
            {
                rb.AddForce(0, 0, 0);
                attackspeed += 5;
                attackStart();
                AttackType = "forward";
                targetChange(hit);
                rb.useGravity = false;
                if (transform.position.y > 1f)
                    rb.AddForce(target.position.x / Mathf.Abs(target.position.x) * (150 + attackspeed), -10f, 0);
                else
                    rb.AddForce(target.position.x / Mathf.Abs(target.position.x) * (150 + attackspeed), 0, 0);
            }
            else if (hit.CompareTag("attack_up_target"))// 포물선 발사
            {
                rb.AddForce(0, 0, 0);
                attackspeed = 0;
                attackStart();
                AttackType = "up_target";
                targetChange(hit);
                rb.useGravity = true;
                //rb.linearVelocity = new Vector3(target.position.x / Mathf.Abs(target.position.x), 0, 0) * 5f + Vector3.up * 5;
            }
            else if (hit.CompareTag("attack_up_player"))// 자신 머리위로 발사
            {
                attackStart();
                rb.useGravity = true;
                AttackType = "up_player";
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && !gameEnd)
        {
            gameEnd = true;
            AttackType = "PlayerHit";
            rb.linearDamping = 0;
            rb.AddForce(0, 0, 0);
            collision.collider.GetComponent<Animator>().StopPlayback();
            collision.collider.GetComponent<Animator>().Play("die");
            collision.collider.GetComponent<Player>().StopAllCoroutines();

            rb.useGravity = true;
            ContactPoint cp = collision.GetContact(0);
            Vector3 dir = /*transform.position*/ new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z - 1f) - cp.point; // 접촉지점에서부터 탄위치 의 방향 https://dallcom-forever2620.tistory.com/42
            rb.AddForce((dir).normalized * 300f);
            downplayer = collision.collider.name;
        }

    }

    void targetChange(Collider hit)
    {
        //target = target == p1? p2: p1;
        target = hit.transform.parent == p1?  p2 : p1;
    }

    void attackStart()
    {
        rb.isKinematic = true;
        rb.isKinematic = false;
        rb.linearDamping = 0;
    }

    void BallReset()
    {
        if (Random.Range(0, 2) == 0)
        {
            transform.position = new Vector3(p1.position.x + 0.375f, p1.position.y + 6, p1.position.z);
            target = p1;
        }
        else
        {
            transform.position = new Vector3(p2.position.x - 0.375f, p2.position.y + 6, p2.position.z);
            target = p2;
        }
        attackspeed = -5;
        AttackType = "";
        gameEnd = false;
        rb.linearDamping = 5;
        time = 3;
        downplayer = "";
        t1.text = "게임시작!";

        p1.GetComponent<Animator>().Play("idle");
        p2.GetComponent<Animator>().Play("idle");
    }

}
