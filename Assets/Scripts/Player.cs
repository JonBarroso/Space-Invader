using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  public GameObject bullet;

  public Transform shottingOffset;
  
  public TextMeshProUGUI scoretext;

  public TextMeshProUGUI hiScoretext;
  public int score = 0;

  public int hiScore = 0;

  public float unitsPerSecond = 3f;

  public AudioClip Shootsound;
  
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

    void OnDestroy()
    {
        PlayerPrefs.SetInt("HighScore", hiScore);
        PlayerPrefs.Save();
      Enemy.OnEnemyDied -= EnemyOnOnEnemyDied;
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
    }

    void Update()
    {
      float horizontalValue = Input.GetAxis("Horizontal");

      Transform ti = GetComponent<Transform>();
      ti.position += Vector3.right * horizontalValue * unitsPerSecond * Time.deltaTime;
      if (Input.GetKeyDown(KeyCode.Space))
      {
        audioSource.PlayOneShot(Shootsound);
        GetComponent<Animator>().SetTrigger("Shoot Trigger");

        GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
        // Debug.Log("Bang!");

        Destroy(shot, 3f);

      }
    }

    void UpdateScoreText()
    {
        string formattedScore = score.ToString().PadLeft(4, '0');
        scoretext.text = "Score\n" + formattedScore;
    }

    void UpdateHiScoreText()
    {
        string formattedHiScore = hiScore.ToString().PadLeft(4, '0');
        hiScoretext.text = "Hi-Score\n" + formattedHiScore;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
      audioSource.PlayOneShot(Deathsound);
      GetComponent<Animator>().SetTrigger("PlayerDied");
      Debug.Log("Player dead");
      Destroy(gameObject);
      Destroy(other.gameObject);
      
      SceneManager.LoadScene("CreditScene");
  }
  void PlayerDeathAnimation()
  {
    Debug.Log("enemy died");
    
    Destroy(gameObject);
  }
  void SomeAnimationFrameCallBack()
  {
      Debug.Log("shoot");
  }
}
