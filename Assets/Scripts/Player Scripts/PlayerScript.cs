using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D myBody;
    public float move_Speed = 2f;

    public float normal_Push = 10f;
    public float extra_Push = 14f;

    private bool initial_Push;

    private int push_Count;

    private bool player_Dead;
 
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        GameObject.Find("Left").GetComponent<Button>().onClick.AddListener(MoveLeft);
        GameObject.Find("Right").GetComponent<Button>().onClick.AddListener(MoveRight);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (player_Dead)
        {
            return;
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            MoveRight();
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            MoveLeft();
        }

    } // player Movement

    void MoveLeft()
    {
        myBody.velocity = new Vector2(-move_Speed, myBody.velocity.y);
    }

    void MoveRight()
    {
        myBody.velocity = new Vector2(move_Speed, myBody.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (player_Dead)
        {
            return;
        }

        if (target.tag == "ExtraPush")
        {
            if (!initial_Push)
            {
                initial_Push = true;

                myBody.velocity = new Vector2(myBody.velocity.x, 18f);

                target.gameObject.SetActive(false);

                //SoundManager
                SoundManager.instance.JumpSoundFX();

                //Exit from the OntriggerEnter becaz of initial push
                return;
            }
        }//because of initial push only I am using this

        if (target.tag == "NormalPush")
        {
            myBody.velocity = new Vector2(myBody.velocity.x, normal_Push);

            target.gameObject.SetActive(false);

            push_Count++;

            //soundManager
            SoundManager.instance.JumpSoundFX();
        }

        if (target.tag == "ExtraPush")
        {
            myBody.velocity = new Vector2(myBody.velocity.x, extra_Push);

            target.gameObject.SetActive(false);

            push_Count++;

            //soundManager
            SoundManager.instance.JumpSoundFX();
        }

        if (push_Count == 2)
        {
            push_Count = 0;
            PlatformSpawner.instance.SpawnPlatforms();
        }

        if (target.tag == "FallDown" || target.tag == "Bird")
        {
            player_Dead = true;

            //SoundManager
            SoundManager.instance.GameOverSoundFX();

            GameManager.instance.RestartGame();
        }
    }

}
