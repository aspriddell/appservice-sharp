# AppServiceSharp
macOS SMAppService bindings for .NET

## Overview
This library provides bindings for the SMAppService framework on macOS 13 and later, covering the ability to manage Login Items, Launch Agents and Launch Daemons.

### Do I need this?
If you are developing a macOS application that needs to launch at login or requires a background service to be run, this library may be useful to you.

It's recommended to check out Apple's documentation on [Service Management](https://developer.apple.com/documentation/servicemanagement?language=objc) and [XPC](https://developer.apple.com/documentation/xpc?language=objc) to decide if this library is suitable for your needs.

## Usage
> [!WARNING]
> Legacy methods (such as `SMJobBless`) are not included in this project.

The library consists of a single class, `AppService`, which provides most the methods found in the [`SMAppService` Objective-C API](https://developer.apple.com/documentation/servicemanagement/smappservice?language=objc).

Static properties and methods in the class can be used to create instances of `AppService`:
- `AppService.MainAppService`
- `AppService.AgentServiceWithPlistName(string plistName)`
- `AppService.DaemonServiceWithPlistName(string plistName)`
- `AppService.LoginItemServiceWithIdentifier(string bundleIdentifier)`

Additional methods and properties are available for convenience:
- `AppService.IsSupported` - returns whether the current macOS version supports the SMAppService framework.
- `AppService.OpenSystemSettingsLoginItems()` - opens System Settings to the Login Items section.

Note compatibility checks are performed at runtime, resulting in a `PlatformNotSupportedException` being thrown if the current macOS version is too old.

### License
This project is licensed under the MIT License.
