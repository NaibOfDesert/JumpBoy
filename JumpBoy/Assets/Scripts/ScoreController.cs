using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TextMeshProUGUI scoreText;



    void Update()
    {
        scoreText.text = playerController.GetPosition().ToString();
    }
}
