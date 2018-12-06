using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(vp_DamageHandler))]
public class StateController : MonoBehaviour {

    public State currentState;
    public EnemyStats enemyStats;
    public Transform eyes;
    public State remainState;
    public RangeWeaponAttack rangeAttackObject;
    public Transform rangeAttackStartPos;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool isRangeAttacking;

    [HideInInspector]public Animator animator;


    private bool aiActive;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void SetupAI(bool aiAcrivationFromEnemyManager, List<Transform> wayPointsFromEnemyManager)
    {
        wayPointList = wayPointsFromEnemyManager;
        aiActive = aiAcrivationFromEnemyManager;

        if (aiActive)
        {
            navMeshAgent.enabled = true;
            chaseTarget = FindObjectOfType<vp_FPController>().transform;
        }

        else
        {
            navMeshAgent.enabled = false;
        }
    }
    private void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isRangeAttacking", isRangeAttacking);
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

    }

    private void OnDrawGizmos()
    {
        if(currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, enemyStats.lookSphereCastRadius);
            
            
        }
    }

    public void TransitionToState(State nextState)
    {
        if(nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
        navMeshAgent.speed = 4;
        isAttacking = false;
    }

    public Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask = -1)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
    public void DestroyEvent()
    {
        //Destroy(gameObject);
        GetComponent<vp_DamageHandler>().Die();
    }
    public void CreateRangeProjectile()
    {
        RangeWeaponAttack go = Instantiate(rangeAttackObject, rangeAttackStartPos.position, Quaternion.identity);
        go.Instantiate(chaseTarget.position);
    }
    public void DeActivateAIEvent()
    {
        aiActive = false;
        navMeshAgent.isStopped = true;
    }

    public void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 360);
    }
}
