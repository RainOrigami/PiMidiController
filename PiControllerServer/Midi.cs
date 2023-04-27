using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TobiasErichsen.teVirtualMIDI;

namespace PiControllerServer
{
    internal class Midi : IDisposable
    {
        private static Guid manufacturer = new Guid("e11d4d2a-9bdc-4ac4-afea-ee5907a8dbfc");
        private static Guid product = new Guid("66a94d67-e211-49fb-b410-c6a011ddddc5");

        private TeVirtualMIDI device;

        public event EventHandler<MidiEvent>? MidiEventReceived;
        public event EventHandler<(int note, int red, int green, int blue)>? ColorEventReceived;

        public Midi(string deviceName)
        {
#if DEBUG
            TeVirtualMIDI.logging(TeVirtualMIDI.TE_VM_LOGGING_RX | TeVirtualMIDI.TE_VM_LOGGING_TX | TeVirtualMIDI.TE_VM_LOGGING_MISC);
#endif
            this.device = new TeVirtualMIDI(deviceName, ushort.MaxValue, TeVirtualMIDI.TE_VM_FLAGS_PARSE_RX | TeVirtualMIDI.TE_VM_FLAGS_INSTANTIATE_BOTH, ref manufacturer, ref product);

            Task.Run(handle);
        }

        private void handle()
        {
            while (true)
            {
                byte[] messageBytes = this.device.getCommand();
                if (messageBytes.Length == 3)
                {
                    try
                    {
                        byte[] paddedMessageBytes = new byte[4];
                        messageBytes.CopyTo(paddedMessageBytes, 0);

                        MidiEvent midiEvent = MidiEvent.FromRawMessage(BitConverter.ToInt32(paddedMessageBytes, 0));
                        this.MidiEventReceived?.Invoke(this, midiEvent);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }
                else if (messageBytes.Length == 6)
                {
                    this.ColorEventReceived?.Invoke(this, ((int)messageBytes[1], (int)messageBytes[2], (int)messageBytes[3], (int)messageBytes[4]));
                }
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
