using System;
using UnityEngine;

public enum PlayerState
{
    Idle, Walk
}

public class PlayerController : MonoBehaviour, IStateMachineOwner
{

    [HideInInspector] public CharacterController characterController;
    /// <summary>
    /// 模拟重力
    /// </summary>
    float gravity = -9.8f;

    /// <summary>
    /// 当前状态
    /// </summary>
    PlayerState currentState;

    /// <summary>
    /// 状态机
    /// </summary>
    StateMachine stateMachine;

    /// <summary>
    /// 动画
    /// </summary>
    Animator animator;

    /// <summary>
    /// 是否移动
    /// </summary>
    [HideInInspector] public Vector3 moveIncrement;

    /// <summary>
    /// 摇杆
    /// </summary>
    Rocker rocker;

    private void Awake()
    {
        stateMachine = new StateMachine(this);
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rocker = FindObjectOfType<Rocker>();
        SwitchState(PlayerState.Idle);
    }

    public void SwitchState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Idle:
                stateMachine.EnterState<PlayerIdleState>();
                break;
            case PlayerState.Walk:
                stateMachine.EnterState<PlayerWalkState>();
                break;
        }
        currentState = state;
    }

    private void Update()
    {
        characterController.Move(new Vector3(0, gravity * Time.deltaTime, 0));

        float x = rocker.GetAxis("H");
        float z = rocker.GetAxis("V");

        moveIncrement = new Vector3(x, 0, z);

        stateMachine.currentState.Update();
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animation"></param>
    /// <param name="durationTransitonTimer"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void PlayAnimation(string animation, float durationTransitonTimer = .25f)
    {
        animator.CrossFadeInFixedTime(animation, durationTransitonTimer);
    }
}
