using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioClip musicClipThree;

    public AudioSource musicSource;

    public Text score;

    public Text lives;

    public Text wintext;

    private int scoreValue = 0;

    private int livesValue = 3;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives.text = livesValue.ToString();
        wintext.text = "";
        musicSource.clip = musicClipThree;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject); 
        }
        if (livesValue == 0)
        {
            wintext.text = "You Lose! Game by Cesar Silva";
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
        if (scoreValue == 8)
        {
            wintext.text = "You Win! Game by Cesar Silva";
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
            }
            if (scoreValue == 4)
        {
            transform.position = new Vector3(27.0f, 50.5f, 0.0f);
            livesValue = 3;
            lives.text = livesValue.ToString();
        }
        }
    }
}