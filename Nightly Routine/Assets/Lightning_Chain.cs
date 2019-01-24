using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning_Chain : MonoBehaviour {

    public float length;
    public float speed;
    public Vector3 begin;
    public Vector3 end;
	// Use this for initialization
	void Start () {
        if (transform.GetChild(0).transform.position == transform.position)
        {
            transform.GetChild(0).transform.position += new Vector3(0, (length + 1) / 2, 0);
            transform.GetChild(1).transform.position -= new Vector3(0, (length + 1) / 2, 0);
        }
        StartCoroutine(Move());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Move()
    {
        bool toend = true;
        while (true)
        {
            if (toend)
            {
                Vector2 direction = end - begin;
                direction.Normalize();
                transform.position += speed * new Vector3(direction.x,direction.y,0)* Time.deltaTime;
                if (Vector2.Dot(transform.position - transform.root.position - end, end - begin) > 0)
                {
                    toend = false;
                    transform.position = transform.root.position+end;
                }
            }
            else
            {
                Vector2 direction = begin-end;
                direction.Normalize();
                transform.position += speed * new Vector3(direction.x, direction.y, 0) * Time.deltaTime;
                if (Vector2.Dot(transform.position- transform.root.position - begin, begin - end) > 0)
                {
                    toend = true;
                    transform.position = transform.root.position + begin;
                }
            }
            yield return null;
        }
    }
}
