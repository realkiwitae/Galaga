using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    [SerializeField]
    TextMesh pop;

    private string _msg = "";
    public string msg{
        get{return _msg;}
        set{_msg=value;}
    }
    float _time_left = 3f;
    public float time_left{
        get{return _time_left;}
        set{_time_left=value;}
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(pop.text != _msg) pop.text = _msg;
        time_left -= Time.deltaTime;
        if(time_left < 0){
            Destroy(this.gameObject);
        }
    }
}
