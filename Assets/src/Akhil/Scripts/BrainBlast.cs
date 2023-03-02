using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BBar : MonoBehaviour {

    public int BBarCount = 0;//  <<------ attach this script to the player and then attach the text corresponding to this script to there
    public Text BBarText;

    void Update() {
        BBarText.text = BBarCount.ToString();
    }
    public void pickUpBBar() {       // <<---- call these func in the item collectible script 
        BBarCount++;
    }
    public void useBBar() {          
        BBarCount--;
    }
}
// attach this script to the gamemanager script and then attach the textObject to this script
