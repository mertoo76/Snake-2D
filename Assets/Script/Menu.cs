using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Menu : MonoBehaviour {


        public Button start;
        public Button exit;


    public GameObject wall;
    public GameObject nonWall;
    public Text Hscore;
    public int score;        

	// Use this for initialization
	void Start () {
        start = start.GetComponent<Button>();
        exit = exit.GetComponent<Button>();
       score  = PlayerPrefs.GetInt("HighScore", 0);
        Hscore.text = "High Score:" + score;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void play()
    {
        wall.SetActive(true);
        nonWall.SetActive(true);
    }

    public void StartLevel1()
    {
        Application.LoadLevel(1);
    }

    public void StartLevel2()
    {
        Application.LoadLevel(2);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
