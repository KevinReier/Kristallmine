using UnityEngine;
using System.Collections;

public class OnCrystalCollect : MonoBehaviour
{
    public int direction;
    private bool IsCollected = false;
    private GameObject player;
    //If the Player exits the Boundaries of the Tile, spawn a new one and destroy this
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !IsCollected)
        {
            Score.Instance.points += 1;
            if (GameSettings.Instance.SpeedbonusActivated && GameSettings.Instance.speed < GameSettings.Instance.maxSpeed)
            {
                GameSettings.Instance.speed += (1/ (10* (1/GameSettings.Instance.speedbonus) * Mathf.Log(GameSettings.Instance.speed)));
            }
            player = other.gameObject;
            IsCollected = true;
            //Destroy(gameObject, 3);
            Destroy(gameObject.transform.parent.gameObject, 0.2F);

            //keep Track of the number of Collected Crystals for the Tutorial
            if (GameSettings.Instance.ShowTutorial && (direction +1) == GameSettings.Instance.status)
            {
                GameSettings.Instance.collectedGems++;
            }
           
        }
    }
    void Update()
    {
        if (IsCollected) MoveCrystalToPlayer();
       
    }
    private void MoveCrystalToPlayer()
    {
            gameObject.transform.parent.localScale = Vector3.Slerp(gameObject.transform.localScale, Vector3.zero, Time.deltaTime * GameSettings.Instance.speed*4);
            gameObject.transform.parent.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * GameSettings.Instance.speed);
    }


}