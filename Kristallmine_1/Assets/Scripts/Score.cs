using UnityEngine;
using System.Collections;


public class Score : MonoBehaviour
{

    internal int points=0;
    internal int currScore = 0;
    internal int multiplier = 0;


    private static Score instance;

    //create Instance of TileManager, so it can be accessed anywhere
    public static Score Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Score>();
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
        if ((((int)Mathf.Ceil(GameSettings.Instance.speed)) - GameSettings.Instance.StartSpeed) < 1) multiplier = 1;
        else  multiplier = ((int)Mathf.Ceil(GameSettings.Instance.speed)) -(int) GameSettings.Instance.StartSpeed;
       
        currScore += points * multiplier;
        points = 0;
    }
}
