using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Interim : MonoBehaviour {

    public int level;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        Scene s = SceneManager.GetActiveScene();
        if (s.name.Equals("SampleScene"))
        {
            GameObject g= GameObject.Find("Bubble").gameObject;
            g.transform.position = GameObject.Find("Level " + level.ToString()).transform.Find("Check_Points").GetChild(0).position;
            Destroy(gameObject);
        }
	}
}
