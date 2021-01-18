using UnityEngine.Networking;
using UnityEngine;
using System.Net.NetworkInformation;
using UnityEngine.UI;

public class Racket : NetworkBehaviour
{
    Manneger manneger;
    private GameCamera gameCamera;
    [SyncVar] private int playerNumber;

    [SerializeField] private float speed = 0.1f;
    [SyncVar(hook = "OnPointsChange")] private int points;

    public int Points {
        get { return points; }
        set {
            if (value > 0 && points != value)
                points = value;
        }
    }

    private void OnPointsChange(int points) {
        gameCamera.UpdateUI(playerNumber, points);
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < GameCamera.Size.y - transform.localScale.y / 2) 
            transform.Translate(0, speed, 0);

            else if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > -GameCamera.Size.y + transform.localScale.y / 2)
                transform.Translate(0, -speed, 0);
        
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        base.OnStartLocalPlayer();
        if (isServer)
            playerNumber = 1;
        
    }

    private void Update()
    {
        if (transform.position.x == 8) {
            
            if (GameObject.Find("Text_R").transform.GetComponent<Text>().text == "10")
            {
                Debug.LogError("R Wiin");
                Destroy(GameObject.Find("Ball(Clone)"));
                GameObject.Find("Main Camera").GetComponent<Manneger>().setLogData("Winn", "10");
            }
            else if (GameObject.Find("Text_L").transform.GetComponent<Text>().text == "10")
            {

                Debug.LogError("R Fall");
                Destroy(GameObject.Find("Ball(Clone)"));
                GameObject.Find("Main Camera").GetComponent<Manneger>().setLogData("Fall", GameObject.Find("Text_R").transform.GetComponent<Text>().text);
            }

        } else

        if (transform.position.x == -8)
        {
            
            if (GameObject.Find("Text_L").transform.GetComponent<Text>().text == "10")
            {

                Debug.LogError("L Wiin");
                Destroy(GameObject.Find("Ball(Clone)"));
                GameObject.Find("Main Camera").GetComponent<Manneger>().setLogData("Winn", "10");
            }
            else if (GameObject.Find("Text_R").transform.GetComponent<Text>().text == "10")
            {

                Debug.LogError("L Fall");
                Destroy(GameObject.Find("Ball(Clone)"));
                GameObject.Find("Main Camera").GetComponent<Manneger>().setLogData("Fall", GameObject.Find("Text_L").transform.GetComponent<Text>().text);
            }

        }

    }

    private void Awake()
    {
        gameCamera = Camera.main.GetComponent<GameCamera>();
        //try
        //{
        //    GameObject go = GameObject.Find("Player(Clone)");
        //    Debug.LogError(go.name);
        //    if (go == null) {
        //        gameCamera.flaf = true;
        //    } else
        //        gameCamera.flaf = false;
        //}
        //catch { }
    }

   
}
