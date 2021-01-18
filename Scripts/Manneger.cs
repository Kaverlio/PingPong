using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class Manneger : MonoBehaviour
{
    public SqliteConnection dbConnection;
    private string path;
    public GameObject prefab, panel;
    public Text timer;
    float y = 44.3f, setTime = 0;
    private List<String> dbContentTime = new List<string>();
    private List<String> dbContentResult = new List<string>();
    private List<String> dbContentPoint = new List<string>();

    [Obsolete]
    void Start()
    {
        path = Application.dataPath + "/StreamingAssets/dataBase.db";
        dbConnection = new SqliteConnection("URI=file:" + path);
        dbConnection.Open();
        if (dbConnection.State == ConnectionState.Open)
        {
            SqliteCommand cmd = new SqliteCommand();
            cmd.Connection = dbConnection;
            cmd.CommandText = "SELECT * FROM logData";
            SqliteDataReader r = cmd.ExecuteReader();
            while (r.Read()) {
                dbContentTime.Add(r[1].ToString());
                dbContentResult.Add(r[2].ToString());
                dbContentPoint.Add(r[3].ToString());
            }
            dbConnection.Close();

            if (dbContentPoint.Count != 0) {
                if (dbContentPoint.Count <= 10)
                {
                    for (int i = 0; i < dbContentPoint.Count; i++)
                    {

                        GameObject obj = Instantiate(prefab);//, new Vector3(panel.transform.position.x, panel.transform.position.y + y, panel.transform.position.z), panel.transform.rotation
                        obj.transform.localScale = new Vector3(1, 1, 1);
                        obj.transform.SetParent(panel.transform);
                        obj.transform.localPosition = new Vector3(0f, prefab.transform.localPosition.y - y, 0f);
                        obj.transform.localScale = new Vector3(1f, 1f, 1f);

                        RectTransform rec = (RectTransform)obj.transform;
                        y += 44.3f;
                        obj.transform.FindChild("Text_Time").GetComponent<Text>().text = dbContentTime[i];
                        obj.transform.FindChild("Text_Result_Battle").GetComponent<Text>().text = dbContentResult[i];
                        obj.transform.FindChild("Text_Point").GetComponent<Text>().text = dbContentPoint[i];

                    }
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        GameObject obj = Instantiate(prefab);//, new Vector3(panel.transform.position.x, panel.transform.position.y + y, panel.transform.position.z), panel.transform.rotation
                        obj.transform.localScale = new Vector3(1, 1, 1);
                        obj.transform.SetParent(panel.transform);
                        obj.transform.localPosition = new Vector3(0f, prefab.transform.localPosition.y - y, 0f);
                        obj.transform.localScale = new Vector3(1f, 1f, 1f);

                        RectTransform rec = (RectTransform)obj.transform;
                        y += 44.3f;
                        obj.transform.FindChild("Text_Time").GetComponent<Text>().text = dbContentTime[dbContentTime.Count - i - 1];
                        obj.transform.FindChild("Text_Result_Battle").GetComponent<Text>().text = dbContentResult[dbContentTime.Count - i - 1];
                        obj.transform.FindChild("Text_Point").GetComponent<Text>().text = dbContentPoint[dbContentTime.Count - i - 1];
                    }
                }
            }

            

        }
    }

    private void Update()
    {
        try {
            if (GameObject.Find("Player(Clone)") != null) {
                panel.SetActive(false);
            }
            if (GameObject.Find("Ball(Clone)") != null)
            {
                
                setTime += 1 * Time.deltaTime;
                timer.text = setTime.ToString("F0");
            }
        } catch { }
        

    }

    public void setLogData(string status, string point) {
        Debug.LogError("S " + status + " P " + point);
        dbConnection.Open();
        if (dbConnection.State == ConnectionState.Open)
        {

            SqliteCommand cmd = new SqliteCommand();
            cmd.Connection = dbConnection;
            cmd.CommandText = "SELECT * FROM logData WHERE id = (SELECT max(id) FROM logData)";
            SqliteDataReader r = cmd.ExecuteReader();
            int index = int.Parse(r[0].ToString()) + 1;
            dbConnection.Close();

            dbConnection.Open();
        cmd = new SqliteCommand();
        cmd.Connection = dbConnection;
        string messText = "INSERT INTO logData(id, time, resultBattle, point) VALUES(" + index + ", '" + timer.text + "', '" + status + "', '" + point + "')";
        cmd.CommandText = messText;
        cmd.ExecuteNonQuery();
        dbConnection.Close();
            SceneManager.LoadScene("SampleScene");
        } }
}
