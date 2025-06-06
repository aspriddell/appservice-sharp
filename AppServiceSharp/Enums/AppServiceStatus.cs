// AppServiceSharp - macOS Service Management Framework bindings for .NET
// Licensed under MIT - see the license file for more information

namespace AppServiceSharp.Enums;

/// <summary>
/// Represents the possible service installation/activation states
/// </summary>
public enum AppServiceStatus : byte
{
    NotRegistered,
    Enabled,
    RequiresApproval,
    NotFound,
    Unknown
}