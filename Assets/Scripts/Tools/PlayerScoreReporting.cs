using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerScoreReporting : MonoBehaviour
{
    void Start()
    {
        Dictionary<string, int> scores = new Dictionary<string, int>();
        scores.Add("Score1", 2);
        scores.Add("Score2", 6);
        scores.Add("Score3", 12);

        string path = Application.dataPath + "/Scores/";
        string fileName = "testScore.txt";
        string absolutePath = path + fileName;

        CreateCSVFile(absolutePath);
        WriteToTextFile(scores, absolutePath);
    }

    public void CreateCSVFile(string path)
    {
        var scoreReport = File.Create(path);
        scoreReport.Close();   
    }

    public void WriteToTextFile(Dictionary<string, int> scores, string fileToModify)
    {
        using (StreamWriter writer = new StreamWriter(fileToModify))
        {
            foreach (KeyValuePair<string, int> score in scores)
            {
                writer.WriteLine(score.Key + "&" + score.Value);
            }
        }
    }

    public void ClearCSVFile(string path, string fileName)
    {
        File.WriteAllText(path + fileName, string.Empty);
    }
}
