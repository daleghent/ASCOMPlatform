;;    Remarks for the template are prefixed like this
;;
;;    2007-sep-08  cdr  Add registry code for DCOM
;;                      Add RegASCOM if COM dll or exe
;;
;;    2007-Sep-09  rbd  Comment for CLSID, make more obvious :-)
;;                      Add dual-interface support, AppId uninstall
;;                      Add driver source code install
;;                      Many small adjustments
;;
;;    2007-Dec-19  cdr  Add /codebase switch to regasm install command
;;                      for .NET assembly driver. Needed for driver to 
;;                      reside outside GAC.
;;
;;    2008-Jan-21  rbd  More AppID/DCOM, the AppId/Exename 
;;
;;    2008-Jan-24  rbd  Add start check for ASCOM 5 and Helpers (thanks
;;                      to Chris Rowland for original code for this).
;;
;;    2008-Apr-28  rbd	Add VersionInfoVersion to [Setup]
;;
;;    2009-Aug-21  rbd  Allow platform versions > 5
;;    2010-Oct-05  cdr  Use Utilities.Util instead of DriverHelper to be
;;                      compatible with 64 bits and allow the use of the
;;                      IsMinimumRequiredVersion(n,m) property.
;;    2011-Jan-20  cdr  Change the .NET assembly registration to register the
;;                      dll as both 32 and 64 bits
;;    2011-Mar-24  rbd  ASCOM-76 Remove generated TODO item to create
;;                      the ServedClasses folder. No longer used.
;;    2012-Feb-26  cdr  Fix bug with IsMinimumRequiredVersion where it was using a full stop
;;                      separator instead of a comma.
;;    2015-Dec-13  cdr  Change Version check to require version 6.2 instead of 6.0.
;
; Script generated by the ASCOM Driver Installer Script Generator %gver%
; Generated by %devn% on %date% (UTC)
;
[Setup]
AppID={{%appid%}
AppName=ASCOM %name% %type% Driver
AppVerName=ASCOM %name% %type% Driver %vers%
AppVersion=%vers%
AppPublisher=%devn% <%deve%>
AppPublisherURL=mailto:%deve%
AppSupportURL=https://ascomtalk.groups.io/g/Help
AppUpdatesURL=https://ascom-standards.org/
VersionInfoVersion=1.0.0
MinVersion=6.1.7601
DefaultDirName="{cf}\ASCOM\%type%"
DisableDirPage=yes
DisableProgramGroupPage=yes
OutputDir="."
OutputBaseFilename="%name% Setup"
Compression=lzma
SolidCompression=yes
; Put there by Platform if Driver Installer Support selected
WizardImageFile="%rscf%\WizardImage.bmp"
LicenseFile="%rscf%\CreativeCommons.txt"
; {cf}\ASCOM\Uninstall\%type% folder created by Platform, always
UninstallFilesDir="{cf}\ASCOM\Uninstall\%type%\%name%"

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Dirs]
Name: "{cf}\ASCOM\Uninstall\%type%\%name%"
; TODO: Add subfolders below {app} as needed (e.g. Name: "{app}\MyFolder")
%srce%
%srce%;  Add an option to install the source files
%srce%[Tasks]
%srce%Name: source; Description: Install the Source files; Flags: unchecked

[Files]
%cdll%; regserver flag only if native COM, not .NET
%coms%Source: "%srcp%\%file%"; DestDir: "{app}" %rs32%
%nbth%Source: "%srcp%\bin\Release\%file%"; DestDir: "{app}"
%nlcs%; TODO: Add driver assemblies into the ServedClasses folder
; Require a read-me HTML to appear after installation, maybe driver's Help doc
Source: "%srcp%\%rdmf%"; DestDir: "{app}"; Flags: isreadme
%srce%; Optional source files (COM and .NET aware)
%srce%Source: "%srcp%\*"; Excludes: *.zip,*.exe,*.dll, \bin\*, \obj\*; DestDir: "{app}\Source\%name% Driver"; Tasks: source; Flags: recursesubdirs
; TODO: Add other files needed by your driver here (add subfolders above)
%cexe%
%cexe%;Only if COM Local Server
%cexe%[Run]
%cexe%Filename: "{app}\%file%"; Parameters: "/regserver"
%cexe%

