using Godot;

public partial class MainScene : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		OS.OpenMidiInputs();
		var inputDevices = OS.GetConnectedMidiInputs();
		GD.Print($"Input device: {inputDevices[0]}");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent is InputEventMidi midiEvent)
		{
			HandleMidi(midiEvent);
			GD.Print();
		}
	}
	
	private static void HandleMidi(InputEventMidi midiEvent)
	{
		var note = MidiUtils.GetNoteName(midiEvent.Pitch);

		// GD.Print(midiEvent);
		// GD.Print($"Channel {midiEvent.Channel}");
		GD.Print($"Message {midiEvent.Message}");
		GD.Print($"Pitch {midiEvent.Pitch} -> {note}");
		GD.Print($"Velocity {midiEvent.Velocity}");
		// GD.Print($"Instrument {midiEvent.Instrument}");
		// GD.Print($"Pressure {midiEvent.Pressure}");
		// GD.Print($"Controller number: {midiEvent.ControllerNumber}");
		// GD.Print($"Controller value: {midiEvent.ControllerValue}");
	}
}
