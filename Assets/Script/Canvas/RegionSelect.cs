using UnityEngine;
using UnityEngine.SceneManagement;

public class RegionSelect : MonoBehaviour
{

    public void select(int Region)
    {
        GamePlayManager.Instance.Player_Region_TypeSetting(Region);
        SceneManager.LoadScene("BattleScene");
    } 
}
