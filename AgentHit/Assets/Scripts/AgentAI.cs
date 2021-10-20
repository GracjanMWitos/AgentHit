using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAI : MonoBehaviour
{
    #region Variables
    public string agentNumber;
    public string agentName;
    public string agentSex;
    public int agentAge;
    public int healthPoints = 3;

    [SerializeField] Vector3 moveDirection;
    float timer;
    #endregion

    #region Assignment
    Rigidbody2D agentRigidbody2D;
    GameController gameController;


    void Awake()
    {
        agentRigidbody2D = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        Movement();
        SetPersonalData();
    }
    void Movement()
    {
        moveDirection = new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), 0f).normalized;
        agentRigidbody2D.AddForce(moveDirection / 60);
    }
    void SetPersonalData()
    {
        #region AgentNumber
        int number = gameController.nextAgentNumber;
        if (number < 10)
            agentNumber = "00" + number;
        else if (number > 10 && number < 100)
            agentNumber = "0" + number;
        else if (number > 100 && number < 1000)
            agentNumber = "" + number;
        gameController.nextAgentNumber++;
        #endregion

        #region AgentSex AgentName AgentAge
        agentSex = gameController.sexes[Random.Range(0, gameController.sexes.Length)];

        if (agentSex == "Male")
            agentName = gameController.maleNames[Random.Range(0, gameController.maleNames.Length)] + " " + gameController.surnames[Random.Range(0,gameController.surnames.Length)];
        if (agentSex == "Famale")
            agentName = gameController.famaleNames[Random.Range(0,gameController.famaleNames.Length)] + " " + gameController.surnames[Random.Range(0, gameController.surnames.Length)];

        agentAge = Random.Range(18, 60);
        #endregion
    }
    #endregion

    private void Update()
    {

        if (timer > 0) 
        {
            timer -= Time.deltaTime;
        }
        if (healthPoints <= 0)
        {
            gameController.currentAgentCount--;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Agent") && timer <= 0) 
        {
            Damage();
        }
    }
    void Damage()
    {
        healthPoints -= 1;
        timer = 0.01f;
    }
}
