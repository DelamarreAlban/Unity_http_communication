using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mascaret;
using System.IO;


public class Capsule_Agent_ChangeColor : BehaviorExecution
{

    public int state = 0;
    public GameObject entity;
    public string entityName = "";
    string[] colors = new string[] { "blue", "red", "green", "magenta" };

    public Capsule_Agent_ChangeColor()
    {
    }

    public override void init(Behavior specif, InstanceSpecification host, Dictionary<string, ValueSpecification> p, bool sync)
    {
        base.init(specif, host, p, sync);
        entityName = Host.name;
        entity = GameObject.Find(entityName);
    }

    public void colorChanged(GameObject go, string color)
    {
        switch (color)
        {
            case "blue": go.GetComponent<Renderer>().material.color = UnityEngine.Color.blue; break;
            case "red": go.GetComponent<Renderer>().material.color = UnityEngine.Color.red; break;
            case "green": go.GetComponent<Renderer>().material.color = UnityEngine.Color.green; break;
            case "magenta": go.GetComponent<Renderer>().material.color = UnityEngine.Color.magenta; break;
            default: go.GetComponent<Renderer>().material.color = UnityEngine.Color.black; break;
        }
    }

    override public double execute(double dt)
    {
        if (state == 0)
        {
            Debug.Log("===================================Change Color==========================================       " + entityName);
            //agent.transform.position = target.transform.position;
            int c = Random.Range(0, 3);
            colorChanged(entity, colors[c]);
            state++;
            return 1.0;
        }
        else
        {
            return 0;
        }
    }

}

