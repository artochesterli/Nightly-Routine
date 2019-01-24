using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    public int index;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Camera.main.GetComponent<Save_Data>().Star_collection[index])
        {
            gameObject.SetActive(false);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Avatar"))
        {
            Camera.main.GetComponent<Save_Data>().Star_collection[index] = true;
            Camera.main.GetComponent<Save_Data>().Write_save();
            Destroy(gameObject);
        }
    }
}
