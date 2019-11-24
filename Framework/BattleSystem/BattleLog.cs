﻿using System;
using System.Collections.Generic;

namespace Framework.BattleSystem
{
    public class BattleLog
    {
        // Properties
        public List<string> Messages { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public BattleLog()
        {
            Messages = new List<string>();
        }

        /// <summary>
        /// Adds a message to the log
        /// </summary>
        public void AddMessage(string message)
        {
            if (Messages.Count == 10)
                Messages.RemoveAt(0);

            Messages.Add(message);
            Console.WriteLine(message);
        }
    }
}
