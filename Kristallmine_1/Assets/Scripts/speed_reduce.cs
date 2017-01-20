using UnityEngine;
using System.Collections;

public class speed_reduce : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Crystal")
        {
            if (GameSettings.Instance.SpeedbonusActivated) {
                reduceSpeed(); 
}
            //keep Track of the number of Collected Crystals for the Tutorial
            if (GameSettings.Instance.ShowTutorial)
            {
               GameSettings.Instance.collectedGems = 0;
            }
        }
    }
    private void reduceSpeed()
    {
        if (GameSettings.Instance.speed > (GameSettings.Instance.StartSpeed + GameSettings.Instance.speedreduce))
        {
            GameSettings.Instance.speed -= GameSettings.Instance.speedreduce;
            
        }
    }
}
