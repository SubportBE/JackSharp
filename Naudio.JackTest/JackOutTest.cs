// Author:
//	   Thomas Mayer <thomas@residuum.org>
//
// Copyright (c) 2016 Thomas Mayer
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.IO;
using System.Linq;
using System.Threading;
using JackSharp;
using Naudio.Jack;
using Naudio.JackTest.WaveIntegration;
using NAudio.Wave;
using NUnit.Framework;

namespace Naudio.JackTest
{
	[TestFixture]
	public class JackOutTest
	{
		static Client _client;
		static JackOut _jackOut;

		[SetUp]
		public static void CreateOutput ()
		{
			_client = new Client ("testing", 0, 2);
			_jackOut = new JackOut (_client);
		}

		[Test]
		public virtual void AudioFormat ()
		{
			_jackOut.Play ();
			Assert.AreEqual (_jackOut.OutputWaveFormat.SampleRate, _client.SampleRate);
			Assert.AreEqual (_jackOut.OutputWaveFormat.Channels, _client.AudioOutPorts.Count ());
		}

		[Test]
		public virtual void PlayAudioFile ()
		{
			string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
			string wavFile = Path.Combine (currentDirectory, "example.wav");
			WaveFileReader reader = new WaveFileReader (wavFile);
			Wave16ToFloatProvider converter = new Wave16ToFloatProvider (reader);				
			Analyser analyser = new Analyser ();
			_client.ProcessFunc += analyser.AnalyseOutAction;
			_jackOut.Init (converter);
			_jackOut.Play ();
			Thread.Sleep (10000);
			_jackOut.Stop ();
			reader.Close ();
			Assert.AreNotEqual (0, analyser.NotEmptySamples);
		}

		[TearDown]
		public static void DestroyClient ()
		{
			_jackOut.Dispose ();
			_client.Dispose ();
		}

	}
}
