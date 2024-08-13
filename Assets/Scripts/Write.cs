using System;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using TMPro;

public class Write : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI gmail;

    public TextMeshProUGUI Score;
    //public TextMeshProUGUI ScoreString;
    //public Text age;
    private string connectionString;
    private MySqlConnection MS_Connection;
    private MySqlCommand MS_Command;
    string query;

    public void sendInfo() {

        connection();
        //String Score = this.Score.text;
       // String ScoreString = this.Score.text;
       // int.TryParse(ScoreString, out int Score);

        query = "insert into score(name_u, email_u, score) values( '" + name.text + "' , '" + gmail.text + "', '" + Score.text +"');";

        MS_Command = new MySqlCommand(query, MS_Connection);

        MS_Command.ExecuteNonQuery();

        MS_Connection.Close();
    }

    public void connection() {

        connectionString = "Server = localhost ; Database = intractive_DB ; User = root; Password = ; Charset = utf8;";
        MS_Connection = new MySqlConnection(connectionString);

        MS_Connection.Open();

    }

}
