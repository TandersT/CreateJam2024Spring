using Godot;
using System;
using System.Collections.Generic;

public interface IProficienct
{
	public List<string> HasInteracted{ get; set; }
	public List<string> Greetings{ get; set; }
	public List<string> Novice { get; set; }
	public List<string> Intermediate { get; set; }
	public List<string> Expert { get; set; }
}
