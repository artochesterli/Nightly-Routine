using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Point : MonoBehaviour {


    public int Level;
    public int index;
    public bool need_ball;
    private bool Avatar_Enter;
    private bool Ball_Enter;
    private bool Activated;
    private Vector3 original_scale;
    private const float shake_low_bound = 0.8f;
    private const float shake_speed = 2;
	// Use this for initialization
	void Start () {
        Avatar_Enter = false;
        Ball_Enter = false;
        Activated = false;
        original_scale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        if (Activated)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 0);
            transform.localScale = original_scale*(1-shake_low_bound)/2 * Mathf.Sin(Time.time * shake_speed)+original_scale*(shake_low_bound+(1-shake_low_bound)/2);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponent<Collider2D>().CompareTag("Avatar"))
        {
            Avatar_Enter = true;
        }
        if (collision.GetComponent<Collider2D>().CompareTag("Blue_Ball"))
        {
            Ball_Enter = true;
        }
        check_activate();
    }

    private void check_activate()
    {
        
        if (need_ball)
        {
            if (Avatar_Enter && Ball_Enter&&(Core_Controller.last_check_point==null||Core_Controller.last_check_point.GetComponent<Check_Point>().Level<Level|| Core_Controller.last_check_point.GetComponent<Check_Point>().index<index))
            {
                Activated = true;
                Core_Controller.last_check_point = gameObject;
                Core_Controller.current_level = Level;
                Destroy(Core_Controller.copying_level);
                Core_Controller.copying_level = Instantiate(GameObject.Find("Level " + Level.ToString()).gameObject);
                Core_Controller.copying_level.name = "Level " + Level.ToString();
                Core_Controller.copying_level.SetActive(false);
                
            }
        }
        else
        {
            if (Avatar_Enter && (Core_Controller.last_check_point == null || Core_Controller.last_check_point.GetComponent<Check_Point>().Level < Level || Core_Controller.last_check_point.GetComponent<Check_Point>().index < index))
            {
                Activated = true;
                Core_Controller.current_level = Level;
                Core_Controller.last_check_point = gameObject;
                Destroy(Core_Controller.copying_level);
                Core_Controller.copying_level = Instantiate(GameObject.Find("Level " + Level.ToString()).gameObject);
                Core_Controller.copying_level.name = "Level " + Level.ToString();
                Core_Controller.copying_level.SetActive(false);
            }
        }
    }

}
