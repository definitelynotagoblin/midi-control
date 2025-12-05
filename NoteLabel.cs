using Godot;

public partial class NoteLabel : Label
{
	[Export] public int TargetPitch;      // MIDI note to match
	[Export] public float Speed = 100f;   // Pixels per second

	public ScoreLabel ScoreLabel;
	public MusicNote TargetNote => MidiUtils.GetNote(TargetPitch);

	public override void _Process(double delta)
	{
		// Move straight down
		Position += new Vector2(0, (float)delta * Speed);

		// Remove if it goes off-screen
		if (Position.Y > GetViewportRect().Size.Y + Size.Y)
		{
			ScoreLabel?.AddPoint(-1);
			QueueFree();
		}
	}

	public void OnMidiNote(MusicNote note)
	{
		if (note.SimpleEquals(TargetNote))
		{
			ScoreLabel?.AddPoint(1);
			QueueFree();
		}
	}
}
