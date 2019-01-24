using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Quit_Menu_Buttons : MonoBehaviour {


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void No_Click()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void Yes_Click()
    {
        StartCoroutine(go_to_title());
    }

    IEnumerator go_to_title()
    {
        transform.parent.GetComponent<Image>().enabled = false;
        transform.parent.GetChild(0).GetComponent<Text>().enabled = false;
        transform.parent.GetChild(1).GetComponent<Image>().enabled = false;
        transform.parent.GetChild(1).GetChild(0).GetComponent<Text>().enabled = false;
        transform.parent.GetChild(2).GetComponent<Image>().enabled = false;
        transform.parent.GetChild(2).GetChild(0).GetComponent<Text>().enabled = false;
        yield return StartCoroutine(Camera.main.transform.GetChild(0).GetComponent<Mask>().To_Filled(false));
        SceneManager.LoadScene(0);
    }
}
