using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RubyController : MonoBehaviour
{
    [SerializeField] private float _shootingTime = 1.2f;
    [SerializeField] private GameObject _gameCanvas;
    [SerializeField] private GameObject _pauseCanvas;
    
    public float Speed = 3.0f;

    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    public int health => currentHealth;
    
    
    int currentHealth;

    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    AudioSource audioSource;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    public GameObject projectilePrefab;
    float horizontal;
    float vertical;

    private float _elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator= GetComponent<Animator>();
        audioSource= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_elapsedTime < _shootingTime) return;
            
            Launch();
            _elapsedTime = 0f;
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider !=null)
            {
                NonPlayerCharacters character= hit.collider.GetComponent<NonPlayerCharacters>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _gameCanvas.SetActive(false);
            _pauseCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + Speed * horizontal * Time.deltaTime;
        position.y = position.y + Speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetTrigger("Hit");
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        ProjecTile projectile = projectileObject.GetComponent<ProjecTile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}



