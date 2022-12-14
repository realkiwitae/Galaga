using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _cy = 3.5f; 

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

    }

    private void OnTriggerEnter(Collider other) {

        if(other.tag == "Player"){
            
            Player p = other.transform.GetComponent<Player>();
            if(p != null){
                p.Damage();
            }
            Destroy(this.gameObject);
            
        }else if (other.tag == "Laser"){
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }


    void Move(){
        // to test collisions
        transform.position += Vector3.down*Time.deltaTime*_cy;
        GameManager g = GameManager.Instance;   
        if(transform.position.y + transform.localScale.y/2 < g.miny){
            transform.position = new Vector3(Random.Range(g.minx,g.maxx),g.maxy - transform.localScale.y/2,0);
        }
    }

}
