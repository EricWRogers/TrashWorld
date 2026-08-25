using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
   public int score = 0;

    [SerializeField]
    private TMP_Text scoreText;

    private void Start()
    {
        scoreText.text = "Points: " + score;
    }

    public void AddPoints()
    {
        score += 1;
        scoreText.text = "Points: " + score;
    }
}
