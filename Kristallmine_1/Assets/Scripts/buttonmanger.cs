using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class buttonmanger : MonoBehaviour {

    public void ButtonManager(int Scene)
    {
        SceneController.Instance.LoadScene(Scene);
    }

	
}
