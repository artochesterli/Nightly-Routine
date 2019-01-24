using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour {


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Exit_Click()
    {
        Application.Quit();
    }

    public void Play_Click()
    {
        for(int i = 0; i < 2; i++)
        {
            Button_Controller.button_list[i].SetActive(false);
        }
        for (int i = 2; i < 7; i++)
        {
            Button_Controller.button_list[i].SetActive(true);
        }
    }

    public void Back_Click()
    {
        for (int i = 0; i < 2; i++)
        {
            Button_Controller.button_list[i].SetActive(true);
        }
        for (int i = 2; i < 7; i++)
        {
            Button_Controller.button_list[i].SetActive(false);
        }
    }

    public void Level_1_Click()
    {
        StartCoroutine(Go_to_level(1));

    }

    public void Level_2_Click()
    {
        StartCoroutine(Go_to_level(2));

    }

    public void Level_3_Click()
    {
        StartCoroutine(Go_to_level(3));
    }

    public void Casual_Level_Click()
    {
        StartCoroutine(Go_to_level(4));
    }

    IEnumerator Go_to_level(int level)
    {
        GameObject.Find("Interim").GetComponent<Interim>().level = level;
        yield return StartCoroutine(Camera.main.transform.GetChild(0).GetComponent<Mask>().To_Filled(true));
        SceneManager.LoadScene(1);
    }
}
