using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPopup : MonoBehaviour
{
    public TMP_Text pointsText;
    public Button restartButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(ReoladScene);
    }

    public void ShowPopup(int points)
    {
        pointsText.text = $"Points: {points}";
        gameObject.SetActive(true);
    }

    private void ReoladScene() =>
        SceneManager.LoadScene(0);
}
