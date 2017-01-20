using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

    private int health;
    public string healthbar;

    private static Health instance;

    //create Instance of TileManager, so it can be accessed anywhere
    public static Health Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Health>();
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
        health = GameSettings.Instance.health;
        healthbar = "";
        for (int i = 0; i < health; i++)
        {
            healthbar += "I";
        }
    }
}
