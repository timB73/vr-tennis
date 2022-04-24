using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
Based on https://youtu.be/Pi4SHO0IEQY
**/

public class DebugDisplay : MonoBehaviour
{
    Dictionary<string, string> debugLogs = new Dictionary<string, string>();

    public TextMeshProUGUI display;

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Log)
        {
            // this splits the message at colons (:) to get key value pairs
            // if logs already contain the key, the new value is set so you can keep updating it without adding new lines to the console
            string[] splitString = logString.Split(char.Parse(":"));
            string debugKey = splitString[0];
            string debugValue = splitString.Length > 1 ? splitString[1] : "";

            if (debugLogs.ContainsKey(debugKey)) // key exists, update value
            {
                debugLogs[debugKey] = debugValue;
            }
            else
            {
                // new key - add to the logs
                debugLogs.Add(debugKey, debugValue);
            }
        }

        // loop through the key value pairs to create the log text to display
        string displayText = "";
        foreach (KeyValuePair<string, string> log in debugLogs)
        {
            if (log.Value == "") // this does not contain a value so append with new line
            {
                displayText += log.Key + "\n";
            }
            else
            {
                // append key and value with new line
                displayText += log.Key + ": " + log.Value + "\n";
            }
        }
        // set the text in the display
        display.text = displayText;
    }
}
