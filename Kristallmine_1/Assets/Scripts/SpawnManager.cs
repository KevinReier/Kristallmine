using UnityEngine;
using System.Collections;
using System;

public class SpawnManager : MonoBehaviour
{

    private int minLength;
    private int maxLength;
    public EditorPathScript currentSpawnPath;
    private float distance;
    public GameObject crystal_green;
    public GameObject crystal_red;
    public GameObject crystal_blue;
    
    private int index = 0;
    private int sinIndex = 0;
    private int spawnDirection = -1;
    private GameObject[] crystals;
    private float YRoation;
    private int lastDirection;
    private float SpawnHeight;


    private static SpawnManager instance;
    public static SpawnManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<SpawnManager>();
            }
            return instance;
        }


    }
    // Use this for initialization
    void Start()
    {
        YRoation = GameSettings.Instance.YRotation; //Initial Rotation of all spawned Crystals
        minLength = GameSettings.Instance.minLength;//How long should the Chain of Crystals be?
        maxLength = GameSettings.Instance.maxLength;

        SpawnHeight = GameSettings.Instance.SpawnHeight; //How far above the ground are the Crystals floating
        distance = GameSettings.Instance.SpawnDistance; //How far from the Center of the Rails are the Crystals(can cause bugs if altered!)
        //If Tutorial is deactivated at Start, set Spawndirection (-1 = left, 0 = middle, 1=right) at random
        if (GameSettings.Instance.TutorialAlreadyOver) 
        {
            spawnDirection = UnityEngine.Random.Range(0, 3) - 1;
        }
       
        //Spawn the first set of Crystals on the given Path
        SpawnCrystals(currentSpawnPath);
    }

    //Fill the Given Path with Crystals according to the Status of the Tutorial and the Scriptinternal counter
    public void SpawnCrystals(EditorPathScript currentSpawnPath)
    {
        //set the length of the chain according to the given parameters in GameSettings
        int randomLength = UnityEngine.Random.Range(minLength, maxLength + 1);
        //if the Tutorial is Running, always set the SpawnLength to 1 to decrease delay caused by prespawned Crystals
        if (GameSettings.Instance.ShowTutorial)
        {
            randomLength = 1;
        }

        //As long as the Tile is not filled, spawn a Crystal
        for (int i = 0; i < currentSpawnPath.path_objects.Count; i++)
        {
            
            GameObject lastCrystal;

            //if the number of Crystals already spawned on the current path exceeds the allowed length of the chain
            //set the counter to zero and choose a new chain length on another position 
            if (index >= randomLength)
            {
                index = 0;
                randomLength = UnityEngine.Random.Range(minLength, maxLength + 1);
                lastDirection = spawnDirection;
                //do so ifthe Tutorial is not running (anymore)
                if (GameSettings.Instance.TutorialAlreadyOver)
                {
                    while (spawnDirection == lastDirection)
                    {

                        spawnDirection = UnityEngine.Random.Range(0, 3) - 1;


                    }
                }
                //if the Tutorial is running, set the Spawndirection according to it's current status
                else
                {
                    spawnDirection = GameSettings.Instance.status - 1;
                }

            }

            //If the Tutorial isn't running and the Crystals are set to spawn in an Sin-curve, determine the color they should have
            //---bbb----------------------bbb----------------------
            //bbb----ggg---------------bbb---ggg---------------bbb-
            //----------ggg---------ggg---------ggg---------ggg----
            //-------------rrr---ggg---------------rrr---ggg-------
            //----------------rrr---------------------rrr----------
            if (GameSettings.Instance.SinusActivted && !GameSettings.Instance.ShowTutorial)
            {
                float maxIndex = 2/ GameSettings.Instance.sinFrequency;
                if ((sinIndex  > 0.25f*maxIndex && sinIndex < 0.5f * maxIndex) || (sinIndex > 0.75f * maxIndex && sinIndex <  maxIndex)) lastCrystal = (GameObject)Instantiate(crystal_green, currentSpawnPath.path_objects[i].position, currentSpawnPath.path_objects[i].rotation);
                else if (sinIndex >= 0.5f * maxIndex && sinIndex <= 0.75f * maxIndex) lastCrystal = (GameObject)Instantiate(crystal_blue, currentSpawnPath.path_objects[i].position, currentSpawnPath.path_objects[i].rotation);
                else lastCrystal = (GameObject)Instantiate(crystal_red, currentSpawnPath.path_objects[i].position, currentSpawnPath.path_objects[i].rotation);
            }
            //if Sinus-Spawn is deactivated, determine the color according to the SpawnDirection
            //left=blue,middle=green,right=red
            else
            {
                if (spawnDirection == 0) lastCrystal = (GameObject)Instantiate(crystal_green, currentSpawnPath.path_objects[i].position, currentSpawnPath.path_objects[i].rotation);
                else if (spawnDirection == 1) lastCrystal = (GameObject)Instantiate(crystal_red, currentSpawnPath.path_objects[i].position, currentSpawnPath.path_objects[i].rotation);
                else lastCrystal = (GameObject)Instantiate(crystal_blue, currentSpawnPath.path_objects[i].position, currentSpawnPath.path_objects[i].rotation);
            }
            //set the Tile the crystal was spawned in as parent of it
            lastCrystal.transform.parent = currentSpawnPath.transform.parent.transform;
            //set the Rotation according to the Path, so the offset can later be applied correctly
            lastCrystal.transform.rotation = currentSpawnPath.path_objects[i].rotation;


            //Determine the offset from the center if it's supposed to be Sinus
            //(If the Tutorial has ended)
            if (GameSettings.Instance.SinusActivted && !GameSettings.Instance.ShowTutorial)
            {
                float a = GameSettings.Instance.sinFrequency * Mathf.PI;
               
                lastCrystal.transform.Translate(Mathf.Sin(sinIndex * a) * new Vector3(distance, 0, 0));
                sinIndex++;
               

                if(sinIndex >= 2* Mathf.PI / a)
                {
                    sinIndex = 0;
                }
                
            }
            //Determine Offset without Sinus applied
            else
            {
                lastCrystal.transform.Translate(spawnDirection * new Vector3(distance, 0, 0));
            }
            
            lastCrystal.transform.localScale = new Vector3(1, 1, 1);
            lastCrystal.transform.position += new Vector3(0, SpawnHeight, 0);
            lastCrystal.transform.GetChild(0).GetComponent<OnCrystalCollect>().direction = spawnDirection;
            index++;
        }
    }

    void Update()
    {
        //update the Min and Max Chainlength, so it can be changed at Runtime by modifying the values in GameSettings
        minLength = GameSettings.Instance.minLength;
        maxLength = GameSettings.Instance.maxLength;
        //Rotate the Crystals
        CrystalRotate();
    }
    private void CrystalRotate()
    {
        YRoation += 5f; 
        //prevent unnecessary big floats
        if(YRoation >= 360)
        {
            YRoation -= 360;
        }

        //get all active Crystals
        crystals = GameObject.FindGameObjectsWithTag("Crystal");


        //rotate them by 5Deg
        foreach (GameObject crystal in crystals)
        {
            crystal.transform.rotation = Quaternion.AngleAxis(YRoation, Vector3.up);

        }
    }
}