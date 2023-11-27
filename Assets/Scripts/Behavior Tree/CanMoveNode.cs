using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTArchitecture;

public class UnableToMoveNode : Node
{
    EnemyController _enemyController;
    NavMeshAgent _agent;
    public UnableToMoveNode() {

    }

    public override NodeState Evaluate() {
        if (_enemyController == null) {
            _enemyController = (EnemyController)this.GetData("controller");
        }
        if (_agent == null) {
            _agent = (NavMeshAgent)this.GetData("agent");
        }
        if (_enemyController.CanMove) {
            _agent.enabled = true;
            return NodeState.FAILURE;
        }
        _agent.enabled = false;
        return NodeState.SUCCESS;
    }



}
