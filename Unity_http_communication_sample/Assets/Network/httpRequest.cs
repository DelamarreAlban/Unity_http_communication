using UnityEngine;
using System.Xml;
using System.Text;
using System.Net;
using System.IO;
using UnityEngine.Experimental.Networking;
using System.Collections;

public class httpRequest {

    #region COROUTINES

    /*
     * In case we want to use coroutines this code could be useful
     * As we load the scene from the xml file, we can't start the game until it is fully loaded = no need of coroutines
     * As we send the all feedback file at the end of the simulation = no need of coroutines
     * 
     * Coroutines will be useful if we want to load or uplaod stuff during the simulation
     * 
     * Future will tell...
     * 
     * Alban
     */
     /*
    public delegate void CallBack(string s);


    public string GET(string url, CallBack cb)
    {
        Debug.Log(url);
        UnityWebRequest request = UnityWebRequest.Get(url);
        Debug.Log("GET");

        StartCoroutine(GETcoroutine(request, cb));



        return request.downloadHandler.text;
    }

    public IEnumerator GETcoroutine(UnityWebRequest request, CallBack cb)
    {
        yield return request.Send();

        if (request.isError)
        {
            Debug.Log(request.error);
        }
        else
        {
            // Show results as text
            cb(request.downloadHandler.text);
            Debug.Log(request.downloadHandler.text);
        }
    }
   
*/

    #endregion
    
    public string GET(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        Debug.Log("GET");
        string responseStr = "";

        WebResponse response = null;
        response = request.GetResponse();
        //try { response = request.GetResponse(); }
        //catch { response = null; }


        if (response != null)
        {
            Stream responseStream = response.GetResponseStream();
            responseStr = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8")).ReadToEnd();
            Debug.Log("*************** GET RESPONSE *******************");
            Debug.Log(responseStr);
        }
        else
        {
            responseStr = "Cannot GET " + response.ResponseUri;
        }

        return responseStr;
    }
    
    public string POST(string fileType, string url,  string filepath)
	{
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        Debug.Log ("POST");
        string responseStr = "";
        byte[] bytes;

        if (fileType == "xml")
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(filepath);
            bytes = System.Text.Encoding.ASCII.GetBytes(xml.OuterXml);

            //Set content 
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;

            //Post
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
        }
		
		//Response
		HttpWebResponse response;
		response = (HttpWebResponse)request.GetResponse();
		if (response.StatusCode == HttpStatusCode.OK)
		{
			Stream responseStream = response.GetResponseStream();
            responseStr = new StreamReader(responseStream).ReadToEnd();
			Debug.Log ("*************** POST RESPONSE *******************");
			Debug.Log (responseStr);
        }else
        {
            responseStr = "Cannot POST " + response.ResponseUri;
        }

        return responseStr;
	}
}
