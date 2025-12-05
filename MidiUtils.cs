public static class MidiUtils
{
    private static readonly string[] NoteNames =
    {
        "C", "C#", "D", "D#", "E", "F",
        "F#", "G", "G#", "A", "A#", "B"
    };

    // midiNote: 0–127, e.g. 60 = middle C
    public static MusicNote GetNoteName(int midiNote)
    {
        var name = NoteNames[midiNote % 12];
        var octave = (midiNote / 12) - 1; // common convention: 60 -> C4
        return new MusicNote {
            Note = name,
            Octave = octave
        };
    }
}

public struct MusicNote
{
    public string Note;
    public int Octave;

    public override string ToString() => $"{Note}{Octave}";
}