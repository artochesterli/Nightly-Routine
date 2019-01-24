using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour {

    public GameObject Connected_level;
    public GameObject Next_Level;
    private bool Ball_ok;
    private bool finish;
    private const float rotation_speed = 180;
    private float show_text_time = 2;
    private float text_fade_delay = 3;
    // Use this for initialization
    void Start () {
        Ball_ok = false;
        finish = false;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, rotation_speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Blue_Ball"))
        {
            Destroy(collision.GetComponent<Collider2D>().gameObject);
            Ball_ok = true;
        }
        if (!finish && collision.GetComponent<Collider2D>().CompareTag("Avatar"))
        {
            if (!Ball_ok)
            {
                StartCoroutine(Show_Tutor_Text());
            }
            else
            {
                finish = true;
                StartCoroutine(Go_to_next_level());
            }
            
        }
    }

    IEnumerator Go_to_next_level()
    {
        yield return StartCoroutine(Camera.main.transform.GetChild(0).GetComponent<Mask>().To_Filled(false));
        if (Connected_level != null)
        {
            Camera.main.GetComponent<Save_Data>().Level_pass[int.Parse(Connected_level.name.Substring(Connected_level.name.Length - 1, 1)) - 1] = true;
            Camera.main.GetComponent<Save_Data>().Write_save();
        }
        if (Next_Level != null)
        {
            Core_Controller.Bubble.transform.position = Next_Level.transform.Find("Check_Points").GetChild(0).position;
            yield return StartCoroutine(Camera.main.transform.GetChild(0).GetComponent<Mask>().To_Transparent(false));
        }
        else
        {
            SceneManager.LoadScene(0);
        }   
    }


    IEnumerator Show_Tutor_Text()
    {
        yield return StartCoroutine(Show_Text());
        yield return StartCoroutine(Fade_Text());
    }

    IEnumerator Show_Text()
    {
        Core_Controller.Tutorial_Text.GetComponent<Text>().color = new Color(1, 1, 1, 0);
        Core_Controller.Tutorial_Text.GetComponent<Text>().text = "Bring the spirit";
        float time = 0;
        while (time < show_text_time)
        {
            Core_Controller.Tutorial_Text.GetComponent<Text>().color = new Color(1, 1, 1, time / show_text_time);
            time += Time.deltaTime;
            yield return null;
        }
        Core_Controller.Tutorial_Text.GetComponent<Text>().color = new Color(1, 1, 1, 1);
    }

    IEnumerator Fade_Text()
    {

        Core_Controller.Tutorial_Text.GetComponent<Text>().color = new Color(1, 1, 1, 1);
        float time = 0;
        while (time < show_text_time)
        {
            Core_Controller.Tutorial_Text.GetComponent<Text>().color = new Color(1, 1, 1, 1 - time / show_text_time);
            time += Time.deltaTime;
            yield return null;
        }
        Core_Controller.Tutorial_Text.GetComponent<Text>().color = new Color(1, 1, 1, 0);
        Core_Controller.Tutorial_Text.GetComponent<Text>().text = "";
    }
}
