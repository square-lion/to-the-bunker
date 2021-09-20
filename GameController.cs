using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject normal;
    public GameObject hard;
    public GameObject Ex;

    public AudioSource n;
    public AudioSource h;
    public AudioSource x;

    public AudioSource nW;
    public AudioSource hW;
    public AudioSource xW;

    public GameObject winMenu;
    public Text winText;
    public Text timeText;
    public Text bestTimeText;

    public GameObject loseMenu;

    public float countDownStartDelay;
    public float countDownDelay;
    private int count = 3;
    public Text countDown;

    public float time;
    public bool timing;


    void Start(){
        int diff = PlayerPrefs.GetInt("Diff");
        if(diff == 0){
            normal.SetActive(true);
            n.Play();
        }
        else if(diff == 1){
            hard.SetActive(true);
            h.Play();
        }
        else{
            Ex.SetActive(true);
            x.Play();
        }

    InvokeRepeating("Countdown", countDownStartDelay, countDownDelay);
    }

    void Update(){
        if(timing)
            time += Time.deltaTime;
    }

    public void Countdown(){
        if(count > 0)
            countDown.text = "" + count;
        else if(count == -1){
            countDown.text = "Go!";
            FindObjectOfType<CarController>().start = true;
            timing = true;
        } 
        else if(count == -2){
            countDown.enabled = false;
        }
        count--;
    }

    public void Win(){
        winMenu.SetActive(true);
        FindObjectOfType<CarController>().crashed = true;
        FindObjectOfType<CarController>().DeactivateTouchControls();
        FindObjectOfType<WatchAd>().ShowAd();

        timing = false;
        timeText.text = "Time: " + String.Format("{0:#,##0.000;(#,##0.000);Zero}", time);

        int diff = PlayerPrefs.GetInt("Diff");
        if(diff == 0){
            n.Stop();
            nW.Play();
            winText.text = "Normal Mode Complete";
            PlayerPrefs.SetInt("Normal", 1);
            if(PlayerPrefs.GetFloat("BestN") > time || PlayerPrefs.GetFloat("BestN") == 0)
                PlayerPrefs.SetFloat("BestN", time);

            bestTimeText.text = "Best Time: " + String.Format("{0:#,##0.000;(#,##0.000);Zero}", PlayerPrefs.GetFloat("BestN")) + "s";
        }
        else if(diff == 1){
            h.Stop();
            hW.Play();
            winText.text = "Hard Mode Complete";
            PlayerPrefs.SetInt("Hard", 1);
            if(PlayerPrefs.GetFloat("BestH") > time || PlayerPrefs.GetFloat("BestH") == 0)
                PlayerPrefs.SetFloat("BestH", time);

            bestTimeText.text = "Best Time: " + String.Format("{0:#,##0.000;(#,##0.000);Zero}", PlayerPrefs.GetFloat("BestH")) + "s";
        }
        else{
            x.Stop();
            xW.Play();
            winText.text = "Extreme Mode Complete";
            PlayerPrefs.SetInt("Extreme", 1);
            if(PlayerPrefs.GetFloat("BestE") > time || PlayerPrefs.GetFloat("BestE") == 0)
                PlayerPrefs.SetFloat("BestE", time);

            bestTimeText.text = "Best Time: " + String.Format("{0:#,##0.000;(#,##0.000);Zero}", PlayerPrefs.GetFloat("BestE")) + "s";
        }
    }

    public void Lose(){
        FindObjectOfType<CarController>().DeactivateTouchControls();
        loseMenu.SetActive(true);
        //FindObjectOfType<WatchAd>().ShowAd();
    }

    public void Restart(){
        SceneManager.LoadScene("Game");
    }

    public void BackToMenu(){
        SceneManager.LoadScene("Menu");
    }
}
