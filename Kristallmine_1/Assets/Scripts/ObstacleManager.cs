using UnityEngine;
using System.Collections;

public class ObstacleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!GameSettings.Instance.ObstaclesActivated) Destroy(gameObject); 
        
	}
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Crystal")
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameSettings.Instance.health -= 1;
            //GameSettings.Instance.speed = GameSettings.Instance.StartSpeed;
            GameSettings.Instance.speed = GameSettings.Instance.speed / 3;
            AudioSourceScript.Instance.audioHitObject.Play();
            Destroy(gameObject);

        }
        if (other.tag == "Crystal")
        {
            Destroy(gameObject);
        }

    }


}
