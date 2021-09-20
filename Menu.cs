using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject main;
    public GameObject play;

    public GameObject settings;

    public string normalDes = "The President, who is at a local coffee shop, has been surrounded by assassins and needs to escape. Safely escort him to his bunker down the road and avoid the assassins' mines. The main road is direct but has many dangers, while the side roads are safer but slower. Choose your path wisely.";
    public Sprite normalI;
    public Image normalB;
    public string hardDes = "Twice as many assassins, but there's no safe roads this time. Pick your route carefully and make it to the bunker.";
    public Sprite hardI;
    public Image hardButton;
    public string xDes = "Good luck.";
    public Sprite xI;
    public Image xButton;

    public Button playButton;

    public Image map;
    public Text header;
    public Text bestTime;
    public Text des;

    void Start(){
        if(PlayerPrefs.GetInt("Normal") != 1)
            hardButton.color = Color.grey;
        if(PlayerPrefs.GetInt("Hard") != 1)
            xButton.color = Color.grey;

        Clicked(0);
    }

    public void ToPlay(){
        main.SetActive(false);
        play.SetActive(true);
    }

    public void ToMain(){
        play.SetActive(false);
        main.SetActive(true);
    }

    public void Clicked(int id){
        if(id == 0){
            header.text = "Normal";
            if(PlayerPrefs.GetFloat("BestN") != 0f)
                bestTime.text = "Best Time: " + String.Format("{0:#,##0.000;(#,##0.000);Zero}", PlayerPrefs.GetFloat("BestN")) + "s";
            else
                bestTime.text = "";

            des.text = normalDes;
            map.sprite = normalI;
            playButton.gameObject.SetActive(true);
        }
        else if(id == 1){
            header.text = "Hard";
            if(PlayerPrefs.GetFloat("BestH") != 0f)
                bestTime.text = "Best Time: " + String.Format("{0:#,##0.000;(#,##0.000);Zero}", PlayerPrefs.GetFloat("BestH")) + "s";
            else
                bestTime.text = "";

            des.text = hardDes;
            map.sprite = hardI;
            if(PlayerPrefs.GetInt("Normal") != 1)
                playButton.gameObject.SetActive(false);
            else
                playButton.gameObject.SetActive(true);
        }
        else{
            header.text = "Extreme";
            if(PlayerPrefs.GetFloat("BestE") != 0f)
                bestTime.text = "Best Time: " + String.Format("{0:#,##0.000;(#,##0.000);Zero}", PlayerPrefs.GetFloat("BestE")) + "s";
            else
                bestTime.text = "";

            des.text = xDes;
            map.sprite = xI;
            if(PlayerPrefs.GetInt("Hard") != 1)
                playButton.gameObject.SetActive(false);
            else
                playButton.gameObject.SetActive(true);
        }
        PlayerPrefs.SetInt("Diff", id);
    }

    public void Play(){
        SceneManager.LoadScene("Game");
    }

    public void OpenSettings(){
        settings.SetActive(true);
        main.SetActive(false);
    }

    public void CloseSettings(){
        settings.SetActive(false);
        main.SetActive(true);
    }
}
