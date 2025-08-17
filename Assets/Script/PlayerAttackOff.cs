using UnityEngine;

public class PlayerAttackOff : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("off", 0.05f);
    }

    void off()
    {
        gameObject.SetActive(false);
    }
}
