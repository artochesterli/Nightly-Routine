using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Button_Controller : MonoBehaviour {


    public static List<GameObject> button_list;

    // Use this for initialization
    void Start () {
        StartCoroutine(Camera.main.transform.GetChild(0).GetComponent<Mask>().To_Transparent(false));
        button_list = new List<GameObject>();
        GameObject g = GameObject.Find("Canvas").transform.Find("Buttons").gameObject;
        foreach (Transform child in g.transform)
        {
            button_list.Add(child.gameObject);
        }
        for (int i = 2; i < 7; i++)
        {
            button_list[i].SetActive(false);
        }
        if (GetComponent<Save_Data>().Level_pass[0])
        {
            button_list[3].GetComponent<Button>().interactable = true;
        }
        else
        {
            button_list[3].GetComponent<Button>().interactable = false;
        }
        if (GetComponent<Save_Data>().Level_pass[1])
        {
            button_list[4].GetComponent<Button>().interactable = true;
        }
        else
        {
            button_list[4].GetComponent<Button>().interactable = false;
        }
        if (!GetComponent<Save_Data>().hidden_level_showed)
        {
            button_list[5].transform.GetChild(0).GetComponent<Text>().text = "?";
            button_list[5].GetComponent<Button>().interactable = false;
            bool all_ok = true;
            for (int i = 0; i < 3; i++)
            {
                if (GetComponent<Save_Data>().Star_collection[i])
                {
                    button_list[5].transform.GetChild(1).transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load("Sprite/star_new", typeof(Sprite)) as Sprite;
                    button_list[5].transform.GetChild(1).transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
                else
                {
                    all_ok = false;
                    button_list[5].transform.GetChild(1).transform.GetChild(i).GetComponent<Image>().sprite = null;
                    button_list[5].transform.GetChild(1).transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                }
            }
            if (all_ok)
            {
                StartCoroutine(activate_casual_level());
            }
        }
        else
        {
            button_list[5].GetComponent<Button>().interactable = true;
            button_list[5].transform.GetChild(0).GetComponent<Text>().text = "Casual Level";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator activate_casual_level()
    {
        
        float time = 0;
        float fade_time = 2;
        while (time < fade_time)
        {
            for (int i = 0; i < 3; i++)
            {
                 button_list[5].transform.GetChild(1).transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1-time/fade_time);
            }
            button_list[5].GetComponent<Image>().color = new Color(1, 1, 1, 1 - time / fade_time);
            button_list[5].transform.GetChild(0).GetComponent<Text>().color= new Color(0, 0, 0, 1 - time / fade_time);
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        button_list[5].transform.GetChild(0).GetComponent<Text>().text = "Casual Level";
        GetComponent<Save_Data>().hidden_level_showed = true;
        GetComponent<Save_Data>().Write_save();
        button_list[5].GetComponent<Button>().interactable = true;
        while (time < fade_time)
        {
            for (int i = 0; i < 3; i++)
            {
                button_list[5].transform.GetChild(1).transform.GetChild(i).GetComponent<Image>().sprite = null;
                button_list[5].transform.GetChild(1).transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
            button_list[5].GetComponent<Image>().color = new Color(1, 1, 1, time / fade_time);
            button_list[5].transform.GetChild(0).GetComponent<Text>().color = new Color(0, 0, 0, time / fade_time);
            time += Time.deltaTime;
            yield return null;
        }
        
        button_list[5].transform.GetChild(0).GetComponent<Text>().text = "Casual Level";
    }
}
