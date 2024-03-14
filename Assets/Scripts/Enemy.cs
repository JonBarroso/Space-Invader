using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public int points = 3;
  public delegate void EnemyDied(int pointsWorth);
  public static event EnemyDied OnEnemyDied;

  private Vector3 direction = Vector3.right;
  public float speed = 1f;

  public GameObject bullet;
  private float shootingTimer = 0f;

  public Transform shottingOffset;
  public float shootingInterval = 2f; 
  public float bulletSpeed = 5f; 

  public AudioClip EnemyShootAudio;
  public AudioClip EnemyDeathSound;
  private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

      GetComponent<Animator>().SetTrigger("Death");
      audioSource.PlayOneShot(EnemyDeathSound);

      Debug.Log("Ouch!");
      Destroy(collision.gameObject);

      OnEnemyDied.Invoke(points);
    }
    void DeathAnimation()
    {
      Destroy(gameObject);
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
    void Shoot()
    {
      GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
      audioSource.PlayOneShot(EnemyShootAudio);

    }
}
