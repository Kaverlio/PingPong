using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private List<Text> pointsText;
    Manneger manneger;
    public bool flaf;
    private static Vector2 size;
    public static Vector2 Size {

        get { return size; }
    
    }

    public void UpdateUI(int racketNumber, int points)
    {
        pointsText[racketNumber].text = points.ToString();
    }

    private void Start()
    {
        var cam = Camera.main;
        size = new Vector2(cam.orthographicSize * cam.aspect, cam.orthographicSize);
    }

    private void Update()
    {
        //try
        //{
        //    if (flaf == true)
        //    {
        //        if (pointsText[0].text == "10")
        //        {
        //            Destroy(GameObject.Find("Ball(Clone)"));
        //            GameObject.Find("Main Camera").GetComponent<Manneger>().setLogData("Fall", pointsText[1].text);
        //        }
        //        else if (pointsText[1].text == "10")
        //        {
        //            Destroy(GameObject.Find("Ball(Clone)"));
        //            GameObject.Find("Main Camera").GetComponent<Manneger>().setLogData("Winn", pointsText[1].text);
        //        }
        //    }
        //    else if (flaf == false){
        //        if (pointsText[0].text == "10")
        //        {
        //            Destroy(GameObject.Find("Ball(Clone)"));
        //            GameObject.Find("Main Camera").GetComponent<Manneger>().setLogData("Winn", pointsText[1].text);
        //        }
        //        else if (pointsText[1].text == "10")
        //        {
        //            Destroy(GameObject.Find("Ball(Clone)"));
        //            GameObject.Find("Main Camera").GetComponent<Manneger>().setLogData("Fall", pointsText[1].text);
        //        }
        //    }
            
        //}
        //catch { }
    }

}
