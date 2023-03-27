/*
    Filename: DemoController.cs
    Developer: Ian King
    Purpose: Control the AI that will play through demo mode
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoController : MonoBehaviour
{
    /*  Needed:
    *   - Find current and next 3? terrain
    *   - Compare distance from top right edge of one terrain COLLIDER to top left edge of next
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
    
    private DemoFacade demo;
    private GameObject platMan;
    private GameObject overMan;
    private GameObject hero;
    // private GameObject[] allTerrain;
    private PlatformManager platScript;
    private OverworldManager overScript;
    private float secCount = 0.0f;
    private bool inDemo = false;

    // Start is called before the first frame update
    void Start()
    {
        overMan = GameObject.FindWithTag("OverworldManager");
        overScript = overMan.GetComponent<OverworldManager>();
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            secCount = 0;
        }
        if(!inDemo)
        {
            secCount += Time.deltaTime;
            Debug.Log("Demo counter = " + secCount);
            if(secCount >= 10.0f)
            {
                inDemo = true;
                SceneManager.LoadScene(overScript.GetHeroLevel());
                platMan = GameObject.Find("UserPlatformManager");
                platScript = platMan.GetComponent<PlatformManager>();
            }
        }
        else
        {
            //Demo functions go here
            Vector2 heroPos = GameObject.FindWithTag("Hero").transform.position;

            demo = new DemoFacade();
            demo.FillGap(heroPos);

            if(Input.anyKey)
            {
                inDemo = false;
                SceneManager.LoadScene("Overworld");
            }
        }
    }
}

//Facade to incorporate Chunk and PlatformManager classes
public class DemoFacade
{
    PlatformManager pM;
    ChunkGroup ch;

    public DemoFacade()
    {
        //HOW DO YOU INITIALIZE PLATFORMMANAGER???
        //pM = ;
        //ch = new ChunkGroup();
    }

    public Vector2 GetNextChunkPos(Vector2 pos){
        ChunkGroup temp = ch.GetNextChunk(pos);
        Vector2 tempLoc = temp.transform.position;
        return tempLoc;
    }

    public Vector2 GetCurChunkPos(Vector2 pos){
        ChunkGroup temp = ch.GetCurChunk(pos);
        Vector2 tempLoc = temp.transform.position;
        return tempLoc;
    }

    public void CreatePlatform(Vector2 topLeft, float width){
        Vector3[] loc = {new Vector3(topLeft.x,topLeft.y,0),new Vector3(topLeft.x + width, topLeft.y-1,0)};
        PlatBox pTemp = pM.CheckPlatValidity(loc);
        pM.MakePlat(pTemp,0);
    }

    public void FillGap(Vector2 heroPos){
        ChunkGroup nextChunk = ch.GetNextChunk(heroPos);
        ChunkGroup curChunk = ch.GetCurChunk(heroPos);
        float distX = nextChunk.GetPos().x - curChunk.GetPos().x;
        float distY = nextChunk.GetPos().y - curChunk.GetPos().y;
        
        //Chunks are not meeting, gap must be filled
        if(distX > 0)
        {
            //Player must be built up to next platform
            if(distY > .5)
            {
                Vector2 topLeftLoc = new Vector2(curChunk.GetPos().x+(curChunk.GetComponent<Collider>().bounds.size.x/2),curChunk.GetPos().y + 0.5f);
                for(int i = 0; i < distY * 2; i++){
                    CreatePlatform(topLeftLoc,distY/distX);
                    topLeftLoc.x += distX/(distY * 2);
                    topLeftLoc.y += 0.5f;
                }
            }
            //Player may be built across on same level
            else
            {
                Vector2 topLeftLoc = new Vector2(curChunk.GetPos().x+(curChunk.GetComponent<Collider>().bounds.size.x/2),curChunk.GetPos().y);
                CreatePlatform(topLeftLoc,distX);
            }
        }
    }
}