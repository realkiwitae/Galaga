using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
// Movement
    [SerializeField]
    private float _t_todx = 4f;
    public float horizontalInput;

    private int _lives = 3;

    private float _cvx = 0f;

    private float _vmax = 0f;
    
    private float _apos = 0f;
    
    private float _aneg = 0f;
 
// Fire 
    [SerializeField]
    private float _cooldown = .15f; 
    private float _canfire = -99f;
    [SerializeField]
    private GameObject _laserPrefab;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3() 
                    + (GameManager.Instance.miny+transform.localScale.y/2)*Vector3.up;
  
        // capped vmax,and accelerations for targetted travel time and ramps
        _vmax = (GameManager.Instance.maxx-GameManager.Instance.minx)/_t_todx;
        _apos = _vmax/.5f;
        _aneg = -_vmax/.5f;

        _lives = GameManager.Instance.rules.player_nblives;

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire){
            FireLaser();
        }
    }

    void Move(){
        // default input in Input Manager 
        horizontalInput = Input.GetAxis("Horizontal");

        // Traveling with acceleration ramp and vmax capped  
        float tx = transform.position.x;

        // target dx
        float dx = (GameManager.Instance.maxx-GameManager.Instance.minx)*horizontalInput;

        float ov = _cvx;
        // smooth factor
        float r = (Math.Abs(horizontalInput)>.2f)?1f:.2f;

	    float v_cible = Math.Clamp(dx / Time.deltaTime,-_vmax*r,_vmax*r);
	    float a_cible = Math.Clamp((v_cible-_cvx) / Time.deltaTime,_aneg,_apos);
	    _cvx += a_cible*Time.deltaTime;

        // ensure continuity of integral
	    tx += (_cvx+ov)/2f*Time.deltaTime;
        tx = Math.Clamp(tx, 
            GameManager.Instance.minx + transform.localScale.x/2 ,
            GameManager.Instance.maxx - transform.localScale.x/2 );
        
        transform.position = new Vector3(
            tx,
            GameManager.Instance.miny + transform.localScale.y/2,
            0f);       
    }

    void FireLaser(){

        GameObject g = Instantiate(
            _laserPrefab,transform.position + (.2f+transform.localScale.y/2)*Vector3.up,
            Quaternion.identity);

        _canfire = Time.time + _cooldown;
    }

    public void Damage(){
    
        if(--_lives < 1){
            GameManager.Instance.playerDeath();
            Destroy(this.gameObject);            
        }
    }
}

