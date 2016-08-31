using UnityEngine;
using System.Collections;

public class Recorder : MonoBehaviour {

    public bool recording = true;
    public string url = "http://localhost:8888";

    public void sendTrainingLogFile(string xmlFile)
    {
        XmlData xmlHandler = new XmlData();
        httpRequest httpHandler = new httpRequest();

        string xmlFilePath = xmlHandler.saveStringToXml("training_log",xmlFile );

        httpHandler.POST("xml", url, xmlFilePath);
    }
}
