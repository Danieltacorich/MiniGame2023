using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
   
    public float speed = 3.0f;

    //Time limit
    public float timeLeft = 10.0f;
    public float commandTime = 2.0f;
    public GameObject loseTimer;
    public TextMeshProUGUI count;
    
    // win loss n intro
    public GameObject winText;
    public GameObject loseText;
    public TextMeshProUGUI Intro;

    // win Square
    public GameObject winBlock;

    //score
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    private int scoreValue = 0;
    private int livesValue = 2;

    // audio
    public AudioClip QuestComplete;
    public AudioClip Alert;
    public AudioClip Coin;
    public AudioClip WinSound;
    public AudioClip LoseSound;
    public AudioSource backgroundManager;
    AudioSource audioSource;

    // Particles
    public ParticleSystem collected;
    
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        scoreValue = 0;
        livesValue = 2;
        SetCountText();
        audioSource = GetComponent<AudioSource>();
        
        winText.SetActive(false);
        loseText.SetActive(false);
        winBlock.SetActive(false);
        collected.Stop();
        
        loseTimer.SetActive(false);  // time Limit
    
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

         timeLeft -= Time.deltaTime;
        count.text = (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
          loseTimer.SetActive(true);
          speed = 0;
          Destroy(gameObject.GetComponent<SpriteRenderer>());
        }
         commandTime -= Time.deltaTime;
        Intro.text = (commandTime).ToString("0");
        if (commandTime > 0)
        {
          Intro.text = "Collect Coins! Get To The Other Side!";
          audioSource.PlayOneShot(Alert);

        }
        if (commandTime < 0)
        {
          Intro.text = "";
          
        }
        if (timeLeft>0)
        {
          timeLeft -= Time.deltaTime; 
        }
       
       
    }
    
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            Destroy(collision.collider.gameObject);
            SetCountText();
            audioSource.PlayOneShot(Coin);
            collected.Play();
        }
        else if (collision.collider.tag == "Enemy")
        {
            livesValue = livesValue - 1; 
            SetCountText();
        }
        else if (collision.collider.tag == "Win")
        {
            Destroy(collision.collider.gameObject);
            winText.SetActive(true);
            audioSource.PlayOneShot(QuestComplete);
            speed = 0;

        }

    }
   
    void SetCountText()
    {
        scoreText.text = "Score: " + scoreValue.ToString();
        livesText.text = "Lives: " + livesValue.ToString();

        if (livesValue == 0)
        {
            loseText.SetActive(true);
            audioSource.clip = LoseSound;
            audioSource.Play();
            Destroy(gameObject.GetComponent<SpriteRenderer>());
        }
        if (scoreValue == 3)
        {
            winBlock.SetActive(true);
        
        }
    }

}