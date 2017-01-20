using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health_writer : MonoBehaviour {
    public Text text;
    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        writeHealth();
    }
    private void writeHealth()
    {
    text.text = Health.Instance.healthbar;
    }
}
