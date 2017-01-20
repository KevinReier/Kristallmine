using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorPathScript : MonoBehaviour {

    public Color rayColor = Color.white;
    public List<Transform> path_objects = new List<Transform>();
    Transform[] array;
	// Use this for initialization

    void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        array = GetComponentsInChildren<Transform>();
        path_objects.Clear();

        foreach(Transform path_object in array)
        {
            if(path_object != this.transform)
            {
                path_objects.Add(path_object);
            }
        }

        for(int i = 0; i < path_objects.Count; i++)
        {
            Vector3 position = path_objects[i].position;
            if (i > 0)
            {
                Vector3 prev = path_objects[i - 1].position;
                Gizmos.DrawLine(prev, position);
                Gizmos.DrawWireSphere(position, 0.2f);
            }
        }
    }

}