%nbth%
%nbth%; Only if driver is .NET
%nbth%[Run]
%nasm%; Only for .NET assembly/in-proc drivers
%nasm%Filename: "{%net32%}\regasm.exe"; Parameters: "/codebase ""{app}\%file%"""; Flags: runhidden 32bit
%nasm%Filename: "{%net64%}\regasm.exe"; Parameters: "/codebase ""{app}\%file%"""; Flags: runhidden 64bit; Check: IsWin64

%nlcs%; Only for .NET local-server drivers
%nlcs%Filename: "{app}\%file%"; Parameters: "/register"

%cexe%;Only if COM Local Server
%cexe%[UninstallRun]
%cexe%Filename: "{app}\%file%"; Parameters: "/unregserver"

%nbth%
%nbth%; Only if driver is .NET
%nbth%[UninstallRun]
%nasm%; Only for .NET assembly/in-proc drivers
%nasm%Filename: "{%net32%}\regasm.exe"; Parameters: "-u ""{app}\%file%"""; Flags: runhidden 32bit
%nbth%; This helps to give a clean uninstall
%nasm%Filename: "{%net64%}\regasm.exe"; Parameters: "/codebase ""{app}\%file%"""; Flags: runhidden 64bit; Check: IsWin64
%nasm%Filename: "{%net64%}\regasm.exe"; Parameters: "-u ""{app}\%file%"""; Flags: runhidden 64bit; Check: IsWin64

%nlcs%; Only for .NET local-server drivers
%nlcs%Filename: "{app}\%file%"; Parameters: "/unregister"

%cexe%;  DCOM setup for COM local Server, needed for TheSky
%cexe%[Registry]
%cexe%; TODO: If needed set this value to the %type% CLSID of your driver (mind the leading/extra '{')
%cexe%#define AppClsid "{%cid1%"
%cex2%; TODO: If needed set this value to the %typ2% CLSID of your driver (mind the leading/extra '{')
%cex2%#define AppClsid2 "{%cid2%"

