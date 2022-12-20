using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _t_todx = 3f;
    private float _cvx = 0f;
    private float _cvy = 0f;
    private float _vmax = 0f;
    private float _apos = 0f;
    private float _aneg = 0f;
    [SerializeField]
    private float yx = 2f; //faster in y
    public enum ESTATE_MACHINE{
        SPAWN,
        TO_GRID,
        READY,
        DIVE,
    };

    private ESTATE_MACHINE state;

    public int x,y;

// Fire 
    [SerializeField]
    private int _score= 100;

    [SerializeField]
    private float _cooldown = .15f; 
    private float _canfire = -99f;
    private bool _bFire = false;
    [SerializeField]
    private GameObject _laserPrefab;
    // Start is called before the first frame update
    private float target_x,target_y;
    private Transform gridpos;

    void Start()
    {
        GameManager g = GameManager.Instance;
        Player p = Player.Instance;
        state = ESTATE_MACHINE.SPAWN;
        gridpos = transform.parent;
        transform.parent = null;
        if(!p || g == null )return;        
        _vmax = (g.maxx-g.minx)/_t_todx;
        _apos = _vmax/.2f;
        _aneg = -_vmax/.01f;


        target_x = p.transform.position.x;
        target_y = p.transform.localScale.y + p.transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        //Rnd fire
        _bFire = UnityEngine.Random.Range(0f,1f) > .9999f;
        if(_bFire && Time.time > _canfire){
            FireLaser();
        }

        switch(state){
            case ESTATE_MACHINE.SPAWN:  // top screen to above player trajectory
                Move();
                if(Mathf.Abs(target_x-transform.position.x) + Mathf.Abs(target_y-transform.position.y) < .001f){
                    state = ESTATE_MACHINE.TO_GRID;
                    //transform.position + (i*transform.localScale.y)*Vector3.down + (j*transform.localScale.x)*Vector3.right,
                }
                break;
            case ESTATE_MACHINE.TO_GRID:
                target_x = gridpos.position.x + x*gridpos.localScale.x;
                target_y = gridpos.position.y - y*gridpos.localScale.y;
                Move();
                if(Mathf.Abs(target_x-transform.position.x) + Mathf.Abs(target_y-transform.position.y) < .001f){
                    state = ESTATE_MACHINE.READY;
                    transform.SetParent(gridpos);
                }
                break;
            case ESTATE_MACHINE.READY: // stay on wave grid
                break;
            case ESTATE_MACHINE.DIVE: // triggered by wave
                break;
            default:
                break;
        }


    }

    private void OnTriggerEnter(Collider other) {

        if(other.tag == "Player"){
            
            EventManager.Instance.PlayerHurt(1);
            EventManager.Instance.EnemyDeath(0,this.x,this.y);
            Destroy(this.gameObject);
            
        }else if (other.tag == "Laser"){
            Laser l = other.gameObject.GetComponent<Laser>();
            if(!l) return;
            if(!l.shouldDestroy(this.tag))return;

            EventManager.Instance.EnemyDeath(_score,this.x,this.y);

            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }


    void Move(){
        // to test collisions
     //   transform.position += Vector3.down*Time.deltaTime*_cy;
        GameManager g = GameManager.Instance;   
        float tx = transform.position.x;
            // target dx
        float dx = target_x-tx;

        float ov = _cvx;
        // smooth factor
        float r = 1f;

	    float v_cible = Math.Clamp(dx / Time.deltaTime,-_vmax*r,_vmax*r);
	    float a_cible = Math.Clamp((v_cible-_cvx) / Time.deltaTime,_aneg,_apos);
	    _cvx += a_cible*Time.deltaTime;

	    dx = (_cvx+ov)/2f*Time.deltaTime;

        float ty = transform.position.y;
            // target dx
        float dy = target_y-ty;
        r = yx;
	    v_cible = Math.Clamp(dy / Time.deltaTime,-_vmax*r,_vmax*r);
	    a_cible = Math.Clamp((v_cible-_cvy) / Time.deltaTime,_aneg,_apos);
	    
        ov = _cvy;
        _cvy += a_cible*Time.deltaTime;

        // ensure continuity of integral
	    dy = (_cvy+ov)/2f*Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position,new Vector3(target_x,target_y,0f),Mathf.Sqrt(dx*dx+dy*dy));

    }
    void FireLaser(){

        GameObject g = Instantiate(
            _laserPrefab,transform.position + (.2f+transform.localScale.y/2)*Vector3.down,
            Quaternion.identity);
        Laser l = null;
        if(g) l = g.GetComponent<Laser>();
        if(l) {
            l.target_tag = "Player";
            l.owner = this.gameObject;
        }
        _canfire = Time.time + _cooldown;

    }

}
