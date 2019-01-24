using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning_Ball : MonoBehaviour {

    private const float bigger_time = 0.2f;
    // Use this for initialization
    private void Awake()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }
    void Start () {
        StartCoroutine(Getting_Big());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Getting_Big()
    {
        float time = 0;
        while (time < bigger_time)
        {
            transform.localScale = new Vector3(1,1,1)/transform.parent.localScale.x * time / bigger_time;
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = new Vector3(1, 1, 1) / transform.parent.localScale.x;
    }
}
