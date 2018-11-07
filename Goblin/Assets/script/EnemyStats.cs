using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/EnemyStats")]
public class EnemyStats : ScriptableObject {

    public float moveSpeed = 1;
    public float lookRange = 40;
    public float lookSphereCastRadius = 1;

    public float attackRange = 1;
    public float attackRate = 1;
    public float attackDamage = 50;

    public float searchDuration = 4f;
    public float searchTurnSpeed = 120;

    //Range is in unity units
    public float wonderRange = 20;
}
