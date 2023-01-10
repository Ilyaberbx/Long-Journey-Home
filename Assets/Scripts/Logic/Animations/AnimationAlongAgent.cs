using ProjectSolitude.Logic.Animations;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BearAnimator))]
public class AnimationAlongAgent : MonoBehaviour
{
    private const float MinimalSpeedVelocity = 0.1f;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private BearAnimator _animator;

    private void Update()
    {

        if (ShouldMove())
            _animator.Move(_agent.velocity.magnitude / _agent.speed);
        else
            _animator.StopMoving();
    }

    private bool ShouldMove()
        => _agent.velocity.magnitude > MinimalSpeedVelocity && _agent.remainingDistance > _agent.radius;
}