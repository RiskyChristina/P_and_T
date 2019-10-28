using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public Text scoreText;
    public Text livesText;
    public Text winText;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;

    private Rigidbody2D rd2d;
    private int scoreValue;
    private bool facingRight = true;
    private object hozMovement;
    private int lives;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        //score.text = scoreValue.ToString();

        anim = GetComponent<Animator>();

        winText.text = "";
        scoreValue = 0;
        lives = 3;
        SetLivesText();
        SetScoreText();

        musicSource.clip = musicClipOne;
        musicSource.Play();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }

        if (facingRight == false && hozMovement > 0)
         {
             Flip();
         }
         else if (facingRight == true && hozMovement < 0)
         {
             Flip();
         }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetScoreText();
            //scoreText.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        else if (collision.collider.tag == "UFO")
        {
            collision.gameObject.SetActive(false);
            lives -= 1;
            SetLivesText();
            if (lives < 1)
            {
                SetLivesText();
                winText.text = "You Lose!";
                gameObject.SetActive(false);
                
            }
        }
        if (scoreValue == 4)
        {
            transform.position = new Vector2(70.0f, 1.0f);
            lives = 3;
            SetLivesText();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    } 
    
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + scoreValue.ToString();
        if (scoreValue >= 9)
        {
            winText.text = "You win! Game created by Christina Leskowyak";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
    }
}
