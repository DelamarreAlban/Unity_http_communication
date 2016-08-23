using System;
using System.Collections.Generic;
using System.Reflection;


namespace Mascaret
{

    public enum AgentBehaviorType { Perception, Decision, Action };

    public class AgentBehavior : Behavior
    {

        private AgentBehaviorType type;
        public AgentBehaviorType Type
        {
            get { return type; }
            set { type = value; }
        }

        //default parameters KBName = "default"
        public AgentBehavior(string name)
            : base(name)
        {

        }

        //default parameters sync = false
        public override BehaviorExecution createBehaviorExecution(InstanceSpecification host, Dictionary<String, ValueSpecification> p, bool sync)
        {
            /*Type [] typesCall = Assembly.GetCallingAssembly().GetTypes();
            Type type = null;
            MascaretApplication.Instance.VRComponentFactory.Log("NAME  "+ name);
            MascaretApplication.Instance.VRComponentFactory.Log("Calling Assembly...  ");
            int counter = 0;
            foreach (Type t in typesCall)
            {
                /*if (t != null)
                {
                    if(t.FullName.Contains("Mascaret"))
                        MascaretApplication.Instance.VRComponentFactory.Log("Type " + counter++ + " :  " + t.FullName);
                }
                if (t.Name == name)
                {
                    MascaretApplication.Instance.VRComponentFactory.Log("Founded !!!!!!!!!!! : " + t.Name);
                    type = t;
                }
            }
            if (type == null)
            {
                Type[] typesEnt = Assembly.GetEntryAssembly().GetTypes();
                MascaretApplication.Instance.VRComponentFactory.Log("Entry Assembly...  ");
                counter = 0;
                foreach (Type t in typesEnt)
                {
                    /*if (t != null)
                    {
                        if (t.FullName.Contains("Mascaret"))
                            MascaretApplication.Instance.VRComponentFactory.Log("Type " + counter++ + " :  " + t.FullName);
                    }
                    if (t.Name == name)
                    {
                        MascaretApplication.Instance.VRComponentFactory.Log("Founded !!!!!!!!!!! : " + t.Name);
                        type = t;
                    }
                }
            }
            if (type == null)
            {
                Type[] typesExe = Assembly.GetExecutingAssembly().GetTypes();
                MascaretApplication.Instance.VRComponentFactory.Log("Executing Assembly...  ");
                counter = 0;
                foreach (Type t in typesExe)
                {
                   /* if (t != null)
                    {
                        if (t.FullName.Contains("Mascaret"))
                            MascaretApplication.Instance.VRComponentFactory.Log("Type " + counter++ + " :  " + t.FullName);
                    }
                    if (t.Name == name)
                    {
                        MascaretApplication.Instance.VRComponentFactory.Log("Founded !!!!!!!!!!! : " + t.Name);
                        type = t;
                    }
                }
            }
            
            
            
            BehaviorExecution be = null;
            if (type != null)
            {
                be = (BehaviorExecution)(Activator.CreateInstance(type));
                be.init((Behavior)this, host, p, false);
            } else*/
            BehaviorExecution be = null;
            if(name == "SimpleCommunicationBehavior")
            {
                MascaretApplication.Instance.VRComponentFactory.Log("Simple Communication behavior created");
                be = new SimpleCommunicationBehavior();
                be.init((Behavior)this, host, p, false);
            }
            else if (name == "ProceduralBehavior")
            {
                MascaretApplication.Instance.VRComponentFactory.Log("Procedural behavior created");
                be = new ProceduralBehavior();
                be.init((Behavior)this, host, p, false);
            }
            else MascaretApplication.Instance.VRComponentFactory.Log("ERREUR : " + name + " not found");
            

            return be;
           
        }



    }
}

