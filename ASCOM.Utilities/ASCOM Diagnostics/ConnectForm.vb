﻿Imports System.Runtime.InteropServices
Public Class ConnectForm

    Private Const DEFAULT_DEVICE_TYPE As String = "Telescope"
    Private Const DEFAULT_DEVICE As String = "ScopeSim.Telescope"

    Private CurrentDevice, CurrentDeviceType As String, Connected As Boolean, Device As Object, Util As ASCOM.Utilities.Util

    'API's for auto drop down combo
    Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long
    Private Const CB_SHOWDROPDOWN = &H14F

    Private Sub ConnectForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim DeviceTypes As ArrayList, Profile As New ASCOM.Utilities.Profile

        AddHandler cmbDeviceType.SelectedIndexChanged, AddressOf DevicetypeChangedhandler
        Try
            Util = New ASCOM.Utilities.Util
            DeviceTypes = Profile.RegisteredDeviceTypes
            For Each DeviceType As String In DeviceTypes
                cmbDeviceType.Items.Add(DeviceType)
            Next
            CurrentDevice = DEFAULT_DEVICE
            CurrentDeviceType = DEFAULT_DEVICE_TYPE
            cmbDeviceType.SelectedItem = CurrentDeviceType
            btnProperties.Enabled = False
            txtDevice.Text = CurrentDevice
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub DevicetypeChangedhandler()
        CurrentDeviceType = cmbDeviceType.SelectedItem.ToString
        CurrentDevice = ""
        txtDevice.Text = ""
        SetScriptButton()
        btnConnect.Enabled = False
        btnProperties.Enabled = False
        btnScript.Enabled = False
        btnGetProfile.Enabled = False
    End Sub

    Sub SetScriptButton()
        If (CurrentDeviceType = "Telescope") And (CurrentDevice <> "") Then 'Enable or disable the run script button as appropriate
            btnScript.Enabled = True
        Else
            btnScript.Enabled = False
        End If
    End Sub

    Private Sub btnChoose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChoose.Click
        Dim Chooser As ASCOM.Utilities.Chooser, NewDevice As String

        CurrentDeviceType = cmbDeviceType.SelectedItem
        Chooser = New ASCOM.Utilities.Chooser
        Chooser.DeviceType = CurrentDeviceType
        If CurrentDevice = "" Then
            Select Case CurrentDeviceType
                Case "Telescope"
                    CurrentDevice = "ScopeSim.Telescope"
                Case "Focuser"
                    CurrentDevice = "ASCOM.Simulator.Focuser"

            End Select
        End If
        NewDevice = Chooser.Choose(CurrentDevice)
        If NewDevice <> "" Then CurrentDevice = NewDevice

        If CurrentDevice <> "" Then
            btnProperties.Enabled = True
            btnConnect.Enabled = True
            btnGetProfile.Enabled = True
        Else
            btnProperties.Enabled = False
            btnConnect.Enabled = False
            btnGetProfile.Enabled = False
        End If

        txtDevice.Text = CurrentDevice
        SetScriptButton()
        Chooser.Dispose()

    End Sub

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        Dim TypeCurrentDevice As Type

        If CurrentDevice <> "" Then
            If Connected Then ' Disconnect
                Try
                    txtStatus.Text = "Disconnecting..."
                    Application.DoEvents()
                    Select Case CurrentDeviceType
                        Case "Focuser"
                            Device.Link = False
                        Case Else
                            Device.Connected = False
                    End Select
                    Connected = False
                    txtStatus.Text = "Disconnected OK"
                    btnConnect.Text = "Connect"
                    btnChoose.Enabled = True
                    If CurrentDevice <> "" Then btnProperties.Enabled = True
                    SetScriptButton() 'Enable or disable script button according todevice type
                    cmbDeviceType.Enabled = True
                Catch ex As Exception
                    txtStatus.Text = "Disconnect Failed..." & ex.Message & vbCrLf & vbCrLf & ex.ToString
                Finally
                    Try : Device.Dispose() : Catch : End Try
                    Try : Marshal.ReleaseComObject(Device) : Catch : End Try
                    Device = Nothing
                End Try

            Else 'Disconnected so connect
                Try
                    txtStatus.Text = "Connecting..."
                    Application.DoEvents()
                    'Device = CreateObject(CurrentDevice)
                    TypeCurrentDevice = Type.GetTypeFromProgID(CurrentDevice) 'Try Activator approach as this may give more meaningful eror messages
                    Device = Activator.CreateInstance(TypeCurrentDevice)
                    Select Case CurrentDeviceType
                        Case "Focuser"
                            Device.Link = True
                        Case Else
                            Device.Connected = True
                    End Select
                    Connected = True
                    txtStatus.Text = "Connected OK"
                    btnConnect.Text = "Disconnect"
                    btnChoose.Enabled = False
                    btnProperties.Enabled = False
                    btnScript.Enabled = False
                    cmbDeviceType.Enabled = False
                Catch ex As Exception
                    txtStatus.Text = "Connect Failed..." & ex.Message & vbCrLf & vbCrLf & ex.ToString
                    Try : Device.Dispose() : Catch dex As Exception
                        txtStatus.AppendText(vbCrLf & "Dispose Failed..." & dex.Message & vbCrLf & vbCrLf & dex.ToString)
                    End Try
                    Try : Marshal.ReleaseComObject(Device) : Catch rex As Exception
                        txtStatus.AppendText(vbCrLf & "Release Failed..." & rex.Message & vbCrLf & vbCrLf & rex.ToString)
                    End Try
                End Try
            End If
        Else
            txtStatus.Text = "Cannot connect, no device has been set"
        End If
    End Sub

    Private Sub btnProperties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProperties.Click
        Dim DeviceType As Type

        'Device = CreateObject(CurrentDevice)
        DeviceType = Type.GetTypeFromProgID(CurrentDevice)
        Device = Activator.CreateInstance(DeviceType)
        Device.SetupDialog()
        Try : Marshal.ReleaseComObject(Device) : Catch : End Try
        Device = Nothing
    End Sub

    Private TL As TraceLogger, DeviceObject As Object

    Private Sub btnScript_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScript.Click
        txtStatus.Clear()
        TL = New TraceLogger("", "DiagnosticScript")
        TL.Enabled = True
        LogMsg("Script", "Diagnostic Script Started")
        ExecuteCommand("CreateObject")
        ExecuteCommand("Connect")
        ExecuteCommand("Description")
        ExecuteCommand("DriverInfo")
        ExecuteCommand("DriverVersion")
        For i As Integer = 1 To 3
            ExecuteCommand("RightAscension")
            ExecuteCommand("Declination")
        Next
        ExecuteCommand("Disconnect")
        ExecuteCommand("DestroyObject")
        LogMsg("Script", "Diagnostic Script Completed")
        TL.Enabled = False
        TL.Dispose()
    End Sub

    Sub ExecuteCommand(ByVal Command As String)
        Dim sw As New Stopwatch, StartTime As Date, Result As String = ""
        Dim DeviceType As Type

        Try
            StartTime = Now
            sw.Start()
            LogMsg(Command, "Started")

            Select Case Command
                Case "CreateObject"
                    'DeviceObject = CreateObject(CurrentDevice)
                    DeviceType = Type.GetTypeFromProgID(CurrentDevice)
                    DeviceObject = Activator.CreateInstance(DeviceType)
                Case "Connect"
                    DeviceObject.Connected = True
                Case "DriverInfo"
                    Result = DeviceObject.DriverInfo
                Case "Description"
                    Result = DeviceObject.Description
                Case "DriverVersion"
                    Result = DeviceObject.DriverVersion
                Case "RightAscension"
                    Result = Util.DegreesToHMS(DeviceObject.RightAscension)
                Case "Declination"
                    Result = Util.DegreesToDMS(DeviceObject.Declination, ":", ":", "")
                Case "Disconnect"
                    DeviceObject.Connected = False
                Case "DestroyObject"
                    Marshal.ReleaseComObject(DeviceObject)
                    DeviceObject = Nothing
                Case Else
                    LogMsg(Command, "***** Unknown command *****")
            End Select
            sw.Stop()

            LogMsg(Command, "Finished - Started: " & Format(StartTime, "HH:mm:ss.fff") & " duration: " & sw.ElapsedMilliseconds & " ms - " & Result)
        Catch ex As Exception
            LogMsg(Command, "Exception - Started: " & Format(StartTime, "HH:mm:ss.fff") & " duration: " & sw.ElapsedMilliseconds & " ms - Exception: " & ex.ToString)
        End Try
    End Sub
    Sub LogMsg(ByVal Command As String, ByVal Msg As String)
        TL.LogMessage(Command, Msg)
        txtStatus.Text = txtStatus.Text & Command & " " & Msg & vbCrLf
    End Sub

    Private Sub btnGetProfile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetProfile.Click
        Dim Prof As New Profile, Result As String = ""
        txtStatus.Clear()
        TL = New TraceLogger("", "DiagnosticScript")
        TL.Enabled = True
        Prof.DeviceType = CurrentDeviceType
        Result = Prof.GetProfileXML(txtDevice.Text)
        LogMsg("GetProfile", Result)
        LogMsg("Script", "Diagnostic Script Completed")
        TL.Enabled = False
        TL.Dispose()

    End Sub


    Sub cmbDeviceType_Click() Handles cmbDeviceType.MouseClick
        'If Not cmbDeviceType.DroppedDown Then cmbDeviceType.
        SendMessage(cmbDeviceType.Handle, CB_SHOWDROPDOWN, 1, 0)
        cmbDeviceType.Cursor = Cursors.Arrow
    End Sub

End Class
