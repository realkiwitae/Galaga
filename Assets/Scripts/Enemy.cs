using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float _vy = 0f; 

// Fire 
    [SerializeField]
    private float _cooldown = .15f; 
    private float _canfire = -99f;
    private bool _bFire = false;
    [SerializeField]
    private GameObject _laserPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameManager g = GameManager.Instance;
        transform.position = new Vector3(Random.Range(g.minx,g.maxx),g.maxy - transform.localScale.y/2,0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        _bFire = Random.Range(0f,1f) > .99f;

        if(_bFire && Time.time > _canfire){
            FireLaser();
        }
    }

    private void OnTriggerEnter(Collider other) {

        if(other.tag == "Player"){
            
            Player p = other.transform.GetComponent<Player>();
            if(p != null){
                p.Damage();
            }
            Destroy(this.gameObject);
            
        }else if (other.tag == "Laser"){
            Laser l = other.gameObject.GetComponent<Laser>();
            if(!l) return;
            if(!l.shouldDestroy(this.tag))return;
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }


    void Move(){
        // to test collisions
     //   transform.position += Vector3.down*Time.deltaTime*_cy;
        GameManager g = GameManager.Instance;   
        if(transform.position.y + transform.localScale.y/2 < g.miny){
            transform.position = new Vector3(Random.Range(g.minx,g.maxx),g.maxy - transform.localScale.y/2,0);
        }
    }
    void FireLaser(){

        GameObject g = Instantiate(
            _laserPrefab,transform.position + (.2f+transform.localScale.y/2)*Vector3.down,
            Quaternion.identity);
        Laser l = null;
        if(g) l = g.GetComponent<Laser>();
        if(l) {
            l.target_tag = "Player";
        }
        _canfire = Time.time + _cooldown;

    }

}
