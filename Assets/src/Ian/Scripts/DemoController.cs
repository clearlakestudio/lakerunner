/*
    Filename: DemoController.cs
    Developer: Ian King
    Purpose: Control the AI that will play through demo mode
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoController : MonoBehaviour
{
    /*  Needed:
    *   - Find current and next 3? platforms
    *   - Compare distance from top right edge of one platform COLLIDER to top left edge of next
    *   - If x-distance > 1?
    *       - If y-distance > 1? then draw stairs
    *       - Else draw platform from top right corner of platform to left edge of next, minimum height
    *   - Item interactions as needed
    *
    *   Checking distances:
    *   - Iterate through terrain pieces one at a time based on relation to player location
    *       - Store last terrain location
    *       - If player moves onto last terrain and no next terrain is found: 
    *           - Make sure finish line is not behind player
    *           - Begin drawing certain length platforms until next terrain is found or player passes finish
    */
    private GameObject[] allTerrain;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
