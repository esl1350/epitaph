using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTArchitecture;
using UnityEngine.AI;

public class FleeFromPlayerNode :  Node
{
    // flees from player if distance is too low
    private EnemyController _enemyController;
    private float _fleeDist;
    private GameObject _target;
    private NavMeshAgent _agent;
    private float _movespeed;

    public FleeFromPlayerNode(GameObject target) {
        _target = target;
    }

    public override NodeState Evaluate() {
        if (_enemyController == null) {
            _enemyController = (EnemyController)this.GetData("controller");
            _fleeDist = _enemyController.FleeDist;
        }
        if (_agent == null) {
            _agent = (NavMeshAgent)this.GetData("agent");
        }
        float dist = Vector3.Distance(_enemyController.transform.position, _target.transform.position);
        if (dist < _fleeDist) {
            _movespeed = ((Entity)this.GetData("entity")).EntityStats.GetStatValue(StatEnum.WALKSPEED);
            _agent.enabled = true;
            _agent.isStopped = false;
            _agent.speed = _movespeed;
            Vector3 dir = _enemyController.transform.position - _target.transform.position;
            Vector3.Normalize(dir);
            _agent.SetDestination(_enemyController.transform.position + dir);
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;        
    }
}
