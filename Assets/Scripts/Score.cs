using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int score;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        Manager.OnCubeSpawned += Manager_OnCubeSpawned;
    }

    private void OnDestroy()
    {
        Manager.OnCubeSpawned -= Manager_OnCubeSpawned;
    }

    private void Manager_OnCubeSpawned()
    {
        score++;
        text.text = "Score: " + score;
        //PlayerPrefs.SetInt("Score", score);
    }
}
