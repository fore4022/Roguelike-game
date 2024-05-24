using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
public class User_DB : MonoBehaviour
{
    private void Start()
    {
        string dbname = "/inventory.db";
        string connectionString = "URI=file:" + Application.streamingAssetsPath + dbname;
        string tableName = "inventory";

        IDbConnection dbConnection = new SqliteConnection(connectionString);
        dbConnection.Open();

        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        IDataReader dataReader = dbCommand.ExecuteReader();

        while(dataReader.Read())
        {
            string id = dataReader.GetString(1);
            int count = dataReader.GetInt32(2); 

            Debug.Log(id + ":" + count);
        }

        dataReader.Close();
    }
}
