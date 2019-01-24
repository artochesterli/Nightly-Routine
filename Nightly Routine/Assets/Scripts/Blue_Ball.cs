using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Ball : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Core_Controller.Going_to_check_point&&collision.GetComponent<Collider2D>().gameObject.CompareTag("Lightning_Ball"))
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(Camera.main.GetComponent<Core_Controller>().go_to_last_check_point(gameObject));
        }
    }
}
