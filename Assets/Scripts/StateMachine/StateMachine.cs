using System;
using System.Collections.Generic;

/// <summary>
/// ״̬��������
/// </summary>
public interface IStateMachineOwner { }
/// <summary>
/// ״̬��
/// </summary>
public class StateMachine
{
    /// <summary>
    /// ��ǰ״̬
    /// </summary>
    public StateBase currentState;

    /// <summary>
    /// ״̬��������
    /// </summary>
    IStateMachineOwner owner;

    /// <summary>
    /// ����״̬
    /// </summary>
    public Dictionary<Type, StateBase> Dic_states;

    /// <summary>
    /// ��ǰ�Ƿ����״̬
    /// </summary>
    public bool HasState { get => currentState != null; }

    /// <summary>
    /// ��ʼ��״̬��
    /// </summary>
    /// <param name="stateMachineOwner"></param>
    public StateMachine(IStateMachineOwner stateMachineOwner)
    {
        owner = stateMachineOwner;
        Dic_states = new Dictionary<Type, StateBase>();
    }

    /// <summary>
    /// ����״̬
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void EnterState<T>() where T : StateBase, new()
    {
        Type targetState = typeof(T);
        //��ǰ��״̬ && ��ǰ״̬==Ŀ��״̬ return 
        if (HasState && targetState == currentState.GetType()) return;

        //�˳�֮ǰ״̬
        if (HasState) currentState.ExitState();

        //����״̬��ִ��״̬
        currentState = LoadState<T>();
        currentState.EnterState();
    }

    /// ����״̬    /// <summary>

    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private StateBase LoadState<T>() where T : StateBase, new()
    {
        Type targetState = typeof(T);
        //�Ƿ���ع�
        if (!Dic_states.TryGetValue(targetState, out StateBase state))
        {
            state = new T();
            state.Init(owner);
            Dic_states.Add(targetState, state);
        }
        return state;
    }
}
