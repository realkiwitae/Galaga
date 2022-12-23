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
    public void OnGameModeChanged(Dropdown dropDown)
    {
        if(dropDown.value == 0)GameManager.Instance.gameMode = EGameMode.POINTS;
        else if(dropDown.value == 1)GameManager.Instance.gameMode = EGameMode.KILLS;
        Debug.Log("DROP DOWN CHANGED -> " + dropDown.value);
    }

    private bool bReady = false;


    void Update(){
        if(bReady)return;
        
        if(!scoretext)return;
        bReady = true;
        if(!PlayerPrefs.HasKey("highscore"))PlayerPrefs.SetFloat("highscore",0);
        if(!PlayerPrefs.HasKey("highkill"))PlayerPrefs.SetInt("highkill",0);

        scoretext.text = "Score to beat: \n" + PlayerPrefs.GetFloat("highscore") + "\n";
        scoretext.text += "Kills to beat: \n" + PlayerPrefs.GetInt("highkill") + "\n\n\n";
        
        if(GameManager.Instance.gameState == EGameState.MENU){
            scoretext.text += "Let's play\n";
            return;
        }


        if(!GameManager.Instance.res.bwin) scoretext.text += "GAME OVER\n";
        else scoretext.text += "WIN\n";
        
        if(GameManager.Instance.res.bwin){
            if(GameManager.Instance.gameMode == 0){
                scoretext.text += "NEW TOP SCORE \n";
            }else{
                scoretext.text += "NEW TOP KILLS \n";    
            }
        }else{
            if(GameManager.Instance.gameMode == 0){
                scoretext.text += "Missed by: "+ (PlayerPrefs.GetFloat("highscore") - GameManager.Instance.res.score )+"\n";
            }else{
                scoretext.text += "Missed by: "+ (PlayerPrefs.GetInt("highkill") - GameManager.Instance.res.kills )+"\n";   
            }        
        }


        System.TimeSpan interval = System.TimeSpan.FromSeconds( (double)GameManager.Instance.res.time );
 
        scoretext.text += "time: " + interval.Hours.ToString("00") + ":" + interval .Minutes.ToString("00") + ":" + interval.Seconds.ToString("00");
    
    }

}

