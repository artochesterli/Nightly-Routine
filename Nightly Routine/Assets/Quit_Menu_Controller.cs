using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Quit_Menu_Controller : MonoBehaviour {

    private GameObject Menu;
    private bool open;
    // Use this for initialization
    private void Awake()
    {
        Menu = transform.GetChild(1).gameObject;
        Menu.SetActive(false);
        open = false;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (open)
            {
                open = false;
                Menu.SetActive(false);
            }
            else
            {
                open = true;
                Menu.SetActive(true);
            }
        }
	}

    public void No_Click()
    {
        open = false;
        Menu.SetActive(false);
    }

    public void Yes_Click()
    {
        StartCoroutine(Camera.main.transform.GetChild(0).GetComponent<Mask>().To_Filled(false));
        SceneManager.LoadScene(0);
    }
}
