using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;


public class Ball : NetworkBehaviour
{

    public TextMeshProUGUI ballSpeedText;
    Rigidbody2D rbod;

    [SyncVar]
    public float ballSpeed = 10;

    [SerializeField] public KeyCode increaseSpeed;
    [SerializeField] public KeyCode decreaseSpeed;

    // Use this for initialization
    void Start()
    {
        rbod = GetComponent<Rigidbody2D>();
        //ballSpeedText.SetText("Speed:" + ballSpeed);
        if (isServer)
        {
            //StartCoroutine("ResetBall");
        }
    }


    public IEnumerator ResetBall()
    {
        if (isServer)
        {
            transform.position = new Vector3(0, 0, 0);
            rbod.velocity = new Vector2(0, 0);
            ballSpeed = 5f;
            yield return new WaitForSeconds(2f);
            rbod.velocity = new Vector2(-1, 1).normalized * ballSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            rbod.velocity = new Vector2(rbod.velocity.x, rbod.velocity.y).normalized * ballSpeed;
            if (Input.GetKeyDown(increaseSpeed))
            {
                ballSpeed++;
            }
            if (Input.GetKeyDown(decreaseSpeed))
            {
                ballSpeed--;
            }
        }
        //ballSpeedText.SetText("Speed:" + ballSpeed);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isServer)
        {
            switch (collision.gameObject.name)
            {
                case "PlayerGoal":
                    //GameObject.FindGameObjectWithTag("Manager").GetComponent<GameController>().p2Score++;
                    StartCoroutine("ResetBall");
                    break;
                case "EnemyGoal":
                    //GameObject.FindGameObjectWithTag("Manager").GetComponent<GameController>().p1Score++;
                    StartCoroutine("ResetBall");
                    break;
                case "Player Paddle(Clone)":
                    ballSpeed *= 1.15f;
                    break;
            }
        }
    }
}
