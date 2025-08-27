using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    Transform attack0, attack1;
    Animator ani;
    Vector3 target = Vector3.zero;
    Vector3 vel = Vector3.zero;

    [SerializeField]
    private CharacterSOMaker SO;

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

                PlaySettingSc.Instance.UIstart();
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
        ani.Play("idle");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball") && !ani.GetCurrentAnimatorStateInfo(0).IsName("down"))
        {
            target = new Vector3(transform.position.x + (transform.position.x / Mathf.Abs(transform.position.x) * 0.75f), transform.position.y, transform.position.z);
        }
    }

    
}
