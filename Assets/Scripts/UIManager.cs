using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private Player p;

    // Start is called before the first frame update
    void Start()
    {   
        text.text = "Score: " + 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(p)text.text = "Score:" + p.score;       
    }
}
