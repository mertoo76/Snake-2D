using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class foodSpawner : MonoBehaviour {

    public GameObject food;
    public GameObject Spider;
    public GameObject snake;
    public GameObject tmp;


    // Borders
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;


    public GameObject[] items = new GameObject[3];
    public int count = 0;
    int score;
    public snake script;

    // Use this for initialization
    void Start () {
        //  InvokeRepeating("Spawn1", 2, 4);
        Spawn1();
        InvokeRepeating("Spawn2", 8, 13);
        InvokeRepeating("SpiderTime", 13, 13);

    //    script  = snake.GetComponent<snake>();
      // score = script.score;
    }


   void SpiderTime()
    {
       
        Destroy(tmp);
    }

    void Spawn2()
    {

        // x position between left & right border
        int x = (int)Random.Range(borderLeft.position.x + 0.5f,
                                  borderRight.position.x - 0.5f);

        // y position between top & bottom border
        int y = (int)Random.Range(borderBottom.position.y + 0.5f,
                                  borderTop.position.y - 0.5f);

        // Instantiate the food at (x, y)
       tmp=  Instantiate(Spider,
                    new Vector2(x, y),
                    Quaternion.identity); // default rotation

    }

   public void Spawn1()
    {
       /* if(count >= 3)
        {
            if (score < script.score)
                count--;
            count--;
            Destroy(items[0]);
        
        }*/
        
        // x position between left & right border
        int x = (int)Random.Range(borderLeft.position.x+ 0.5f,
                                  borderRight.position.x - 0.5f);

        // y position between top & bottom border
        int y = (int)Random.Range(borderBottom.position.y + 0.5f,
                                  borderTop.position.y - 0.5f);

        // Instantiate the food at (x, y)
     /*   items[0]=*/ Instantiate(food,
                    new Vector2(x, y),
                    Quaternion.identity); // default rotation

        //items[count] = food;
        count++;

    }


}
