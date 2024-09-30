using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Invoker
{
    private Command com;
    public bool disableLog = false;

    public void SetCommand(Command command)
    {
        com = command;
    }

    public void ExecuteCommmand()
    {
        if (!disableLog)
        {
            CommandLog.logs.Enqueue(com);
        }

        com.Execute();
    }
}
