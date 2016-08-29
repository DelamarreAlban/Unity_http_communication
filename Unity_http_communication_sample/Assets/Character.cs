using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {

    private Historic historic;
    private httpRequest request;
    private XmlData xml;

    public string url = "http://localhost:8888";

    


    // Use this for initialization
    void Start()
    {
        historic = new Historic();
        request = new httpRequest();
        //request = new httpRequest();
        xml = new XmlData();
    }

    public void GETxmlParameters(string xmlString)
    {
        string xmlURL = xml.saveStringToXml("parameter", xmlString);
        xml.GetObjectFromXML(xmlURL);
        setScene(xml.Objects);
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
            historic.recordAction(this.gameObject, "UP");

        if (Input.GetKey("down"))
            historic.recordAction(this.gameObject, "DOWN");

        if (Input.GetKey("right"))
            historic.recordAction(this.gameObject, "RIGHT");

        if (Input.GetKey("left"))
            historic.recordAction(this.gameObject, "LEFT");

        if (Input.GetKey("p"))
        {
            string filepath = historic.write_txtFile();
            request.POST("xml", url, filepath);
        }

        if (Input.GetKeyDown("g"))
        {
            //Coroutines
            //string receivedXML = request.GET(url + "/parameter", GETxmlParameters);
            string receivedXML = request.GET(url + "/parameter");
            GETxmlParameters(receivedXML);
        }

        if (Input.GetKeyDown("c"))
        {
            colorChanged(this.gameObject, "magenta");
        }
    }

    public void colorChanged(GameObject go, string color)
    {
        switch (color)
        {
            case "blue": go.GetComponent<Renderer>().material.color = Color.blue; break;
            case "red": go.GetComponent<Renderer>().material.color = Color.red; break;
            case "green": go.GetComponent<Renderer>().material.color = Color.green; break;
            case "magenta": go.GetComponent<Renderer>().material.color = Color.magenta; break;
            default: go.GetComponent<Renderer>().material.color = Color.black; break;
        }
    }

    public void setScene(List<Dictionary<string, string>> objects)
    {
        foreach (Dictionary<string, string> obj in objects)
        {
            Debug.Log("Object : ");
            GameObject go = this.gameObject;
            foreach (KeyValuePair<string, string> item in obj)
            {
                if(item.Key == "type"){
                    go = GameObject.Find(item.Value);
                    if(go == null)
                    {
                        switch (item.Value)
                        {
                            case "Cube":
                                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                cube.transform.position = new Vector3(-3.0F,1.0F, 3.0F);
                                go = cube;
                                break;
                            case "Cylinder":
                                GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                                cylinder.transform.position = new Vector3(3F, 1.0F, -4.0F);
                                go = cylinder;
                                break;
                            case "Capsule":
                                GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                                capsule.transform.position = new Vector3(0.0F, 1.0F, -0.5F);
                                go = capsule;
                                break;
                            case "Sphere":
                                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                                sphere.transform.position = new Vector3(2.0F, 1.0F, 2.0F);
                                go = sphere;
                                break;
                        }
                    }
                }
                else if (item.Key == "color"){
                    colorChanged(go, item.Value);
                }
                else if (item.Key == "text"){
                    Debug.Log(go.name + " say : " + item.Value);
                }
            }
        }
    }
}
