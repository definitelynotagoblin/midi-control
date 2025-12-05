using System;
using System.Collections.Generic;
using Godot;

public partial class MainScene : Node2D
{
	[Export] public PackedScene NoteLabelScene;
	[Export] public NodePath ScoreLabelPath;

	[Export] public float SpawnInterval = 2.0f;
	private ScoreLabel _scoreLabel;
	private double _time;
	private readonly List<NoteLabel> _activeNotes = [];
	private readonly Random _rnd = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		OS.OpenMidiInputs();
		var inputDevices = OS.GetConnectedMidiInputs();
		GD.Print($"Input device: {inputDevices[0]}");

		_scoreLabel = GetNode<ScoreLabel>(ScoreLabelPath);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_time += delta;
		if (_time >= SpawnInterval)
		{
			_time -= SpawnInterval;
			var randomPitch = _rnd.Next(128);
			SpawnNote(randomPitch);
		}
	}
	
	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent is InputEventMidi midiEvent)
		{
			DebugMidiInput(midiEvent);

			// Event is key press
			if (midiEvent.Velocity > 0)
			{
				var inputNote = MidiUtils.GetNote(midiEvent.Pitch);

				foreach (var note in _activeNotes.ToArray())
				{
					if (!IsInstanceValid(note))
					{
						_activeNotes.Remove(note);
						continue;
					}

					note.OnMidiNote(inputNote);
					if (!IsInstanceValid(note))
						_activeNotes.Remove(note);
				}
			}
		}
	}
	
	private static void DebugMidiInput(InputEventMidi midiEvent)
	{
		var note = MidiUtils.GetNote(midiEvent.Pitch);

		// GD.Print(midiEvent);
		// GD.Print($"Channel {midiEvent.Channel}");
		GD.Print($"Message {midiEvent.Message}");
		GD.Print($"Pitch {midiEvent.Pitch} -> {note}");
		GD.Print($"Velocity {midiEvent.Velocity}");
		// GD.Print($"Instrument {midiEvent.Instrument}");
		// GD.Print($"Pressure {midiEvent.Pressure}");
		// GD.Print($"Controller number: {midiEvent.ControllerNumber}");
		// GD.Print($"Controller value: {midiEvent.ControllerValue}");
		GD.Print();
	}

	private void SpawnNote(int pitch)
	{
		var note = NoteLabelScene.Instantiate<NoteLabel>();
		note.ScoreLabel = _scoreLabel;
		note.TargetPitch = pitch;
		note.Text = note.TargetNote.ToString();
		var startingX = (GetViewportRect().Size.X - note.Size.X) / 2;
		note.Position = new Vector2(startingX, -note.Size.Y); // start just above screen
		AddChild(note);
		_activeNotes.Add(note);
	}
}
