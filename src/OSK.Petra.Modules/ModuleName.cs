using System;

namespace OSK.Petra.Modules;

/// <summary>
/// A strongly typed name for a module
/// </summary>
public readonly struct ModuleName : IEquatable<ModuleName>
{
    #region Variables

    public string Name { get; }

    #endregion

    #region Constructors

    public ModuleName(string name)
    {
        Name = name ?? string.Empty;
    }

    #endregion

    #region IEquatable

    public bool Equals(ModuleName other)
    => string.Equals(Name, other.Name, StringComparison.Ordinal);

    public override bool Equals(object obj)
        => obj is ModuleName other && Equals(other);

    public override int GetHashCode()
        => StringComparer.Ordinal.GetHashCode(Name ?? string.Empty);

    #endregion

    #region Object Overrides

    public override string ToString() => Name;

    #endregion

    #region Operators

    public static bool operator ==(ModuleName left, ModuleName right) => left.Equals(right);
    public static bool operator !=(ModuleName left, ModuleName right) => !left.Equals(right);

    public static implicit operator ModuleName(string name) => new(name);

    #endregion
}
