using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

// This class encapsulates all of the metrics that need to be tracked in your game. These may range
// from number of deaths, number of times the player uses a particular mechanic, or the total time
// spent in a level. These are unique to your game and need to be tailored specifically to the data
// you would like to collect. The examples below are just meant to illustrate one way to interact
// with this script and save data.
public class CollectFeedback : MonoBehaviour
{
    public static CollectFeedback instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
    // You'll have more interesting metrics, and they will be better named.
    private int woodBranch =0;
    private int stone=0;
    private int mush = 0;
    private int water = 0;
    private int tree = 0;
    private int death = 0;
    private List<string> reasonOfDeath = new List<string>();
    private List<float> deathTime = new List<float>();
    private bool axeCrafted;
    private bool campFireCrafted;
    private bool cave;
    private float startTime;

    private string dataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\PollyGone_Builds\FeedBackData\";
    //private string dataFolderPath = @"C:\Users\yyycc\Desktop\PollyGone_Builds\FeedBackData\";

    // Public method to add to Metric 1.
    public void AddToWoodBranch(int value)
    {
        woodBranch +=value;
    }

    // Public method to add to Metric 2.
    public void AddToStone(int value)
    {
        stone +=value;
    }
    public void AddtoMushroom(int value)
    {
        mush += value;
    }
    public void AddtoWater(int value)
    {
        water += value;
    }

    public void AddDeath(int value)
    {
        death += value;
    }
    public void AddtoTree(int value)
    {
        tree += tree;
    }
    public void DeathReason(string reason)
    {
        if (reason != "")
        {
            reasonOfDeath.Add(reason);
        }
    }
    public void AxeCrafted()
    {
        axeCrafted = true;
    }
    public void CampfireCrafted()
    {
        campFireCrafted = true;
    }
    public void FoundCave()
    {
        cave = true;
    }

    public void startTimer()
    {
        startTime = Time.time;
    }

    public void endTimer()
    {
        deathTime.Add(Time.time - startTime);
    }

    public string GetDeathReason()
    {
        if (death == 0)
        {
            return "";
        }
        else
        {
            return reasonOfDeath[death-1];
        }
    }
    // Converts all metrics tracked in this script to their string representation
    // so they look correct when printing to a file.
    private string ConvertMetricsToStringRepresentation()
    {
        string metrics = "Here are my metrics:\n";
        metrics += "Resources:           Amount:\n";
        metrics += "Wood Branch: " + woodBranch.ToString() + "\n";
        metrics += "Stond: " + stone.ToString() + "\n";
        metrics += "Mushroom: " + mush.ToString() + "\n";
        metrics += "Water: " + water.ToString() + "\n";
        metrics += "Tree Chopped: " + tree.ToString() + "\n";
        metrics += "Item Crated:\n";
        metrics += "Axe: " + axeCrafted.ToString() + "\n";
        metrics += "Camp Fire: " + campFireCrafted.ToString() + "\n";
        metrics += "Find Cave: " + cave.ToString() + "\n";
        metrics += "Death: " + death.ToString() + "\n";
        metrics += "Reason of Death:  ";
        for (int i = 0; i < death; i++)
        {
            metrics += reasonOfDeath[i] + " for " + deathTime[i] +" seconds\n";
        }

        return metrics;
    }

    // Uses the current date/time on this computer to create a uniquely named file,
    // preventing files from colliding and overwriting data.
    private string CreateUniqueFileName()
    {
        string dateTime = System.DateTime.Now.ToString();
        dateTime = dateTime.Replace("/", "_");
        dateTime = dateTime.Replace(":", "_");
        dateTime = dateTime.Replace(" ", "___");
        return "PollyGone_metrics_" + dateTime + ".txt";
    }

    // Generate the report that will be saved out to a file.
    public void WriteMetricsToFile()
    {
        string totalReport = "Report generated on " + System.DateTime.Now + "\n\n";
        totalReport += "Total Report:\n";
        totalReport += ConvertMetricsToStringRepresentation();
        totalReport = totalReport.Replace("\n", System.Environment.NewLine);
        string reportFile = CreateUniqueFileName();
        //File.WriteAllText(reportFile, totalReport);
#if !UNITY_WEBPLAYER
        File.WriteAllText(reportFile, totalReport);
#endif
    }

    // The OnApplicationQuit function is a Unity-Specific function that gets
    // called right before your application actually exits. You can use this
    // to save information for the next time the game starts, or in our case
    // write the metrics out to a file.
    private void OnApplicationQuit()
    {
        WriteMetricsToFile();
    }
}