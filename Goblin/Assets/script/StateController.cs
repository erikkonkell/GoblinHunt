using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(vp_DamageHandler))]
public class StateController : MonoBehaviour {

    public EnemyStats enemyStats;
    public RangeWeaponAttack rangeAttackObject;
    public Collider AoeZone;
    public Transform AoeZoneLookAt;
    public State currentState;
    public State remainState;
    public Transform eyes;
    public Transform rangeAttackStartPos;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public float currentHealthLastFrame;
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool isRangeAttacking = false;

    [HideInInspector]public Animator animator;

    

    private bool aiActive;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        currentHealthLastFrame = GetComponent<vp_DamageHandler>().CurrentHealth;
        isRangeAttacking = false;
        isAttacking = false;
        currentState.SetupState(this);
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
        animator.SetFloat("AttackDistants", Vector3.Distance(chaseTarget.position, transform.position));

    }

    private void OnDrawGizmos()
    {
        if(currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, enemyStats.lookSphereCastRadius);
            //Gizmos.DrawWireCube(colitionTransform.position, colitionTransform.localScale);

        }
    }

    public void TransitionToState(State nextState)
    {
        if(nextState != remainState)
        {
            currentState = nextState;
            Debug.Log(currentState.name);
            OnExitState();
            currentState.SetupState(this);
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
        isAttacking = false;
        isRangeAttacking = false;
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
    public bool LostHealth()
    {
        if (currentHealthLastFrame < GetComponent<vp_DamageHandler>().CurrentHealth)
            return true;
        else
            return false;
    }

    public void DamageOnAnimation(int damage)
    {
        if(damage == 0)
        {
            chaseTarget.gameObject.GetComponentInParent<vp_DamageHandler>().Damage(enemyStats.attackDamage);
        }
        else
        {
            chaseTarget.gameObject.GetComponentInParent<vp_DamageHandler>().Damage(damage);
        }

    }
    public void CreateAoeDamageCollider(float damage)
    {
        GameObject go = Instantiate(AoeZone, rangeAttackStartPos.position, Quaternion.identity,rangeAttackStartPos).gameObject;
        if(damage == 0)
            go.GetComponent<AoeAttack>().spawnGamgeObject(enemyStats.attackDamage,AoeZoneLookAt);
        else
            go.GetComponent<AoeAttack>().spawnGamgeObject(damage, AoeZoneLookAt);
    }

}
