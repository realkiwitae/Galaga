using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public static EventManager Instance;
    
    void Awake()
    {
        Instance = this;
    }

    // Events
    public event Action<int,int> onEnemyDeath;
    public void EnemyDeath(float s, int x, int y){
        if(onEnemyDeath != null)onEnemyDeath(x,y);
        if(onAddScore != null)onAddScore(s);
    }
    public event Action<int,int> onEnemyReady;
    public void EnemyReady(int x, int y){
        if(onEnemyReady != null)onEnemyReady(x,y);
    }
    public event Action<int> onPlayerHurt;
    public void PlayerHurt(int d){
        if(onPlayerHurt == null) return;
        onPlayerHurt(d);
    }
    public event Action<float> onAddScore;
    public void AddScore(float s){
        if(onAddScore == null) return;
        onAddScore(s);
    }

    public event Action<int,int> onEnemyDive;
    public void EnemyDive(int i, int j){
        if(onEnemyDive == null) return;
        onEnemyDive(i,j);
    }
}
