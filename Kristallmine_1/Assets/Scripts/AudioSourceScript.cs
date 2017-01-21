using UnityEngine;
using System.Collections;

public class AudioSourceScript : MonoBehaviour {
    public static AudioSourceScript instance;

    public static AudioSourceScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<AudioSourceScript>();
            }
            return instance;
        }
    }

    public AudioClip ambientMusic;
    public AudioClip hitObject;
    public AudioClip collectCrystal;
    //public AudioClip railSound;

    internal AudioSource audioAmbient;
    internal AudioSource audioHitObject;
    internal AudioSource audioCollectCrystal;
    //private AudioSource audioRailSound;

    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        return newAudio;
    }

    public void Awake()
    {
        // add the necessary AudioSources:
        audioAmbient = AddAudio(ambientMusic, true, true, 0.6f);
        audioHitObject = AddAudio(hitObject, false, false, 0.4f);
        audioCollectCrystal = AddAudio(collectCrystal, false, false, 0.1f);
        //audioRailSound = AddAudio(railSound, false, false, 0.8f);
    }
    // Use this for initialization
    void Start () {
        Awake();
        audioAmbient.pitch = 0.8f;
        audioAmbient.Play();
        audioCollectCrystal.pitch = 1.2f; 
        audioHitObject.pitch = 0.8f;
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
