using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Historic {

	private List<string> data = new List<string>();

	public string generateHistoricXML()
	{
		string historicXML = "";
		for(int i =0;i < data.Count;i++)
		{
			string line = "<p>" + data[i] + "</p>";
			historicXML += line;
		}
		return historicXML;
	}

	public void recordAction(GameObject go, string direction)
	{
        string date = "<date>" +  System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "</date>";
        string entity = "<entity>" + go.name + "</entity>";
        string move = "<move>" + "MOVE " + direction + " !" + "</move>";
        string newRecord = "<action>" + date + entity + move + "</action>";
        Debug.Log (newRecord);
		data.Add (newRecord);
	}

	public string write_txtFile()
	{
		List<string> datasetToTXT = new List<string>();
        datasetToTXT.Add("<?xml version=\"1.0\"?>");
        datasetToTXT.Add("<xml>");
        datasetToTXT.Add("<feedback>");
        datasetToTXT.Add("<scene>");
        for (int i =0;i < data.Count;i++)
		{
			string line = data[i];
			datasetToTXT.Add(line);
		}
        datasetToTXT.Add("</scene>");
        datasetToTXT.Add("</feedback>");
        datasetToTXT.Add("</xml>");

        string[] readyToWrite = datasetToTXT.ToArray();

        string url = Application.dataPath + @"\" + "historic" + ".xml";


        System.IO.File.WriteAllLines(url, readyToWrite);
		Debug.Log("Writting file at " + Application.dataPath + @"\" + "historic" + ".xml");
        return url;
	}
}
