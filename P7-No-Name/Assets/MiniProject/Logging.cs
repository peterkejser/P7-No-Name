using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Logging : MonoBehaviour {

    public Transform player;
    private string headers = "Date;Time;Fruit;PosX;PosZ";
    private StreamWriter writer;
    private string directory;
    private string fileName;
    private string sep = ";";
    private string currentEntry;

    private string date;
    private string time;

    void Start()
    {
        Debug.Log(Application.persistentDataPath + "/Data/");
        directory = Application.persistentDataPath + "/Data/";

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        NewLog();
    }

    public void NewEntry(string fruitType,float PosX, float PosZ)
    {

        date = System.DateTime.Now.ToString("yyyy-MM-dd");
        time = System.DateTime.Now.ToString("HH:mm:ss:ffff");
        currentEntry = date + sep +
                       time + sep +
                       fruitType + sep +
                       PosX + sep +
                       PosZ + sep;

        using (StreamWriter writer = File.AppendText(directory + fileName))
        {
            writer.WriteLine(currentEntry);
        }
    }

    public void NewLog()
    {

        fileName = System.DateTime.Now.ToString() + ".txt";
        fileName = fileName.Replace('/', '-');
        fileName = fileName.Replace(':', '-');

        using (StreamWriter writer = File.AppendText(directory + fileName))
        {
            writer.WriteLine(headers);
        }
        StartCoroutine("logPos", player);

    }
    IEnumerator logPos(Transform player)
    {
        NewEntry("NA",player.position.x,player.position.z);
        yield return new WaitForSeconds(0.3f);
    }
}
