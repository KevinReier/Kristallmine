using UnityEngine;
using System.Collections;

public class SkipTutorial : MonoBehaviour {

	public void SkipTut()
    {
        GameSettings.Instance.ShowTutorial = false;
        gameObject.transform.parent.localScale = Vector3.zero;
    }
}
