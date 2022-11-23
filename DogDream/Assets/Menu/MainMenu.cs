using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int numScen;
    public void PlayerGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + numScen);
    }
}
