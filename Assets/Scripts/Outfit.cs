using System;

public enum CastorMood { Normal, Happy, Sad }

// Two sprite “sets”: filenames with _2_ vs without.
public enum CastorVariant { V1, V2 }

[Flags]
public enum Outfit
{
    None = 0,
    Manteau = 1 << 0,
    Salopette = 1 << 1,
    Cache = 1 << 2,
    Gants = 1 << 3,
    Tuque = 1 << 4,
    Bottes = 1 << 5,
}