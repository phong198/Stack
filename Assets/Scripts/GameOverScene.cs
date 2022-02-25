using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    //private TextMeshProUGUI text;
    //private int score = PlayerPrefs.GetInt("Score");
   // private void Start()
    //{
        //text = GetComponent<TMPro.TextMeshProUGUI>();
    //}
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            SceneManager.LoadScene("Main");
       // text.text = "Score: " + score;
       
    }
}
