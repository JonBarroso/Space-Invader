using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
  public int points = 10;

  public float speed = 1f;
  private float currentSpeed; // Current speed of the enemies
  private Vector3 direction = Vector3.right; 

  public GameObject bullet;

  public Transform shottingOffset;
  public float shootingInterval = 2f; 
  public float bulletSpeed = 5f; 

  private float shootingTimer = 0f;
  public delegate void EnemyDied(int pointWorth);
  public static event EnemyDied OnEnemyDied;

  public AudioClip enemydeathsound;

  public AudioClip enemyShootSound;

  private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
         if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        currentSpeed = speed;
    }
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        if (transform.position.x <= -5f || transform.position.x >= 5f)
        {
            direction = -direction;
            transform.Translate(Vector3.down * 0.5f);
        }
        shootingTimer += Time.deltaTime;
        if (shootingTimer >= shootingInterval)
        {
            shootingTimer = 0f;
            Shoot();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
      GetComponent<Animator>().SetTrigger("Death");
      audioSource.PlayOneShot(enemydeathsound);
      // Debug.Log("Ouch!");
      Destroy(collision.gameObject);

      OnEnemyDied.Invoke(points);

      IncreaseSpeed();
    }
    void DeathAnimation()
    {
      Debug.Log("enemy died");
      
      Destroy(gameObject);
    }

    void Shoot()
{
    audioSource.PlayOneShot(enemyShootSound);

    // Check if the shottingOffset is null or has been destroyed
    if (shottingOffset != null)
    {
        // Instantiate a bullet at the position of the shottingOffset
        GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
    }
    else
    {
        Debug.LogWarning("shottingOffset is null or destroyed. Cannot shoot.");
    }
}

    void IncreaseSpeed()
    {
        currentSpeed += 0.1f; 
    }
}
