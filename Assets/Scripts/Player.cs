using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _t_todx = 4f;
    public float horizontalInput;

    private float _minx;
    private float _maxx;

    private float _cvx = 0f;

    private float _vmax = 0f;
    
    private float _apos = 0f;
    
    private float _aneg = 0f;
 
    // Start is called before the first frame update
    void Start()
    {
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        transform.position = new Vector3() + (stageDimensions.y+transform.localScale.y/2)*Vector3.up;
        _minx = -stageDimensions.x;
        _maxx = stageDimensions.x;

        // capped vmax,and accelerations for targetted travel time and ramps
        _vmax = (_maxx-_minx)/_t_todx;
        _apos = _vmax/.5f;
        _aneg = -_vmax/.5f;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move(){
        // default input in Input Manager 
        horizontalInput = Input.GetAxis("Horizontal");

        // Traveling with acceleration ramp and vmax capped  
        float tx = transform.position.x;

        // target dx
        float dx = (_maxx-_minx)*horizontalInput;

        float ov = _cvx;
        // smooth factor
        float r = (Math.Abs(horizontalInput)>.2f)?1f:.2f;

	    float v_cible = Math.Clamp(dx / Time.deltaTime,-_vmax*r,_vmax*r);
	    float a_cible = Math.Clamp((v_cible-_cvx) / Time.deltaTime,_aneg,_apos);
	    _cvx += a_cible*Time.deltaTime;

        // ensure continuity of integral
	    tx += (_cvx+ov)/2f*Time.deltaTime;
        tx = Math.Clamp(tx, _minx + transform.localScale.x/2 , _maxx - transform.localScale.x/2 );
        
        transform.position = new Vector3(
            tx,
            transform.position.y,
            transform.position.z);       
    }
}
