// AppServiceSharp - macOS Service Management Framework bindings for .NET
// Licensed under MIT - see the license file for more information

using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using AppServiceSharp.Enums;

namespace AppServiceSharp;

/// <summary>
/// Service class for managing helper executables that live inside an app's main bundle
/// </summary>
/// <remarks>
/// This is a .NET-mapped version of the SMAppService class. For more information, refer to https://developer.apple.com/documentation/servicemanagement/smappservice?language=objc
/// </remarks>
[SupportedOSPlatform("macos13.0")]
public class AppService : CriticalFinalizerObject, IDisposable
{
    private static readonly Lazy<AppService> MainAppServiceInstance = new(() => new AppService());

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly bool _isMainAppService;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly IntPtr _nativeHandle;

    private AppService(string identifier, AppServiceType serviceType)
    {
        CheckPlatformSupported();

        var handle = NativeMethods.CreateAppServiceFromIdentifier(identifier, serviceType);
        if (handle == IntPtr.Zero)
        {
            throw new ExternalException("App service object creation failed.");
        }

        _nativeHandle = handle;
        _isMainAppService = false;

        Identifier = identifier;
        ServiceType = serviceType;
    }

    private AppService()
    {
        CheckPlatformSupported();

        _nativeHandle = NativeMethods.GetMainAppServiceManagerHandle();
        _isMainAppService = true;

        Identifier = "mainAppService";
        ServiceType = AppServiceType.LoginItem;
    }

    /// <summary>
    /// Gets the identifier associated with this app service object.
    /// </summary>
    public string Identifier { get; }

    /// <summary>
    /// Gets the type associated with this app service object.
    /// </summary>
    public AppServiceType ServiceType { get; }

    /// <summary>
    /// Gets the current installation/activation status of the service.
    /// </summary>
    public AppServiceStatus Status => NativeMethods.GetServiceStatus(_nativeHandle);

    /// <summary>
    /// Releases any resources held by the app service object.
    /// </summary>
    public void Dispose()
    {
        if (!_isMainAppService)
        {
            NativeMethods.ReleaseAppService(_nativeHandle);
        }

        GC.SuppressFinalize(this);
    }

    ~AppService()
    {
        Dispose();
    }

    /// <summary>
    /// Registers the service so it can begin launching (subject to user approval)
    /// </summary>
    public ServiceUpdateError RegisterService() => NativeMethods.RegisterService(_nativeHandle);

    /// <summary>
    /// Unregisters the service so the system no longer launches it
    /// </summary>
    public ServiceUpdateError UnregisterService() => NativeMethods.UnregisterService(_nativeHandle);

    /// <summary>
    /// Gets whether the <see cref="AppService"/> is supported on the current system.
    /// </summary>
    public static bool IsSupported => OperatingSystem.IsMacOSVersionAtLeast(13);

    /// <summary>
    /// Gets an app service object that corresponds to the main application as a login item.
    /// </summary>
    public static AppService MainAppService => MainAppServiceInstance.Value;

    /// <summary>
    /// Initializes an app service object with a launch agent with the provided property list name.
    /// </summary>
    public static AppService AgentServiceWithPlistName(string plistName) => new(plistName, AppServiceType.Agent);

    /// <summary>
    /// Initializes an app service object with a launch daemon with the provided property list name.
    /// </summary>
    public static AppService DaemonServiceWithPlistName(string plistName) => new(plistName, AppServiceType.Daemon);

    /// <summary>
    /// Initializes an app service object for a login item corresponding to the bundle with the provided identifier.
    /// </summary>
    public static AppService LoginItemServiceWithIdentifier(string bundleIdentifier) => new(bundleIdentifier, AppServiceType.LoginItem);

    /// <summary>
    /// Opens System Settings to the Login Items control panel.
    /// </summary>
    public static void OpenSystemSettingsLoginItems()
    {
        CheckPlatformSupported();
        NativeMethods.OpenSystemSettings();
    }

    private static void CheckPlatformSupported()
    {
        if (!IsSupported)
        {
            throw new PlatformNotSupportedException("The current platform is not supported. SMAppService requires macOS 13 Ventura or later to function.");
        }
    }
}