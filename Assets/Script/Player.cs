using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Transform attack0, attack1;
    Animator ani;
    Vector3 target = Vector3.zero;
    Vector3 vel = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attack0 = transform.GetChild(0);
        attack1 = transform.GetChild(1);
        ani = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && ani.GetCurrentAnimatorStateInfo(0).IsName("idle")) //GetButtonDown 나중에 써보자
        {
            ani.Play($"attack{Random.Range(1, 4)}");
        }
        if (Input.GetKeyDown(KeyCode.X) && ani.GetCurrentAnimatorStateInfo(0).IsName("idle")) //GetButtonDown 나중에 써보자
        {
            ani.Play("attack4");
        }

        if (ani.GetCurrentAnimatorStateInfo(0).IsName("die"))
            transform.position = Vector3.SmoothDamp(transform.position, target, ref vel, 0.25f * Time.deltaTime);
    }


    void BoxOn(int type)
    {
        if (!ani.GetCurrentAnimatorStateInfo(0).IsName("die"))
            transform.GetChild(type).gameObject.SetActive(true);
    }
    IEnumerator ReturnIdle()
    {
        yield return new WaitForSeconds(0.25f);
        ani.Play("idle");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball") && !ani.GetCurrentAnimatorStateInfo(0).IsName("die"))
        {
            target = new Vector3(transform.position.x + (transform.position.x / Mathf.Abs(transform.position.x) * 0.5f), transform.position.y, transform.position.z);
        }
    }
}
