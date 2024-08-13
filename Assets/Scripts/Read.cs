using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using System.Collections;

public class Read : MonoBehaviour
{
    private string connectionString;
    private string query;
    private MySqlConnection MS_Connection;
    private MySqlCommand MS_Command;
    private MySqlDataReader MS_Reader;
    public Text textCanvas;

    private void Start()
    {
        StartCoroutine(UpdateViewInfo());
    }

    private IEnumerator UpdateViewInfo()
    {
        while (true)
        {
            viewInfo();
            yield return new WaitForSeconds(5); // Wait for 5 seconds before checking again
        }
    }

    private void viewInfo()
    {
        textCanvas.text = ""; // Clear the previous data

        query = "SELECT * FROM score";

        connectionString = "Server = localhost ; Database = intractive_DB ; User = root; Password = ; Charset = utf8;";

        MS_Connection = new MySqlConnection(connectionString);
        MS_Connection.Open();

        MS_Command = new MySqlCommand(query, MS_Connection);

        MS_Reader = MS_Command.ExecuteReader();
        while (MS_Reader.Read())
        {
            textCanvas.text += $"{MS_Reader[1],25}{MS_Reader[3],5}\n";
        }
        MS_Reader.Close();
        MS_Connection.Close();
    }
}/*

using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

public class Read : MonoBehaviour
{
    private string connectionString;
    private string query;
    private MySqlConnection MS_Connection;
    private MySqlCommand MS_Command;
    private MySqlDataReader MS_Reader;
    public Text firstPlaceText;
    public Text secondPlaceText;
    public Text thirdPlaceText;
    public Text otherScoresText;

    public void viewInfo()
    {
        query = "SELECT * FROM score ORDER BY score DESC";

        connectionString = "Server = localhost ; Database = intractive_DB ; User = root; Password = ; Charset = utf8;";

        MS_Connection = new MySqlConnection(connectionString);
        MS_Connection.Open();

        MS_Command = new MySqlCommand(query, MS_Connection);

        MS_Reader = MS_Command.ExecuteReader();

        List<(string name, int score)> scores = new List<(string, int)>();
        while (MS_Reader.Read())
        {
            scores.Add((MS_Reader[0].ToString(), int.Parse(MS_Reader[1].ToString())));
        }
        MS_Reader.Close();

        // Sort the scores from highest to lowest
        scores = scores.OrderByDescending(s => s.Item2).ToList();

        // Display the top 3 scores
        if (scores.Count > 0)
        {
            firstPlaceText.text = $"1st Place: {scores[0].Item1} - {scores[0].Item2} points";
        }
        if (scores.Count > 1)
        {
            secondPlaceText.text = $"2nd Place: {scores[1].Item1} - {scores[1].Item2} points";
        }
        if (scores.Count > 2)
        {
            thirdPlaceText.text = $"3rd Place: {scores[2].Item1} - {scores[2].Item2} points";
        }

        // Display the other scores
        string otherScores = "";
        for (int i = 3; i < scores.Count; i++)
        {
            otherScores += $"{scores[i].Item1} - {scores[i].Item2} points\n";
        }
        otherScoresText.text = otherScores;

        MS_Connection.Close();
    }
}*/
