using JackSharp.Events;
using System;

namespace JackSharp
{
	public interface IClient
	{
		int BufferSize { get; }
		bool IsConnectedToJack { get; }
		int SampleRate { get; }

		event EventHandler<BufferSizeEventArgs> BufferSizeChanged;
		event EventHandler<ErrorEventArgs> Error;
		event EventHandler<InfoEventArgs> Info;
		event EventHandler<NotAvailableEventArgs> NotAvailable;
		event EventHandler<SampleRateEventArgs> SampleRateChanged;
		event EventHandler<EventArgs> Shutdown;
		event EventHandler<XrunEventArgs> Xrun;
	}
}