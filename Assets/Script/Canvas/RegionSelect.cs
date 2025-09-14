using UnityEngine;
using UnityEngine.SceneManagement;

public class RegionSelect : MonoBehaviour
{

    public void select(int Region)
    {
        GamePlayManager.Instance.Race++;

        //PlayerCheckManager.Instance.newPlayer("Player");
        PlayerCheckManager.Instance.PlayerRegion(Region);

        SceneManager.LoadScene("VersusScene");
    } 
}
