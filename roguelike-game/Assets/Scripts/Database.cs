using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
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

    public List<Item> inventoryData;

    public User userData;

    public List<Item> inventory { get { setInventory(); return inventoryData; } }
    public User user { get { setUser(); return userData; } }

    private IDataReader _inventoryReader;
    private IDataReader _userReader;
    private void setInventory()
    {
        if (inventoryData != null) { return; }

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
        if(userData != null) { return; }

        string path = "URI=file:" + Application.streamingAssetsPath + DBName2;

        IDbConnection Connection = new SqliteConnection(path);

        Connection.Open();

        IDbCommand Command = Connection.CreateCommand();
        Command.CommandText = "SELECT * FROM " + TableName2;
        _userReader = Command.ExecuteReader();

        _userReader.Close();
    }
    public void inventory_insert()
    {
        if(insert != null) { return; }
        setInventory();
        insert.Invoke();
    }
    public void user_insert()
    {
        if(insert != null) { return; }
        setUser();
        insert.Invoke();
    }
}