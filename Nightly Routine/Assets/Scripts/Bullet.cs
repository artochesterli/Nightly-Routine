using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Vector2 direction;
    public float speed;
    public float trace_angle;
    public float lifetime;
    private float time;
	// Use this for initialization
	void Start () {
        time = 0;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (lifetime>0&&time > lifetime)
        {
            StartCoroutine(Destroy_Self());
        }

        float offset_angle = Vector2.SignedAngle(Core_Controller.Bubble.transform.position - transform.position, direction);
        if (offset_angle != 0)
        {

            if (offset_angle > 0)
            {
                direction= Rotate_Vector2(direction, -Time.deltaTime * trace_angle * Mathf.Deg2Rad);
            }
            else
            {
                direction= Rotate_Vector2(direction, Time.deltaTime * trace_angle * Mathf.Deg2Rad);
            }
            
        }
        float angle = Mathf.Rad2Deg * Mathf.Atan(direction.y / direction.x);
        if (direction.y < 0 && direction.x < 0 || direction.y > 0 && direction.x < 0)
        {
            angle -= 180;
        }
        transform.rotation=Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        transform.position += speed * new Vector3(direction.x, direction.y, 0)*Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    private Vector2 Rotate_Vector2(Vector2 v,float angle)
    {
        v.x = v.x * Mathf.Cos(angle) -v.y *Mathf.Sin(angle);
        v.y = v.x * Mathf.Sin(angle) +v.y *Mathf.Cos(angle);
        return v;
    }

    IEnumerator Destroy_Self()
    {
        Vector3 ori = new Vector3(transform.localScale.x,transform.localScale.y,transform.localScale.z);
        float time = 0;
        while (time < 0.3f)
        {
            transform.localScale = ori * (1 - time / 0.3f);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
