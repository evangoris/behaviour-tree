using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : Node
    /*
     * Root node of a Behaviour Tree
     * 
     * Should contain just one Behaviour node as child
     */
{
    public BehaviourTree()
    {
        this.name = "Tree";
    }


    public override Status Process()
    {
        switch (this.status)
        {
            case Status.SUCCESS:
            case Status.FAILED:
                return this.status;
            default:
                this.status = children[currentChild].Process();
                return this.status;
        }
    }


    public void Print()
    {
        Debug.Log(this.Print(0));
    }
}
