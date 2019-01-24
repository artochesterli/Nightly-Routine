using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int level;
    public int hit_points;
    public float speed;
    public bool emit_bullet;
    public float bullet_interval;
    public float rage_bullet_interval;
    public float activate_dis;
    public float bullet_speed;
    public float bullet_trace_angle;
    public float bullet_lifetime;
    public float destroy_speed;
    public List<Vector2> move_seq;
    public float rage_interval;
    public float rage_time;
    public bool rage;
    private float bullet_time_count;
    private const float pause_time = 0.5f;
    private const float rotation_speed = 30;
    public List<GameObject> bullets;
    
	// Use this for initialization
	void Start () {
        
        bullets = new List<GameObject>();
        bullet_time_count = bullet_interval;
        if (move_seq.Count >= 2)
        {
            StartCoroutine(move());
        }
        if (rage_interval >= 0)
        {
            StartCoroutine(rage_switch());
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, rotation_speed * Time.deltaTime);
        if (rage)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        if (Vector2.Distance(transform.position, Core_Controller.Bubble.transform.position) < activate_dis&&emit_bullet)
        {
            bullet_time_count += Time.deltaTime;
            float interval;
            if (rage)
            {
                interval = rage_bullet_interval;
            }
            else
            {
                interval = bullet_interval;
            }
            if (bullet_time_count >= interval)
            {
                bullet_time_count = 0;
                Vector2 direction = Core_Controller.Bubble.transform.position - transform.position;
                direction.Normalize();
                float angle =  Mathf.Rad2Deg * Mathf.Atan(direction.y / direction.x);
                if (direction.y < 0 && direction.x < 0|| direction.y > 0 && direction.x < 0)
                {
                    angle -= 180;
                }
                string s;
                if (level == 3)
                {
                    s = "flameBall_trace";
                }
                else
                {
                    s = "flameBall";
                }
                GetComponent<AudioSource>().Play();
                GameObject bullet = (GameObject)Instantiate(Resources.Load("Prefabs/"+s), transform.position, Quaternion.AngleAxis(angle, new Vector3(0, 0, 1)));
                //bullet.transform.parent = GameObject.Find("Level " + Core_Controller.current_level.ToString()).transform.Find("Effects");
                bullet.transform.parent = transform.root.Find("Effects");
                bullets.Add(bullet);
                bullet.GetComponent<Bullet>().direction = direction;
                bullet.GetComponent<Bullet>().speed = bullet_speed;
                bullet.GetComponent<Bullet>().trace_angle = bullet_trace_angle;
                bullet.GetComponent<Bullet>().lifetime = bullet_lifetime;
            }
        }
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Blue_Ball"))
        {
            
            float angle = Mathf.Deg2Rad* Vector2.Angle(collision.relativeVelocity, transform.position - collision.collider.transform.position);
            Vector2 relative_speed = collision.relativeVelocity * Mathf.Cos(angle);
            if (relative_speed.magnitude > destroy_speed)
            {
                Destroy_Self();
            }
            
        }
    }

    public void Destroy_Self()
    {
        if (rage)
        {
            GameObject ex = (GameObject)Instantiate(Resources.Load("Prefabs/Explosion"), transform.position, new Quaternion(0, 0, 0, 0));
            ex.transform.parent = GameObject.Find("Level " + Core_Controller.current_level.ToString()).transform.Find("Effects");
        }
        else
        {
            GameObject ex = (GameObject)Instantiate(Resources.Load("Prefabs/Normal_Death"), transform.position, new Quaternion(0, 0, 0, 0));
            ex.transform.parent = GameObject.Find("Level " + Core_Controller.current_level.ToString()).transform.Find("Effects");
        }
        for(int i = 0; i < bullets.Count; i++)
        {
            Destroy(bullets[i]);
        }
        bullets.Clear();
        Destroy(gameObject);
    }
    IEnumerator move()
    {
        int index = 1;
        bool increase = true;
        while (true)
        {
            Vector2 direction = move_seq[index] - new Vector2(transform.position.x-transform.parent.parent.position.x,transform.position.y-transform.parent.parent.position.y);
            direction.Normalize();
            while (Vector2.Dot(direction, move_seq[index] - new Vector2(transform.position.x - transform.parent.parent.position.x, transform.position.y - transform.parent.parent.position.y)) > 0)
            {
                transform.position += speed * new Vector3(direction.x,direction.y,0)*Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(pause_time);
            transform.position = new Vector3(move_seq[index].x+transform.parent.parent.position.x, move_seq[index].y+ transform.parent.parent.position.y, 0);
            if (index == move_seq.Count - 1)
            {
                increase = false;
            }
            if (index == 0)
            {
                increase = true;
            }
            if (increase)
            {
                index++;
            }
            else
            {
                index--;
            }
        }
    }

    IEnumerator rage_switch()
    {
        rage = false;
        while (true)
        {
            if (rage_interval == 0)
            {
                rage = true;
                yield break;
            }
            else
            {
                if (rage)
                {
                    yield return new WaitForSeconds(rage_time);
                    rage = false;
                }
                else
                {
                    yield return new WaitForSeconds(rage_interval);
                    rage = true;
                }
            }
            

        }
    }
}
