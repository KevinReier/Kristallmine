using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections.Generic;
//using Boo.Lang;
//This Script is controlling the whole process of the Game
public class GameController : MonoBehaviour
{
    private float time;
    private GameObject esc_menue;
    public bool Escapepressed;

    private bool highscoreupdated = false;

    private static GameController instance;
    private bool tutorialAlreadyOver = false;
    private bool tutorialEnd = false;


    internal int[] tempScore = new int[10];
    //private String file = Path.Combine(Directory.GetCurrentDirectory(), "Assets/Scripts/Highscores.txt");
    private String file = "Highscores.txt";


    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameController>();
            }
            return instance;
        }
    }



    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        //Only show the Countdown after the Tutorial
        GameSettings.Instance.CountdownActivated = false;
        //If the Tutorial should be shown, initialize it
        if (GameSettings.Instance.ShowTutorial)
        {
            StartTutorial();
        }
        //initialize the Game
        init();
        LoadHighscores(file);
        RenderSettings.fogColor = new Color32(111, 105, 80, 1);
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;

        RenderSettings.fogStartDistance = 15;
        RenderSettings.fogEndDistance = RenderSettings.fogStartDistance + 20;

    }

    private void StartTutorial()
    {
        //Change the Settings to Tutorialmode:
        //No Obstacles, No SpeedBonus
        GameSettings.Instance.ObstaclesActivated = false;
        GameSettings.Instance.SpeedbonusActivated = false;

    }

    // Update is called once per frame
    void Update()
    {
        //Get Rid of the Fog if the Real Game is Starting
        //Initialize the Method that is ending the Tutorial
        if (!GameSettings.Instance.ShowTutorial && !GameSettings.Instance.CountdownActivated)
        {
            RenderSettings.fogStartDistance = Mathf.Lerp(RenderSettings.fogStartDistance, 70, Time.deltaTime / 3);
            RenderSettings.fogEndDistance = Mathf.Lerp(RenderSettings.fogEndDistance, 120, Time.deltaTime / 3);

            if (!GameSettings.Instance.TutorialAlreadyOver)
            {
                tutorialEnd = true;
            }
            GameSettings.Instance.TutorialAlreadyOver = true;
        }
        if (tutorialEnd)
        {
            EndTutorial();
        }
        //accelerate the Minecart until it reaches the Minimum (Start)Speed which is set in GameSettings
        accelerateMinecart();
        //Check if the SpaceBar is pressed
        SpacePressed();
        //Check if the Menu is open/ the Game is paused
        EscapeMenue();
        EndGame();

        time += Time.deltaTime;

        //Test if the conditions for ending the Tutorial are completed
        if (GameSettings.Instance.ShowTutorial)
        {
            TestTutorial();
        }

        UpdateHighscore();
    }

    private void TestTutorial()
    {
        //update the status if the Player collected a bunch of Crystals in a Row
        int status = GameSettings.Instance.status;
        int collectedGems = GameSettings.Instance.collectedGems;
        int GemsToCollectInTut = GameSettings.Instance.GemsToCollectInTut;
        //Variables are set in speedReduce and OnCrystalCollect
        //increase if Crystals are collected in a Row
        //set to zero if a Crystal is missed by the Player
        if (status == 3)
        {
            GameSettings.Instance.ShowTutorial = false;
        }
        else if(collectedGems == GemsToCollectInTut)
        {
            GameSettings.Instance.status++;
            GameSettings.Instance.collectedGems = 0;
        }
    }

    private void EndTutorial()
    {
        //Set to Game Mode
        GameSettings.Instance.CountdownActivated = true;
        GameSettings.Instance.ObstaclesActivated = true;
        GameSettings.Instance.SpeedbonusActivated = true;
        Score.Instance.currScore = 0;
        if (GameSettings.Instance.TutorialAlreadyOver)
        {
            for (int i = 0; i < 5; i++)
            {
                TileManager.Instance.SpawnTile();
            }
        }
        tutorialEnd = false;
    }
    private void SpacePressed()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkipTutorial.Instance.SkipTut();
        }
    }

    private void EscapeMenue()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Escapepressed = !Escapepressed;

        if (Escapepressed)
        {
            Time.timeScale = 0;
            // GameObject.Find("Countdown").transform.localScale = Vector3.zero;

            esc_menue.transform.localScale = Vector3.one;
            Cursor.visible = true;
        }
        if (!Escapepressed)
        {
            Time.timeScale = 1;
            esc_menue.transform.localScale = Vector3.zero;
            Cursor.visible = false;
        }
    }

    private void init()
    {

        time = 0F;
        Escapepressed = false;
        GameSettings.Instance.speed = 0f;
        esc_menue = GameObject.Find("EscPressed");
        esc_menue.transform.localScale = Vector3.zero;
        //if (!GameSettings.Instance.CountdownActivated) GameSettings.Instance.speed = GameSettings.Instance.StartSpeed;

    }
    private void accelerateMinecart()
    {
        if (GameSettings.Instance.speed < GameSettings.Instance.StartSpeed)
        {
            GameSettings.Instance.speed = Mathf.Lerp(GameSettings.Instance.speed, GameSettings.Instance.StartSpeed, Time.deltaTime / 2);
            //time += Time.deltaTime;
            //if (time > 7 && GameSettings.Instance.speed < GameSettings.Instance.StartSpeed) GameSettings.Instance.speed += GameSettings.Instance.StartSpeed / 200F;
        }
    }

    private void EndGame()
    {
        if (GameSettings.Instance.health <= 0)
        {
            Escapepressed = true;
            GameObject resume = GameObject.Find("resume");
            Destroy(resume, 0);
        }
    }

    private void UpdateHighscore()
    {
   
            if (!highscoreupdated && GameSettings.Instance.health <= 0)
            {
                addScore(Score.Instance.currScore);
                Debug.Log("GAME OVER!");
                highscoreupdated = true;
            }
    
    }

    void addScore(int score)
    {
        List<int> temp = new List<int>();
        //ArrayList temp = new ArrayList();
        for(int i = 0; i < tempScore.Length; i++)
        {
            temp.Add(tempScore[i]);
        }
        
        temp.Add(score);
           
          
        temp.Sort();
        temp.Reverse();
        temp.RemoveAt(10);
        tempScore = temp.ToArray();

        if (File.Exists(file))
        {
            StreamWriter sr = File.CreateText(file);

            for (int i = 0; i < tempScore.Length; i++)
            {
                sr.WriteLine(tempScore[i]);
            }
            sr.Close();
        }
        else
        {
            File.Create(file);
            StreamWriter sr = File.CreateText(file);

            for (int i = 0; i < tempScore.Length; i++)
            {
                sr.WriteLine(tempScore[i]);
            }
            sr.Close();

        }

    }

    
    void LoadHighscores(String file)
    {
        if (!File.Exists(file))
        {
            File.Create(file).Dispose();
            using (TextWriter tw = new StreamWriter(file))
            {
                for (int i = 0; i < 10; i++)
                {
                    tw.WriteLine("0");
                }
                tw.Close();
            }

        }
        else if (File.Exists(file))
        {
            StreamReader sr = File.OpenText(file);
            String line = sr.ReadLine();

            int i = 0;
            while (line != null)
            {

                if (tempScore.Length > i)
                {
                    tempScore[i] = int.Parse(line);
                    i++;
                }
                line = sr.ReadLine();

            }

        }
        else
        {
            Debug.Log("Could not Open the file: " + file + " for reading.");
            return;
        }
        Array.Sort(tempScore);
        Array.Reverse(tempScore);
        
    }


}
