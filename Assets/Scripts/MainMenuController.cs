using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    //public method press play is called when the user presses the play button on the main menu
   public void PressPlay()
    {
        //when the user presses the play button, load the game scene
        SceneManager.LoadScene("Game Scene");
    }//end method
}
