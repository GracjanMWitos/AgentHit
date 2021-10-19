using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    GameObject[] spawns;
    float timer;
    [SerializeField] float min_timeBetweenSpawns;
    [SerializeField] float max_timeBetweenSpawns;
    [Space]
    [SerializeField] int maxAgentNumber;
    [HideInInspector] public int currentAgentNumber;
    [SerializeField] Rigidbody2D agentRigidbody2D;

    private void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Respawn");

    }
    void Update()
    {
        SpawnsAgent();
    }
    void SpawnsAgent()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0 && currentAgentNumber < maxAgentNumber)
        {
            float timeBetweenSpawns = Random.Range(min_timeBetweenSpawns, max_timeBetweenSpawns);
            int randomNumber = Random.Range(0, spawns.Length);

            Rigidbody2D agentClone = Instantiate(agentRigidbody2D, spawns[randomNumber].transform.position, spawns[randomNumber].transform.rotation);

            timer = timeBetweenSpawns;
            currentAgentNumber++;
        }
    }
}
