using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Data;
using Mono.Data.Sqlite;


public class Registr_Autorize : MonoBehaviour
{
    public SqliteConnection dbConnection;
    private string path;
    public Button btnAutorize, btnRegister;
    public InputField loginField, passField;
    public GameObject textMessage;

    void Start() {

        btnAutorize.onClick.AddListener(delegate{ // this chekin autorize acc

            path = Application.dataPath + "/StreamingAssets/dataBase.db";
            dbConnection = new SqliteConnection("URI=file:" + path);
            dbConnection.Open();
            if (dbConnection.State == ConnectionState.Open)
            {
                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = dbConnection;
                cmd.CommandText = "SELECT * FROM privateData";
                
                SqliteDataReader r = cmd.ExecuteReader();
              
                while (r.Read())
                {
                    if (r[1].ToString() == loginField.text && r[2].ToString() == passField.text)
                    {
                   //     dbConnection.Open();
                        cmd = new SqliteCommand();
                        cmd.Connection = dbConnection;
                        cmd.CommandText = "SELECT * FROM logUsers WHERE id = (SELECT max(id) FROM logUsers)";
                         r = cmd.ExecuteReader();
                        int index = int.Parse(r[0].ToString()) + 1;
                        dbConnection.Close();
                        //dbConnection.Open();
                        //cmd = new SqliteCommand();
                        //cmd.Connection = dbConnection;
                        //string messText = "INSERT INTO logUsers(id, date, id_users) VALUES(" + index + ", '" + DateTime.Now + "', '" + r[0].ToString() + "')";
                        //cmd.CommandText = messText;
                        //cmd.ExecuteNonQuery();
                        //dbConnection.Close();
                        SceneManager.LoadScene("SampleScene");
                    }
                    else {
                        btnRegister.gameObject.SetActive(true);
                        textMessage.SetActive(true); 
                    }
                }
                
            }
        });
        
        btnRegister.onClick.AddListener(delegate{ // this chekin autorize acc
            string login = loginField.text;
            string pass = passField.text;
            path = Application.dataPath + "/StreamingAssets/dataBase.db";
            dbConnection = new SqliteConnection("URI=file:" + path);
            dbConnection.Open();
            if (dbConnection.State == ConnectionState.Open)
            {

                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = dbConnection;
                cmd.CommandText = "SELECT * FROM privateData WHERE id = (SELECT max(id) FROM privateData)";
                SqliteDataReader r = cmd.ExecuteReader();
                int index = int.Parse(r[0].ToString()) + 1;
                dbConnection.Close();

                dbConnection.Open();
                cmd = new SqliteCommand();
                cmd.Connection = dbConnection;
                string messText = "INSERT INTO privateData(id, login, pass) VALUES(" + index + ", '" + login + "', '" + pass + "')";
                cmd.CommandText = messText;
                cmd.ExecuteNonQuery();
                dbConnection.Close();
                SceneManager.LoadScene("SampleScene");
            }
        });

    }

    private void Update()
    {
        
    }

    void setConnection ()
    {

    }
}
 