using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EGameState{
    MENU,
    
    ENDGAME,
    PLAYING,

}

public class Results{
    public float score;
    public float time;
    
    public bool bwin = false;
};

public class Rules{
    public int player_nblives = 3;
    public float player_laser_vy = 15f;
    public float enemy_laser_vy = -10f;

    // win condition
    public float win_score = 999f;
    public long win_kills = 10;

};

public class GameManager{
    private static GameManager _instance;
    GameManager() {
        startStage();
        res = new Results();
        rules = new Rules();
        gameState = EGameState.MENU;
        
    }    

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

    public void playerDeath(float score){
        res.score = score;
        res.time = Time.timeSinceLevelLoad;
        res.bwin = false;
        gameState = EGameState.ENDGAME;
        
        handleScore(score);

        SceneManager.LoadScene(0);
    }

    public void startStage(){
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        maxy = -stageDimensions.y;
        miny = stageDimensions.y;
        minx = -stageDimensions.x;
        maxx = stageDimensions.x;
    }

    private void handleScore(float score){
        if(!PlayerPrefs.HasKey("highscore"))PlayerPrefs.SetFloat("highscore",0);
        if(score > PlayerPrefs.GetFloat("highscore")){
            PlayerPrefs.SetFloat("highscore",score);
        }

    }
 }
