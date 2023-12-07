using System.Reflection;

namespace BuildingBlocks;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}