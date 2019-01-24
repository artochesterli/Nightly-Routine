using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallsprites : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		int number = (Random.Range(0, 3));
		string s = "wall" + (number + 1).ToString() + "_resize";
		GetComponent<SpriteRenderer>().sprite=Resources.Load("Sprite/"+s,typeof(Sprite)) as Sprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
