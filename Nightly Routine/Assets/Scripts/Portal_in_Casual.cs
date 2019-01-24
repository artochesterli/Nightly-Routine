using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_in_Casual : MonoBehaviour {

    private bool finish;
    private const float rotation_speed = 60;
    // Use this for initialization
    void Start () {
        finish = false;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, rotation_speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!finish&&collision.GetComponent<Collider2D>().CompareTag("Avatar"))
        {
            finish = true;
            collision.GetComponent<Collider2D>().gameObject.GetComponent<Collider2D>().enabled=false;
            StartCoroutine(Go_to_next_level());
        }
    }
    IEnumerator Go_to_next_level()
    {
        yield return StartCoroutine(Camera.main.transform.GetChild(0).GetComponent<Mask>().To_Filled(false));
        SceneManager.LoadScene(0);
    }
}
