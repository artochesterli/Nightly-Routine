using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public List<GameObject> connected_objects;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (check_open())
        {
            Destroy(gameObject);
        }
	}

    private bool check_open()
    {
        bool ok = true;
        for(int i = 0; i < connected_objects.Count; i++)
        {
            if (connected_objects[i] != null)
            {
                ok = false;
            }
        }
        return ok;
    }
}
