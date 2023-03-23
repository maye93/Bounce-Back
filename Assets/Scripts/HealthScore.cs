using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HealthScore : MonoBehaviour
{
    public TennisBall ball;
    public Hand hand;

    public TextMeshProUGUI scoreText;

    public RawImage heart1;
    public RawImage heart2;
    public RawImage heart3;

    private int score = 0;
    private int hearts = 4;

    private void Update()
    {
        // Check if ball has been picked up by player
        if (hand.heldObject != null)
        {
            ball.groundTouch = false;
        }

        // Check if player has lost a heart
        if (ball.groundTouch == true)
        {
            hearts--;
            ball.groundTouch = false;
            UnityEngine.Debug.Log("Heart lost");
        }

        // Player Hearts
        if (hearts == 2)
        {
            heart1.gameObject.SetActive(false);
        }
        else if (hearts == 1)
        {
            heart1.gameObject.SetActive(false);
            heart2.gameObject.SetActive(false);
        }
        else if (hearts == 0)
        {
            heart1.gameObject.SetActive(false);
            heart2.gameObject.SetActive(false);
            heart3.gameObject.SetActive(false);
            StartCoroutine(GameOverScene());
        }
    }

    // Scoring
    public void Scoring()
    {
        UnityEngine.Debug.Log("Hit");
        score += 10;
        scoreText.text = "Score: " + score.ToString();
    }

    public IEnumerator GameOverScene()
    {
        yield return new WaitForSeconds(1.5f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("GameOver");
    }
}