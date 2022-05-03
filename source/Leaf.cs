using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
    public delegate Status Tick();

    public Tick processMethod;

    public Leaf()
    {
    }

    public Leaf(string n, Tick pm) : base(n)
    {
        processMethod = pm;
    }

    public Leaf(string n, Node parent, Tick pm) : base(n)
    {
        processMethod = pm;
        parent.AddChild(this);
    }


    public override Status Process()
    {
        switch(this.status)
        {
            case Status.READY:
            case Status.RUNNING:
                return this.ProcessTick();
            case Status.SUCCESS:
            case Status.FAILED:
                return this.status;

            default:
                return this.status;
        }
    }


    private Status ProcessTick()
    {
        if (processMethod != null)
        {
            Status s = processMethod();
            this.status = s;
            if(s!=Status.RUNNING)
            {
                Debug.Log(this.name + ": " + s);
            }
            return s;
        }
        else
        {
            Debug.Log(this.name + ": Failed");
            return Status.FAILED;
        }
    }
}
