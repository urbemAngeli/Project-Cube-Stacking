﻿namespace ExtraAssets.Scripts.Infrastructure.StateMachine
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}