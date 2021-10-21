using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Assignment
    GameObject[] spawns;
    GameControls inputActions;

    TMPro.TextMeshProUGUI agentDataText;
    TMPro.TextMeshProUGUI buttonClickText;

    [SerializeField] Rigidbody2D agentRigidbody2D;
    [SerializeField] AudioClip mouseClickClip;
    AudioSource audioSource;
    Transform highlightingObjectTransform;
    private void Awake()
    {
        inputActions = new GameControls();
        inputActions.Player.Interaction.performed += ctx => Interaction();

        agentDataText = GameObject.Find("AgentDataText").GetComponent<TMPro.TextMeshProUGUI>();
        buttonClickText = GameObject.Find("DisplayText").GetComponent<TMPro.TextMeshProUGUI>();

        spawns = GameObject.FindGameObjectsWithTag("Respawn");
        timer = Random.Range(min_timeBetweenSpawns, max_timeBetweenSpawns);

        highlightingObjectTransform = GameObject.Find("[Sprite] Highlighting").transform;
        audioSource = GetComponent<AudioSource>();
    }
    #endregion

    #region Variables
    [Space]
    [Header("Time Between Spawns")]
    [SerializeField] float min_timeBetweenSpawns;
    [SerializeField] float max_timeBetweenSpawns;
    [Space]
    [Header("Agent's Counts")]
    [SerializeField] int maxAgentNumber;
    [HideInInspector] public int currentAgentCount;
    [HideInInspector] public int nextAgentNumber;
    [HideInInspector] public string highlightedAgentNumber = "start";
    public string aname;

    string displayText = "";
    [HideInInspector] public string[] maleNames = { "Liam", "Noah", "Oliver", "Elijah", "William", "James", "Benjamin", "Lucas", "Henry", "Alexander" };
    [HideInInspector] public string[] famaleNames = { "Olivia", "Emma", "Ava", "Charlotte", "Sophia", "Amelia", "Isabella", "Mia", "Evelyn", "Harper" };
    [HideInInspector] public string[] surnames = {"Smith", "Brown", "Wilson", "Thomson", "Robertson", "Cambell", "Stewart", "Anderson", "Scott",
                                                "Reid", "Murray", "Taylor", "Clark", "Mitchell", "Ross", "Walker", "Peterson", "Morrison", "Fraser",
                                                "White", "Jones", "Dickson", "Henderson", "Hamilton", "Johnson", "Martin", "Duncan", "Hunter"};
    [HideInInspector] public string[] sexes = { "Male", "Famale" };

    Vector3 mousePosition;

    float timer;
    #endregion

    #region AgentSpawning Interactions
    void Update()
    {
        SpawnsAgent();

        mousePosition = inputActions.Player.Mouse.ReadValue<Vector2>();
    }
    void Interaction()
    {
        audioSource.PlayOneShot(mouseClickClip);
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);

        if (hit.collider.gameObject.tag == "Agent")
        {
            AgentAI hittedObject = hit.collider.gameObject.GetComponent<AgentAI>();
            if (aname != hittedObject.agentName)
            {
                agentDataText.text = "Agent " + hittedObject.agentNumber +
                "\nName: " + hittedObject.agentName +
                "\nSex: " + hittedObject.agentSex +
                "\nAge: " + hittedObject.agentAge +
                "\nHP: " + hittedObject.healthPoints;
                aname = hittedObject.agentName;
            }
            else if (name == hittedObject.agentName)
            {
                agentDataText.text = "Agent " +
                "\nName: " +
                "\nSex: " +
                "\nAge: " +
                "\nHP: ";
                name = " ";
                highlightingObjectTransform.position = new Vector3(40f, 40f, 0f);
            }
        }
        else return;
    }
    void SpawnsAgent()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0 && currentAgentCount < maxAgentNumber)
        {
            float timeBetweenSpawns = Random.Range(min_timeBetweenSpawns, max_timeBetweenSpawns);
            int randomNumber = Random.Range(0, spawns.Length);

            Rigidbody2D agentClone = Instantiate(agentRigidbody2D, spawns[randomNumber].transform.position, spawns[randomNumber].transform.rotation);

            timer = timeBetweenSpawns;
            currentAgentCount++;
        }
    }
    public void DisplayText()
    {
        displayText = "";
        for (int i=1; i<101; i++)
        {
            if (i % 3 == 0 && i % 5 != 0)
                displayText = displayText.ToString() + i.ToString() + " Marko, ";

            if (i % 5 == 0 && i % 3 != 0)
                displayText = displayText.ToString() + i.ToString() + " Polo, \n";

            if (i % 3 == 0 && i % 5 == 0)
                displayText = displayText.ToString() + i.ToString() + "MarkoPolo, \n";

            if (i % 3 != 0 && i % 5 != 0) 
                displayText = displayText.ToString() + i.ToString() + ", ";

        }
        buttonClickText.text = displayText;
    }
    #endregion

    #region OnEnable OnDisable
    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }
    #endregion
}
