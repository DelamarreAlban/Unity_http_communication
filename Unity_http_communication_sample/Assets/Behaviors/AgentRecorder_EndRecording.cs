using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mascaret;
using System.IO;


public class AgentRecorder_EndRecording : BehaviorExecution
{

    public int state = 0;
    public Recorder entity;
    public string entityName = "";

    public AgentRecorder_EndRecording()
    {
    }

    public override void init(Behavior specif, InstanceSpecification host, Dictionary<string, ValueSpecification> p, bool sync)
    {
        
        base.init(specif, host, p, sync);
        entityName = Host.name;
        entity = (Recorder)GameObject.Find(entityName).GetComponent<Recorder>();
    }


    override public double execute(double dt)
    {
        if (entity.recording)
        {
            entity.recording = !entity.recording;
            return 1.0;
        }else
        {
            Debug.Log("===================================    End Recording    ===================================");
            return 0;
        }

    }

}

