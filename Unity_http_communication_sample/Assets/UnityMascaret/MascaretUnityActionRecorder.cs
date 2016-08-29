using UnityEngine;
using System.Collections;
using Mascaret;
using System.Collections.Generic;

public class MascaretUnityActionRecorder {

    public struct AgentActions
    {
        Agent _agent;
        public Agent Agent
        {
            get { return _agent; }
            set { _agent = value;}
        }

        List<ActionNode> actionNodes;
        public List<ActionNode> ActionNodes
        {
            get { return actionNodes; }
            set { actionNodes = value; }
        }

        List<TimeExpression> actionTime;
        public List<TimeExpression> ActionTime
        {
            get { return actionTime; }
            set { actionTime = value; }
        }

    };

    List<AgentActions> recordedActions = new List<AgentActions>();

    List<Agent> Agents = new List<Agent>();
    public List<ProcedureExecution> recordedRunningProcedures = new List<ProcedureExecution>();

    List<ActionNode> allActions = new List<ActionNode>();

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
            recordedRunningProcedures.Add(motherProcedure);
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
                //Debug.Log("action  : " + n.name + "      kind : " + n.Action.Kind);
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
                int i = 0;
                foreach (ActionNode n in pe.getAllActions())
                {
                    if (n.Kind == "CallBehavior")
                    {
                        //Debug.Log("ALL ACTION  " + pe.procedure.name);
                        allActions.AddRange(getActionNodesFromProcedure(pe));
                    }
                    else
                    {
                        allActions.Add(n);
                        //Debug.Log("action " + (i++) + " : " + n.name + "      kind : " + n.Action.Kind);
                    }

                }
            }
        }
    }

    bool printed = false;
    public void showAllActions()
    {
        if (!printed)
        {
            Debug.Log("ALL ACTION : ");
            getAllActions();
            
            for (int i = 0; i < allActions.Count; i++)
                Debug.Log("Action " + i + " : " + allActions[i].name + "    kind : " + allActions[i].Action.Kind);

        }
        printed = true;
    }

    public void getAllActionsDone()
    {
        foreach (Agent a in Agents)
        {
            AgentActions newAgentActions = new AgentActions();
            newAgentActions.Agent = a;
            newAgentActions.ActionNodes = new List<ActionNode>();
            newAgentActions.ActionTime = new List<TimeExpression>();
            recordedActions.Add(newAgentActions);
            getAllActionsDonebyAgent(newAgentActions);
        }
    }

    
    public void getAllActionsDonebyAgent(AgentActions agentActions)
    {
        
        foreach (ProcedureExecution pe in recordedRunningProcedures)
        {
            List<ActionNode> actions = pe.getAllActionNodesDoneBy(agentActions.Agent.Aid);
            List<TimeExpression> timestep = pe.getAllActionsDoneTimestampsBy(agentActions.Agent.Aid);
            for (int i =0; i < actions.Count; i++)
            {
                agentActions.ActionNodes.Add(actions[i]);
                //Debug.Log(actions[i].name);
                //Debug.Log(actions[i].Action.Kind);
                //Debug.Log("action " + (i++) + " executed by " + agentActions.Agent.name + " at " + "TIME" + "  : " + actions[i].name + "      kind : " + actions[i].Action.Kind);
            }
        }
    }

    public void sortAgentActions()
    {
        //sorts all action by time when you'll fixed it
    }

    public string publishRecordedActions()
    {
        XmlData xmlHandler = new XmlData();
        string xmlFilePath;
        string xmlActions = "";
        xmlActions+= "<?xml version=\"1.0\"?>\n";
        xmlActions += "<xml>\n";
        xmlActions += "<feedback>\n";
        xmlActions += "<scene>\n";
        foreach (AgentActions aa in recordedActions)
        {
            string newAction = "<action>\n";
            newAction += "<entity>" + aa.Agent.name + "</entity>\n";
            foreach (ActionNode an in aa.ActionNodes)
            {
                newAction += "<"+an.Action.Kind+">" + an.name + "</"+an.Action.Kind+">\n";
            }
            newAction += "</action>\n";
            //Debug.Log(newAction);
            xmlActions += newAction;
        }
        xmlActions += "</scene>\n";
        xmlActions += "</feedback>\n";
        xmlActions += "</xml>\n";

        xmlFilePath = xmlHandler.saveStringToXml("scene1", xmlActions);
        Debug.Log(xmlFilePath);
        return xmlFilePath;
    }
}
