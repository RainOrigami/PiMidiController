using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TobiasErichsen.teVirtualMIDI;

namespace PiControllerServer
{
    internal class Midi : IDisposable
    {
        private TeVirtualMIDI device;

        public event EventHandler<MidiEvent>? MidiEventReceived;

        public Midi(string deviceName)
        {
            this.device = new TeVirtualMIDI(deviceName, flags: 0);

            Task.Run(handle);
        }

        private void handle()
        {
            while (true)
            {
                byte[] messageBytes = this.device.getCommand();
                byte[] paddedMessageBytes = new byte[Math.Max(messageBytes.Length, 4)];
                messageBytes.CopyTo(paddedMessageBytes, 0);

                MidiEvent midiEvent = MidiEvent.FromRawMessage(BitConverter.ToInt32(paddedMessageBytes, 0));

                this.MidiEventReceived?.Invoke(this, midiEvent);
            }
        }

        public void Send(MidiEvent midiEvent)
        {
            this.device.sendCommand(BitConverter.GetBytes(midiEvent.GetAsShortMessage()));
        }

        public void Dispose()
        {
            this.device.shutdown();
        }

        internal void SendControlChange(int note, int value)
        {
            Send(new ControlChangeEvent(0, 1, (MidiController)note, value));
        }

        internal void SendNoteOff(int note)
        {
            Send(new NoteEvent(0, 1, MidiCommandCode.NoteOff, note, 0));
        }

        internal void SendNoteOn(int note, int velocity)
        {
            Send(new NoteOnEvent(0, 1, note, velocity, 0));
        }
    }
}
