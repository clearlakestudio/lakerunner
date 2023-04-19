/*
 * FileName: ChangeScene.cs
 * Developer: Akhil
 * Purpose: Loading and Changing Scenes in the game.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * The classes below implement
 * Dynamic Binding
 * to Enable or Disable Next Level button in the Gameover Screen
*/
class DisNxtBt 
{ 
    public virtual bool DisplayNextLevelButton() 
    {
        Debug.Log("button disabled");
        return false;
    }
}

// this class is the subclass and it overrides its function when called
class EbNxtBt : DisNxtBt
{
    public override bool DisplayNextLevelButton()
    {
        Debug.Log("button enabled");
        return true;
    }
}


/*
 * This class has 
 * functions to change, load and activate scenes
*/
public class ChangeScene : MonoBehaviour 
{
    /*
    * This function is a Singleton pattern
    * to ensure that only one instance of the object is present
    */
    private static ChangeScene instance;

    public static ChangeScene Instance()
    {
        if (instance == null) {
            instance = FindObjectOfType<ChangeScene>();
        }

    return instance;
    }

    // public gameobject for attaching the Next Level Button
    public GameObject NextLevelButtn;
    // public gameobject for attaching the gameoverScreen Prefab
    public GameObject gameOverUI;
    // initializing the object of the class which has dynamic binding
    private DisNxtBt d;
    // this bool will tell true if the player has finished the level successfully
    bool finishGame = true;
    // this bool stores value to show or hide the button
    bool button = true;

    /*
     * this function activates the gameover screen
     * when the player completes the level
     * or loses   
    */
    public void GameOver()
    {
        /*
        * if the player has successfully completed 
        * the level then the 
        * next level button is shown
        */
        if (finishGame == true) {
            d = new DisNxtBt();
            button = d.DisplayNextLevelButton();
        }
        else if(finishGame == false) {
            d = new EbNxtBt();
            button = d.DisplayNextLevelButton();
        }

        // frezes the game to stop when it is over
        Time.timeScale = 0;
        Debug.Log("Time Freeze");
        // activate gameover screen panel
        gameOverUI.SetActive(true);

        /*
         * if player didnt complete the level and lost 
         * then the next level button will be hidden
         */

        if (button == false) {
            Debug.Log("button enabled");
            NextLevelButtn.SetActive(false);
        }
        else {
            Debug.Log("button diabled");
            NextLevelButtn.SetActive(true);
        }

    }

    // this func will take to the next level
    public void NextLevel()
    {
        Debug.Log("next level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // this func will load the current scene again
    public void Restart()
    {
        Debug.Log("restart");
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // this func will take to the main menu
    public void MainMenu()
    {
        Debug.Log("MainMenu");
        SceneManager.LoadScene("MainMenu");
    }

    public void MoveToScene(int SceneID) 
    {
        SceneManager.LoadScene(SceneID);
    }

    // this func will quit the application
    public void Quit() 
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}