using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score_writer : MonoBehaviour {
    public Text text;
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        WriteScore();
    }
    private void WriteScore()
    {
        if (GameSettings.Instance.TutorialAlreadyOver && !GameSettings.Instance.CountdownActivated)
        {
            text.text = "Score  " + Score.Instance.currScore + " X " + Score.Instance.multiplier;
        }
        else
        {
            text.text = "";
        }
    
    }
}
