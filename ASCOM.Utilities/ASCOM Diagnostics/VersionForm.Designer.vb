﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VersionForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VersionForm))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblPlatformName = New System.Windows.Forms.Label()
        Me.lblPlatformVersion = New System.Windows.Forms.Label()
        Me.NameLbl = New System.Windows.Forms.Label()
        Me.Version = New System.Windows.Forms.Label()
        Me.LblBuildSha = New System.Windows.Forms.Label()
        Me.BuildSha = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.InitialImage = CType(resources.GetObject("PictureBox1.InitialImage"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(87, 88)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'lblPlatformName
        '
        Me.lblPlatformName.AutoSize = True
        Me.lblPlatformName.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlatformName.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblPlatformName.Location = New System.Drawing.Point(137, 117)
        Me.lblPlatformName.Name = "lblPlatformName"
        Me.lblPlatformName.Size = New System.Drawing.Size(116, 18)
        Me.lblPlatformName.TabIndex = 1
        Me.lblPlatformName.Text = "Platform Name:"
        '
        'lblPlatformVersion
        '
        Me.lblPlatformVersion.AutoSize = True
        Me.lblPlatformVersion.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlatformVersion.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblPlatformVersion.Location = New System.Drawing.Point(125, 154)
        Me.lblPlatformVersion.Name = "lblPlatformVersion"
        Me.lblPlatformVersion.Size = New System.Drawing.Size(127, 18)
        Me.lblPlatformVersion.TabIndex = 3
        Me.lblPlatformVersion.Text = "Platform Version:"
        '
        'NameLbl
        '
        Me.NameLbl.AutoSize = True
        Me.NameLbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NameLbl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.NameLbl.Location = New System.Drawing.Point(257, 117)
        Me.NameLbl.Name = "NameLbl"
        Me.NameLbl.Size = New System.Drawing.Size(53, 19)
        Me.NameLbl.TabIndex = 4
        Me.NameLbl.Text = "Name"
        '
        'Version
        '
        Me.Version.AutoSize = True
        Me.Version.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Version.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Version.Location = New System.Drawing.Point(257, 154)
        Me.Version.Name = "Version"
        Me.Version.Size = New System.Drawing.Size(67, 19)
        Me.Version.TabIndex = 5
        Me.Version.Text = "Version"
        '
        'LblBuildSha
        '
        Me.LblBuildSha.AutoSize = True
        Me.LblBuildSha.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBuildSha.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.LblBuildSha.Location = New System.Drawing.Point(33, 267)
        Me.LblBuildSha.Name = "LblBuildSha"
        Me.LblBuildSha.Size = New System.Drawing.Size(84, 18)
        Me.LblBuildSha.TabIndex = 6
        Me.LblBuildSha.Text = "Build SHA:"
        '
        'BuildSha
        '
        Me.BuildSha.AutoSize = True
        Me.BuildSha.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BuildSha.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BuildSha.Location = New System.Drawing.Point(123, 267)
        Me.BuildSha.Name = "BuildSha"
        Me.BuildSha.Size = New System.Drawing.Size(357, 19)
        Me.BuildSha.TabIndex = 7
        Me.BuildSha.Text = "a6231f4c20c7a241acf288d1655c65bf7adcaabf"
        '
        'VersionForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(520, 295)
        Me.Controls.Add(Me.BuildSha)
        Me.Controls.Add(Me.LblBuildSha)
        Me.Controls.Add(Me.Version)
        Me.Controls.Add(Me.NameLbl)
        Me.Controls.Add(Me.lblPlatformVersion)
        Me.Controls.Add(Me.lblPlatformName)
        Me.Controls.Add(Me.PictureBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "VersionForm"
        Me.Text = "ASCOM Platfom Version"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblPlatformName As System.Windows.Forms.Label
    Friend WithEvents lblPlatformVersion As System.Windows.Forms.Label
    Friend WithEvents NameLbl As System.Windows.Forms.Label
    Friend WithEvents Version As System.Windows.Forms.Label
    Friend WithEvents LblBuildSha As Label
    Friend WithEvents BuildSha As Label
End Class
