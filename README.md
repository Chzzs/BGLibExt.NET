# monomyo

A C# implementation of the [Myo protocol](https://github.com/thalmiclabs/myo-bluetooth)

Special thanks to Jeff Rowberg and his Bluetooth protocol implementation [BgLib](https://github.com/jrowberg/bglib),
which I, well, just stole. Some minor changes there though: all the event args classes now constitute in different files.

Also a lot of thanks go to [dzhu](https://github.com/dzhu/myo-raw) and [Ramir0](https://github.com/Ramir0/Myo4Linux) for the proof of 
concept provided by their libraries.

### So what's this?
 
It's a library entirely written in __C#__ that provides an SDK-like interface to the [Thalmic's Myo Armbrand](https://www.myo.com/).
It's __not__ a language binding on top of the C++ sdk library Thalmic released nor it needs the Myo Connect software to run.
Thus it is well suited for every platform having .NET installed like Windows, Linux via Mono, OSX via Mono.
Actually the library was developed entirely on a linux platform using MonoDevelop IDE.


### Architecture

It's pretty simple: 
* There is one thread reading from the I/O port and parsing the bluetooth messages
* There is another thread reading from a buffer containing notifications received from the armbrand and executing hooked up events

### Examples

There is an example project ConsoleMyo in the solution. Just hook up the correct port name on your system (like dev/ttyACM0 or COM3)!
It's pretty simple and straight-forward. Don't forget to issue the command _exit_ before closing the console otherwise you'll have to reconnect the dongle.
However if you plan to do a Forms or Gtk application, don't forget to _INVOKE_ methods in the events rather than execute them
because they happen on a different to the UI thread.

### Dependencies

You need .net for windows or mono and bluez libraries for linux.
It's tested on Windows 10 and Arch Linux.

