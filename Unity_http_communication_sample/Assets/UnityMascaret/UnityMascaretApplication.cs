using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mascaret;
using System.IO;

public class UnityMascaretApplication : MonoBehaviour
{
	//public Dictionary<string, TextAsset> XMLFiles = new Dictionary<string, TextAsset> ();

	public TextAsset modFile;
	public TextAsset masFile;
	public TextAsset envFile;

    httpRequest httpHandler;
    XmlData xmlHandler;
    public string url = "http://localhost:8888";

    #region Useless
    [HideInInspector]
    public Vector2 scrollPosition;
    [HideInInspector]
    public bool showmenu;
    public string procedure;
    #endregion

    #region Simulation
    [HideInInspector]
    public UnityEngine.Camera camera_principale;
    [HideInInspector]
    public GameObject tech;
    #endregion

	public bool loadAll = true;

    private VRApplication m_Mascaret;
    public VRApplication Mascaret
    {
        get { return this.m_Mascaret; }
    }
    public string m_BaseDir;
    public string m_ApplicationFile;

	public bool m_DebugMode = true;

	private List<string> m_KeyPressed = new List<string>();

	void Awake()
	{
        httpHandler = new httpRequest();
        xmlHandler = new XmlData();

        Debug.Log("Loading ....");
        string xmlFile = httpHandler.GET(url + "/ignition");
        //Wait that the file is received
        


        xmlHandler.saveStringToXml(@"Resources\MOD_http_com", xmlFile);
        StreamReader sr = new StreamReader(Application.dataPath + @"\Resources\" + "MOD_http_com" + ".xml");

        Debug.Log(sr.ReadToEnd());
        
        m_Mascaret = VRApplication.Instance;
		m_Mascaret.window = new UnityWindow3D();
		m_Mascaret.VRComponentFactory = new UnityVirtualRealityComponentFactory();
		m_Mascaret.window.addPeripheric(new UnityKeyboard());
		m_Mascaret.window.addPeripheric(new UnityMouse());

        //ScriptableObject communicationManager = ScriptableObject.CreateInstance("HttpCommunicationManager");
        //communicationManager.instantiate(portNumber, ressourceDir, noServer);
        //m_Mascaret.Log += new LogHandler((message) => { Debug.LogWarning("Mascaret Message :" + message); });
        //m_Mascaret.parse(m_ApplicationFile, Application.dataPath + "/StreamingAssets/" + m_BaseDir + "/", loadAll);

        m_Mascaret.parse(m_ApplicationFile, "", loadAll);

        //m_Mascaret.parse(m_ApplicationFile, Application.streamingAssetsPath + m_BaseDir + "/", loadAll);

    }

	void Start()
	{
        

        tech = GameObject.Find("char_avatar_h_parent_MESH");
        if (showmenu)
        {
        }

		//camera_principale = GameObject.Find ("Camera_principale").GetComponent<UnityEngine.Camera> ();
		//camera_principale = GameObject.Find ("Main Camera").GetComponent<UnityEngine.Camera> ();

		if (procedure != "") 
		{
            startProcedure(procedure);
        }

        
    }
	
	void Update()
	{
		m_Mascaret.step();

        if (Input.GetKeyDown("m"))
        {
            launchProcedure("moveProcedure");
        }
        if (Input.GetKeyDown("r"))
        {
            launchProcedure("action4Ralph");
        }
        if (Input.GetKeyDown("a"))
        {
            launchProcedure("action4Jack");
        }

        if (Input.GetKeyDown("j"))
        {
            launchProcedure("jumpProcedure");
        }

        if (Input.GetKeyDown("p"))
        {
            launchProcedure("recording");
        }

        if (Input.GetKeyDown("b"))
        {
            launchProcedure("MoveJump");
        }

	}

	public void OnGUI()
	{
		if (showmenu) 
		{
			//ShowMenuFunction();
		}

		Event current = Event.current;
		if (current.isMouse)
		{
			int buttonNumber = current.button +1;
			string buttonName = "button" + buttonNumber;
			Button b = this.m_Mascaret.window.getPeripheric("mouse").getButton(buttonName);
			if (Input.GetMouseButtonDown(current.button))
				b.setPressed(true);
			else
				b.setPressed (false);
		}
		else if (current.isKey)// && camera_principale.enabled)
		{
			if (current.keyCode.ToString() != "None")
			{
				Button b = this.m_Mascaret.window.getPeripheric("keyboard").getButton(current.keyCode.ToString());
				if(b != null) 
				{
					if (current.type == EventType.keyDown) 
					{
						if (!m_KeyPressed.Contains(current.keyCode.ToString()))
						{
                            if (this.m_DebugMode)
							    Debug.Log(current.keyCode.ToString() + " / " + current.type + " : " + current.clickCount);
							b.setPressed(true);
							m_KeyPressed.Add(current.keyCode.ToString());
						}
					}
					else if (current.type == EventType.keyUp) 
					{
						b.setPressed(false);
						m_KeyPressed.Remove(current.keyCode.ToString());
					}
				}
			}
		}
	}

    private void startProcedure(string procedure)
    {
        string orgEntity = null;

        List<OrganisationalStructure> structs = VRApplication.Instance.AgentPlateform.Structures;
        foreach (OrganisationalStructure s in structs)
        {
            List<Procedure> procs = s.Procedures;
            foreach (Procedure p in procs)
            {
                if (p.name == procedure)
                {
                    orgEntity = s.Entities[0].name;
                }
            }
        }
        if (this.m_DebugMode)
            Debug.Log("RUNNING : " + procedure + " / " + orgEntity);

        List<Entity> entities = MascaretApplication.Instance.getEnvironment().getEntities();
        Entity entity = entities[0];
        Action action2 = null;
        action2 = new CallProcedureAction();
        ((CallProcedureAction)(action2)).Procedure = procedure;
        ((CallProcedureAction)(action2)).OrganisationalEntity = orgEntity;
        BehaviorScheduler.Instance.executeBehavior(action2, entity, new Dictionary<string, ValueSpecification>(), false);

        Debug.Log("startProcedure #########################################################################");
    }

    private void launchProcedure(string procedure)
    {
        string orgEntity = null;

        List<OrganisationalStructure> structs = VRApplication.Instance.AgentPlateform.Structures;
        foreach (OrganisationalStructure s in structs)
        {
            List<Procedure> procs = s.Procedures;
            foreach (Procedure p in procs)
            {
                if (p.name == procedure)
                {
                    orgEntity = s.Entities[0].name;

                }
            }
        }
        Debug.Log("RUNNING : " + procedure + " / " + orgEntity);
        List<Entity> entities = MascaretApplication.Instance.getEnvironment().getEntities();
        Entity entity = entities[0];
        Action action2 = null;
        action2 = new CallProcedureAction();
        ((CallProcedureAction)(action2)).Procedure = procedure;
        ((CallProcedureAction)(action2)).OrganisationalEntity = orgEntity;
        BehaviorScheduler.Instance.executeBehavior(action2, entity, new Dictionary<string, ValueSpecification>(), false);

        Debug.Log("startProcedure #########################################################################");
    }


    #region UselessMethods
    public void ShowMenuFunction()
    {
        int posX = 300;
        int posY = 150;
        int heigth = 30;
        int width = 300;
        List<Procedure> allProcs = new List<Procedure>();
        List<OrganisationalStructure> structures = VRApplication.Instance.AgentPlateform.Structures;
        foreach (OrganisationalStructure struc in structures)
        {
            List<Procedure> procs = struc.Procedures;
            foreach (Procedure proc in procs)
            {
                allProcs.Add(proc);
            }
        }
        int nbProc = 0;
        GUI.Box(new Rect(posX - 5, posY - 25, width + 10, (heigth + 5) * allProcs.Count + 35), "Procedures");
        foreach (Procedure proc in allProcs)
        {
            if (GUI.Button(new Rect(posX, posY + ((heigth + 5) * nbProc), width, heigth), proc.name))
            {
                Application.LoadLevel(proc.name);
            }
            nbProc++;
        }

    }
    #endregion
	
	
}

