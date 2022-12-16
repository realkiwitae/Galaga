using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text scoretext;
    public void StartGame(){
        Debug.Log("Starting game");
        SceneManager.LoadScene(1);
    }
    public void ExitGame(){
        Debug.Log("Exit Game");
        Application.Quit();
    }
    private bool bReady = false;


    void Update(){
        if(bReady)return;
        
        if(!scoretext)return;
        bReady = true;
        if(!PlayerPrefs.HasKey("highscore"))PlayerPrefs.SetFloat("highscore",0);
        scoretext.text = "Highest score ever: " + PlayerPrefs.GetFloat("highscore") + "\n\n";
        
        if(GameManager.Instance.gameState == EGameState.MENU){
            scoretext.text += "Let's play\n";
            return;
        }


        if(!GameManager.Instance.res.bwin) scoretext.text += "GAME OVER\n";
        else scoretext.text += "WIN\n";
        scoretext.text += "score: " + GameManager.Instance.res.score + "\n";

        System.TimeSpan interval = System.TimeSpan.FromSeconds( (double)GameManager.Instance.res.time );
 
        scoretext.text += "time: " + interval.Hours.ToString("00") + ":" + interval .Minutes.ToString("00") + ":" + interval.Seconds.ToString("00");
    
    }

}

