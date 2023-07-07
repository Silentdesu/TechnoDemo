﻿using System;
using MessagePipe;
using TechnoDemo.Input;
using TechnoDemo.Interfaces;
using TechnoDemo.Player;
using UnityEngine;

namespace TechnoDemo.Actions
{
    public readonly struct GroundMessage : IEquatable<GroundMessage>
    {
        public readonly bool Grounded;

        public GroundMessage(bool grounded)
        {
            Grounded = grounded;
        }

        public bool Equals(GroundMessage other)
        {
            return Grounded == other.Grounded;
        }

        public override bool Equals(object obj)
        {
            return obj is GroundMessage other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Grounded.GetHashCode();
        }
    }
    
    public sealed class GroundAction : BaseAction, ISetuper<GroundAction>, IUpdateTickable
    {
        private Transform m_transform;
        private Animator m_animator;
        private PlayerSettingsSO m_playerSettings;

        private IPublisher<GroundMessage> m_publisher;

        public GroundAction(IActionHandler handler, IPublisher<GroundMessage> publisher) : base(handler)
        {
            m_publisher = publisher;
        }
        
        public GroundAction Setup(in IPlayer player)
        {
            m_transform = player.Transform;
            m_animator = player.Animator;
            m_playerSettings = player.Settings;
            
            return this;
        }
        
        public void UpdateTick(in IInput input)
        {
            if (!IsRunning()) return;
            GroundCheck();
        }
        
        private void GroundCheck()
        {
            var origin = m_transform.position - Vector3.up * m_playerSettings.GroundOffset;
            var grounded = Physics.CheckSphere(origin, m_playerSettings.GroundSphereCastRadius,
                m_playerSettings.GroundLayer);

            if (m_animator)
            {
                m_animator.SetInteger(AnimatorParameters.ActionID, Data.Id);
                m_animator.SetBool(AnimatorParameters.Grounded, grounded);
            }
            
            m_publisher?.Publish(new GroundMessage(grounded));
        }

    }
}