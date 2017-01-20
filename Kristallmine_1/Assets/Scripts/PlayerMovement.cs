using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;

public class PlayerMovement : MonoBehaviour {

    public EditorPathScript PathToFollow; //the current path of the Tile the Player is in
    public int currentWayPointID; 
    private float reachDistance = 1.21f; //Error Tolerance between the Waypoint and the Player Positions
    private float rotationSpeed = 10; //How fast is the Player turning in the curves (only change if necessary, may cause bugs at big values)
    private int index = 0; //keep track of the path
    private static PlayerMovement instance;

    public void setIndex(int i)
    {
        index = i;
    }
    public int getIndex()
    {
        return index;
    }


    public static PlayerMovement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerMovement>();
            }
            return instance;
        }
    }
	// Use this for initialization
	void Start () {
        

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Move();

    }
    private void Move()
    {
        //The Player is not actually moving to prevent big coordinates when the game is running for a while
        //The Tiles are moving in the opposite direction that the Player is supposed to move to
        //So determine the Distance between the Player and the Waypoint he is currently aiming for and the direction it is in
        float distance = Vector3.Distance(PathToFollow.path_objects[currentWayPointID].position, transform.position);
        Vector3 direction = transform.position - PathToFollow.path_objects[currentWayPointID].position;


        //Tell the TileManager in which direction the Tiles should move
        TileManager.Instance.setDirection(direction);

        ////////////
        //Turn the Player so the Camera is facing in the correct direction
        var rotation = Quaternion.LookRotation(PathToFollow.path_objects[currentWayPointID].position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, (Time.deltaTime * rotationSpeed) / distance);
       
        //if the Player has Reached the next waypoint (+-Tolerance) increase the WaypointID
        if (distance <= reachDistance)
        {
            currentWayPointID++;
        }



        //if the Path the Player is currently following is ending, get the current List of Tiles and get the path 
        //of the next one
        //Initialise the removal of the last Tile and Spawn a new one
        if (currentWayPointID >= PathToFollow.path_objects.Count)
        {
            currentWayPointID = 1;
            index++;

            TileManager.Instance.Delete();

            TileManager.Instance.SpawnTile();
            List<GameObject> tilesList = TileManager.Instance.getTilesList();
            PathToFollow = tilesList[index].transform.GetChild(1).GetComponent(typeof(EditorPathScript)) as EditorPathScript;


        }
    }
}
