// AppServiceSharp - macOS Service Management Framework bindings for .NET
// Licensed under MIT - see the license file for more information

using System;
using System.Runtime.InteropServices;
using AppServiceSharp.Enums;

namespace AppServiceSharp;

internal static partial class NativeMethods
{
    private const string LibraryName = "libappservice";

    [LibraryImport(LibraryName, EntryPoint = "openSystemSettings")]
    public static partial void OpenSystemSettings();

    [LibraryImport(LibraryName, EntryPoint = "getMainAppServiceManager")]
    public static partial IntPtr GetMainAppServiceManagerHandle();

    [LibraryImport(LibraryName, EntryPoint = "createAppServiceFromIdentifier", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr CreateAppServiceFromIdentifier(string identifier, AppServiceType serviceType);

    [LibraryImport(LibraryName, EntryPoint = "destroyAppService")]
    public static partial void ReleaseAppService(IntPtr appService);

    [LibraryImport(LibraryName, EntryPoint = "getServiceStatus")]
    public static partial AppServiceStatus GetServiceStatus(IntPtr appService);

    [LibraryImport(LibraryName, EntryPoint = "registerService")]
    public static partial ServiceUpdateError RegisterService(IntPtr appService);

    [LibraryImport(LibraryName, EntryPoint = "unregisterService")]
    public static partial ServiceUpdateError UnregisterService(IntPtr appService);
}