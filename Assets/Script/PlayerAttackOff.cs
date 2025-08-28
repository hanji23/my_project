using UnityEngine;

public class PlayerAttackOff : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("off", Time.deltaTime * 5);
    }

    void off()
    {
        if (isActiveAndEnabled)
            gameObject.SetActive(false);
    }
}
