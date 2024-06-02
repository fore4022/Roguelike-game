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

    public Action edit;

    public List<Slot> inventoryData = new List<Slot>();
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
        IDbConnection connection = connect(DBName1);

        connection.Open();

        IDbCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM " + TableName1;
        IDataReader inventoryReader = command.ExecuteReader();

        while (inventoryReader.Read())
        {
            Slot slot = new();

            slot.itemName = inventoryReader.GetString(0);
            slot.count = inventoryReader.GetInt32(1);
            slot.equipped = inventoryReader.GetInt32(2);

            inventoryData.Add(slot);
        }

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
    public void inventory_edit(string name, int count, int equipped = 0, bool isInsert = false)
    {
        if (edit == null) { return; }

        IDbConnection connection = connect(DBName1);

        connection.Open();

        IDbCommand command = connection.CreateCommand();

        command.CommandType = CommandType.Text;

        if(isInsert && inventoryData.Any(item => item.itemName == name))
        {
            command.CommandText = "DELETE FROM inventory WHERE ItemID = @ItemID";

            command.Parameters.Add(new SqliteParameter("@ItemID", name));
        }
        else if (inventoryData.Any(item => item.itemName != name) || inventoryData.Count == 0)
        {
            command.CommandText = "INSERT INTO inventory (ItemID, Count, Equipped) VALUES (@ItemID, @Count, @Equipped)";

            command.Parameters.Add(new SqliteParameter("@ItemID", name));
            command.Parameters.Add(new SqliteParameter("@Count", count));
            command.Parameters.Add(new SqliteParameter("@Equipped", equipped));
        }
        else if(inventoryData.Any(item => item.itemName == name))
        {
            command.CommandText = "UPDATE inventory SET Count = @Count, Equipped = @Equipped WHERE ItemID = @ItemID";

            command.Parameters.Add(new SqliteParameter("@Count", count));
            command.Parameters.Add(new SqliteParameter("@Equipped", equipped));
            command.Parameters.Add(new SqliteParameter("@ItemID", name));
        }

        command.ExecuteNonQuery();

        command.Dispose();
        connection.Close();

        setInventory();
        edit.Invoke();
    }
    public void user_edit(int level, int exp, int gold, int topStage, int stage, string equippedItem)
    {
        if (edit == null) { return; }

        IDbConnection connection = connect(DBName2);

        connection.Open();
        IDbCommand command = connection.CreateCommand();

        command.CommandText = "INSERT INTO user (level, exp, gold, topstage, stage, EquippedItem) VALUES (@level, @exp, @gold, @topStage, @stage, @equippedItem)";

        command.Parameters.Add(new SqliteParameter("@level", level));
        command.Parameters.Add(new SqliteParameter("@exp", exp));
        command.Parameters.Add(new SqliteParameter("@gold", gold));
        command.Parameters.Add(new SqliteParameter("@topStage", topStage));
        command.Parameters.Add(new SqliteParameter("@stage", stage));
        command.Parameters.Add(new SqliteParameter("@equippedItem", equippedItem));

        command.ExecuteNonQuery();

        command.Dispose();

        connection.Close();

        setUser();
        edit.Invoke();
    }
}