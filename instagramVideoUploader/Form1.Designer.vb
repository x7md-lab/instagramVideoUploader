<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.LoginBtn = New System.Windows.Forms.Button()
        Me.UserBox = New System.Windows.Forms.TextBox()
        Me.PassBox = New System.Windows.Forms.TextBox()
        Me.UserLb = New System.Windows.Forms.Label()
        Me.PassLb = New System.Windows.Forms.Label()
        Me.Statuslb = New System.Windows.Forms.Label()
        Me.togglePass = New System.Windows.Forms.CheckBox()
        Me.Filepicker = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Destb = New System.Windows.Forms.TextBox()
        Me.Deslb = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Uploadlb = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LoginBtn
        '
        Me.LoginBtn.Location = New System.Drawing.Point(122, 108)
        Me.LoginBtn.Name = "LoginBtn"
        Me.LoginBtn.Size = New System.Drawing.Size(72, 23)
        Me.LoginBtn.TabIndex = 2
        Me.LoginBtn.Text = "Login"
        Me.LoginBtn.UseVisualStyleBackColor = True
        '
        'UserBox
        '
        Me.UserBox.Location = New System.Drawing.Point(11, 25)
        Me.UserBox.Name = "UserBox"
        Me.UserBox.Size = New System.Drawing.Size(180, 20)
        Me.UserBox.TabIndex = 0
        '
        'PassBox
        '
        Me.PassBox.Location = New System.Drawing.Point(11, 68)
        Me.PassBox.Name = "PassBox"
        Me.PassBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.PassBox.Size = New System.Drawing.Size(180, 20)
        Me.PassBox.TabIndex = 1
        '
        'UserLb
        '
        Me.UserLb.AutoSize = True
        Me.UserLb.Location = New System.Drawing.Point(11, 7)
        Me.UserLb.Name = "UserLb"
        Me.UserLb.Size = New System.Drawing.Size(58, 13)
        Me.UserLb.TabIndex = 2
        Me.UserLb.Text = "Username:"
        '
        'PassLb
        '
        Me.PassLb.AutoSize = True
        Me.PassLb.Location = New System.Drawing.Point(11, 50)
        Me.PassLb.Name = "PassLb"
        Me.PassLb.Size = New System.Drawing.Size(56, 13)
        Me.PassLb.TabIndex = 2
        Me.PassLb.Text = "Password:"
        '
        'Statuslb
        '
        Me.Statuslb.AutoSize = True
        Me.Statuslb.Location = New System.Drawing.Point(7, 114)
        Me.Statuslb.Name = "Statuslb"
        Me.Statuslb.Size = New System.Drawing.Size(66, 13)
        Me.Statuslb.TabIndex = 3
        Me.Statuslb.Text = "Status: ide..."
        '
        'togglePass
        '
        Me.togglePass.AutoSize = True
        Me.togglePass.Location = New System.Drawing.Point(10, 94)
        Me.togglePass.Name = "togglePass"
        Me.togglePass.Size = New System.Drawing.Size(102, 17)
        Me.togglePass.TabIndex = 4
        Me.togglePass.Text = "Show Password"
        Me.togglePass.UseVisualStyleBackColor = True
        '
        'Filepicker
        '
        Me.Filepicker.Location = New System.Drawing.Point(13, 137)
        Me.Filepicker.Name = "Filepicker"
        Me.Filepicker.Size = New System.Drawing.Size(177, 23)
        Me.Filepicker.TabIndex = 5
        Me.Filepicker.Text = "Choose a file"
        Me.Filepicker.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(15, 289)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(176, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "share it"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(14, 225)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(176, 23)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "Video thumbnail"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Destb
        '
        Me.Destb.Location = New System.Drawing.Point(14, 182)
        Me.Destb.Multiline = True
        Me.Destb.Name = "Destb"
        Me.Destb.Size = New System.Drawing.Size(176, 37)
        Me.Destb.TabIndex = 8
        '
        'Deslb
        '
        Me.Deslb.AutoSize = True
        Me.Deslb.Location = New System.Drawing.Point(13, 164)
        Me.Deslb.Name = "Deslb"
        Me.Deslb.Size = New System.Drawing.Size(63, 13)
        Me.Deslb.TabIndex = 9
        Me.Deslb.Text = "Description:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 255)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(167, 26)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Aspect Ratio of picture should be " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "the same of video"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Uploadlb
        '
        Me.Uploadlb.AutoSize = True
        Me.Uploadlb.Location = New System.Drawing.Point(13, 329)
        Me.Uploadlb.Name = "Uploadlb"
        Me.Uploadlb.Size = New System.Drawing.Size(94, 13)
        Me.Uploadlb.TabIndex = 11
        Me.Uploadlb.Text = "Upload Status: ide"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(204, 356)
        Me.Controls.Add(Me.Uploadlb)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Deslb)
        Me.Controls.Add(Me.Destb)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Filepicker)
        Me.Controls.Add(Me.togglePass)
        Me.Controls.Add(Me.Statuslb)
        Me.Controls.Add(Me.PassLb)
        Me.Controls.Add(Me.UserLb)
        Me.Controls.Add(Me.PassBox)
        Me.Controls.Add(Me.UserBox)
        Me.Controls.Add(Me.LoginBtn)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(220, 395)
        Me.MinimumSize = New System.Drawing.Size(220, 395)
        Me.Name = "Form1"
        Me.Text = "mmti"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LoginBtn As Button
    Friend WithEvents UserBox As TextBox
    Friend WithEvents PassBox As TextBox
    Friend WithEvents UserLb As Label
    Friend WithEvents PassLb As Label
    Friend WithEvents Statuslb As Label
    Friend WithEvents togglePass As CheckBox
    Friend WithEvents Filepicker As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Destb As TextBox
    Friend WithEvents Deslb As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Uploadlb As Label
End Class
