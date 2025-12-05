using Godot;
using System;

public partial class ScoreLabel : Label
{
	private int _score = 0;

	public void AddPoint(int amount)
	{
		_score += amount;
		Text = $"Score: {_score}";
	}
}
