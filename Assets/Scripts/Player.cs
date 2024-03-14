using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  public GameObject bullet;
  public Transform shottingOffset;
  public TextMeshProUGUI ScoreText;
  public TextMeshProUGUI HiScoreText;
  public int score = 0;
  public int hiScore = 0;
  public float unitsPerSecond = 3f;
  public AudioClip shootAudio;
  public AudioClip Deathsound;
  private AudioSource audioSource;


  void Start()
  {
    audioSource = GetComponent<AudioSource>();
    hiScore = PlayerPrefs.GetInt("HighScore", 0);
    UpdateHiScoreText();
    UpdateScoreText();

    Enemy.OnEnemyDied += EnemyOnOnEnemyDied;
  }
  // void OnCollisionEnter2D(Collision2D collision)
  // {
  //   GetComponent<Animator>().SetTrigger("Death");

  //   Debug.Log("Player Death");
  //   Destroy(collision.gameObject);
  // }
  void OnDestroy()
  {
    PlayerPrefs.SetInt("HighScore", hiScore);
    PlayerPrefs.Save();

    Enemy.OnEnemyDied += EnemyOnOnEnemyDied;
    
  }

  void EnemyOnOnEnemyDied(int pointsWorth)
  {
    score += pointsWorth;
    UpdateScoreText();
    if (score > hiScore)
    {
      hiScore = score;
      UpdateHiScoreText();
    }
    // Debug.Log($"player recieved 'EnemyDied' worth {pointsWorth}");
  }

    void Update()
    {
      float horizontalValue = Input.GetAxis("Horizontal");
      Transform ti = GetComponent<Transform>();
      ti.position += Vector3.right * horizontalValue * unitsPerSecond * Time.deltaTime;


      if (Input.GetKeyDown(KeyCode.Space))
      {
        audioSource.PlayOneShot(shootAudio);
        GetComponent<Animator>().SetTrigger("Shoot Trigger");
        GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
        Debug.Log("Bang!");

        Destroy(shot, 3f);

      }
    }
    void SomeAnimationFrameCallBack()
    {
      Debug.Log("Something Happened");
    }
    void OnTriggerEnter2D(Collider2D player)
    {
 
        if (player.CompareTag("Bullet"))
        {
            audioSource.PlayOneShot(Deathsound);
            GetComponent<Animator>().SetTrigger("PlayerDied");
            Destroy(gameObject);   
            Destroy(player.gameObject);
            // SceneManager.LoadScene("CreditScene");
        }
    }
    void DeathAnimation()
    {
      Destroy(gameObject);
    }
    void UpdateScoreText()
    {
          string formattedScore = score.ToString().PadLeft(4, '0');
          ScoreText.text = "SCORE\n " + formattedScore;
    }
    void UpdateHiScoreText()
    {
        string formattedHiScore = hiScore.ToString().PadLeft(4, '0');
        HiScoreText.text = "Hi-SCORE\n" + formattedHiScore;
    }
}
