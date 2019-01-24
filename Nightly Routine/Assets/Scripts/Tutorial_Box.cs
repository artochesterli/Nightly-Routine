using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial_Box : MonoBehaviour {

    public string text;
    private float show_text_time = 2;
    private float text_fade_delay = 3;
    private bool showed;
    private bool breaked;
	// Use this for initialization
	void Start () {
        showed = false;
        breaked = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!showed&&collision.GetComponent<Collider2D>().gameObject.CompareTag("Avatar"))
        {
            showed = true;
            StartCoroutine(Show_Tutor_Text());
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
        Core_Controller.Tutorial_Text.GetComponent<Text>().text = text;
        float time = 0;
        while (time < show_text_time)
        {
            if (Core_Controller.Tutorial_Text.GetComponent<Text>().text!=text)
            {
                yield break;
            }
            Core_Controller.Tutorial_Text.GetComponent<Text>().color = new Color(1, 1, 1, time/show_text_time);
            time += Time.deltaTime;
            yield return null;
        }
        Core_Controller.Tutorial_Text.GetComponent<Text>().color = new Color(1, 1, 1, 1);
    }

    IEnumerator Fade_Text()
    {
        
        yield return new WaitForSeconds(text_fade_delay);
        if (Core_Controller.Tutorial_Text.GetComponent<Text>().text != text)
        {
            yield break;
        }
        Core_Controller.Tutorial_Text.GetComponent<Text>().color = new Color(1, 1, 1, 1);
        float time = 0;
        while (time < show_text_time)
        {
            if (Core_Controller.Tutorial_Text.GetComponent<Text>().text != text)
            {
                yield break;
            }
            Core_Controller.Tutorial_Text.GetComponent<Text>().color = new Color(1, 1, 1, 1-time / show_text_time);
            time += Time.deltaTime;
            yield return null;
        }
        Core_Controller.Tutorial_Text.GetComponent<Text>().color = new Color(1, 1, 1, 0);
        Core_Controller.Tutorial_Text.GetComponent<Text>().text = "";
    }


}
