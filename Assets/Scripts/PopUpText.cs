using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    [SerializeField]
    TextMesh pop;
    float time_left = 3;
    // Start is called before the first frame update
    void Start()
    {
        pop.text = "You Won!";
    }

    // Update is called once per frame
    void Update()
    {
        time_left -= Time.deltaTime;
        if(time_left < 0){
            Destroy(this.gameObject);
        }
    }
}
