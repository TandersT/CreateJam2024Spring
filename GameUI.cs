using Godot;
using System;

public partial class GameUI : Control
{
	HSlider TimerSlider => GetNode<HSlider>("%TimerSlider");
	public override void _Ready()
	{
		ResetRound();
	}

	public void ResetRound()
	{
		TimerSlider.MaxValue = Global.RoundDuration;
		TimerSlider.Value = Global.RoundDuration;
		CreateTween().TweenProperty(TimerSlider, "value", 0f, Global.RoundDuration);
	}
}
