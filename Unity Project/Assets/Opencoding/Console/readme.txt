Thank you for purchasing TouchConsole Pro!
Version 1.3.0.

I really hope you enjoy using TouchConsole Pro. Please do send me any questions, suggestions or feedback at support@opencoding.net!

The documentation for TouchConsole Pro is available here: http://opencoding.net/TouchConsolePro/getting_started.php

If you just want to quickly have a play around with the console, open the ConsoleDemoScene scene in the Demo folder and press Play!

=== Changelog ===
== Version 1.4.0 ==
New features
- Automatically disables Unity GUI input when the console is opened. This can be disabled in the Settings if you have your own way of doing this. This works by setting the EventSystem.current property to null while the console is open and restoring it once the console is closed.
- Added two new callbacks - DebugConsole.Instance.ConsoleAboutToOpen and DebugConsole.Instance.ConsoleAboutToClose. These allow you to be notified when the console is going to open or close and prevent it, if you wish.

Bug fixes
- DebugConsole game objects will now destroy themselves if there is already an instance of DebugConsole loaded.
- Fixed the Instance field for the DebugConsole class not being set to null when it was Destroyed.

== Version 1.3.0 ==
New features
- Added Run button to the end of the input line on mobile devices. This is makes the console at least partly useable in landscape on Nexus and Xperia devices (where a Unity bug causes the touch screen keyboard to be non-interactive).
- Suggestion buttons now automatically execute the command if it takes no parameters.
- Made the keys that open/close the console configurable. By default this is one of ~, \, `, |, § or ±.

Bug fixes
- Fixed Android log emails not including the attachment sometimes.
- Fixed a very infrequent issue where an exception would sometimes be thrown when log messages were emitted from a non-main thread.
- Fixed error that sometimes occurred when running the Demo scene.
- Fixed an issue on Windows where the console would flash up for a single frame when opened.
- Fixed compilation errors with Unity 5 on iOS.

== Version 1.2.0 ==

IMPORTANT: If upgrading to this version, make sure you rebuild your Xcode project from scratch or you will get compile errors.

New Features
- Added CommandHandlers.BeforeCommandExecutedHook that allows you to prevent a command from being executed.
- Added a demo scene and code that shows how you can use this to ask the user for a password before certain commands are executed - useful for public betas.

Changes
- Switched to a different method for modifying the Xcode project. This should be more compatible with other Unity plugins, most notably the Facebook SDK. On upgrade, the old code for this will be automatically deleted to avoid the unnecessary code hanging around - you may notice this in your version control system.
- The filter bar is now automatically closed when the console is.
- On Mobile: Opening the filter bar with the console maximized now temporarily minimizes the console so the keyboard doesn’t overlay the console.

Bug Fixes
- Worked around a bug in Unity 4.6.1 that caused a crash on iOS and Android (thanks to the multiple users who noticed this!)
- Fixed copying text not working in the web player (thanks jerotas!)
- Fixed an exception that occurred when the filter bar was closed using the Done/Return button on the Touch Screen Keyboard.

== Version 1.1.1 ==

New Features
- Added a new method for opening the console - holding down three fingers for about half a second. This can be enabled in the settings.
- Added hook to allow you to customise the email that is sent - extra attachments can be added and the message modified or replaced. This is useful for adding your save file or screenshots etc.
- Added method for triggering the log email to be sent, if you want to provide another method for sending it.

Bug Fixes
- Fixed the log being blank on Unity 5.
- Fixed WebGL builds on Unity 5.
- Fixed an error when the console was used in a game with stripping enabled (added a link.xml file).
- Fix for builds failing when the console was used in a game with the Facebook SDK included.