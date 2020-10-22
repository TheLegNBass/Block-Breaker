using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float ballX = 2f;
    [SerializeField] float ballY = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0f;

    //states
    Vector2 paddleBallDif;
    bool launched = false;
    [SerializeField]Vector2 lastPos = new Vector2();

    //Cached Components
    AudioSource audioSource;
    Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        paddleBallDif = transform.position - paddle1.transform.position;
        audioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!launched)
        {
            LockBallToPaddle();
            LaunchBall();
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleBallDif;
    }

    private void LaunchBall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            launched = true;
            myRigidBody2D.velocity = new Vector2(ballX, ballY);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO Change to grab the X,Y and compare to make sure they aren't even, and if they are add a random velocity.
               
        Vector2 velTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));
        
        if (launched)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            audioSource.PlayOneShot(clip);
            //myRigidBody2D.velocity += velTweak;

            Vector2 tweak = new Vector2(.1f, .1f);
            if (myRigidBody2D.velocity.x - lastPos.x <= 1)
            {
                myRigidBody2D.velocity += tweak;
            }
            else if (myRigidBody2D.velocity.y - lastPos.y <= 1)
            {
                myRigidBody2D.velocity += tweak;
            }

            lastPos.x = myRigidBody2D.velocity.x;
            lastPos.y = myRigidBody2D.velocity.y;
        }        
    }
}
