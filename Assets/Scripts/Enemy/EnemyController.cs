using UnityEngine;

public enum EnemyState
{
    Idle, Walk, Fllow, Attack
}

public class EnemyController : MonoBehaviour, IStateMachineOwner
{
    [HideInInspector] public CharacterController characterController;

    float gravity = -9.8f;

    public float fllowDistance = 3f;

    public float attackDistance = 1.5f;

    [HideInInspector] public float playerDistance;

    [HideInInspector] public Transform player;

    public Transform pathParent;

    StateMachine stateMachine;

    [HideInInspector] public Animator animator;

    EnemyState enemyState;

    [HideInInspector] public AnimatorStateInfo animInfo;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        stateMachine = new StateMachine(this);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SwitchState(EnemyState.Idle);
    }

    public void SwitchState(EnemyState state)
    {
        switch (state)
        {
            default:
            case EnemyState.Idle:
                stateMachine.EnterState<EnemyIdleState>();
                break;
            case EnemyState.Walk:
                stateMachine.EnterState<EnemyWalkState>();
                break;
            case EnemyState.Fllow:
                break;
            case EnemyState.Attack:
                stateMachine.EnterState<EnemyAttackState>();
                break;
        }
        enemyState = state;
    }

    public void PlayAnimation(string animationName, float animationTransitionDuration = .25f)
    {
        animator.CrossFadeInFixedTime(animationName, animationTransitionDuration);
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        characterController.Move(new Vector3(0, gravity * Time.deltaTime, 0));
        stateMachine.currentState.Update();

        animInfo = animator.GetCurrentAnimatorStateInfo(0);
    }

}
