using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TileManager : MonoBehaviour
{

    //the Last Tile that was spawned, Reference for the Spawn of next Tile
    //at script-start this is the Start-Tile
    public GameObject currentTile;

    //Array of every possible Tile
    public GameObject[] tilePrefabs;

    //List containing every spawned Tile
    private List<GameObject> tiles = new List<GameObject>();


    private static TileManager instance;

    //Direction and Speed for the Tiles to move.
    //set by PlayerMovement-Script
    private Vector3 direction;

    private bool TutorialIsRunning;
    public void setDirection(Vector3 dir)
    {
        direction = dir;
    }






    //create Instance of TileManager, so it can be accessed anywhere
    public static TileManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<TileManager>();
            }
            return instance;
        }


    }

    //get TileList, so Player knows the path of the Tiles
    public List<GameObject> getTilesList()
    {
        return tiles;
    }

    // Use this for initialization
    void Start()
    {
        TutorialIsRunning = GameSettings.Instance.ShowTutorial;
        //spawn x Tiles in the beginning, so the Player doesn't see them plop into existence
        InitMap();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Move every Tile according to the current direction and speed
        moveMap();

    }

    //Calculates the Rotation the next random Tile would have and Spawns it if there is no Intersection-hazard
    public void SpawnTile()
    {
        TutorialIsRunning = GameSettings.Instance.ShowTutorial;
        if (!TutorialIsRunning)
        {
            //choose a random Tile from the List
            int randomIndex = UnityEngine.Random.Range(0, tilePrefabs.Length);
            GameObject temp = tilePrefabs[randomIndex];
            //calculate the Rotation of the next Tile
            temp.transform.rotation = currentTile.transform.GetChild(0).transform.GetChild(0).rotation;
            Vector3 newRot = calculateRotation(temp.transform.GetChild(0).transform.GetChild(0).rotation.eulerAngles);
            //While there could be a Intersection between Tiles, choose another one and repeat
            while (newRot.y > 90 || newRot.y < -90)
            {
                randomIndex = UnityEngine.Random.Range(0, tilePrefabs.Length);
                temp = tilePrefabs[randomIndex];
                temp.transform.rotation = currentTile.transform.GetChild(0).transform.GetChild(0).rotation;
                newRot = calculateRotation(temp.transform.GetChild(0).transform.GetChild(0).rotation.eulerAngles);
            }


            //set the current end of the road and spawn the actual GameObject
            currentTile = (GameObject)Instantiate(tilePrefabs[randomIndex], currentTile.transform.GetChild(0).transform.GetChild(0).position, currentTile.transform.GetChild(0).transform.GetChild(0).rotation);
            //Set the Tilemanager as Parent
            currentTile.transform.parent = gameObject.transform;
            //add new tile to tiles list
            tiles.Add(currentTile);

            if (SpawnManager.Instance != null)
            {
                SpawnManager.Instance.SpawnCrystals(currentTile.transform.GetChild(2).GetComponent(typeof(EditorPathScript)) as EditorPathScript);
            }
        }
        else
        {
            //choose a random Tile from the List
            float errorTolerance = 5;
            int randomIndex = UnityEngine.Random.Range(0, tilePrefabs.Length);
            GameObject temp = tilePrefabs[randomIndex];
            //calculate the Rotation of the next Tile
            temp.transform.rotation = currentTile.transform.GetChild(0).transform.GetChild(0).rotation;
            Vector3 newRot = calculateRotation(temp.transform.GetChild(0).transform.GetChild(0).rotation.eulerAngles);
            //While there could be a Intersection between Tiles, choose another one and repeat
            while (newRot.y >= 0 + errorTolerance || newRot.y <= 0 - errorTolerance)
            {
                randomIndex = UnityEngine.Random.Range(0, tilePrefabs.Length);
                temp = tilePrefabs[randomIndex];
                temp.transform.rotation = currentTile.transform.GetChild(0).transform.GetChild(0).rotation;
                newRot = calculateRotation(temp.transform.GetChild(0).transform.GetChild(0).rotation.eulerAngles);
            }
            //set the current end of the road and spawn the actual GameObject
            currentTile = (GameObject)Instantiate(tilePrefabs[randomIndex], currentTile.transform.GetChild(0).transform.GetChild(0).position, currentTile.transform.GetChild(0).transform.GetChild(0).rotation);
            //Set the Tilemanager as Parent
            currentTile.transform.parent = gameObject.transform;
            //add new tile to tiles list
            tiles.Add(currentTile);

            if (SpawnManager.Instance != null)
            {
                SpawnManager.Instance.SpawnCrystals(currentTile.transform.GetChild(2).GetComponent(typeof(EditorPathScript)) as EditorPathScript);
            }
        }

    }

    //calculate the actual rotation: for example 270 = -90, -270 = 90...
    private Vector3 calculateRotation(Vector3 rot)
    {
        if (rot.y > 90)
        {
            rot -= new Vector3(0, 360, 0);
        }
        else if (rot.y < -90)
        {
            rot += new Vector3(0, 360, 0);
        }

        return rot;
    }

    //Start Coroutine and remove the oldest Tile after x Amount of Seconds
    public void Delete()
    {
        StartCoroutine(removeOldestTile());
    }

    //wait x seconds before deleting, so player doesn't see it disappear
    IEnumerator removeOldestTile()
    {
        yield return new WaitForSeconds(2);
        Destroy(TileManager.Instance.tiles[0]);
        TileManager.Instance.tiles.RemoveAt(0);
        PlayerMovement.Instance.setIndex(PlayerMovement.Instance.getIndex() - 1);
    }

    private void InitMap()
    {
        tiles.Add(currentTile);
        if (TutorialIsRunning)
        {
            for (int i = 0; i < 1; i++)
            {
                SpawnTile();
            }
        }
      

    }
    private void moveMap()
    {
        foreach (GameObject g in tiles)
        {
            if (g != null)
            {
                // g.transform.position += Vector3.MoveTowards(new Vector3(0, 0, 0), direction, Time.deltaTime * GameSettings.Instance.speed);
                g.transform.position += Vector3.MoveTowards(new Vector3(0, 0, 0), direction, Time.deltaTime * GameSettings.Instance.speed);

            }
        }
    }



}





