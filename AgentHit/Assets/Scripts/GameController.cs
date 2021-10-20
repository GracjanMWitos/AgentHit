using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Assignment
    GameObject[] spawns;
    GameControls inputActions;
    TMPro.TextMeshProUGUI textMeshProUGUI;
    [SerializeField] Rigidbody2D agentRigidbody2D;
    #endregion

    float timer;

    [SerializeField] float min_timeBetweenSpawns;
    [SerializeField] float max_timeBetweenSpawns;
    [Space]
    [SerializeField] int maxAgentNumber;

    [HideInInspector] public int currentAgentCount;

    public string agentName;
    public int nextAgentNumber;
    Vector3 mousePosition;

    public string[] maleNames = { "Liam", "Noah", "Oliver", "Elijah", "William", "James", "Benjamin", "Lucas", "Henry", "Alexander" };
    public string[] famaleNames = { "Olivia", "Emma", "Ava", "Charlotte", "Sophia", "Amelia", "Isabella", "Mia", "Evelyn", "Harper" };
    public string[] surnames = {"Smith", "Brown", "Wilson", "Thomson", "Robertson", "Cambell", "Stewart", "Anderson", "Scott",
                                "Reid", "Murray", "Taylor", "Clark", "Mitchell", "Ross", "Walker", "Peterson", "Morrison", "Fraser",
                                "White", "Jones", "Dickson", "Henderson", "Hamilton", "Johnson", "Martin", "Duncan", "Hunter"};
    public string[] sexes = { "Male", "Famale" };
    private void Awake()
    {
        inputActions = new GameControls();
        inputActions.Player.Interaction.performed += ctx => Interaction();
        textMeshProUGUI = GameObject.Find("AgentDataText").GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Respawn");
        timer = Random.Range(min_timeBetweenSpawns, max_timeBetweenSpawns);
    }
    void Update()
    {
        SpawnsAgent();
        mousePosition = inputActions.Player.Mouse.ReadValue<Vector2>();
    }
    void Interaction()
    {

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);
        AgentAI hittedObject = hit.collider.gameObject.GetComponent<AgentAI>();
        if (hittedObject.tag == "Agent")
            textMeshProUGUI.text = "Agent " + hittedObject.agentNumber + "\nName: " + hittedObject.agentName + "\nSex: " + hittedObject.agentSex
                + "\nAge: " + hittedObject.agentAge + "\nHP: " + hittedObject.healthPoints;
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
