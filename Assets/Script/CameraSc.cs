using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraSc : MonoBehaviour
{
    Transform player1, player2;
    float distance;

    bool moveOn;
    float t;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        player1 = GameObject.Find("Player1").transform;
        player2 = GameObject.Find("Player2").transform;
    }

    private void LateUpdate()
    {
        if (moveOn)
        {
            if (t < 1.25f)
            {
                t = Time.deltaTime * 2;
                distance = Vector3.Distance(player1.position, player2.position);
                //transform.position -= new Vector3((player1.position.x + player2.position.x) / 2, 0, 0);
                transform.position = Vector3.Lerp(transform.position, new Vector3((player1.position.x + player2.position.x) / 2, transform.position.y, transform.position.z), t);
            }
           
        }
        
    }

    public void moveOnOff(bool b)
    {
        t = 0;
        moveOn = b;
    }
}
