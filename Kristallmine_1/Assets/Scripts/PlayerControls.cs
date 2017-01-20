using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

    private Quaternion originalRotationValue;
    private Quaternion left_rotation;
    private Quaternion right_rotation;
    private float rotationSpeed;
    private Vector3 standardPosition;
    private Vector3 desiredRotation;


    //Tilt the Player according to Keyboard-Input
    //Tilt-speed and Tilt-angle are set in GameSettings



    void Start () {
        desiredRotation = new Vector3(0, 0, GameSettings.Instance.PlayerTilt);
        rotationSpeed = GameSettings.Instance.PlayerRotationSpeed;
;
         standardPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update () {
        desiredRotation = new Vector3(0, 0, GameSettings.Instance.PlayerTilt);
        rotationSpeed = GameSettings.Instance.PlayerRotationSpeed;

        PlayerTilt();



    }
    private void PlayerTilt()
    {
   if(Input.GetKey(KeyCode.RightArrow) && !(Input.GetKey(KeyCode.LeftArrow)))
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, transform.GetChild(0).localPosition, Time.deltaTime * rotationSpeed);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(-desiredRotation), Time.deltaTime * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !(Input.GetKey(KeyCode.RightArrow)))
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, transform.GetChild(1).localPosition, Time.deltaTime * rotationSpeed);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(desiredRotation), Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, standardPosition, Time.deltaTime * rotationSpeed);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * rotationSpeed);
        }
    } 
}
