using UnityEngine;
using System.Collections;

public class IgnoreTimeScale : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Time.timeScale <= 0.01f)
        {
            transform.GetComponent<ParticleSystem>().Simulate(Time.unscaledDeltaTime, true, false);
        }
	}
}
