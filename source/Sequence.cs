using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
    /*
     * Behaviour tree node that processes its children in order
     * If one of the children fails, this node fails
     * If all succeed this node succeeds
     */
{
    public Sequence() : base()
    {
    }

    public Sequence(string n) : base(n)
    {
    }

    public Sequence(string n, Node parent) : base(n)
    {
        parent.AddChild(this);
    }

    public override Node.Status Process()
    {
        switch(this.status)
        {
            case Status.FAILED:
            case Status.SUCCESS:
                return this.status;

            case Status.READY:
            case Status.RUNNING:
                return this.ProcessChild();

            default:
                return this.status;
        } 
    }


    private Status ProcessChild()
    {
        Node.Status nodeStatus = this.children[currentChild].Process();
        switch (nodeStatus)
        {
            case Status.FAILED:
            case Status.RUNNING:
                this.status = nodeStatus;
                return nodeStatus;

            case Status.SUCCESS:
                this.status = this.GoToNextNode();
                return this.status;

            default:
                return nodeStatus;
        }
    }


    private Status GoToNextNode()
    {
        if (currentChild+1 >= this.children.Count)
        {
            return Status.SUCCESS;
        }
        else
        {
            currentChild += 1;
            return Status.RUNNING;
        }
    }
}
