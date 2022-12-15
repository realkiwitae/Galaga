using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{


    [SerializeField]
    private float _vy = 0f;
    
    private GameObject  _owner = null;
    public GameObject owner {
    get {
        return _owner;
    }
    set{_owner = value;}
    }

    public float vy {
        get {
            return _vy;
        }
        set{
            _vy = value;
            
        }
    }
    private string _target_tag = "Enemy";
    public string target_tag {
    get {
        return _target_tag;
    }
    set{_target_tag = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
        _vy = target_tag == "Player"? GameManager.Instance.rules.enemy_laser_vy 
            :GameManager.Instance.rules.player_laser_vy;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up* _vy * Time.deltaTime);
        if(transform.position.y > GameManager.Instance.maxy + transform.localScale.y
        || transform.position.y < GameManager.Instance.miny - transform.localScale.y)
        {
            Destroy(this.gameObject);
        }
    }

    public bool shouldDestroy(string _tag){
        return _target_tag == _tag;
    }
}
