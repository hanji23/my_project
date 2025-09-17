using UnityEngine;

public class BallAction : MonoBehaviour
{

    Rigidbody rb;
    public Transform player1Transform, player2Transform;

    [SerializeField]
    private Transform targetTransform;
    public enum EAttackType
    {
        None,           // 기본값, 공격 없음 상태
        Forward,        // 앞으로 던지는 공격
        UpTarget,       // 목표를 향해 포물선 공격
        UpPlayer,       // 플레이어 머리 위로 올리는 공격
        PlayerHit       // 플레이어 맞은 상태
    }

    [SerializeField]
    private EAttackType attackType;

    [SerializeField]
    private float ballSpeed;

    [SerializeField]
    private float attackSpeed;

    bool isGameEnded;

    float countdownTime, lerpProgress;

    ready gameCanvas;

    Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        gameCanvas = GameObject.Find("fightCanvas").transform.GetComponent<ready>();

        //t1 = GameObject.Find("PlayerCanvas").transform.Find("Text").GetComponent<TextMeshProUGUI>();

        ballSpeed = 3.5f;
    }

    private void FixedUpdate()
    {
        switch (attackType)
        {
            case EAttackType.Forward:
                Vector3 newPos = Vector3.MoveTowards(transform.position, GetTargetPosition(), (ballSpeed + attackSpeed) * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
                break;
            case EAttackType.UpTarget:

                if (lerpProgress < 1.25f)
                {
                    lerpProgress += Time.deltaTime / 2;

                    transform.position = Vector3.Lerp(startPosition,
                        new Vector3(targetTransform.position.x + (targetTransform.position.x / Mathf.Abs(targetTransform.position.x) * 0.5f), targetTransform.position.y, targetTransform.position.z), lerpProgress);
                    // 추가: Y축에만 포물선 오프셋
                    transform.position += new Vector3(0f, Mathf.Sin(lerpProgress * Mathf.PI) * 2.5f, 0f);
                }

                break;
            case EAttackType.UpPlayer:

                break;
            case EAttackType.PlayerHit:

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attackType.Equals("PlayerHit"))
        {
            if (countdownTime <= 0 && GamePlayManager.Instance.currentRound <= 3)
            {
                ResetAttackState();
                SetupNextRound();
            }
            else
            {
                countdownTime -= Time.deltaTime;
            }
            
        }
    }
    void OnTriggerEnter(Collider hit)
    {
        if (!isGameEnded)
        {
            if (hit.CompareTag("attack_forward"))//전방 발사
            {
                //Debug.Log($"{hit.name} {hit.transform.parent.name}");
                hit.gameObject.SetActive(false);
                rb.AddForce(0, 0, 0);
                attackSpeed += 0.25f;
                ResetAttackState();
                attackType = EAttackType.Forward;
                SwitchTarget(hit);
                rb.useGravity = false;
                //if (transform.position.y > 1f)
                //    rb.AddForce(target.position.x / Mathf.Abs(target.position.x) * (150 + attackspeed), -10f, 0);
                //else
                //    rb.AddForce(target.position.x / Mathf.Abs(target.position.x) * (150 + attackspeed), 0, 0);
            }
            else if (hit.CompareTag("attack_up_target"))// 포물선 발사
            {
                rb.AddForce(0, 0, 0);
                attackSpeed = 0;
                ResetAttackState();
                attackType = EAttackType.UpTarget;
                SwitchTarget(hit);
                rb.useGravity = false;

                //rb.linearVelocity = new Vector3(target.position.x / Mathf.Abs(target.position.x), 0, 0) * 5f + Vector3.up * 5;

            }
            else if (hit.CompareTag("attack_up_player"))// 자신 머리위로 발사
            {
                ResetAttackState();
                rb.useGravity = true;
                attackType = EAttackType.UpPlayer;
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && !isGameEnded)
        {
            Camera.main.GetComponent<MainCamera>().moveOnOff(false);
            isGameEnded = true;
            attackType = EAttackType.PlayerHit;
            rb.linearDamping = 0;
            rb.AddForce(0, 0, 0);
            

            rb.useGravity = true;
            ContactPoint cp = collision.GetContact(0);
            Vector3 dir = /*transform.position*/ new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z - 1f) - cp.point; // 접촉지점에서부터 탄위치 의 방향 https://dallcom-forever2620.tistory.com/42
            rb.AddForce((dir).normalized * 300f);

            if (collision.collider.transform == player1Transform)
                player2Transform.GetComponent<Player>().UpdateRoundWinUI();
            else if (collision.collider.transform == player2Transform)
                player1Transform.GetComponent<Player>().UpdateRoundWinUI();

            GamePlayManager.Instance.currentRound++;

            if (GamePlayManager.Instance.currentRound <= 3)
            {
                StartCoroutine(gameCanvas.UIdown());
            }
            else
            {
                StartCoroutine(gameCanvas.UIFINISH());
            }
            
        }

    }

    void SwitchTarget(Collider hit)
    {
        //target = target == p1? p2: p1;
        targetTransform = hit.transform.parent == player1Transform?  player2Transform : player1Transform;
        lerpProgress = 0;
        startPosition = transform.position;
        
        //if (target == p1)
        //{
        //    startpostion = p2.transform.position;
        //}
        //else
        //{
        //    startpostion = p1.transform.position;
        //}
    }

    void ResetAttackState()
    {
        rb.isKinematic = true;
        rb.isKinematic = false;
        rb.linearDamping = 0;
    }

    public void ResetBall()
    {
        if (Random.Range(0, 2) == 0)
            targetTransform = player1Transform;
        else
            targetTransform = player2Transform;

        transform.position = new Vector3(targetTransform.position.x + -(targetTransform.position.x / Mathf.Abs(targetTransform.position.x) * 0.375f), targetTransform.position.y + 6, targetTransform.position.z);
        attackSpeed = -0.25f;
        attackType = EAttackType.None;
        isGameEnded = false;
        rb.linearDamping = 5;
        countdownTime = 3;
        //t1.text = "게임시작!";

        player1Transform.GetComponent<Animator>().Play("idle");
        player2Transform.GetComponent<Animator>().Play("idle");
        Camera.main.GetComponent<MainCamera>().moveOnOff(true);
        
    }

    void SetupNextRound()
    {
        transform.position = new Vector3(targetTransform.position.x + -(targetTransform.position.x / Mathf.Abs(targetTransform.position.x) * 0.375f), targetTransform.position.y + 6, targetTransform.position.z);
        attackSpeed = -0.25f;
        attackType = EAttackType.None;
        isGameEnded = false;
        rb.linearDamping = 5;
        countdownTime = 3;
        //t1.text = "게임시작!";

        player1Transform.GetComponent<Animator>().Play("idle");
        player2Transform.GetComponent<Animator>().Play("idle");
        Camera.main.GetComponent<MainCamera>().moveOnOff(true);
    }

    Vector3 GetTargetPosition()
    {
        return new Vector3(targetTransform.position.x, targetTransform.position.y + 0.75f, targetTransform.position.z);
    }
}
