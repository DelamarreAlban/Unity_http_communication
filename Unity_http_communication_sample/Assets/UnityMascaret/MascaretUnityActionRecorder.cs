using UnityEngine;
using System.Collections;
using Mascaret;
using System.Collections.Generic;

public class MascaretUnityActionRecorder {

    List<Agent> Agents = new List<Agent>();
    List<Procedure> procs = new List<Procedure>();
    List<ProcedureExecution> runningProcedures = new List<ProcedureExecution>();

    ProceduralBehavior pb;
    public ProcedureExecution procExe;

    bool currentProcFound = false;

    string runningProcedureName = "";



    CallProcedureBehaviorExecution motherProcedure;


    public MascaretUnityActionRecorder(AgentPlateform agentPlatform)
    {
        getAgentsToRecord(agentPlatform);
    }

    public void getAgentsToRecord(AgentPlateform agentPlatform)
    {
        foreach (KeyValuePair<string, Agent> a in agentPlatform.Agents)
        {
            Debug.Log("Key : " + a.Key + "           Value : " + a.Value);
            if (a.Value.GetType().Equals(typeof(VirtualHuman)))
            {
                Agents.Add(a.Value);
                Debug.Log("Agent to record : " + a.Key);
                /*if(a.Value.getBehaviorExecutingByName("ProceduralBehavior") != null)
                {
                    Debug.Log("ProceduralBehavior found");
                    Debug.Log(((ProceduralBehavior)a.Value.getBehaviorExecutingByName("ProceduralBehavior")).RunningProcedures.Count);
                    
                }*/
                /*foreach(Behavior b in a.Value.Behaviors)
                {
                    Debug.Log("Behavior : " + b.ToString() + " type : " + b.GetType());
                }*/
            }
        }
    }

    public void setRunningProcedureName(string name)
    {
        runningProcedureName = name;
    }

    public void getProcedureExecutions(CallProcedureBehaviorExecution cpbe)
    {
        runningProcedures = cpbe.ProceduralBehavior.RunningProcedures;

        foreach(ProcedureExecution pe in runningProcedures)
        {
            procExe = pe;
            if (procExe != null)
            {
                Debug.Log("ALL ACTION  " + procExe.procedure.name);
                int i = 0;
                foreach (ActionNode n in procExe.getAllActions())
                {
                    Debug.Log("action " + (i++) + " : " + n.name + "      kind : " + n.Action.Kind);
                }
            }
        }
        


        //
        //mais on veut (behavior fille):
        //ProceduralBehavior pb = (ProceduralBehavior)be;


        /*if (procExe == null)
        {
            //Debug.Log("get Procedure execution ::::::::::::::::::::::::::::::::::::");
            List<OrganisationalStructure> structs = VRApplication.Instance.AgentPlateform.Structures;
            foreach (OrganisationalStructure s in structs)
            {
                procs = s.Procedures;
            }

            foreach (Procedure p in procs)
            {
                //Debug.Log("Procedure to record : " + p.name);
                if (p.ProcedureExecution != null && p.name == procedureName)
                {
                    //Debug.Log("Procedure Execution found!");
                    procExe = p.ProcedureExecution;
                }
            }
        }


        if (procExe != null)
        {
            Debug.Log("ALL ACTION  " + procExe.procedure.name);
            int i = 0;
            foreach (ActionNode n in procExe.getAllActions())
            {
                Debug.Log("action " + (i++) +" : " + n.name   + "      kind : " + n.Action.Kind );
            }
        }*/
    }

    /*
    public IEnumerator GETProcedureExecutionCoroutine()
    {
        while(procExe == null)
        {
        }
        yield return 0;
        
    }
    */

    /*Debug.Log("ALL ACTION  " + pb.RunningProcedures);
    foreach(ProcedureExecution pe in pb.RunningProcedures)
    {
        if (pe != null)
        {
            Debug.Log(pe.procedure.name);
            foreach (ActionNode n in pe.getAllActions())
            {
                Debug.Log(n.name);
            }
        }
    }*/
}
