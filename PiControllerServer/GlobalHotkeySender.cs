﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static PiControllerServer.GlobalHotkeySender;

namespace PiControllerServer
{
    public class GlobalHotkeySender
    {
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint SendInput(uint cInputs, [MarshalAs(UnmanagedType.LPArray)] lpInput[] inputs, int cbSize);

        public enum InputType : uint
        {
            INPUT_MOUSE = 0,
            INPUT_KEYBOARD = 1,
            INPUT_HARDWARE = 3
        }

        [Flags]
        internal enum KEYEVENTF : uint
        {
            KEYDOWN = 0x0,
            EXTENDEDKEY = 0x0001,
            KEYUP = 0x0002,
            SCANCODE = 0x0008,
            UNICODE = 0x0004
        }

        [Flags]
        internal enum MOUSEEVENTF : uint
        {
            ABSOLUTE = 0x8000,
            HWHEEL = 0x01000,
            MOVE = 0x0001,
            MOVE_NOCOALESCE = 0x2000,
            LEFTDOWN = 0x0002,
            LEFTUP = 0x0004,
            RIGHTDOWN = 0x0008,
            RIGHTUP = 0x0010,
            MIDDLEDOWN = 0x0020,
            MIDDLEUP = 0x0040,
            VIRTUALDESK = 0x4000,
            WHEEL = 0x0800,
            XDOWN = 0x0080,
            XUP = 0x0100
        }

        // Master Input structure
        [StructLayout(LayoutKind.Sequential)]
        public struct lpInput
        {
            internal InputType type;
            internal InputUnion Data;
            internal static int Size { get { return Marshal.SizeOf(typeof(lpInput)); } }
        }

        // Union structure
        [StructLayout(LayoutKind.Explicit)]
        internal struct InputUnion
        {
            [FieldOffset(0)]
            internal MOUSEINPUT mi;
            [FieldOffset(0)]
            internal KEYBDINPUT ki;
            [FieldOffset(0)]
            internal HARDWAREINPUT hi;
        }

        // Input Types
        [StructLayout(LayoutKind.Sequential)]
        internal struct MOUSEINPUT
        {
            internal int dx;
            internal int dy;
            internal int mouseData;
            internal MOUSEEVENTF dwFlags;
            internal uint time;
            internal UIntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct KEYBDINPUT
        {
            internal short wVk;
            internal short wScan;
            internal KEYEVENTF dwFlags;
            internal int time;
            internal UIntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct HARDWAREINPUT
        {
            internal int uMsg;
            internal short wParamL;
            internal short wParamH;
        }

        public static void KeyDown(short key)
        {
            lpInput[] KeyInputs = new lpInput[1];
            lpInput KeyInput = new lpInput();
            // Generic Keyboard Event
            KeyInput.type = InputType.INPUT_KEYBOARD;
            KeyInput.Data.ki.wScan = 0;
            KeyInput.Data.ki.time = 0;
            KeyInput.Data.ki.dwExtraInfo = UIntPtr.Zero;


            // Push the correct key
            KeyInput.Data.ki.wVk = key;
            KeyInput.Data.ki.dwFlags = KEYEVENTF.KEYDOWN;
            KeyInputs[0] = KeyInput;
            Debug.WriteLine("Key down: " + key);
            uint res = SendInput(1, KeyInputs, lpInput.Size);
            Debug.WriteLine("Result: " + res);
        }

        public static void KeyUp(short key)
        {
            lpInput[] KeyInputs = new lpInput[1];
            lpInput KeyInput = new lpInput();
            // Generic Keyboard Event
            KeyInput.type = InputType.INPUT_KEYBOARD;
            KeyInput.Data.ki.wScan = 0;
            KeyInput.Data.ki.time = 0;
            KeyInput.Data.ki.dwExtraInfo = UIntPtr.Zero;

            // Push the correct key
            KeyInput.Data.ki.wVk = key;
            KeyInput.Data.ki.dwFlags = KEYEVENTF.KEYUP;
            KeyInputs[0] = KeyInput;
            uint res = SendInput(1, KeyInputs, lpInput.Size);
            Debug.WriteLine("Result: " + res);
        }

        public static void KeyPress(short key)
        {
            //lpInput[] KeyInputs = new lpInput[1];
            //lpInput KeyInput = new lpInput();
            //// Generic Keyboard Event
            //KeyInput.type = InputType.INPUT_KEYBOARD;
            //KeyInput.Data.ki.wScan = 0;
            //KeyInput.Data.ki.time = 0;
            //KeyInput.Data.ki.dwExtraInfo = UIntPtr.Zero;


            //// Push the correct key
            //KeyInput.Data.ki.wVk = key;
            //KeyInput.Data.ki.dwFlags = KEYEVENTF.KEYDOWN;
            //KeyInputs[0] = KeyInput;
            //Debug.WriteLine("Key down: " + key);
            //uint res = SendInput(1, KeyInputs, lpInput.Size);
            //Debug.WriteLine("Result: " + res);

            KeyDown(key);
            Thread.Sleep(100);
            KeyUp(key);

            //// Release the key
            //KeyInput.Data.ki.dwFlags = KEYEVENTF.KEYUP;
            //KeyInputs[0] = KeyInput;
            //Debug.WriteLine("Key up: " + key);
            //res = SendInput(1, KeyInputs, lpInput.Size);
            //Debug.WriteLine("Result: " + res);
        }
    }

}
