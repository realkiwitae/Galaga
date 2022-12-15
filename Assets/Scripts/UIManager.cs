using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoretext;
    [SerializeField]
    private Text livestext;
    [SerializeField]
    private Player p;

    // Start is called before the first frame update
    void Start()
    {   
        scoretext.text = "Score: " + 0;
        livestext.text = "Lives: " + GameManager.Instance.rules.player_nblives;
    }

    // Update is called once per frame
    void Update()
    {
        if(p){
            scoretext.text = "Score:" + p.score;   
            livestext.text = "Lives: " + p.lives;
        }else{
            livestext.text = "Lives: 0";
        }
    }
}
