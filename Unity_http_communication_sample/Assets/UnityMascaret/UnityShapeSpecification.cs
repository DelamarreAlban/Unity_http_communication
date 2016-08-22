using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mascaret;
//using TextToSpeech;

//----------------------------------------------------------------

//----------------------------------------------------------------

public class UnityShapeSpecification : ShapeSpecification {
    //----------------------------------------------------------------

    public Entity entity;

	int stateforAnimation =0;
	//protected TTSChannel tc;
	//SpeechData currentSD = null;

    public GameObject entityGO;
    public GameObject EntityGO
    {
        get{return entityGO;}
        set{entityGO = value;}
    }

	public UnityShapeSpecification(string name) : base(name)
	{
	}

    //Default parameters movable=true, recursive =false, shader=""
    public UnityShapeSpecification(string name, List<double> distances,List<string> urls, bool movable,bool recursive, string shader) : base(name, distances, urls, movable, recursive, shader)
        
    {
        
    }

	public UnityShapeSpecification(string name, string url, bool movable,bool recursive, string shader) : base(name, url, movable, recursive, shader)
		
	{
//        Debug.Log(" Looking for : " + url);

		entityGO = GameObject.Find(url);

        if (entityGO != null)
        {
            EntityGo ego = (EntityGo)entityGO.GetComponent("EntityGo");
            if (ego == null) 
            {
                entityGO.AddComponent<EntityGo>();
                ego = (EntityGo)entityGO.GetComponent("EntityGo");
            }
            else Debug.Log(" Already has EntityGO Component");

            Dictionary<string, Agent> agents = VRApplication.Instance.AgentPlateform.Agents;
            string agentName = url + "@localhost:8080";

            if (agents.ContainsKey(agentName))
            {
                Mascaret.Agent agt = agents [agentName];
            }
        }
        else Debug.Log("Game object : " + url + " non trouvé");

		GameObject go = GameObject.Find("MascaretApplication");
		UnityMascaretApplication uma = go.GetComponent<UnityMascaretApplication>();

    }
   
	public override void setVisibility(bool v){}
	public override bool getVisibility(){ return true;}
	
	public override void setScale(Mascaret.Vector3 scale){}
	public override Mascaret.Vector3 getScale(){return null;}
	
	public override double playAnimation(string animationName)
	{
		if (stateforAnimation == 0) {
			Debug.Log ("unity playanimation **************    " + animationName);
			double animationDuration = 1;
			Debug.Log (entityGO.name);

			//FOR ANIMATOR
			Animator animator = entityGO.GetComponent<Animator> ();
			if (animator != null) {
				animator.Play (animationName);
				animationDuration += animator.GetCurrentAnimatorStateInfo (0).length;
			}

			//FOR ANIMATION
			Animation anim = entityGO.GetComponent<Animation> ();
			AnimationClip clip = null;

			if (anim != null) {
				clip = anim.GetClip (animationName);
			}

			else if (anim == null || clip == null) {
				/*
				GlobalAnimManager manager = (GlobalAnimManager)GameObject.Find ("AnimationManager").GetComponent ("GlobalAnimManager");

				GlobalAnimManager.Action[] actions = manager.actions;

				foreach (GlobalAnimManager.Action ac in actions) 
				{
					if (ac.Name == animationName) 
					{
						ac.DoIt = true;
					}
				}*/
			} else {
				Debug.Log ("unity playanimation Queued **************" + animationName);
				anim.PlayQueued (animationName);
				
			}
			stateforAnimation++;
			return animationDuration;
		} else {
			stateforAnimation = 0;
			return stateforAnimation;
		}
	}
		public override void setEntity(Entity entity)
	{
        EntityGo ego = null;
        if (entityGO != null)
            ego = (EntityGo)entityGO.GetComponent("EntityGo");
        if (ego != null) ego.entity = entity;
	}

	public override Entity getEntity()
	{
        EntityGo ego = (EntityGo)entityGO.GetComponent("EntityGo");
        return ego.entity;
	}

    public override double prepareSpeak (string text)
    {
		return 4;
    }

    public override bool speak (string text)
    {

		
		return true;
    }
	
	// --- Shape Modifier Operations ---
	public override Mascaret.Color getColor(){return null;}
	public override void setColor(Mascaret.Color color){}
	public override void setTransparent(Mascaret.Color color){}
	public override void growUp(float percent){}
	public override bool restaureColor(Mascaret.Color color){return false;}
	public override bool restaureShape(){return false;}
}
