using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // config params
    [Header("Player Settings")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 100;

    [Header("Projectile Settings")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f; 
    [SerializeField] float projectileFirePeriod = 0.1f;

    [Header("Sound Settings")]
    [SerializeField] AudioClip killSound;
    [SerializeField] [Range(0,1)] float killSoundVolume = 0.7f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0,1)] float shootSoundVolume  = 0.7f;

    Coroutine  firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBounderies();
    }

    // Update is called once per frame
    void Update()
    {
       Move(); 
       Fire();
    }

    private void Move() 
    {
        // number of units you want to move * frames per second *  duration of frame = distance per second
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire() 
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }

    }

    private void SetUpMoveBounderies()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3 (0, 0,0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint (new Vector3 (1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3 (0, 0,0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint (new Vector3 (0, 1, 0)).y - padding;

    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject; // Quaternion.identity is for rotation
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFirePeriod);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer){return;} // to solve the null error
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.getDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        AudioSource.PlayClipAtPoint(killSound, Camera.main.transform.position, killSoundVolume); // the last param is the volume
        Destroy(gameObject);
    }

    public int getHealth()
    {
        return health;
    }

    
}
