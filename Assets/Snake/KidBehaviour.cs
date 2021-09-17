using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidBehaviour : Enum
{
    public GameObject player; //一つ前の体の状態を格納する変数
    //public float speed = 5;
    //public Direction direction; //動いてる向き
    //private Rigidbody2D rb;
    private Vector3 position = new Vector3(50,50,0);    //一個前の体の座標を格納する変数

    // Start is called before the first frame update
    void Start()
    {
       // rb = this.GetComponent<Rigidbody2D>(); 
       StartCoroutine("PositionUpdate");    //コルーチン開始
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Move(){
        this.transform.position = position; //このオブジェクトをpositionの位置(0.2秒前の一個前の体の位置)へ移動
        position = player.transform.position;   //1こ前の体の位置を取得(*ディープコピー)
    }

    private IEnumerator PositionUpdate(){   //0.2秒毎に座標を更新する
        for(;;){
            Move();
            yield return new WaitForSeconds(0.2f);
        }
    }
    /*
    private void Move(){
        //this.transform.position = player.transform.position + new Vector3(3.0f,0.0f,0.0f);
        var parentposition = player.transform.position;
        x = y = 0.0f;
        if ((parentposition.y - this.transform.position.y > 0.5 ) && direction != direction.up) {     //親が自分より上にいたら上へ
            y = speed;
            this.transform.position.y = parentposition.y + 0.5f;
        }
        if (parentposition.y - this.transform.position.y < -0.5) { //親が下にいたら下へ
            y = -speed;
        }  
        if (parentposition.x - this.transform.position.x > 0.5) {  //親が右にいたら右へ
            x = speed;
        }  
        if (parentposition.x - this.transform.position.x < -0.5) {    //親が左にいたら左へ
            x = -speed;
        } 
        rb.velocity = new Vector3(x,y,0);
    }
    */
}
