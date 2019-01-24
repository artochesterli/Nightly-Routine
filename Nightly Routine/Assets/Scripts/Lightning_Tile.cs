using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning_Tile : MonoBehaviour {

    public float interval;
    public float lightning_time;
    public int number_of_lightning_ball;
    public Vector2 lightning_direction;
    private int anim_index;
    private const float emit_interval = 0.1f;
	// Use this for initialization
	void Start () {
        int number = (Random.Range(1, 8));
        anim_index = number;
        string s = "lightning_wall" + number.ToString();
        GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprite/" + s, typeof(Sprite)) as Sprite;
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        StartCoroutine(Emit_Lightning());
        
    }
	
	// Update is called once per frame
	void Update () {
        /*string s = "lightning_wall" + anim_index.ToString();
        GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprite/" + s, typeof(Sprite)) as Sprite;
        anim_index = (anim_index + 1) % 8 + 1;*/
    }


    IEnumerator Emit_Lightning()
    {
        if (lightning_time == 0)
        {
            yield break;
        }
        if (lightning_time == Mathf.Infinity)
        {
            int count = 0;
            while (count < number_of_lightning_ball)
            {
                count++;
                GameObject g = (GameObject)Instantiate(Resources.Load("Prefabs/Lightning_Ball"), transform.position + count * new Vector3(lightning_direction.x, lightning_direction.y, 0), new Quaternion(0, 0, 0, 0));
                g.transform.parent = transform;
            }
            yield break;
        }
        bool cooldown = true;
        while (true)
        {
            if (cooldown&&interval>0)
            {
                yield return new WaitForSeconds(interval);
                cooldown = false;
            }
            else
            {
                float time = 0;
                int count = 0;
                while (time < lightning_time)
                {
                    while (count<number_of_lightning_ball)
                    {
                        count++;
                        GameObject g = (GameObject)Instantiate(Resources.Load("Prefabs/Lightning_Ball"), transform.position + count * new Vector3(lightning_direction.x, lightning_direction.y, 0), new Quaternion(0, 0, 0, 0));
                        g.transform.parent = transform;
                    }
                    time += Time.deltaTime;
                    yield return null;
                }
                foreach(Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
                cooldown = true;
            }
            
        }
    }
}
