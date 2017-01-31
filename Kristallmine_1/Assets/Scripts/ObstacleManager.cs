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
            GameSettings.Instance.speed = GameSettings.Instance.speed-((GameSettings.Instance.speed-GameSettings.Instance.StartSpeed) * 0.75f);
            AudioSourceScript.Instance.audioHitObject.Play();
            //Tell the Player it was hit so the Blinking Animation is triggered
            PlayerTransparency.Instance.hit = true;
            Destroy(gameObject);

        }
        if (other.tag == "Crystal")
        {
            Destroy(gameObject);
        }

    }


}
