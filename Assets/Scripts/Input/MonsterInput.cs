﻿using UnityEngine;

namespace CM.Input
{
    public class MonsterInput : IInput
    {
        public Vector2 MovementDirection { get; private set; }

        public void Init()
        {
            throw new System.NotImplementedException();
        }

        public void Simulate(float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public void ResetInput()
        {
            MovementDirection = Vector2.zero;
        }
    }
}