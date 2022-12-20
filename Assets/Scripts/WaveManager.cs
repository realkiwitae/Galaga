using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // Start is called before the first frame update

    private bool bActive = false;
    int[] grid;
    float[] row_scores;
    [SerializeField]
    private float _vx = 1f;
    int gridempty;
    GameManager g;
    
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _popupprefab;

    [SerializeField]
    private Player _player;
    void Start()
    {
        g = GameManager.Instance;
        transform.position = new Vector3(g.minx+transform.localScale.x ,g.maxy - 3*transform.localScale.y/2,0);


        bActive = false;
        grid = new int[g.rules.H];

        for(int i = 0; i < grid.Length;i++){
            grid[i] = 0;
        }
        gridempty = 0;//(1 << g.rules.H) - 1;
        row_scores = new float[] {g.rules.bonus_row_0,g.rules.bonus_row_1,g.rules.bonus_row_2,g.rules.bonus_row_3,g.rules.bonus_row_4};

        EventManager.Instance.onEnemyDeath += onEnemyDeath;
    }

    private void OnDestroy() {
        EventManager.Instance.onEnemyDeath -= onEnemyDeath;        
    }

    // Update is called once per frame
    void Update()
    {
        if(!bActive){
            SpawnWave();
        }
        Move();
    }

    void Move(){

        if(g.minx + 1 > transform.position.x + _vx*Time.deltaTime){
            _vx *= -1; 
        }
        if(g.maxx - 1 < transform.position.x + GameManager.Instance.rules.W - 1 + _vx*Time.deltaTime){
            _vx *= -1; 
        }      
        transform.position += _vx*Time.deltaTime*Vector3.right;
    }   

    void SpawnWave(){
        bActive = true;
        gridempty = (1 << g.rules.H) - 1;
        for(int i = 0; i < grid.Length;i++){
            grid[i] = (1 << (g.rules.W)) - 1;
            for(int j = 0; j < g.rules.W; j++){
                GameObject a = Instantiate(
                    _enemyPrefab, 
                         (g.maxy + (i*g.rules.W +((i%2==0)?j:g.rules.W-1-j))*transform.localScale.y)*Vector3.up
                         + (g.maxx+g.minx)/2f*Vector3.right, 
                    Quaternion.identity);
                Enemy l = null;
                if(a) l = a.GetComponent<Enemy>();
                if(l) {
                    l.x = j;
                    l.y = i;
                    l.transform.SetParent(this.transform);
                }

            }
        }
    }

    void onEnemyDeath(int x, int y){
        grid[y] &= ~(1 << x);
        if(grid[y] == 0){
            // we get bonus points for row y
            EventManager.Instance.AddScore(row_scores[y]);
            // Instanciate popup
            GameObject a = Instantiate(
            _popupprefab,transform.position 
                + (y*transform.localScale.y)*Vector3.down + (x*transform.localScale.x)*Vector3.right
                ,
            Quaternion.identity);
            PopUpText p = a.GetComponent<PopUpText>();
            if(p){
                p.time_left = 1f;
                p.msg = "+" + row_scores[y] + "!";
            }
            gridempty &= ~(1 << y);
            if(gridempty == 0){
                bActive = false;
            }
        }
    }

}
