using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : Enum
{
    public GameObject enemy;    //敵のプレハブ
    public GameObject food;     //餌のプレハブ
    public GameObject kidprefab;    //体のプレハブ
    public GameObject player;   //頭のプレハブ
    public Text scoreText;  //スコア表示用テキスト
    private int score = 0;  //スコア
    private List<GameObject> kids = new List<GameObject>(); //体を管理するためのリスト
    public GameStatus status = GameStatus.GameStart;    //ゲームの状態を管理する列挙型変数
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);  //乱数のシードを設定
        InvokeRepeating("Generate",2.0f,3.0f);  //3s毎にてきかエサを生成
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(score);
        //ゲームの状態で処理を変える    
        switch (status) 
        {
            case GameStatus.GameStart:  //ゲームが動いている場合
                    scoreText.text = string.Format("Score:{0}",score);  //テキスト更新
                    //UpdateKids(); 
                break;
            case GameStatus.GamePause:  //ポーズした場合(未実装)
                break;
            case GameStatus.GameOver:   //ゲームオーバーの時(未実装)
                    GameOver();
                break;
        }
    }

    private void Generate(){    //敵と餌を生成する関数
        if(Random.Range(0,10) > 5){
            Instantiate(enemy,new Vector3(Random.Range(-10,10),Random.Range(-5,5),0), Quaternion.identity); //敵を生成
        }else{
            Instantiate(food,new Vector3(Random.Range(-10,10),Random.Range(-5,5),0), Quaternion.identity); //餌を生成
        }
    }

    public void GenerateKids(){ //体を生成する関数
        GameObject obj,kid;
        if(kids.Count == 0) //一番最初だけ
        {
            obj = player.gameObject;    
            //頭の座標を元に体を生成
            kid = Instantiate(kidprefab,obj.transform.position ,Quaternion.identity) as GameObject; 
        }else{
            obj = kids[kids.Count - 1] as GameObject;
            //一番おしりの体の座標から新しい体を生成
            kid = Instantiate(kidprefab,obj.transform.position ,Quaternion.identity) as GameObject;
        }        
        KidBehaviour kb = kid.GetComponent<KidBehaviour>();
        kb.player = obj;    //名前と新しい体から見て一つ前の体を格納
        kb.name = "kid" + kids.Count.ToString();
        /*
        if(kids.Count == 0){    
            kb.direction = obj.GetComponent<snake>().direction;
        }else{
            kb.direction = obj.GetComponent<KidBehaviour>().direction;
        }
        */
        kids.Add(kid);  //リストに生成した体を追加
    }

/*    private void UpdateKids(){ 
        if(kids.Count == 0) return;
        for(int i = kids.Count - 1 ; i > 1 ; i--)
        {
            kids[i].GetComponent<KidBehaviour>().direction = kids[i - 1].GetComponent<KidBehaviour>().direction;
        }
        kids[0].GetComponent<KidBehaviour>().direction = player.gameObject.GetComponent<snake>().direction;
    
    }
*/

    private Vector3 KidVector(Direction dr){    //生成する位置を入力の向きに応じて返す関数
        switch (dr)
        {
            case Direction.Up:
                return new Vector3(0.0f,-2.0f,0.0f);
            case Direction.Down:
                return new Vector3(0.0f,2.0f,0.0f);
            case Direction.Right:
                return new Vector3(-2.0f,0.0f,0.0f);
            case Direction.Left:
                return new Vector3(2.0f,0.0f,0.0f);
            default:
                return new Vector3(0.0f,0.0f,0.0f);
        }
    }

    private void GameOver(){    //ゲームオーバー時の処理
        scoreText.text = string.Format("Game Over...Score:{0}",score);
    }

    public int Score{   //スコアのプロパティ
        set {this.score = value;}
        get {return this.score;}
    }

}
