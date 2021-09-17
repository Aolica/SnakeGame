using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class snake : Enum
{
    public float speed = 1.0f;  //動く速度
    public float xrange;    //xの範囲(未実装)
    public float yrange;    //yの範囲
    //private float distance;
    public Direction direction; //動いている方向
    //private int kidnumber = 0;
    private Rigidbody2D rb;
    public GameObject manager;  //gamemanagerにアクセスするための変数
    // Start is called before the first frame update
    void Start() { 
        // Rigidbodyコンポーネントを取得する 
        rb = this.GetComponent<Rigidbody2D>(); 
        //manager = GameObject.Find("GameManager");
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void OnTriggerEnter2D(Collider2D other){    //衝突時の処理
        GameManager gm = manager.GetComponent<GameManager>();
        //Debug.Log("triggered!");
        switch(other.tag){
            case "Enemy":   //敵ならゲームオーバー
                    gm.status = GameStatus.GameOver;
                    //Destroy(other.gameObject);
                break;
            case "Food":    //餌ならスコア追加+体生成
                    Debug.Log("Yummy!");
                    gm.Score += 10;
                    gm.GenerateKids();  //体を生成
                    Destroy(other.gameObject);  //衝突した食料を壊す
                break;
            default:
                break;
        }
    }

    private void Move(){    //入力から動きを決め、rigidbodyの速度を変化させる
        if (Input.GetKey("up") && (direction != Direction.Down)) { 
            direction = Direction.Up;
            rb.velocity = new Vector3(0, speed, 0); 
        }
        if (Input.GetKey("down") && (direction != Direction.Up)) { 
            direction = Direction.Down;
            rb.velocity = new Vector3(0, -speed, 0); 
        }  
        if (Input.GetKey("right") && (direction != Direction.Left)) { 
            direction = Direction.Right;
            rb.velocity = new Vector3(speed, 0, 0); 
        }  
        if (Input.GetKey("left") && (direction != Direction.Right)) { 
            direction = Direction.Left;
            rb.velocity = new Vector3(-speed, 0, 0); 
        } 
        
    }
}
