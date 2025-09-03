using UnityEngine;
using UnityEngine.SceneManagement;

public class RegionSelect : MonoBehaviour
{

    public void select(int Region)
    {
        //GamePlayManager.Instance.Player_Region_TypeSetting(Region);
        GamePlayManager.Instance.SetParty();
        GamePlayManager.Instance.SetRace();

        //PlayerCheckManager.Instance.newPlayer("Player");
        PlayerCheckManager.Instance.PlayerRegion(Region);

        SceneManager.LoadScene("VersusScene");
    } 
}
