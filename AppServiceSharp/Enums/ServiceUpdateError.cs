// AppServiceSharp - macOS Service Management Framework bindings for .NET
// Licensed under MIT - see the license file for more information

namespace AppServiceSharp.Enums;

/// <summary>
/// States a service installation/removal can produce.
/// </summary>
public enum ServiceUpdateError : byte
{
    None,
    NoDetails,
    AuthFailure,
    InternalFailure,
    InvalidPlist,
    InvalidSignature,
    JobMustBeEnabled,
    JobNotFound,
    JobPlistNotFound,
    LaunchDeniedByUser,
    ServiceUnavailable,
    ToolNotValid
}