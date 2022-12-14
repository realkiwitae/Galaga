using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{


    [SerializeField]
    private float _vy = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up* _vy * Time.deltaTime);
        if(transform.position.y > GameManager.Instance.maxy + transform.localScale.y){
            Destroy(this.gameObject);
        }
    }
}
