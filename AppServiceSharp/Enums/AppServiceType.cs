// AppServiceSharp - macOS Service Management Framework bindings for .NET
// Licensed under MIT - see the license file for more information

namespace AppServiceSharp.Enums;

/// <summary>
/// The type of app service being managed
/// </summary>
public enum AppServiceType : byte
{
    Agent = 1,
    Daemon = 2,
    LoginItem = 3
}