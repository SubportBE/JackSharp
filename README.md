# Jack-Sharp
Jack-Sharp is a .NET/mono binding for [Jack](http://jackaudio.org/).

## Tested Platforms
* Ubuntu 20.04

## Dependencies
* [Jack](http://jackaudio.org/)
* .NET >= 5.0

## JackSharp
C# Wrapper around libjack API. Uses the following classes to structure the API into manageable chunks. Abstracts away all pointers.

### Client: Base Class for Processor and Controller
Emits events on general Jack information, that consumers can subscribe to. See the source code comments for details.

### Processor
Audio and MIDI client. Useful for creating an application with inputs and outputs.

Add your logic for processing a buffer on audio and MIDI input and output by adding a `Func<JackSharp.Processing.ProcessBuffer>` to `ProcessFunc` of an instance of `JackSharp.Processor`. Multiple methods can be added and removed.

### Controller
Client for controlling the Jack server and connections. Useful for creating a control application.

Can connect and disconnect ports of different applications.

If your application needs functionality from both `Processor` and `Controller`, then you must create instances of both classes in your consumer with different names.

