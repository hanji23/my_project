using UnityEngine;
using UnityEngine.SceneManagement;

public class RegionSelect : MonoBehaviour
{

    public void select(int Region)
    {
        GamePlayManager.Instance.currentRace++;

        //PlayerCheckManager.Instance.newPlayer("Player");
        PlayerManager.Instance.SetPlayerRegion(Region);

        SceneManager.LoadScene("VersusScene");
    } 
}
