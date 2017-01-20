using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Button_resume : MonoBehaviour
{

    public void Resume()
    {
        Time.timeScale = 1;
        GameController.Instance.Escapepressed = false;
    }

}
