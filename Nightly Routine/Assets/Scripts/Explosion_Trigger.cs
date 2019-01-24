using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Trigger : MonoBehaviour {


    private const float explosion_time=0.4f;
    private const float explosion_r=7;
	// Use this for initialization
	void Start () {
        StartCoroutine(explosion());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Collider2D>().gameObject.GetComponent<Enemy>().Destroy_Self();
        }
        if (collision.GetComponent<Collider2D>().gameObject.CompareTag("Avatar"))
        {
            //StartCoroutine(Camera.main.GetComponent<Core_Controller>().go_to_last_check_point(collision.GetComponent<Collider2D>().gameObject));
        }
    }
    IEnumerator explosion()
    {
        
        float time = 0;
        float ori_r = GetComponent<CircleCollider2D>().radius;
        while (time < explosion_time)
        {
            time += Time.deltaTime;
            GetComponent<CircleCollider2D>().radius = (explosion_r - ori_r)*time/explosion_time+ori_r;
            yield return null;
        }
        GetComponent<CircleCollider2D>().radius = explosion_r;
    }
}
