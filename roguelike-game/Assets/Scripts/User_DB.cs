using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
public class User_DB : MonoBehaviour
{
    [SerializeField]
    private string inventoryDBName = "/inventory.db";
    [SerializeField]
    private string inventoryTableName = "inventory";

    [SerializeField]
    private string userDBName = "/user.db";
    [SerializeField]
    private string userTableName = "inventory";

    public IDataReader inventoryReader;
    public IDataReader userReader;

    private string pathString = "URI=file" + Application.streamingAssetsPath;

    private void Start()
    {
        IDbConnection Connection = new SqliteConnection(pathString);

        Connection.Open();

        IDbCommand Command = Connection.CreateCommand();
        Command.CommandText = "SELECT * FROM " + inventoryTableName;
        inventoryReader = Command.ExecuteReader();

        inventoryReader.Close();//

        Connection = new SqliteConnection(pathString);

        Connection.Open();

        Command = Connection.CreateCommand();
        Command.CommandText = "SELECT * FROM " + userTableName;
        userReader = Command.ExecuteReader();

        userReader.Close();
    }
}
/*
 string dbname = "/inventory.db";
        string connectionString = "URI=file:" + Application.streamingAssetsPath + dbname;
        string tableName = "inventory";

        IDbConnection dbConnection = new SqliteConnection(connectionString);
        dbConnection.Open();

        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        IDataReader dataReader = dbCommand.ExecuteReader();

        //FieldCount
        while(dataReader.Read())
        {
            string id = dataReader.GetString(1);
            long count = dataReader.GetInt64(2);

            Debug.Log(id + " : " + count);
        }

        dataReader.Close();
 */