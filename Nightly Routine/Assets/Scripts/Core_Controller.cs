using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_Controller : MonoBehaviour {

    public static GameObject Bubble;
    public static int current_level;
    public static GameObject last_check_point;
    public static GameObject copying_level;
    public static bool Going_to_check_point;
    public static GameObject Tutorial_Text;
    public static List<Sprite> lightning_sprite_list;
    public static List<string> Tutor_list;
    public static bool change_tutorial_text;
    // Use this for initialization
    private void Awake()
    {
        Tutor_list = new List<string>();
        change_tutorial_text = false;
    }
    void Start () {
        
        lightning_sprite_list = new List<Sprite>();
        for (int i = 0; i < 5; i++)
        {
            lightning_sprite_list.Add(Resources.Load("Sprite/lightning2_" + i.ToString(), typeof(Sprite)) as Sprite);
        }
        current_level = 2;
        Bubble = GameObject.Find("Bubble").gameObject;
        last_check_point = null;
        Going_to_check_point = false;
        Tutorial_Text = GameObject.Find("Canvas").transform.Find("Tutorial_Text").gameObject;
        StartCoroutine(transform.GetChild(0).GetComponent<Mask>().To_Transparent(true));
	}
	
	// Update is called once per frame
	void Update () {

	}

    

    public IEnumerator go_to_last_check_point(GameObject dead_object)
    {
        Camera.main.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        if (!Camera.main.GetComponent<AudioSource>().isPlaying)
        {
            Camera.main.GetComponent<AudioSource>().Play();
        }
        Going_to_check_point = true;
        Bubble.GetComponent<Bubble>().Recieve_Input = false;
        Bubble.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GameObject original_level = GameObject.Find(copying_level.name).gameObject;

        if (!dead_object.CompareTag("Avatar"))
        {
            yield return StartCoroutine(Camera_Move(Camera.main.transform.position, dead_object.transform.position, 0.5f));
        }
        GameObject effect=(GameObject)Instantiate(Resources.Load("Prefabs/AvatarDeath"), dead_object.transform.position, new Quaternion(0, 0, 0, 0));
        dead_object.transform.position += new Vector3(0, 0, -100);
        dead_object.GetComponent<Collider2D>().enabled = false;
        //Bubble.SetActive(false);
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(Camera.main.gameObject.transform.GetChild(0).GetComponent<Mask>().To_Filled(true));
        copying_level.SetActive(true);
        
       

        if (last_check_point.GetComponent<Check_Point>().need_ball)
        {
            GameObject blue_ball = copying_level.transform.Find("Blue_Ball").gameObject;
            Destroy(blue_ball);
            GameObject new_ball=(GameObject) Instantiate(Resources.Load("Prefabs/Blue_Ball"), last_check_point.transform.position, new Quaternion(0, 0, 0, 0));
            new_ball.name = "Blue_Ball";
            new_ball.transform.parent = copying_level.transform;
        }
        copying_level = null;
        Destroy(Bubble);
        Going_to_check_point = false;
        Bubble = (GameObject)Instantiate(Resources.Load("Prefabs/Bubble"), last_check_point.transform.position, new Quaternion(0, 0, 0, 0));
        Bubble.name = "Bubble";
        Destroy(effect);
        Destroy(original_level);
        yield return StartCoroutine(Camera.main.gameObject.transform.GetChild(0).GetComponent<Mask>().To_Transparent(true));
        last_check_point = null;
        copying_level = null;
        
        
    }

    IEnumerator Camera_Move(Vector3 Start,Vector3 End,float move_time)
    {
        Bubble.GetComponent<Bubble>().Camera_Follow = false;
        if (Start.x == End.x && Start.y == End.y)
        {
            yield break;
        }
        float time = 0;
        Camera.main.transform.position = Start;
        while (time < move_time)
        {
            Camera.main.transform.position += (End - Start) * Time.deltaTime / move_time;
            time += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = End-new Vector3(0,0,10);
    }
}
