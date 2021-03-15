using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text scoreText;
    public Text winText;
    public Text livesText;
    private int score;
    private int lives;
    public Vector2 motion;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score = 0;
        lives = 3;
        winText.text = "";
        SetCountText();
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(motion.x, 0.0f, motion.y);
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKey("escape"))
            {
                Application.Quit();
            }

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            score += 1;
            Destroy(collision.collider.gameObject);
            SetCountText();
        }

        if (collision.collider.tag == "Enemy")
        {
            lives -= 1;
            Destroy(collision.collider.gameObject);
            SetCountText();
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void SetCountText()
    {
        scoreText.text = "Score: " + score.ToString();
        livesText.text = "Lives: " + lives.ToString();

        if (score == 4)
        {
            transform.position = new Vector3(35.37f, -2.10f, -7.67f);
        }

        if (score == 4)
        {
            lives = 3;
        }

        if (lives == 0)
        {
            Destroy(this);
            winText.text = "You Lose!";
        }

        if (score >= 8)
        {
            winText.text = "You Win! Game created by Alan Castro";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }

    }
}