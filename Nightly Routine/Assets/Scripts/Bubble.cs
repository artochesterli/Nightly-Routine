using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bubble : MonoBehaviour {

    public List<GameObject> Shock_wave_objects;
    public bool Camera_Follow;
    public bool Recieve_Input;
    private bool controllable;
    private bool shock_waving;
    private bool invulnerable;
    private bool charging;
    private float current_charging_time;
    private float total_time;
    private bool cooling_down;
    private const float pushing_force=2.5f;
    private const float Ac_resistance_factor = 1.85f;
    private const float NAc_resistance_factor = 3f;
    private const float Shock_Wave_cooldown = 0.2f;
    private const float Shock_Wave_time = 0.1f;
    private const float Shock_Wave_r_low = 1.5f;
    private const float Shock_Wave_r_high = 12.5f;
    private const float Charge_Shock_Wave_r_high = 12.5f;
    private const float Shock_Wave_Image_low = 1.4f;
    private const float Shock_Wave_image_high = 5.6f;
    private const float force_to_object = 10;
    private const float charge_force_to_object = 20;
    private const float force_to_self = 5;
    private const float charge_force_to_self = 10;
    private const float invulnerable_time=1;
    private const float charging_time=1f;
    private const float charging_limit_time = 2.5f;
    // Use this for initialization
    void Start () {
        controllable = true;
        shock_waving = false;
        invulnerable = false;
        current_charging_time = 0;
        transform.Find("Shock_Wave_Prepare").GetComponent<SpriteRenderer>().enabled = false;
        cooling_down = false;
        Camera_Follow = true;
        Recieve_Input = true;
    }
	
	// Update is called once per frame
	void Update () {
        total_time += Time.deltaTime;
        
        
        if (charging)
        {
            if (current_charging_time >= charging_time)
            {
                GameObject g = transform.Find("Shock_Wave_Prepare").gameObject;
                g.GetComponent<SpriteRenderer>().color = new Color(0,1,1, 1);
                float shake_length = 0.03f + (current_charging_time - charging_time) / (charging_limit_time - charging_time)*0.07f;
                g.transform.position =transform.position+ new Vector3(shake_length, 0, 0) * Mathf.Sin(total_time*50);
            }
            if (current_charging_time >= charging_limit_time)
            {
                if (charging)
                {
                    charging = false;
                    StartCoroutine(Shock_Wave(true));
                    GameObject g = transform.Find("Shock_Wave_Prepare").gameObject;
                    g.GetComponent<SpriteRenderer>().enabled = false;
                    g.GetComponent<SpriteRenderer>().color = new Color(1, 242f / 255f, 0, 1);
                    g.transform.position = transform.position;
                }
            }
            current_charging_time += Time.deltaTime;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        if (Input.GetMouseButtonDown(0)&&!shock_waving&&!cooling_down&&Recieve_Input)
        {
            transform.Find("Shock_Wave_Prepare").GetComponent<SpriteRenderer>().enabled = true;
            charging = true;
        }
        if (Input.GetMouseButtonUp(0)&&Recieve_Input)
        {
            GameObject g = transform.Find("Shock_Wave_Prepare").gameObject;
            g.GetComponent<SpriteRenderer>().color = new Color(1, 242f / 255f, 0, 1);
            g.GetComponent<SpriteRenderer>().enabled = false;
            
            g.transform.position = transform.position;
            if (charging)
            {
                if (current_charging_time < charging_time)
                {
                    StartCoroutine(Shock_Wave(false));
                }
                else
                {
                    StartCoroutine(Shock_Wave(true));
                }
                
            }
            charging = false;
            //StartCoroutine(Shock_Wave_Cool_Down());
        }

    }

    private IEnumerator Shock_Wave_Cool_Down()
    {
        cooling_down = true;
        yield return new WaitForSeconds(Shock_Wave_cooldown);
        cooling_down = false;
    }
    private void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 relative_v = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = 120 + Vector2.SignedAngle( new Vector2(1, 0),relative_v);
        transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        if (Input.GetKey(KeyCode.Space))
        {
            if (Recieve_Input)
            {
                if (!GetComponents<AudioSource>()[0].isPlaying)
                {
                    GetComponents<AudioSource>()[0].Play();
                }
                relative_v.Normalize();
                rb.AddForce(relative_v * pushing_force * 9.8f);
                rb.drag = Ac_resistance_factor;

            }
            else
            {
                GetComponents<AudioSource>()[0].Stop();
            }
        }
        else
        {
            rb.drag = NAc_resistance_factor;
            GetComponents<AudioSource>()[0].Stop();
        }
        if (Camera_Follow)
        {
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!Core_Controller.Going_to_check_point&&(collision.collider.gameObject.CompareTag("Deadly_Object")|| collision.collider.gameObject.CompareTag("Enemy")))
        {
            if (!Core_Controller.Going_to_check_point)
            {
                StartCoroutine(Camera.main.GetComponent<Core_Controller>().go_to_last_check_point(gameObject));
            }


        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!Core_Controller.Going_to_check_point && (collision.GetComponent<Collider2D>().gameObject.CompareTag("Deadly_Object")|| collision.GetComponent<Collider2D>().gameObject.CompareTag("Lightning_Ball")))
        {
            if (!Core_Controller.Going_to_check_point)
            {
                StartCoroutine(Camera.main.GetComponent<Core_Controller>().go_to_last_check_point(gameObject));
            }
        }

    }

    public void Shock_Wave_Trigger(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Blue_Ball") && !inlist(collision.GetComponent<Collider2D>().gameObject))
        {
            Shock_wave_objects.Add(collision.GetComponent<Collider2D>().gameObject);
            if (current_charging_time < charging_time)
            {
                StartCoroutine(Force_To_Object(collision.GetComponent<Collider2D>().gameObject,force_to_object));
                StartCoroutine(Force_To_Self(collision.GetComponent<Collider2D>().gameObject, force_to_self));
            }
            else
            {
                StartCoroutine(Force_To_Object(collision.GetComponent<Collider2D>().gameObject,charge_force_to_object));
                StartCoroutine(Force_To_Self(collision.GetComponent<Collider2D>().gameObject, charge_force_to_self));
            }
            
        }
    }


    IEnumerator Shock_Wave(bool charged)
    {
        if (charged)
        {
            GetComponents<AudioSource>()[1].Play();
            GetComponents<AudioSource>()[1].volume = 1;

        }
        else
        {
            GetComponents<AudioSource>()[1].Play();
            GetComponents<AudioSource>()[1].volume = 0.5f;
        }
        shock_waving = true;
        float time = 0;
        GameObject Shock_Wave = transform.Find("Shock_Wave").gameObject;
        Shock_Wave.GetComponent<CircleCollider2D>().enabled = true;
        Shock_Wave.GetComponent<CircleCollider2D>().radius= Shock_Wave_r_low;
        if (charged)
        {
            transform.Find("Shock_Wave_Effect_Charged").GetComponent<ParticleSystem>().Play();
        }
        else
        {
            transform.Find("Shock_Wave_Effect").GetComponent<ParticleSystem>().Play();
        }
        
        float r = Shock_Wave.GetComponent<CircleCollider2D>().radius;
        while (time < Shock_Wave_time)
        {
            time += Time.deltaTime;
            if (charged)
            {
                Shock_Wave.GetComponent<CircleCollider2D>().radius = r + (Charge_Shock_Wave_r_high - Shock_Wave_r_low) * (time / Shock_Wave_time);
            }
            else
            {
                Shock_Wave.GetComponent<CircleCollider2D>().radius = r + (Shock_Wave_r_high - Shock_Wave_r_low) * (time / Shock_Wave_time);
            }
            yield return null;
        }
        Shock_Wave.GetComponent<CircleCollider2D>().radius = Shock_Wave_r_high;
        Shock_Wave.GetComponent<CircleCollider2D>().enabled = false;
        Shock_Wave.GetComponent<CircleCollider2D>().radius = Shock_Wave_r_low;
        Shock_wave_objects.Clear();
        current_charging_time = 0;
        shock_waving = false;
    }

    public IEnumerator Force_To_Object(GameObject collider,float force_to_object)
    {
        GameObject.Find("Bubble").GetComponent<Bubble>().Shock_wave_objects.Add(collider);
        Vector2 direction = collider.transform.position - transform.position;
        direction.Normalize();
        Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * force_to_object * 9.8f*7);
        yield return null;

    }

    public IEnumerator Force_To_Self(GameObject collider, float force_to_self)
    {
        Vector2 direction = transform.position - collider.transform.position;
        direction.Normalize();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * force_to_self * 9.8f*7);
        yield return null;
    }

    private bool inlist(GameObject g)
    {
        bool inl = false;
        List<GameObject> list = GameObject.Find("Bubble").GetComponent<Bubble>().Shock_wave_objects;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == g)
            {
                inl = true;
            }
        }
        return inl;
    }
}
