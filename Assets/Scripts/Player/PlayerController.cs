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
    /// ģ������
    /// </summary>
    float gravity = -9.8f;

    /// <summary>
    /// ��ǰ״̬
    /// </summary>
    PlayerState currentState;

    /// <summary>
    /// ״̬��
    /// </summary>
    StateMachine stateMachine;

    /// <summary>
    /// ����
    /// </summary>
    Animator animator;

    /// <summary>
    /// �Ƿ��ƶ�
    /// </summary>
    [HideInInspector] public Vector3 moveIncrement;

    /// <summary>
    /// ҡ��
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
    /// ���Ŷ���
    /// </summary>
    /// <param name="animation"></param>
    /// <param name="durationTransitonTimer"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void PlayAnimation(string animation, float durationTransitonTimer = .25f)
    {
        animator.CrossFadeInFixedTime(animation, durationTransitonTimer);
    }
}
