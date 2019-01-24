using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_in_casual_level : MonoBehaviour {

    public bool activated;
    public bool finish;
    public List<Vector3> Move_Seq;
    public float speed;
    private const float rotation_speed = 90;
	// Use this for initialization
	void Start () {
        finish = false;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!finish && activated)
        {
            finish = true;
            StartCoroutine(move());
        }
        if (activated)
        {
            transform.Rotate(0, 0, rotation_speed * Time.deltaTime);
        }
    }

    IEnumerator move()
    {
        yield return new WaitForSeconds(1);
        for(int i = 0; i < Move_Seq.Count; i++)
        {
            Vector2 direction = Move_Seq[i] - transform.localPosition;
            direction.Normalize();
            while (Vector2.Dot(direction , Move_Seq[i] - transform.localPosition) > 0)
            {
                Vector2 v = direction * speed * Time.deltaTime;
                transform.position += new Vector3(v.x, v.y, 0);
                yield return null;
            }
            transform.localPosition = Move_Seq[i];
        }
        Destroy(gameObject);
    }
}
