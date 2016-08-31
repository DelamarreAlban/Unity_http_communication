using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mascaret;
using System.IO;


public class Capsule_Agent_Move : BehaviorExecution  {
	
	public int state = 0;
	public GameObject target;
	public GameObject entity;
	public string entityName = "";
    NavMeshAgent agent;
	
	public Capsule_Agent_Move ()
	{
    }
	
	public override void init(Behavior specif,InstanceSpecification host,Dictionary<string,ValueSpecification> p,bool sync)
	{
		base.init(specif, host, p,sync);
		entityName = Host.name;
		Debug.Log ("Name : " + entityName);
        entity = GameObject.Find (entityName);
        agent = entity.GetComponent<NavMeshAgent>();
        target = GameObject.Find ("Target");
        UnityEngine.Vector3 position = new UnityEngine.Vector3(Random.Range(-20.0f, 20.0f), 0, Random.Range(-20.0f, 20.0f));
        //position += agent.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(position, out hit, 3.0f, 1);
        target.transform.position = hit.position;

    }
	
	override public double execute (double dt)
	{
		if (state == 0) {
			Debug.Log ("===================================MOVE==========================================       " + entityName);
			//agent.transform.position = target.transform.position;
			//agent.SetDestination (target.transform.position);
			state ++;
			return 1.0;

		} else if (state == 1) {
			//if (AtEndOfPath())
            //{
                state++;
            //}
			return 1.0;
		} else {
			return 0;
		}
	}

    public float pathEndThreshold = 0.1f;
    private bool hasPath = false;
    bool AtEndOfPath()
    {
        hasPath |= agent.hasPath;
        if (hasPath && agent.remainingDistance <= agent.stoppingDistance + pathEndThreshold )
        {
            // Arrived
            Debug.Log("Arrived !");
            hasPath = false;
            return true;
        }

        return false;
    }


}