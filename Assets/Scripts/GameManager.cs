using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager{
    private static GameManager _instance;
    GameManager() {
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        maxy = -stageDimensions.y;
        miny = stageDimensions.y;
        minx = -stageDimensions.x;
        maxx = stageDimensions.x;

    }    
 
    public static GameManager Instance {
        get {
            if(_instance==null) {
                _instance = new GameManager();
            }
 
            return _instance;
        }
    }
    public string test = "YOO";
    public float maxx,minx,miny,maxy;

 }
