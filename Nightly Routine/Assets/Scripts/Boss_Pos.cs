
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Pos : MonoBehaviour {

    private bool Avatar_Enter;
    private bool Ball_Enter;
    private GameObject Avatar;
    private bool activated;
    private bool finished;
    private bool centered;
    private GameObject connected_door;
    public GameObject connected_boss;
    // Use this for initialization
    void Start () {
        Avatar = GameObject.Find("Bubble").gameObject;
        activated = false;
        connected_door = transform.GetChild(0).gameObject;
        connected_door.SetActive(false);
        finished = false;
        centered = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!finished && connected_door == null)
        {
            finished = true;
            StartCoroutine(Camera_Move(true));
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Avatar"))
        {
            Avatar_Enter = false;
        }
        if (collision.GetComponent<Collider2D>().CompareTag("Blue_Ball"))
        {
            Ball_Enter = false;
        }
        check_activate();
    }

    private void check_activate()
    {
        if (Avatar_Enter && Ball_Enter)
        {
            activated = true;
            if (!centered)
            {
                centered = true;
                StartCoroutine(Camera_Move(false));
            }
        }
        else
        {
            activated = false;
        }
    }

    IEnumerator Camera_Move(bool to_avatar)
    {
        Camera.main.GetComponent<AudioSource>().Stop();
        if (Avatar == null)
        {
            Avatar= GameObject.Find("Bubble").gameObject;
        }
        float moving_time = 2;
        if (to_avatar)
        {
            Avatar.GetComponent<Bubble>().Recieve_Input = false;
            float time = 0;
            while (time < moving_time)
            {
                Camera.main.transform.position += (Avatar.transform.position - transform.position) * Time.deltaTime / moving_time;
                time += Time.deltaTime;
                yield return null;
            }
            Camera.main.transform.position = Avatar.transform.position+new Vector3(0,0,-10);
            Avatar.GetComponent<Bubble>().Camera_Follow = true;
            Avatar.GetComponent<Bubble>().Recieve_Input = true;
            Destroy(gameObject);
        }
        else
        {
            Avatar.GetComponent<Bubble>().Recieve_Input = false;
            Avatar.GetComponent<Bubble>().Camera_Follow = false;
            connected_door.SetActive(true);
            Vector3 begin_point = Avatar.transform.position;
            float time = 0;
            while (time < moving_time)
            {
                Camera.main.transform.position += (transform.position - begin_point) * Time.deltaTime / moving_time;
                time += Time.deltaTime;
                yield return null;
            }
            Camera.main.transform.position = transform.position+new Vector3(0,0,-10);
            Avatar.GetComponent<Bubble>().Recieve_Input = true;
            connected_boss.GetComponent<Boss>().activate = true;
        }
        
    }
}
