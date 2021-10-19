using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAI : MonoBehaviour
{
    #region Assignment
    Rigidbody2D agentRigidbody2D;
    GameController gameController;
    #endregion

    #region Variables
    Vector3 moveDirection;
    public int hp = 3;
    float timer;

    #endregion

    void Awake()
    {
        agentRigidbody2D = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        moveDirection = new Vector3(x, y, 0f).normalized;
        print(moveDirection);
        agentRigidbody2D.AddForce(moveDirection/100);

    }

    private void Update()
    {
        if (timer > 0) 
        {
            timer -= Time.deltaTime;
        }
        if (hp <= 0)
        {
            gameController.currentAgentNumber--;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Agent") && timer <= 0) 
        {
            hp -= 1;
            timer = 0.01f;
        }
    }
}
