using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class snake_nonwall : MonoBehaviour {

    public float speed = 1f;
    Vector2 dir = Vector2.right;

    foodSpawner foodspawn;
    public GameObject camera;

    public int way = 0;


    public Animator anim;


    int gameover = 0;

    public int score = 0;
    public Text text;
    public int highScore;

    bool ate = false;
    public GameObject tailPrefab;

    // Keep Track of Tail
    List<Transform> tail = new List<Transform>();


    // Use this for initialization
    void Start () {



        highScore = PlayerPrefs.GetInt("HighScore", 0);

        way = 0;
        dir = Vector2.right * speed;

        InvokeRepeating("Move", 0.1f, 0.1f);

        foodspawn = camera.GetComponent<foodSpawner>();



        int i;
        // Save current position (gap will be here)
        Vector2 v = transform.position;
        for (i = 0; i < 3; i++)
        {
            v.x = v.x - 1;
            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);
            // Keep track of it in our tail list
            tail.Insert(0, g.transform);
        }
    }
	
	// Update is called once per frame
	void Update () {

        text.text = "Score: " + score;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (way != 2)
            {
                dir = Vector2.right * speed;
                way = 0;
            }


        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (way != 3)
            {
                dir = -Vector2.up * speed;   // '-up' means 'down'
                way = 1;
            }
        }



        else if (Input.GetKey(KeyCode.LeftArrow))
        {

            if (way != 0)
            {
                dir = -Vector2.right * speed; // '-right' means 'left'
                way = 2;
            }


        }

        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (way != 1)
            {
                dir = Vector2.up * speed;
                way = 3;
            }

        }



    }

    void Move()
    {

        if (gameover != 1)
        {
          if(transform.position.x <= foodspawn.borderLeft.position.x)
            {

                Vector2 tmp = foodspawn.borderRight.position;
                tmp.y = transform.position.y;

                transform.position = tmp;
             //   transform.position=foodspawn.borderRight.position;

                dir = -Vector2.right * speed; // '-right' means 'left'
                way = 2;
            }
          else
            if (transform.position.x >= foodspawn.borderRight.position.x)
            {
                Vector2 tmp = foodspawn.borderLeft.position;
                tmp.y = transform.position.y;
                transform.position = tmp;
//                transform.position = foodspawn.borderLeft.position;

                dir = Vector2.right * speed;
                way = 0;
            }else
            if (transform.position.y >= foodspawn.borderTop.position.y)
            {

                Vector2 tmp = foodspawn.borderBottom.position;
                tmp.x = transform.position.x;
                transform.position = tmp ;
     
                dir = Vector2.up * speed;
                way = 3;
            }
          else
            if (transform.position.y <= foodspawn.borderBottom.position.y)
            {

                Vector2 tmp = foodspawn.borderTop.position;
                tmp.x = transform.position.x;


                transform.position =tmp;
                dir = -Vector2.up * speed;   // '-up' means 'down'
                way = 1;
            }

            // Save current position (gap will be here)
            Vector2 v = transform.position;
            // Move head into new direction (now there is a gap)
            transform.Translate(dir * speed);

            // Ate something? Then insert new Element into gap
            if (ate)
            {
                // Load Prefab into the world
                GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);
                // Keep track of it in our tail list
                tail.Insert(0, g.transform);



                if (score > highScore)
                {
                    PlayerPrefs.SetInt("HighScore", score);
                    PlayerPrefs.Save();
                }

                // Reset the flag
                ate = false;




            }
            else // Do we have a Tail?
                if (tail.Count > 0)
            {
                // Move last Tail Element to where the Head was
                tail.Last().position = v;

                // Add to front of list, remove from the back
                tail.Insert(0, tail.Last());
                tail.RemoveAt(tail.Count - 1);


            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Food?
        if (collision.name.StartsWith("food"))
        {
            // Get longer in next Move call
            ate = true;

            score++;



            // Remove the Food
            Destroy(collision.gameObject);

            foodspawn.Spawn1();
        }
        // Collided with Tail or Border
        else
             if (collision.name.StartsWith("Giant_Spider"))
        {
            // Get longer in next Move call
            ate = true;
            score = score + 5;

            // Remove the Food
            Destroy(collision.gameObject);
        }
        // Collided with Tail or Border
        if(collision.name.StartsWith("snake"))
        {

            anim.SetTrigger("gameover");
            StartCoroutine(MyLoadLevel(3.0f, 0));
            gameover = 1;
            // ToDo 'You lose' screen
        }
    }

    IEnumerator MyLoadLevel(float delay, int level)
    {
        yield return new WaitForSeconds(delay);
        Application.LoadLevel(level);
    }


}
