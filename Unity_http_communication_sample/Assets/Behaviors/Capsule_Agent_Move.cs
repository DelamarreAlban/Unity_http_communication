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
	}
	
	override public double execute (double dt)
	{
		if (state == 0) {
			Debug.Log ("===================================MOVE==========================================       " + entityName);
			//agent.transform.position = target.transform.position;
			agent.SetDestination (target.transform.position);
			state ++;
			return 1.0;

		} else if (state == 1) {
			//if (AtEndOfPath ())
				//return 0;
			state++;
			return 0.0;
		} else {
			return 1.0;
		}
	}
		
}