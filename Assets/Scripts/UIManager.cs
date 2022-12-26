using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoretext;
    [SerializeField]
    private Text targettext;
    [SerializeField]
    private Text livestext;
    [SerializeField]
    private Player p;

    // Start is called before the first frame update
    void Start()
    {   
        if(GameManager.Instance.gameMode == 0){
            scoretext.text = "Score: " + 0;
            targettext.text = "Target: " + PlayerPrefs.GetFloat("highscore"); 
        }else{
            scoretext.text = "Kills: " + 0;
            targettext.text = "Target: " + PlayerPrefs.GetInt("highkill"); 
        }

        livestext.text = "Lives: " + GameManager.Instance.rules.player_nblives;

        EventManager.Instance.onPlayerUpdate += PlayerUpdate;
    }

    private void OnDestroy() {
        EventManager.Instance.onPlayerUpdate -= PlayerUpdate;     
    }

    private void PlayerUpdate(){
        if(p){
            if(GameManager.Instance.gameMode == 0){
                scoretext.text = "Score: " + p.score;
            }else{
                scoretext.text = "Kills: " + p.kill_count;  
            }
            livestext.text = "Lives: " + p.lives;   
        }else{
            livestext.text = "Lives: " + 0; 
        }
    }
}
