using UnityEngine;
using System.Collections;
using Mascaret;
using System.Collections.Generic;

public class MascaretUnityActionRecorder {

    List<Agent> Agents = new List<Agent>();
    public List<ProcedureExecution> recordedRunningProcedures = new List<ProcedureExecution>();

    List<ActionNode> allActions = new List<ActionNode>();

    List<ActionNode> recordedActions = new List<ActionNode>();
    CallProcedureBehaviorExecution procedureBehaviorExecution;

    public ProcedureExecution motherProcedure;
    bool motherProcedureAcquired = false;

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
            }
        }
    }

    public void getProcedureExecution(CallProcedureBehaviorExecution cpbe)
    {
        procedureBehaviorExecution = cpbe;
    }

    //Record all different procedures
    public void getProceduresToRecord()
    {
        if(procedureBehaviorExecution.ProceduralBehavior.RunningProcedures[0] != null && !motherProcedureAcquired)
        {
            motherProcedureAcquired = true;
            motherProcedure = procedureBehaviorExecution.ProceduralBehavior.RunningProcedures[0];
        }
        for(int i = 1;i < procedureBehaviorExecution.ProceduralBehavior.RunningProcedures.Count;i++ )
        {
            ProcedureExecution pe = procedureBehaviorExecution.ProceduralBehavior.RunningProcedures[i];
            if (pe != null && !recordedRunningProcedures.Contains(pe))
            {
                Debug.Log("Added procedure execution : " + pe.procedure.name);
                recordedRunningProcedures.Add(pe);
            }
        }
    }

    public List<ActionNode> getActionNodesFromProcedure(ProcedureExecution procedure)
    {
        List<ActionNode> actionNodes = new List<ActionNode>();
        foreach(ActionNode n in procedure.getAllActions())
        {
            if (n.Kind == "CallBehavior")
            {
            }
            else
            {
                actionNodes.Add(n);
                Debug.Log("action  : " + n.name + "      kind : " + n.Action.Kind);
            }
        }
        return actionNodes;
    }

    public void getAllActions()
    {
        foreach (ProcedureExecution pe in recordedRunningProcedures)
        {
            if (pe != null)
            {
                Debug.Log("ALL ACTION  " + pe.procedure.name);
                allActions.AddRange(getActionNodesFromProcedure(pe));
            }
        }
    }

    public void showAllActions()
    {
        for (int i = 0; i < allActions.Count; i++)
            Debug.Log("Action " + i + " : " + allActions[i].name + "    kind : " + allActions[i].Kind);
    }

    public void getAllActionsDone()
    {
        foreach (ProcedureExecution pe in recordedRunningProcedures)
        {
            if (pe != null)
            {
                Debug.Log("ALL ACTION  " + pe.procedure.name);
                int i = 0;
                foreach (ActionNode n in pe.getAllActions())
                {
                    if(n.Kind == "CallBehavior")
                    {

                    }
                    else
                    {
                        recordedActions.Add(n);
                        Debug.Log("action " + (i++) + " : " + n.name + "      kind : " + n.Action.Kind);
                    }
                    
                }
            }
        }
    }

    public List<ActionNode> getActionFromProcedure(Procedure procedure)
    {
        List<ActionNode> actionNodes = new List<ActionNode>();


        return actionNodes;
    }
}
