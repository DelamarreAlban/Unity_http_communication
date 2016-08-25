using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mascaret;
using System.IO;


public class Capsule_Agent_Jump : BehaviorExecution
{

    public int state = 0;
    public GameObject entity;
    public string entityName = "";

    public Capsule_Agent_Jump()
    {
    }

    public override void init(Behavior specif, InstanceSpecification host, Dictionary<string, ValueSpecification> p, bool sync)
    {
        base.init(specif, host, p, sync);
        entityName = Host.name;
        entity = GameObject.Find(entityName);
    }

    override public double execute(double dt)
    {
        if (state == 0)
        {
            Debug.Log("===================================JUMP==========================================       " + entityName);
            //agent.transform.position = target.transform.position;
            entity.GetComponent<Controller>().jump();
            state++;
            return 1.0;
        }
        else if (state == 1)
        {
            //if (AtEndOfPath ())
            //return 0;
            state++;
            return 0.0;
        }
        else
        {
            return 1.0;
        }
    }

}
