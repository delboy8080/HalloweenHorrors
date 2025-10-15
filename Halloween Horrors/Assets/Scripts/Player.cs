using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public float JumpHeight;
    private bool jumping = false;
    private Animator animator;
    private int score=0;

    public float defaultPowerUpTime = 5;
    private bool isPowerUp=false;
    private float powerUpTimeRemaining = 5;
    
    [SerializeField] private UIManager ui;
    public GameObject projectilePrefab;
    private Vector2 startPosition;
    public int lives = 3;
    private AudioSource _audio;
    private bool isPlaying = false;
    public AudioClip collectSound;
    public Image powerUpTimer;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        _audio = GetComponent<AudioSource>();
        powerUpTimer.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        Vector2 position = transform.position;

        if(move != 0 && !isPlaying && !jumping)
        {
            _audio.Play();
            isPlaying = true;
        }
        if((move == 0 && isPlaying)||jumping)
        {
            _audio.Pause();
            isPlaying = false;
        }
        if (position.y < -10.5)
        {
            position = startPosition;
        }
        else
        {
            position.x = position.x + (speed * Time.deltaTime * move);
        }
        transform.position = position;
        updateAnimation(move);
       
        if(Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            rb.AddForce(new Vector2(0, Mathf.Sqrt(-2 * Physics2D.gravity.y * JumpHeight)),
                ForceMode2D.Impulse);
            jumping = true;
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            GameObject projectile = Instantiate(projectilePrefab,
                rb.position, Quaternion.identity);
            Projectile pr = projectile.GetComponent<Projectile>();
            pr.Launch(new Vector2(animator.GetInteger("Direction"), 0), 300);
            
        }






        if (isPowerUp)
        {
            powerUpTimeRemaining -= Time.deltaTime;
            powerUpTimer.fillAmount = powerUpTimeRemaining / defaultPowerUpTime;
            if(powerUpTimeRemaining < 0)
            {
                isPowerUp = false;
                powerUpTimeRemaining = defaultPowerUpTime;
                animator.speed /= 2;
                speed /= 2;
            }
        }


    }

    private void updateAnimation(float move)
    {
        animator.SetFloat("Move", move);
        if(move > 0)
        {
            animator.SetInteger("Direction", 1);
        }
        else if (move < 0)
        {
            animator.SetInteger("Direction", -1);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumping = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPowerUp && collision.gameObject.tag == "SpeedPowerUp")
        {
            Destroy(collision.gameObject);
            speed = speed * 2;
            isPowerUp = true;
            animator.speed *= 2;
            powerUpTimer.enabled = true;
        }
        if (!isPowerUp && collision.gameObject.tag == "Checkpoint")
        {
            startPosition = transform.position;
        }
        if (collision.gameObject.name.Contains("EnemyProjectile"))
        {
            lives--;
            ui.UpdateLives(lives);
            transform.position = startPosition;
        }
    }
    public void CollectPumpkin()
    {
        score++;
        ui.SetScore(score);
        _audio.PlayOneShot(collectSound);
    }
}
