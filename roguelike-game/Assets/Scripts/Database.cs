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

    public List<Slot> inventoryData;
    public User userData;

    public List<Slot> inventory { get { setInventory(); return inventoryData; } }
    public User user { get { setUser(); return userData; } }


    private IDbConnection connect(string dbName)
    {
        string path = "URI=file:" + Application.streamingAssetsPath + dbName;
        return new SqliteConnection(path);
    }
    private void setInventory()
    {
        if (inventoryData != null) { return; }

        IDbConnection connection = connect(DBName1);

        connection.Open();

        IDbCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM " + TableName1;
        IDataReader inventoryReader = command.ExecuteReader();

        inventoryReader.Close();
    }
    private void setUser()
    {
        if (userData != null) { return; }

        IDbConnection connection = connect(DBName2);

        connection.Open();

        IDbCommand Command = connection.CreateCommand();
        Command.CommandText = "SELECT * FROM " + TableName2;
        IDataReader userReader = Command.ExecuteReader();

        userReader.Close();
    }
    public void inventory_edit(string name, int count)
    {
        //if(insert == null) { return; }

        IDbConnection connection = connect(DBName1);

        connection.Open();

        IDbCommand command = connection.CreateCommand();

        command.CommandType = CommandType.Text;
        string a = $"INSERT INTO inventory (ItemID, Count) VALUES ({name}, {count})";
        command.CommandText = a;

        command.ExecuteNonQuery();

        command.Dispose();
        command = null;

        setInventory();
        //insert.Invoke();
    }
    public void user_edit(int level, int exp, int gold, int topStage, int stage, string equippedItem)
    {
        //if(insert == null) { return; }

        IDbConnection connection = connect(DBName2);

        connection.Open();
        IDbCommand command = connection.CreateCommand();

        command.CommandType = CommandType.Text;
        string a = $"INSERT INTO user (level, exp, gold, topstage, stage, EquippedItem) VALUES ({level}, {exp}, {gold}, {topStage}, {stage}, {equippedItem})";
        command.CommandText = a;

        command.ExecuteNonQuery();

        command.Dispose();
        command = null;

        setUser();
        //insert.Invoke();
    }
}