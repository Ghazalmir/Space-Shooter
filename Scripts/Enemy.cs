using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float health = 100;

    [Header("Shot Settings")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;

    [Header("Projectile Settings")]
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFirePeriod = 1f;

    [Header("Explosion Settings")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;

    [Header("Sound Settings")]
    [SerializeField] AudioClip killSound;
    [SerializeField] [Range(0,1)] float killSoundVolume = 0.7f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0,1)] float shootSoundVolume = 0.7f;

    [Header("Game Session Settings")]
    [SerializeField] int scorePerEnemyDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer){return;} // to solve the null error
        ProcessHit (damageDealer);
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

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

        }

    }

    private void Fire()
    {
        GameObject laser = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed); //negative projectileSpeed because it is shooting downwards
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);


    }

    private void Die()
    {
        FindObjectOfType<GameSession>().addToScore(scorePerEnemyDestroyed);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(killSound, Camera.main.transform.position, killSoundVolume); // the last param is the volume

    }

    

}
