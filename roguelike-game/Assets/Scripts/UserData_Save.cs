using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class UserData_Save : MonoBehaviour
{
    public User user;
    private System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new();
    private MemoryStream ms = new();
    private void Save()
    {
        bf.Serialize(ms, user);
        ms.Position = 0;
        User u = (User)bf.Deserialize(ms);
    }
}
