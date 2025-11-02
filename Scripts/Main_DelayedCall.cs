using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node
{
    public class DelayedCallInfo
    {
        public Action Action;
        public float TimeRemaining;
        public string[] CallStack;

        public DelayedCallInfo(Action action, float delay)
        {
            Action = action;
            TimeRemaining = delay;
            CallStack = System.Environment.StackTrace.Split('\n');
        }
    }

    private List<DelayedCallInfo> _delayedCalls = new List<DelayedCallInfo>();

    public static void DelayedCall(Action action, float delay)
    {
        x._delayedCalls.Add(new DelayedCallInfo(action, delay));
    }

    public void ProcessDelayedCalls(double delta)
    {
        for (int i = _delayedCalls.Count - 1; i >= 0; i--)
        {
            _delayedCalls[i].TimeRemaining -= (float)delta;
            if (_delayedCalls[i].TimeRemaining <= 0)
            {
                DelayedCallInfo call = _delayedCalls[i];
                // ---
                _delayedCalls.RemoveAt(i); // fist remove it - so that you can add others in the Action call
                // ---
                call.Action();
            }
        }
    }
}
