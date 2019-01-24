using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
public class Save_Data : MonoBehaviour {

    public List<bool> Level_pass;
    public List<bool> Star_collection;
    public bool hidden_level_showed;

    private const int total_level_number = 3;
    private const int total_star_collection=3;
    // Use this for initialization
    private void Awake()
    {
        Level_pass = new List<bool>();
        Star_collection = new List<bool>();
        hidden_level_showed = false;
        /*for (int i = 0; i < 3; i++)
        {
            Level_pass.Add(false);
            Star_collection.Add(false);
        }*/
        if (!File.Exists(Application.dataPath + "/Save/save.xml"))
        {
            Create_Initial_Save();
        }
        Read_save();
    }
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Write_save()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Application.dataPath + "/Save/save.xml");
        XmlElement data = (XmlElement)doc.FirstChild;
        for (int i = 0; i < total_level_number; i++)
        {
            string s = "Level" + (i + 1).ToString();
            data.SetAttribute(s, Level_pass[i].ToString());
        }
        for (int i = 0; i < total_star_collection; i++)
        {
            string s = "Star" + (i + 1).ToString();
            data.SetAttribute(s, Star_collection[i].ToString());
        }
        data.SetAttribute("Hidden_level_appear", hidden_level_showed.ToString());
        doc.Save(Application.dataPath + "/save/save.xml");
    }

    private void Read_save()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Application.dataPath + "/Save/save.xml");
        XmlElement data = (XmlElement)doc.FirstChild;
        for(int i = 0; i < total_level_number; i++)
        {
            string s = "Level" + (i + 1).ToString();
            Level_pass.Add(bool.Parse(data.GetAttribute(s)));
        }
        for (int i = 0; i < total_star_collection; i++)
        {
            string s = "Star" + (i + 1).ToString();
            Star_collection.Add(bool.Parse(data.GetAttribute(s)));
        }
        hidden_level_showed = bool.Parse(data.GetAttribute("Hidden_level_appear"));
        doc.Save(Application.dataPath + "/save/save.xml");

    }
    private void Create_Initial_Save()
    {
        Directory.CreateDirectory(Application.dataPath + "/Save");
        XmlDocument doc = new XmlDocument();
        XmlElement data = doc.CreateElement("Data");
        doc.AppendChild(data);
        for (int i = 0; i < total_level_number; i++)
        {
            string s = "Level" + (i + 1).ToString();
            XmlAttribute level = doc.CreateAttribute(s);
            level.Value = "False";
            data.Attributes.Append(level);
        }
        for (int i = 0; i < total_star_collection; i++)
        {
            string s = "Star" + (i + 1).ToString();
            XmlAttribute star = doc.CreateAttribute(s);
            star.Value = "False";
            data.Attributes.Append(star);
        }
        XmlAttribute hidden_level_appear = doc.CreateAttribute("Hidden_level_appear");
        hidden_level_appear.Value = "False";
        data.Attributes.Append(hidden_level_appear);
        doc.Save(Application.dataPath + "/Save/save.xml");
    }
}
