using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public float jumpForce;
    
    public float lives;

    public Text score;

    public Text jumpText;

    public Text hozText;

    public Text winText;

    public AudioClip defultmusic;

    public AudioClip winmusic;

    public AudioSource musicSource;

    Animator anim;

    public Text livesText;

    private int scoreValue = 0;

    private bool facingRight = true;

    private bool isOnGround;

    public Transform groundcheck;

    public float checkRadius;

    public LayerMask allGround;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives = 3;
        livesText.text = "Lives: " + lives;
        musicSource.clip = defultmusic;
        musicSource.Play();
        musicSource.loop = true;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        /*if (Input.GetKey(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }
        else*/ if (isOnGround == false)
        {
            anim.SetInteger("State", 3);
        }
       else if (Input.GetKey(KeyCode.D))
       {
           anim.SetInteger("State", 1);
       }
       else if (Input.GetKey(KeyCode.A))
       {
           anim.SetInteger("State", 1); 
       }
       else if (Input.GetKey(KeyCode.LeftArrow))
       {
           anim.SetInteger("State", 1);
       }
       else if (Input.GetKey(KeyCode.RightArrow))
        {
          anim.SetInteger("State", 1);
        }
        else
        {
          anim.SetInteger("State", 0);
        }

        //if (isOnGround == false)
        //{
        //    anim.SetInteger("State",3);
        //}

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);



        if (facingRight == false && hozMovement > 0)
        {
            Flip ();
        }

        else if (facingRight == true && hozMovement < 0)

        {
            Flip();

        }

        if (hozMovement > 0 && facingRight == true)
        {
            Debug.Log ("Facing Right");
            hozText.text = "Facing Right";
        }
                if (hozMovement < 0 && facingRight == false)
        {
            Debug.Log ("Facing Left");
            hozText.text = "Facing Left";
        }

        if (vertMovement > 0 && isOnGround == false)
        {
            Debug.Log ("Jumping");
            jumpText.text = "Jumping";
        }

        else if (vertMovement == 0 && isOnGround == true)
        {
            Debug.Log ("Not Jumping");
            jumpText.text = "Not Jumping";
        }
    }
        void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            WinCheck();
                if (scoreValue == 4)
                {
                transform.position = new Vector2(56.0f, 1.0f);
                lives=3;
                SetLivesText ();
                }
        }

        if (collision.collider.tag == "Enemy")
        {
        lives = lives - 1;
        Destroy(collision.collider.gameObject);
        SetLivesText();
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
        
    }

    
        void WinCheck ()
    {
        if (scoreValue >= 8)
        {
            winText.text = "You Win!!!\nBy Danielle Brewer";
            musicSource.clip = winmusic;
            musicSource.Play();
            musicSource.loop = false;
        }
    }
         void SetLivesText ()
    {
        livesText.text= "Lives: " + lives.ToString ();
        if (lives == 0)
        {
            winText.text= "You Lose!!! \nBy Danielle Brewer";
            Destroy(gameObject);
        }
    }
}
