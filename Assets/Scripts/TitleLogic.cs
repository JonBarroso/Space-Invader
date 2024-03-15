using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleLogic : MonoBehaviour
{
    public TMPro.TextMeshProUGUI titleText;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void Startgame()
    {
        StartCoroutine(FindPlayer());



    }
    IEnumerator FindPlayer()
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync("DemoScene");
        while (!asyncOp.isDone)
        {
             yield return null;
        }
            GameObject playerObj = GameObject.Find("Player");
            Debug.Log(playerObj);

    }
    public void LoadCredit()
    {
        // SceneManager.LoadScene("CreditScene");
        float delayInSeconds = 5f; 
        StartCoroutine(ReturnCredit(delayInSeconds));
    }
    public void ReturnMain(float delay)
    {
        StartCoroutine(ReturnMainCoroutine(delay));
    }

    IEnumerator ReturnMainCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenuScene");
    }
    IEnumerator ReturnCredit(float delay)
    {
    SceneManager.LoadScene("CreditScene");

    yield return new WaitForSeconds(delay);

    SceneManager.LoadScene("MainMenuScene");
    }
}
