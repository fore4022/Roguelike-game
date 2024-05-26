using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
using System.Xml.Linq;

public class Database
{
    [SerializeField]
    private string DBName1 = "/inventory.db";
    [SerializeField]
    private string TableName1 = "inventory";

    [SerializeField]
    private string DBName2 = "/user.db";
    [SerializeField]
    private string TableName2 = "user";

    public Action insert;

    //List<Item>

    public IDataReader inventoryReader { get { setInventory(); return _inventoryReader; } }
    public IDataReader userReader { get { setUser(); return _userReader; } }

    private IDataReader _inventoryReader;
    private IDataReader _userReader;

    private string pathString = "URI=file" + Application.streamingAssetsPath;

    private void setInventory()
    {
        string path = "URI=file:" + Application.streamingAssetsPath + DBName1;

        IDbConnection Connection = new SqliteConnection(path);

        Connection.Open();

        IDbCommand Command = Connection.CreateCommand();
        Command.CommandText = "SELECT * FROM " + TableName1;
        _inventoryReader = Command.ExecuteReader();

        _inventoryReader.Close();//
    }
    private void setUser()
    {
        IDbConnection Connection = new SqliteConnection(pathString + DBName2);

        Connection.Open();

        IDbCommand Command = Connection.CreateCommand();
        Command.CommandText = "SELECT * FROM " + TableName2;
        _userReader = Command.ExecuteReader();

        _userReader.Close();
    }
    public void inventory_insert()
    {
        if(insert != null) { return; }
        insert.Invoke();
    }
    public void user_insert()
    {
        if(insert != null) { return; }
        insert.Invoke();
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