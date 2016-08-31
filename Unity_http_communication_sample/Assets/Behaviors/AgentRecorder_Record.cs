using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mascaret;
using System.IO;


public class AgentRecorder_Record : BehaviorExecution {

    public int state = 0;
    public Recorder entity;
    public string entityName = "";
    Agent recorder;

    List<ACLMessage> messages;

    public AgentRecorder_Record()
    {
    }

    public override void init(Behavior specif, InstanceSpecification host, Dictionary<string, ValueSpecification> p, bool sync)
    {
        Debug.Log("===================================    Start Recording    ===================================");
        base.init(specif, host, p, sync);
        recorder = (Agent)Host;
        entityName = Host.name;
        entity = (Recorder)GameObject.Find(entityName).GetComponent<Recorder>();
        messages = new List<ACLMessage>();
    }


    override public double execute(double dt)
    {
        if(state == 0)
        {
            Debug.Log("===================================    Record    ===================================");
            foreach (ACLMessage m in recorder.Mailbox.MessagesChecked)
            {
                if (m != null && !messages.Contains(m))
                {
                    Debug.Log("Message : " + m.Content);
                    messages.Add(m);
                }
            }
            if (!entity.recording)
                state++;
            return 1.0;
        }
        else
        {
            entity.sendTrainingLogFile(generateXMLTrainingLog());
            return 0;
        }
    }

    private string generateXMLTrainingLog()
    {
        string xmlfile = "";
        xmlfile += "<?xml version=\"1.0\"?>\n";
        xmlfile += "<xml>";
        xmlfile += "<messages>";
        for (int i=0;i < messages.Count;i++)
        {
            xmlfile += messages[i].toXML();
        }
        xmlfile += "</messages>";
        xmlfile += "</xml>";
        return xmlfile;
    }
}


