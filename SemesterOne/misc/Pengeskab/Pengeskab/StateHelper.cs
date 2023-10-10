using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pengeskab
{
    public class StateHelper
    {
        
            public State Lock(State someState)
            {
                if (someState == State.Closed)
                    someState = State.Locked;
                return someState;

            }
            public State Unlock(State someState)
            {
                if (someState == State.Locked)
                    someState = State.Closed;
                return someState;
            }
            public State Open(State someState)
            {
                if (someState == State.Closed)
                    someState = State.Open;
                return someState;
            }
            public State Close(State someState)
            {
                if (someState == State.Open)
                    someState = State.Closed;
                return someState;
            }
    }
}