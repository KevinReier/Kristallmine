using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Countdown : MonoBehaviour {
    public Text text;
    private float timer;
    // Use this for initialization
    void Start () {
        text.text = "";
        timer = 0f;
       
	}
	
	// Update is called once per frame
	void Update () {

        pauseCountdown();
        InitCountdown();

    }
    private void InitCountdown()
    {

        if (GameSettings.Instance.CountdownActivated)
        {
            
            timer += Time.deltaTime;
            if (timer >= 0.5f && timer <= 1.5f) text.text = "3";
            if (timer > 1.5f && timer < 2f) text.text = "";
            if (timer >= 2f && timer <= 2.5f) text.text = "2";
            if (timer > 2.5f && timer < 3f) text.text = "";
            if (timer >= 3f && timer <= 4f) text.text = "1";
            if (timer > 4 && timer < 5)
            {
                Score.Instance.currScore = 0;
               GameSettings.Instance.CountdownActivated = false;
               Destroy(gameObject, 0);
            
            }
            
        }
    }
    private void pauseCountdown()
    {
        if (GameController.Instance.Escapepressed) gameObject.transform.localScale = Vector3.zero;
        else gameObject.transform.localScale = Vector3.one;
    }
    
}
