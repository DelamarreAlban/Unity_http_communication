using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


public class XmlData 
{

    private List<Dictionary<string, string>> _objects = new List<Dictionary<string, string>>();
    Dictionary<string, string> _obj;

    public List<Dictionary<string, string>> Objects
    {
        get
        {
            return _objects;
        }
    }

    //returns the xml URL
    public string saveStringToXml(string name, string stringToXml)
    {
        string url = Application.dataPath + @"\" + name + ".xml";
        if (File.Exists(Application.dataPath + @"\" + name + ".xml"))
        {
            File.Delete(Application.dataPath + @"\" + name + ".xml");
        }
        System.IO.File.WriteAllText (url, stringToXml);
        Debug.Log("Writting file at " + Application.dataPath + @"\" + name + ".xml");

        

        return url;
    }

    public void replaceAllInFile(string filePath, string newText)
    {
        File.WriteAllText(filePath, "");
        System.IO.File.WriteAllText(filePath, newText);
    }

    public void GetObjectFromXML(string xmlPath)
    {
        XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
        xmlDoc.Load(xmlPath); // load the file.
        XmlNodeList objectsList = xmlDoc.GetElementsByTagName("scene");

        foreach (XmlNode objectInfo in objectsList)
        {
            XmlNodeList objectContent = objectInfo.ChildNodes;
            foreach (XmlNode objectItems in objectContent) // scenes itens nodes.
            {
                _obj = new Dictionary<string, string>(); // Create a object(Dictionary) to colect the both nodes inside the object node and then put into objects[] array.
                if (objectItems.Name == "object")
                {
                    Debug.Log("New object!");
                    _obj.Add("type", objectItems.Attributes["type"].Value);
                    _obj.Add("color", objectItems.Attributes["color"].Value);
                    _obj.Add("text", objectItems.InnerText);
                }
                _objects.Add(_obj); // add whole obj dictionary in the objects[].
            }
        }
    }
}
