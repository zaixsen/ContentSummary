using System;
using System.Collections.Generic;

/// <summary>
/// 状态机持有者
/// </summary>
public interface IStateMachineOwner { }
/// <summary>
/// 状态机
/// </summary>
public class StateMachine
{
    /// <summary>
    /// 当前状态
    /// </summary>
    public StateBase currentState;

    /// <summary>
    /// 状态机持有者
    /// </summary>
    IStateMachineOwner owner;

    /// <summary>
    /// 所有状态
    /// </summary>
    public Dictionary<Type, StateBase> Dic_states;

    /// <summary>
    /// 当前是否持有状态
    /// </summary>
    public bool HasState { get => currentState != null; }

    /// <summary>
    /// 初始化状态机
    /// </summary>
    /// <param name="stateMachineOwner"></param>
    public StateMachine(IStateMachineOwner stateMachineOwner)
    {
        owner = stateMachineOwner;
        Dic_states = new Dictionary<Type, StateBase>();
    }

    /// <summary>
    /// 进入状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void EnterState<T>() where T : StateBase, new()
    {
        Type targetState = typeof(T);
        //当前有状态 && 当前状态==目标状态 return 
        if (HasState && targetState == currentState.GetType()) return;

        //退出之前状态
        if (HasState) currentState.ExitState();

        //加载状态并执行状态
        currentState = LoadState<T>();
        currentState.EnterState();
    }

    /// 加载状态    /// <summary>

    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private StateBase LoadState<T>() where T : StateBase, new()
    {
        Type targetState = typeof(T);
        //是否加载过
        if (!Dic_states.TryGetValue(targetState, out StateBase state))
        {
            state = new T();
            state.Init(owner);
            Dic_states.Add(targetState, state);
        }
        return state;
    }
}
