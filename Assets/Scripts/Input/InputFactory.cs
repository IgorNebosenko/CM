﻿using System;

namespace CM.Input
{
    public class InputFactory
    {
        public InputFactory( )
        {
            
        }
        
        public IInput CreatePlayerInput()
        {
            throw new NotImplementedException();
        }

        public IInput CreateMonsterInput(IHavePosition playerPosition)
        {
            throw new NotImplementedException();
        }
    }
}