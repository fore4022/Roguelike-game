using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
public class User_DB : MonoBehaviour
{
    public static MySqlConnection SqlConn;

    public static string ipAddress = "127.0.0.1";
    public static string db_id;
    public static string db_pw;
    public static string db_name;

    private string strConn = string.Format("", ipAddress, db_id, db_pw, db_name);
    private void Awake()
    {
        try
        {
            SqlConn = new MySqlConnection(strConn);
        }
        catch(System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    private void Start()
    {
        string query = "select + from TB_TABLE";
    }
}
