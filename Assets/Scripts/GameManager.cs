using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EGameState{
    MENU,
    
    ENDGAME,
    PLAYING,
}
public enum EGameMode{
    POINTS,
    KILLS,
};

public class Results{
    public float score;
    public float time;
    
    public bool bwin = false;
};

public class Rules{
    public int player_nblives = 3;
    public float player_laser_vy = 15f;
    public float enemy_laser_vy = -10f;

    public float bonus_row_0 = 10000;
    public float bonus_row_1 = 1000;
    public float bonus_row_2 = 800;
    public float bonus_row_3 = 300;
    public float bonus_row_4 = 150;
    // dimensions enemy grid

    public int W = 8;
    public int H = 5;




};

public class GameManager{
    private static GameManager _instance;
    GameManager() {
        startStage();
        res = new Results();
        rules = new Rules();
        gameState = EGameState.MENU;
        
    }    

    public EGameMode gameMode = EGameMode.POINTS;
    public Results res;

    public static GameManager Instance {
        get {
            if(_instance==null) {
                _instance = new GameManager();
            }
 
            return _instance;
        }
    }
    public Rules rules;
    public float maxx,minx,miny,maxy;

    public EGameState gameState = EGameState.PLAYING; 

    public void playerDeath(Player p){
  
        res.score = p.score;
        res.time = Time.timeSinceLevelLoad;
        gameState = EGameState.ENDGAME;
        
        handleScore(p.score);
        handleKill(p.kill_count);

        SceneManager.LoadScene(0);
    }

    public void startStage(){
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        maxy = -stageDimensions.y;
        miny = stageDimensions.y;
        minx = -stageDimensions.x;
        maxx = stageDimensions.x;
        if(!PlayerPrefs.HasKey("highscore"))PlayerPrefs.SetFloat("highscore",0);
        if(!PlayerPrefs.HasKey("highkill"))PlayerPrefs.SetInt("highkill",0);

    }

    private void handleScore(float score){
        if(score > PlayerPrefs.GetFloat("highscore")){
            PlayerPrefs.SetFloat("highscore",score);
        }

    }
    private void handleKill(int score){
        if(score > PlayerPrefs.GetInt("highkill")){
            PlayerPrefs.SetInt("highkill",score);
        }

    }
    public bool checkXWin(Player p){
        if(p){
            bool b = res.bwin;
            switch(gameMode){
                case EGameMode.POINTS:
                
                    res.bwin = PlayerPrefs.GetFloat("highscore") < p.score;
                break;
                case EGameMode.KILLS:
                    res.bwin = PlayerPrefs.GetInt("highkill") < p.kill_count;
                break;
                default:
                break;
            }
            return b != res.bwin;
        }

        return false;
    }
 }
