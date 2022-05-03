using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : Object
    /*
     * Base class for Behaviour Tree nodes
     */
{
    public enum Status {READY, RUNNING, FAILED, SUCCESS} ;

    // Status of the node right after the last call to Process()
    protected Status status=Status.READY;

    public string name;

    protected List<Node> children = new List<Node>();

    // Index of child that will be executed on the next call to Process()
    protected int currentChild = 0;

    public Node()
    {
    }

    public Node(string n)
    {
        this.name = n;
    }

    public void AddChild(Node child)
    {
        this.children.Add(child);
    }

    public void Reset()
    {
        this.status = Status.READY;
        foreach(Node child in this.children)
        {
            child.Reset();
        }
        this.currentChild = 0;
    }

    public virtual Status Process()
        /*
         * Virtual method that should be overriden in derived classes
         * 
         * If a node has a status of FAILED or SUCCESS this method should
         * just return the status
         * 
         * If a node has a status of RUNNING or IDLE this method should do some
         * actual processing
         */
    {
        return Status.SUCCESS;
    }

    public string Print(int indent)
    {
        string p = new string('>', indent);
        string ret = p + this.name + "\n";
        foreach(Node child in this.children)
        {
            ret += child.Print(indent + 1);
        }
        return ret;
    }
}
