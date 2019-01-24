using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force_Field : MonoBehaviour {

    public float force;
    private List<GameObject> list;
	// Use this for initialization
	void Start () {
        list = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        list.Add(collision.GetComponent<Collider2D>().gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().gameObject.GetComponent<Rigidbody2D>() != null)
        {
            collision.GetComponent<Collider2D>().gameObject.GetComponent<Rigidbody2D>().AddForce(force * 9.8f * transform.up);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for(int i = 0; i < list.Count; i++)
        {
            if (collision.GetComponent<Collider2D>().gameObject == list[i])
            {
                list.RemoveAt(i);
                break;
            }
        }
    }
}
