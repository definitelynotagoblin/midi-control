public static class MidiUtils
{
    public static readonly string[] NoteNames =
    {
        "C", "C#", "D", "D#", "E", "F",
        "F#", "G", "G#", "A", "A#", "B"
    };

    // pitch: 0–127, e.g. 60 = middle C
    public static MusicNote GetNote(int pitch)
    {
        var name = NoteNames[pitch % 12];
        var octave = (pitch / 12) - 1; // common convention: 60 -> C4
        return new MusicNote {
            Pitch = pitch,
            Note = name,
            Octave = octave
        };
    }
}

public struct MusicNote
{
    public int Pitch;
    public string Note;
    public int Octave;

    public override string ToString() => Note;
    public string ToFullString() => $"{Note}{Octave}";

    public bool SimpleEquals(MusicNote other)
    {
        return this.Note == other.Note;
    }
}