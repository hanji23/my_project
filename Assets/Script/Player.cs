using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Transform attack0, attack1;
    Animator ani;

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
}
