;
; Script generated by the ASCOM Driver Installer Script Generator 1.2.0.0
; Generated by asd qwe on 10/08/2008 (UTC)
; Changed into Platform Updater by Bob Denny 18-Nov-08
;
[Setup]
AppName=ASCOM Dome Simulator Updater
AppVerName=ASCOM Dome Simulator Updater 5.0.8
AppVersion=5.0.8
AppPublisher=ASCOM Initiative
AppPublisherURL=https://ascom-standards.org/
AppSupportURL=https://ascomtalk.groups.io/g/Help/topics
AppUpdatesURL=https://ascom-standards.org/
MinVersion=0,5.0.2195sp4
DefaultDirName="{cf}\ASCOM\Dome"
DisableDirPage=yes
DisableProgramGroupPage=yes
OutputDir="."
OutputBaseFilename="DomeSimulatorUpdate(5.0.8)"
Compression=lzma
SolidCompression=yes
; Put there by Platform if Driver Installer Support selected
WizardImageFile="C:\Program Files\ASCOM\InstallGen\Resources\WizardImage.bmp"
; {cf}\ASCOM\Uninstall\Dome folder created by Platform, always
Uninstallable=no
DirExistsWarning=no
UninstallFilesDir="{cf}\ASCOM\Uninstall\Dome\DomeSim"

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Dirs]
Name: "{cf}\ASCOM\Uninstall\Dome\DomeSim"

;  Add an option to install the source files
[Tasks]
Name: source; Description: Install the Source files; Flags: unchecked

[Files]
Source: "DomeSim.exe"; DestDir: "{app}" ;
Source: "*"; Excludes: *.zip, *.dll, \bin\*, \obj\*; DestDir: "{app}\Source\DomeSim"; Tasks: source; Flags: recursesubdirs

;
; Next 2 sections are to remove traces of stand-alone driver installation of this
; Platform component that may have been made by installing the 5.0.7 simulator
;
[Registry]
Root: HKLM; Subkey: "Software\Microsoft\Windows\CurrentVersion\Uninstall\ASCOM DomeSim Dome Driver_is1"; ValueType: none; Flags: deletekey

[Run]
Filename: "{cmd}"; Parameters: "/c del /q /f ""{cf}\ASCOM\Uninstall\Dome\DomeSim\*.*"""; Flags: runhidden
Filename: "{cmd}"; Parameters: "/c rmdir ""{cf}\ASCOM\Uninstall\Dome\DomeSim"""; Flags: runhidden

[CODE]
//
// Before the installer UI appears, verify that the (prerequisite)
// ASCOM Platform 5.x is installed, including both Helper components.
// Helper is required for all typpes (COM and .NET)!
//
function InitializeSetup(): Boolean;
var
   H : Variant;
   H2 : Variant;
begin
   Result := FALSE;  // Assume failure
   try               // Will catch all errors including missing reg data
      H := CreateOLEObject('DriverHelper.Util');  // Assure both are available
      H2 := CreateOleObject('DriverHelper2.Util');
      if ((H2.PlatformVersion >= 5.0) and (H2.PlatformVersion < 6.0)) then
         Result := TRUE;
   except
   end;
   if(not Result) then
      MsgBox('The ASCOM Platform 5 is required for this driver.', mbInformation, MB_OK);
end;

