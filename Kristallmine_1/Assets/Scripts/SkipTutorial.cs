using UnityEngine;
using System.Collections;

public class SkipTutorial : MonoBehaviour {
    public static SkipTutorial instance;
    public static SkipTutorial Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<SkipTutorial>();
            }
            return instance;
        }
    }

    public void SkipTut()
    {
        GameSettings.Instance.ShowTutorial = false;
        gameObject.transform.parent.localScale = Vector3.zero;
    }
}
