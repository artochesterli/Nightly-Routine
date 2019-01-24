using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock_Wave : MonoBehaviour {

    float time;
	// Use this for initialization
	void Start () {
        time = 0;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent.GetComponent<Bubble>().Shock_Wave_Trigger(collision);
    }
}
