using JackSharp.Ports;
using JackSharp.Processing;
using System;
using System.Collections.Generic;

namespace JackSharp
{
	public interface IProcessor : IClient
	{
		IEnumerable<AudioInPort> AudioInPorts { get; }
		IEnumerable<AudioOutPort> AudioOutPorts { get; }
		IEnumerable<MidiInPort> MidiInPorts { get; }
		IEnumerable<MidiOutPort> MidiOutPorts { get; }
		string PortNameFormat { set; }
		Action<ProcessBuffer> ProcessFunc { get; set; }

		void ConnectOutPorts(IEnumerable<(int output, int input)> portConnections);
		bool Start(bool startServer = false);
		bool Stop();
	}
}