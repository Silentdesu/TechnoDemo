﻿using TechnoDemo.Input;
using TechnoDemo.Player;

namespace TechnoDemo.Interfaces
{
    public interface ISetuper<out TReturner>
    {
        TReturner Setup(in IPlayer player);
    }

    public interface IUpdateTickable
    {
        void UpdateTick(in IInput input);
    }
}