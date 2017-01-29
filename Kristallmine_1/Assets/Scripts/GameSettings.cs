using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour
{
    //All necessary and/or changable Variables are stored here
    //This Script doesn't do anything else
    public int health;

    public float StartSpeed;

    public bool SpeedbonusActivated;
    public float speedbonus;
    public float speedreduce;
    public  float speed;
    public float maxSpeed;


    public float PlayerRotationSpeed;
    public float PlayerTilt;

    public bool SinusActivted;
    public float SpawnDistance;
    public float SpawnHeight;
    public int minLength;
    public int maxLength;
    public float YRotation;

    public bool CountdownActivated;

    internal bool ObstaclesActivated;
    public float obsticaleChance;
    public bool ShowTutorial;
    internal bool TutorialAlreadyOver= false;

    //Tutorial Variables
    internal int collectedGems = 0;
    public int GemsToCollectInTut = 15;
    public int status = 0;

    //PlayerHit
    internal float hitDuration = 0.2f;
    internal float hitAnimationTime = 2f;

    //Sinusfrequency for Crystal-spawning
    public float sinFrequency;

    private static GameSettings instance;
    

    //create Instance of Game_Variables, so it can be accessed anywhere
    public static GameSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameSettings>();
            }
            return instance;
        }
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}