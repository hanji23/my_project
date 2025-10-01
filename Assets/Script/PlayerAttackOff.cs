using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackOff : MonoBehaviour
{

    private void OnEnable()
    {
        StopCoroutine(Off());
        StartCoroutine(Off());

        if (transform.parent.transform.localScale.x < 0)
            transform.GetComponent<BoxCollider>().size = new Vector3(transform.GetComponent<BoxCollider>().size.x * -1, transform.GetComponent<BoxCollider>().size.y, transform.GetComponent<BoxCollider>().size.z);
    }

    IEnumerator Off()
    {
        yield return new WaitForSeconds(0.1f);
        if (isActiveAndEnabled)
            gameObject.SetActive(false);
    }
}
