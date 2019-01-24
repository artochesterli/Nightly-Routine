using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour {

    private const float Transparent_time = 1;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    public IEnumerator To_Transparent(bool black)
    {
        float number;
        if (black)
        {
            number = 0;
        }
        else
        {
            number = 1;
        }
        Cursor.visible = false;
        GetComponent<SpriteRenderer>().color = new Color(number, number, number, 1);
        float time = 0;
        while (time < Transparent_time)
        {
            GetComponent<SpriteRenderer>().color = new Color(number, number, number, 1 - time / Transparent_time);
            time += Time.deltaTime;
            yield return null;
        }
        GetComponent<SpriteRenderer>().color = new Color(number, number, number, 0);
        Cursor.visible = true;
    }

    public IEnumerator To_Filled(bool black)
    {
        float number;
        if (black)
        {
            number = 0;
        }
        else
        {
            number = 1;
        }
        Cursor.visible = false;
        GetComponent<SpriteRenderer>().color = new Color(number, number, number, 0);
        float time = 0;
        while (time < Transparent_time)
        {
            GetComponent<SpriteRenderer>().color = new Color(number, number, number, time / Transparent_time);
            time += Time.deltaTime;
            yield return null;
        }
        GetComponent<SpriteRenderer>().color = new Color(number, number, number, 1);
        Cursor.visible = true;
    }
}