%cexe%; set the DCOM access control for TheSky on the %type% interface
%cexe%Root: HKCR; Subkey: CLSID\{#AppClsid}; ValueType: string; ValueName: AppID; ValueData: {#AppClsid}
%cexe%Root: HKCR; Subkey: AppId\{#AppClsid}; ValueType: string; ValueData: "ASCOM %name% %type% Driver"
%cexe%Root: HKCR; Subkey: AppId\{#AppClsid}; ValueType: string; ValueName: AppID; ValueData: {#AppClsid}
%cexe%Root: HKCR; Subkey: AppId\{#AppClsid}; ValueType: dword; ValueName: AuthenticationLevel; ValueData: 1
%cex2%; set the DCOM access control for TheSky on the %typ2% interface
%cex2%Root: HKCR; Subkey: CLSID\{#AppClsid2}; ValueType: string; ValueName: AppID; ValueData: {#AppClsid2}
%cex2%Root: HKCR; Subkey: AppId\{#AppClsid2}; ValueType: string; ValueData: "ASCOM %name% %type% Driver"
%cex2%Root: HKCR; Subkey: AppId\{#AppClsid2}; ValueType: string; ValueName: AppID; ValueData: {#AppClsid2}
%cex2%Root: HKCR; Subkey: AppId\{#AppClsid2}; ValueType: dword; ValueName: AuthenticationLevel; ValueData: 1
%cexe%; set the DCOM key for the executable as a whole
%cexe%Root: HKCR; Subkey: AppId\%file%; ValueType: string; ValueName: AppID; ValueData: {#AppClsid}
%cexe%; CAUTION! DO NOT EDIT - DELETING ENTIRE APPID TREE WILL BREAK WINDOWS!
%cexe%Root: HKCR; Subkey: AppId\{#AppClsid}; Flags: uninsdeletekey
%cex2%Root: HKCR; Subkey: AppId\{#AppClsid2}; Flags: uninsdeletekey
%cexe%Root: HKCR; Subkey: AppId\%file%; Flags: uninsdeletekey

[Code]
const
   REQUIRED_PLATFORM_VERSION = 6.2;    // Set this to the minimum required ASCOM Platform version for this application

//
// Function to return the ASCOM Platform's version number as a double.
//
function PlatformVersion(): Double;
var
   PlatVerString : String;
begin
   Result := 0.0;  // Initialise the return value in case we can't read the registry
   try
      if RegQueryStringValue(HKEY_LOCAL_MACHINE_32, 'Software\ASCOM','PlatformVersion', PlatVerString) then 
      begin // Successfully read the value from the registry
         Result := StrToFloat(PlatVerString); // Create a double from the X.Y Platform version string
      end;
   except                                                                   
      ShowExceptionMessage;
      Result:= -1.0; // Indicate in the return value that an exception was generated
   end;
end;

//
// Before the installer UI appears, verify that the required ASCOM Platform version is installed.
//
function InitializeSetup(): Boolean;
var
   PlatformVersionNumber : double;
 begin
   Result := FALSE;  // Assume failure
   PlatformVersionNumber := PlatformVersion(); // Get the installed Platform version as a double
   If PlatformVersionNumber >= REQUIRED_PLATFORM_VERSION then	// Check whether we have the minimum required Platform or newer
      Result := TRUE
   else
      if PlatformVersionNumber = 0.0 then
         MsgBox('No ASCOM Platform is installed. Please install Platform ' + Format('%3.1f', [REQUIRED_PLATFORM_VERSION]) + ' or later from https://www.ascom-standards.org', mbCriticalError, MB_OK)
      else 
         MsgBox('ASCOM Platform ' + Format('%3.1f', [REQUIRED_PLATFORM_VERSION]) + ' or later is required, but Platform '+ Format('%3.1f', [PlatformVersionNumber]) + ' is installed. Please install the latest Platform before continuing; you will find it at https://www.ascom-standards.org', mbCriticalError, MB_OK);
end;

// Code to enable the installer to uninstall previous versions of itself when a new version is installed
procedure CurStepChanged(CurStep: TSetupStep);
var
  ResultCode: Integer;
  UninstallExe: String;
  UninstallRegistry: String;
begin
  if (CurStep = ssInstall) then // Install step has started
	begin
      // Create the correct registry location name, which is based on the AppId
      UninstallRegistry := ExpandConstant('Software\Microsoft\Windows\CurrentVersion\Uninstall\{#SetupSetting("AppId")}' + '_is1');
      // Check whether an extry exists
      if RegQueryStringValue(HKLM, UninstallRegistry, 'UninstallString', UninstallExe) then
        begin // Entry exists and previous version is installed so run its uninstaller quietly after informing the user
          MsgBox('Setup will now remove the previous version.', mbInformation, MB_OK);
          Exec(RemoveQuotes(UninstallExe), ' /SILENT', '', SW_SHOWNORMAL, ewWaitUntilTerminated, ResultCode);
          sleep(1000);    //Give enough time for the install screen to be repainted before continuing
        end
  end;
end;

%coms%//
%coms%// Register and unregister the driver with the Chooser
%coms%// We already know that the Helper is available
%coms%//
%coms%procedure RegASCOM();
%coms%var
%coms%   P: Variant;
%coms%begin
%coms%   P := CreateOleObject('ASCOM.Utilities.Profile');
%coms%   P.DeviceType := '%type%';
%coms%   P.Register('%name%.%type%', '%fnam%');
%cex2%   P.DeviceType := '%typ2%';
%cex2%   P.Register('%name%.%typ2%', '%fnam%');
%coms%end;
%coms%
%coms%procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
%coms%var
%coms%   P: Variant;
%coms%begin
%coms%   if CurUninstallStep = usUninstall then
%coms%   begin
%coms%     P := CreateOleObject('ASCOM.Utilities.Profile');
%coms%     P.DeviceType := '%type%';
%coms%     P.Unregister('%name%.%type%');
%cex2%     P.DeviceType := '%typ2%';
%cex2%     P.Unregister('%name%.%typ2%');
%coms%  end;
%coms%end;
