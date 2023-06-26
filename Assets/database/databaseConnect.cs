using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql;
using MySql.Data.MySqlClient;
using Unity.VisualScripting;
using System;

public class databaseConnect : Exception
{
    static string conStr = "server = localhost; user = root; database = testConnect; port = 3306; password = 156278Lsk";
    MySqlConnection conne = new MySqlConnection(conStr);
    
}


