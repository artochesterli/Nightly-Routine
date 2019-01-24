using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Avatar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 relative_v = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = 120 + Vector2.SignedAngle(new Vector2(1, 0), relative_v);
        transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
    }
}
