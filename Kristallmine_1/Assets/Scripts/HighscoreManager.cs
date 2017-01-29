using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour {
    public static HighscoreManager instance;

    public static HighscoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<HighscoreManager>();
            }
            return instance;
        }
    }
    private String file = Path.Combine(Directory.GetCurrentDirectory(), "Assets/Scripts/Highscores.txt");
    //private String file = "Highscores.txt";
    public String highscores = "1";
    private int[] tempScore = new int[10];


    void Start () {
        LoadHighscores(file);
	}
   

    //public void addScore(int score)
    //{
    //    for(int i=0; i < tempScore.Length; i++)
    //    {
    //        if(score > tempScore[i])
    //        {
    //            tempScore[i] = score;
    //            break;
    //        }
    //    }

    //    if (File.Exists(file))
    //    {
    //        StreamWriter sr = File.CreateText(file);

    //        for(int i = 0; i < tempScore.Length; i++)
    //        {
    //            sr.WriteLine(tempScore[i]);
    //        }
    //        sr.Close();
    //    }
    //    else
    //    {
    //        File.Create(file);
    //        StreamWriter sr = File.CreateText(file);

    //        for (int i = 0; i < tempScore.Length; i++)
    //        {
    //            sr.WriteLine(tempScore[i]);
    //        }
    //        sr.Close();

    //    }
        
    //}

    void LoadHighscores(String file)
    {
        
        if (File.Exists(file))
        {
            StreamReader sr = File.OpenText(file);
            String line = sr.ReadLine();
            
            int i = 0;
            while (line != null)
            {
                
                if(tempScore.Length > i )
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
        for(int j = 0; j < tempScore.Length; j++)
        {
            highscores += j+1 + ": " + tempScore[j] +" "+ Environment.NewLine;
      
        }
    }



    public void showHighscores()
    {
        Debug.Log("HighscoreButton!!!");
        GameObject.FindGameObjectWithTag("MainMenue").transform.localScale = Vector3.zero;
        GameObject.FindGameObjectWithTag("HighscoreMenue").transform.localScale = Vector3.one;
        GameObject.FindGameObjectWithTag("HighscoreMenue").transform.GetChild(0).GetComponent<Text>().text = highscores;
        

    }

    public void hideHighscores()
    {
        Debug.Log("BackButton!!!");
        GameObject.FindGameObjectWithTag("MainMenue").transform.localScale = Vector3.one;
        GameObject.FindGameObjectWithTag("HighscoreMenue").transform.localScale = Vector3.zero;
    }

}

