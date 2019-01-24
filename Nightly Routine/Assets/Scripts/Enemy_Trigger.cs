using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Trigger : MonoBehaviour {


    public GameObject Connected_Enemy;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().gameObject.CompareTag("Avatar"))
        {
            Connected_Enemy.GetComponent<Enemy_in_casual_level>().activated = true;
        }
    }
}
