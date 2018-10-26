
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class AIMovement : MonoBehaviour {
    [SerializeField]
    NavMeshAgent agent;
    Vector3 destination;
    [SerializeField]
    GameObject disTarget;
    [SerializeField]
    float attackDistance;

    float maxSpeed;
    [SerializeField]
    float rotSpeed;

    GameObject player;

    GameObject[] Waypoint;
    enum State { Idle, Chase, Attack, Wonder}
    State aiState = State.Idle;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        // for InstantlyTurn
        agent.updateRotation = false;

        player = GameObject.FindGameObjectWithTag("Player");
        Waypoint = GameObject.FindGameObjectsWithTag("Waypoint");

        SetState();
    }

    private void SetState()
    {
        if(Vector3.Distance(player.transform.position,transform.position ) < attackDistance)
        {
            aiState = State.Attack;
            Debug.Log("State = Attack");
            GetComponent<vp_DamageHandler>().Damage(100);
        }
        else if(Vector3.Distance(player.transform.position, transform.position) < 25)
        {
            aiState = State.Chase;
            Debug.Log("State = Chase");
            agent.speed = 5;
            agent.SetDestination(player.transform.position);
        }
        else
        {
            aiState = State.Wonder;
            Debug.Log("State = Wonder");
            agent.speed = 4;
            agent.SetDestination(Waypoint[UnityEngine.Random.Range(0, Waypoint.Length)].transform.position);
        }
    }

 
    private bool ChangeState()
    {
        bool willChange = false;
        switch (aiState)
        {
            case State.Chase:
                if (Vector3.Distance(player.transform.position, transform.position) < attackDistance || Vector3.Distance(player.transform.position, transform.position) > 25)
                    willChange = true;
                break;
            case State.Attack:
                if (Vector3.Distance(player.transform.position, transform.position) > attackDistance)
                    willChange = true;
                break;
            case State.Idle:
                break;
            case State.Wonder:
                if (Vector3.Distance(player.transform.position, transform.position) < 25)
                    willChange = true;                
                break;
        }
        return willChange;
    }
    private void InstantlyTurn(Vector3 destination)
    {
        //When on target -> dont rotate!
        if ((destination - transform.position).magnitude < 0.1f) return;

        Vector3 direction = (destination - transform.position).normalized;
        Quaternion qDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * rotSpeed);

    }

    private void Update()
    {
        InstantlyTurn(transform.position+ agent.velocity);
        if (ChangeState())
        {
            SetState();
        }
        else
        {
            switch (aiState)
            {
                case State.Chase:
                    agent.SetDestination(player.transform.position);
                    break;
                case State.Attack:
                    break;
                case State.Idle:
                    break;
                case State.Wonder:
                    if (Vector3.Distance(transform.position, agent.destination) < 2f)
                        agent.SetDestination(Waypoint[Random.Range(0, Waypoint.Length)].transform.position);
                    break;

            }
        }
    }
}
