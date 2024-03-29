using Godot;
using System;
using System.Threading;

public partial class Npc : CharacterBody2D
{
    public override void _EnterTree()
    {
        Global.AllNpcs.Add(this);
    }
    public void OnInteractedWith(){
        GD.Print(Position.DistanceSquaredTo(Global.Player.Position));
    }
}
