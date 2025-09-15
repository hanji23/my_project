using UnityEngine;

public class PlayerAttackOff : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("off", Time.deltaTime * 5);

        if (transform.parent.transform.localScale.x < 0)
            transform.GetComponent<BoxCollider>().size = new Vector3(transform.GetComponent<BoxCollider>().size.x * -1, transform.GetComponent<BoxCollider>().size.y, transform.GetComponent<BoxCollider>().size.z);
    }

    void off()
    {
        if (isActiveAndEnabled)
            gameObject.SetActive(false);
    }
}
