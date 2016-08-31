using UnityEngine;
using System.Collections;
using Mascaret;
using System.Collections.Generic;

public class MascaretUnityActionRecorder {

    private List<Agent> Agents = new List<Agent>();

    private CallProcedureBehaviorExecution callProcedureBehaviorExecution;
    public CallProcedureBehaviorExecution CallProcedureBehaviorExecution
    {
        get{return callProcedureBehaviorExecution;}
        set {callProcedureBehaviorExecution = value;}
    }
    public bool acquiredCPBE
    {
        get {if (callProcedureBehaviorExecution == null)
                return false;
            else
                return true;}
    }

    private ProcedureExecution procedure;
    public ProcedureExecution Procedure
    {
        get
        {
            return procedure;
        }

        set
        {
            procedure = value;
        }
    }

    

   

    public MascaretUnityActionRecorder(AgentPlateform agentPlatform)
    {
        foreach (KeyValuePair<string, Agent> a in agentPlatform.Agents)
        {
            Debug.Log("Key : " + a.Key + "           Value : " + a.Value);
            if (a.Value.GetType().Equals(typeof(VirtualHuman)))
            {
                Agents.Add(a.Value);
                Debug.Log("Agent to record : " + a.Key);
            }
        }
    }



    
}
