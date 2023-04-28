# Pi Midi Controller (for VoiceMeeter)

This is a simple midi controller for the Raspberry Pi. It is designed to be used with VoiceMeeter on Windows.

Most recommended usage is with VoiceMeeter to control your audio environment and use MacroButtons to control your computer, for example OBS.

This program requires a Raspberry Pi with a screen. It is recommended to use a Raspberry Pi 4 and a 10" touchscreen. It is written in C# and uses .NET 6. For the linux UI, it uses GTK#.

**Features**
- 5x3 grid of customizable buttons and knobs
- Tabs for different pages
- Customizable names for buttons and knobs
- Customizable midi notes and controller numbers
- Customizable soft stops for knobs
- Customizable min, max and overdrive values for knobs
- Feedback for knobs
- Button colors from SysEx midi messages

## Screenshots
![image](https://user-images.githubusercontent.com/51454971/234832611-12c306cf-df48-4dbc-a800-8b2fc50f7815.png)
![image](https://user-images.githubusercontent.com/51454971/234832676-b4f0d333-c37a-465c-ae33-a5ee7f98a81b.png)
![image](https://user-images.githubusercontent.com/51454971/234832730-2de053ae-4754-4542-b64f-c7bafeb3878c.png)
![image](https://user-images.githubusercontent.com/51454971/234833125-b16264b3-a041-4705-824f-93d2ec86b646.png)

## Installation

### Raspberry Pi

1. Install Raspberry Pi OS (32-bit) on your Raspberry Pi
2. Install dotnet6 on your Raspberry Pi (see [here](https://learn.microsoft.com/en-us/dotnet/iot/deployment))
3. Download the latest release from the [releases page](https://github.com/Longoon12000/PiMidiController/releases)
4. Extract the PiControllerClient folder to the directory of your choice

You can manually run the program by running `dotnet PiControllerClient.dll hostiporname port` in the PiControllerClient folder or you can create a service to run it automatically on startup:

1. Create a new service file with `sudo nano /etc/systemd/system/pimidi.service`
2. Paste the following into the file:
```
[Unit]
Description=Pi Midi Controller Client
After=graphical.target

[Service]
User=your-username
Group=your-usergroup
Environment=DOTNET_ROOT=$HOME/.dotnet
Environment=PATH=$PATH:$HOME/.dotnet
Environment=DISPLAY=:0
ExecStart=/home/your-username/.dotnet/dotnet /your/install/dir/PiControllerClient.dll hostiporname port
Restart=always

[Install]
WantedBy=graphical.target
```
3. Replace `your-username` with your username and `your-usergroup` with your usergroup
4. Replace `/your/install/dir` with the directory you installed the program to
5. Replace `hostiporname` with the ip or hostname of the server and `port` with the port of the server (your VoiceMeeter host device)
6. Save the file with `Ctrl+X` and `Y`
7. Enable the service with `sudo systemctl enable pimidi.service`
8. Start the service with `sudo systemctl start pimidi.service`

This will automatically start the program on startup and restart it if it crashes. Auto-login is recommended.

### Windows

**Prerequisites**

This program requires the [LoopMidi](https://www.tobias-erichsen.de/software/loopmidi.html) driver to be installed. Please install it before running the program.

This program required dotnet6 to be installed. If you have Windows 11, you already have it installed. If you have Windows 10, you can install it from [here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

1. Download the latest release from the [releases page](https://github.com/Longoon12000/PiMidiController/releases)
2. Extract the PiControllerServer.exe to the directory of your choice (eg. `%LocalAppdata%\PiMidiController\`)
3. Run the PiControllerServer.exe

To automatically start the program on startup, you can create a shortcut to the exe in the startup folder `%Appdata%\Microsoft\Windows\Start Menu\Programs\Startup`

Please note that this program has to start before VoiceMeeter for VoiceMeeter to correctly detect the midi controller. VoiceMeeter has an automatic delay of 5 seconds, so you can use that to your advantage.

## Usage

### Raspberry Pi

This application is self-contained and does not require any configuration. Simply run the program with the correct parameters and it will automatically connect to the server.

`dotnet PiControllerClient.dll hostiporname port`
- `hostiporname`: The ip or hostname of the server
- `port`: The port of the server (your VoiceMeeter host device)

Example: `dotnet PiControllerClient.dll 192.168.0.25 14817`

### Windows

The default port is 14817. You can change it by supplying the port as a command line argument:

`PiControllerServer.exe 12345`

Upon startup you will not see a window but instead a new icon in your system tray. Right-clicking on the icon will open a menu with the following options:
- Show: Shows the window
- Exit: Exits the program

You can also double-click the icon to show the window.

The window has the tab list on the left, where you can add new tabs to your controller.  
To add a new tab, click the "Add" button.  
To change the tab name, select the tab and type in the new name in the text box to the right of the "Remove" button.  
To remove a tab, select the tab and click the "Remove" button.

Each tab can contain a maximum of 15 controls (5x3 grid). Each control is pre-populated and set to blank. Blank controls will not be shown on the controller but still take up the corresponding position.  
By default, controls of a new tab all have Note -1, which automatically assigns the correct note to the control when saving.

To edit a control, click the corresponding "Edit" button. This will open a new window where you can edit the control. Changes in this window are not saved and transmitted until you click the "Save" button.

The Name field is the name of the control. This is only used for the controller and does not affect the midi output.  
The control type determines the type of control. The following types are available:
- Blank: Does not show a control on the controller
- Button: A button that sends a NoteOn event when pressed and a NoteOff event when released
- Knob: A knob that sends a ControlChange event when turned
The Note field is the note that is sent when the control is pressed. For knobs, this is the Controller number.

Knobs have additional options:

**Soft stops**  
Soft stops are used to increase the required travel to move off the soft stop value. This makes it easier to pre-define commonly used values that can quickly be set.  
For example, you can set the soft stops to 50, 75 and 100 and the positions of these values will require more travel to move off of. This allows you to quickly set the value to 50, 75 or 100 without having to be very precise.

To add a soft stop, enter the soft stop value in the number input field and click the "+" button. To remove a soft stop, select the soft stop in the list and click the "-" button.

**Knob settings**
- Minimum: The minimum value of the knob
- Maximum: The maximum value of the knob
- Overdrive: The overdrive value of the knob.
- Centered: Whether the knob is centered or not. If the knob is centered, the value will be 0 when the knob is in the center position. If the knob is not centered, the value will be 0 when the knob is in the minimum position.

A knob displays the percentage of the current value between the minimum and maximum value. The overdrive value is used to set the unsafe value range of the knob. For example, if the minimum value is 0 and the maximum value is 100 and the overdrive value is 127, the knob will add a red area past 100% that goes from the maximum value to the overdrive value.

## VoiceMeeter

To use the controller with VoiceMeeter, you need to add the midi device "Pi Midi Controller" to VoiceMeeter.  
Go to Menu -> M.I.D.I. Mapping and select the "Pi Midi Controller" device as your M.I.D.I. Input Device. You can now map the controls to VoiceMeeter.  
The Pi Midi Controller also supports feedback, so you can set it as the M.I.D.I. Output Device and set F on the corresponding mappings to get feedback from VoiceMeeter. This will show the current value of the control on the controller and initialise the UI with the correct values from VoiceMeeter.  

For use in MacroButtons you can open the context menu of the MacroButtons window and select "Pi Midi Controller" as the MIDI OUT1 or OUT2 device. This will allow you to send midi events to the controller, to set button colors or knob positions.

To set the value of a knob from MacroButtons, you can use the following command:  
`System.SendMidi("out1", "ctrl-change", 1, C, V);`  
This will send a ControlChange event to OUT1 with the controller number C and the value V. The controller number is the same as the note number of the knob.  
For example to set the value of knob with note 7 to value 50, you can use the following command:  
`System.SendMidi("out1", "ctrl-change", 1, 7, 50);`

To set the color of a button from MacroButtons, you can use the following command:  
`System.SendMidi("out1", "data", F0, CC, RR, GG, BB, F7);`  
This will send a SysEx event to OUT1 with the button number BB color values RR, GG and BB. The controller number is the same as the note number of the button.  
**Important**: The values for CC, RR, GG and BB are hexadecimal and have a maximum value of 7F (127). Colors are sent as RGB values that range from 0 to 127 where 127 is maximum brightness. To convert a normal RGB value to the value used by the controller, divide the RGB (0-255) value by 2.  
For example to set the color of button with note 4 to red (RGB 255, 0, 0), you can use the following command:  
`System.SendMidi("out1", "data", F0, 0, 4, 7F, 0, 0, F7);`
