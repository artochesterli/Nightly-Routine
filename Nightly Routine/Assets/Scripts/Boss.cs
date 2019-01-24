
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    public bool activate;
    public float horizontal_end;
    public float horizontal_begin;
    public float normal_height;
    public float dash_height;
    public float cooldown;

    private List<Vector3> relative_body_part_pos;
    private List<int> body_part_level_list = new List<int>();
    private List<GameObject> body_part;
    private bool just_activate;
    private bool move;
    private bool rotate;
    private bool shoot_bullet;
    private bool move_right;
    private bool using_skill;
    private const float horizontal_speed=8;
    private const float rotation_speed = 30;
    private const float skill_interval = 8;
    private const float center = 23;
    private const float regenerate_time = 2;
	// Use this for initialization
	void Start () {
        
        body_part = new List<GameObject>();
        relative_body_part_pos = new List<Vector3>();
        foreach(Transform child in transform)
        {
            body_part.Add(child.gameObject);
            relative_body_part_pos.Add(child.transform.localPosition*transform.localScale.x);
            body_part_level_list.Add(child.GetComponent<Enemy>().level);
        }
        cooldown = 0;
        move = true;
        rotate = true;
        shoot_bullet = true;
        just_activate = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (activate)
        {
            if (!just_activate)
            {
                just_activate = true;
                GetComponents<AudioSource>()[1].Play();
            }
            if (!using_skill)
            {
                cooldown -= Time.deltaTime;
                if (cooldown < 0 && Mathf.Abs(transform.position.x - center) < 3)
                {
                    cooldown = skill_interval;
                    if (!need_regenerate())
                    {
                        StartCoroutine(Storm());
                    }
                    else
                    {
                        float number = Random.Range(0, 100)/100.0f;
                        if (number > 0.3f)
                        {
                            StartCoroutine(Storm());
                        }
                        else
                        {
                            StartCoroutine(Regenerate());
                        }

                    }

                }
            }
            if (rotate)
            {
                transform.Rotate(0, 0, rotation_speed * Time.deltaTime);
                if (transform.position.x > horizontal_end && move_right)
                {
                    transform.position = new Vector3(horizontal_end, transform.position.y, transform.position.z);
                    move_right = false;
                }
                if (transform.position.x < horizontal_begin && !move_right)
                {
                    transform.position = new Vector3(horizontal_begin, transform.position.y, transform.position.z);
                    move_right = true;
                }
            }
            if (move)
            {
                if (move_right)
                {
                    transform.position += new Vector3(horizontal_speed, 0, 0) * Time.deltaTime;
                }
                else
                {
                    transform.position -= new Vector3(horizontal_speed, 0, 0) * Time.deltaTime;
                }
            }
           
            if (shoot_bullet)
            {
                for (int i = 0; i < body_part.Count; i++)
                {
                    if (body_part[i] != null && body_part[i].GetComponent<Enemy>().level != 1)
                    {
                        body_part[i].GetComponent<Enemy>().emit_bullet = true;
                    }
                }
                GetComponent<Enemy>().emit_bullet = true;
            }
            else
            {
                for (int i = 0; i < body_part.Count; i++)
                {
                    if (body_part[i] != null)
                    {
                        body_part[i].GetComponent<Enemy>().emit_bullet = false;
                    }
                }
                GetComponent<Enemy>().emit_bullet = false;
            }
        }

	}
    private bool need_regenerate()
    {
        bool result = false;
        for(int i = 0; i < body_part.Count; i++)
        {
            if (body_part[i] == null)
            {
                result = true;
            }
        }
        return result;
    }

    IEnumerator Storm()
    {
        using_skill = true;
        move = false;
        rotate = false;
        shoot_bullet = false;
        yield return new WaitForSeconds(1);
        for (int i = -2; i < 48; i++)
        {
            GameObject bullet = (GameObject)Instantiate(Resources.Load("Prefabs/flameBall"), new Vector3(i,167,0), Quaternion.AngleAxis(90, new Vector3(0, 0, 1)));
            bullet.transform.parent = transform.root.Find("Effects");
            GetComponent<Enemy>().bullets.Add(bullet);
            bullet.GetComponent<Bullet>().direction = new Vector2(0, 1);
            bullet.GetComponent<Bullet>().speed = 1.8f;
            bullet.GetComponent<Bullet>().trace_angle = 0;
            bullet.GetComponent<Bullet>().lifetime = 6;
        }
        float time = 0;
        bool x = true;
        while (time < 5)
        {
            float angle;
            if (x)
            {
                angle = 0;
                
            }
            else
            {
                angle = -9;
            }
            x = !x;
            GetComponents<AudioSource>()[0].Play();
            foreach(Transform child in transform)
            {
                child.GetComponent<AudioSource>().Play();
            }
            for (int i = 0; i < 10; i++)
            {
                Vector2 current_direction = Rotate(new Vector2(1,0),angle);
                GameObject bullet = (GameObject)Instantiate(Resources.Load("Prefabs/flameBall"), transform.position, Quaternion.AngleAxis(angle, new Vector3(0, 0, 1)));
                bullet.transform.parent = transform.root.Find("Effects");
                GetComponent<Enemy>().bullets.Add(bullet);
                bullet.GetComponent<Bullet>().direction = current_direction;
                bullet.GetComponent<Bullet>().speed = GetComponent<Enemy>().bullet_speed;
                bullet.GetComponent<Bullet>().trace_angle = 0;
                bullet.GetComponent<Bullet>().lifetime = 0;
                angle -= 18;
            }
            //float random_time = Random.Range(0.5f,1.5f);

            yield return new WaitForSeconds(1);
            time += 1f;
        }
        yield return new WaitForSeconds(1);
        move = true;
        rotate = true;
        shoot_bullet = true;
        using_skill = false;
    }

    IEnumerator Regenerate()
    {
        using_skill = true;
        move = false;
        List<int> body_part_index_list=new List<int>();
        
        for(int i = 0; i < body_part.Count; i++)
        {
            if (body_part[i] == null)
            {
                body_part_index_list.Add(i);
                
            }
        }
        int number = 2;
        if (body_part_index_list.Count < 2)
        {
            number = body_part_index_list.Count;
        }
        for(int i = 0; i < number; i++)
        {
            if (body_part_level_list[body_part_index_list[i]] == 1)
            {
                body_part[body_part_index_list[i]] = (GameObject)Instantiate(Resources.Load("Prefabs/Enemy level 1"),transform.position , new Quaternion(0, 0, 0, 0));
                body_part[body_part_index_list[i]].transform.localScale = new Vector3(0, 0, 0);
                
                body_part[body_part_index_list[i]].transform.parent = transform;
                //Quaternion q = transform.rotation;
                //transform.rotation = new Quaternion(0, 0, 0, 0);
                //body_part[body_part_index_list[i]].transform.position = transform.position + relative_body_part_pos[body_part_index_list[i]];
                body_part[body_part_index_list[i]].transform.localPosition = relative_body_part_pos[body_part_index_list[i]]/transform.localScale.x;
                //transform.rotation = q;
            }
            else
            {
                body_part[body_part_index_list[i]] = (GameObject)Instantiate(Resources.Load("Prefabs/Enemy level 2"), transform.position, new Quaternion(0, 0, 0, 0));
                body_part[body_part_index_list[i]].transform.parent = transform;
                Quaternion q = transform.rotation;
                //transform.rotation = new Quaternion(0, 0, 0, 0);
               // body_part[body_part_index_list[i]].transform.position = transform.position + relative_body_part_pos[body_part_index_list[i]];
                body_part[body_part_index_list[i]].transform.localPosition = relative_body_part_pos[body_part_index_list[i]] / transform.localScale.x;
                //transform.rotation = q;
            }
            float time = 0;
            while(time < regenerate_time)
            {
                if (body_part[body_part_index_list[i]]==null)
                {
                    break;
                }
                body_part[body_part_index_list[i]].transform.localScale = new Vector3(1, 1, 1) * time / regenerate_time;
                time += Time.deltaTime;
                yield return null;
            }
            if (body_part[body_part_index_list[i]] != null)
            {
                body_part[body_part_index_list[i]].transform.localScale = new Vector3(1, 1, 1);
            }
        }
        yield return new WaitForSeconds(2);
        //body_part_index_list.Clear();
        move = true;
        using_skill = false;
    }
    public Vector2 Rotate(Vector2 v, float degrees)
    {
        Vector2 temp = new Vector2(v.x, v.y);
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        temp.x = (cos * tx) - (sin * ty);
        temp.y = (sin * tx) + (cos * ty);
        return temp;
    }

}
