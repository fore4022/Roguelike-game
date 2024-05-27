using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
using System.Linq;

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

    public List<Slot> inventoryData;
    public User userData;

    public List<Slot> inventory { get { setInventory(); return inventoryData; } }
    public User user { get { setUser(); return userData; } }

    private IDataReader Connection1()
    {
        string path = "URI=file:" + Application.streamingAssetsPath + DBName1;

        IDbConnection Connection = new SqliteConnection(path);

        Connection.Open();

        IDbCommand Command = Connection.CreateCommand();
        Command.CommandText = "SELECT * FROM " + TableName1;

        return Command.ExecuteReader();
    }
    private IDataReader Connection2()
    {
        string path = "URI=file:" + Application.streamingAssetsPath + DBName2;

        IDbConnection Connection = new SqliteConnection(path);

        Connection.Open();

        IDbCommand Command = Connection.CreateCommand();
        Command.CommandText = "SELECT * FROM " + TableName2;

        return Command.ExecuteReader();
    }
    private void setInventory()
    {
        if (inventoryData != null) { return; }

        IDataReader inventoryReader = Connection1();

        while (inventoryReader.Read())
        {
            Slot slot = new();

            slot.itemName = inventoryReader.GetString(1);
            slot.count = Convert.ToInt32(inventoryReader.GetString(2));

            inventoryData.Add(slot);
        }

        inventoryReader.Close();
    }
    private void setUser()
    {
        if(userData != null) { return; }

        IDataReader userReader = Connection2();

        userData.level = Convert.ToInt32(userReader.GetString(1));
        userData.exp = Convert.ToInt32(userReader.GetString(2));
        userData.gold = Convert.ToInt32(userReader.GetString(3));
        userData.topStage = Convert.ToInt32(userReader.GetString(4));
        userData.stage = Convert.ToInt32(userReader.GetString(5));
        userData.equippedItem = userReader.GetString(6);

        userReader.Close();
    }
    public void inventory_edit(string name, int count)
    {
        if(insert != null) { return; }

        IDataReader inventoryReader = Connection1();

        if(inventoryData.Any(item => item.itemName == name))
        {
            //insert
        }    
        else
        {
            //Update
        }

        setInventory();
        insert.Invoke();
    }
    public void user_edit(int level, int exp, int gold, int topStage, int stage, string equippedItem)
    {
        if(insert != null) { return; }

        IDataReader userReader = Connection2();

        setUser();
        insert.Invoke();
    }
}