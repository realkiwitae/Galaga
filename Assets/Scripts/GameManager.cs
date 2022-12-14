using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameState{
    GAMEOVER,
    PLAYING
}

public class Rules{
    public int player_nblives = 3;
    public float player_laser_vy = 15f;
    public float enemy_laser_vy = -10f;
};

public class GameManager{
    private static GameManager _instance;
    GameManager() {
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        maxy = -stageDimensions.y;
        miny = stageDimensions.y;
        minx = -stageDimensions.x;
        maxx = stageDimensions.x;

        rules = new Rules();
        Debug.Log("PLAYING");
    }    
 
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

    public void playerDeath(){
        gameState = EGameState.GAMEOVER;
        Debug.Log("GAMEOVER");
    }
 }
