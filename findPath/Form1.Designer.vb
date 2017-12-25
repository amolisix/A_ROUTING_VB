<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.tStp1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel
        Me.sptxtSize = New System.Windows.Forms.ToolStripTextBox
        Me.spbtnMadeMap = New System.Windows.Forms.ToolStripButton
        Me.spbtnFindPath = New System.Windows.Forms.ToolStripButton
        Me.spbtnClearPath = New System.Windows.Forms.ToolStripButton
        Me.pn1 = New System.Windows.Forms.Panel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rb_End = New System.Windows.Forms.RadioButton
        Me.rb_Start = New System.Windows.Forms.RadioButton
        Me.rb_UnReach = New System.Windows.Forms.RadioButton
        Me.rb_Reach = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chk1 = New System.Windows.Forms.CheckBox
        Me.tStp1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'tStp1
        '
        Me.tStp1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.sptxtSize, Me.spbtnMadeMap, Me.spbtnFindPath, Me.spbtnClearPath})
        Me.tStp1.Location = New System.Drawing.Point(0, 0)
        Me.tStp1.Name = "tStp1"
        Me.tStp1.Size = New System.Drawing.Size(537, 25)
        Me.tStp1.TabIndex = 0
        Me.tStp1.Text = "ToolStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(53, 22)
        Me.ToolStripLabel1.Text = "地图大小"
        '
        'sptxtSize
        '
        Me.sptxtSize.Name = "sptxtSize"
        Me.sptxtSize.Size = New System.Drawing.Size(50, 25)
        Me.sptxtSize.Text = "10,10"
        '
        'spbtnMadeMap
        '
        Me.spbtnMadeMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.spbtnMadeMap.Image = CType(resources.GetObject("spbtnMadeMap.Image"), System.Drawing.Image)
        Me.spbtnMadeMap.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.spbtnMadeMap.Name = "spbtnMadeMap"
        Me.spbtnMadeMap.Size = New System.Drawing.Size(57, 22)
        Me.spbtnMadeMap.Text = "生成地图"
        '
        'spbtnFindPath
        '
        Me.spbtnFindPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.spbtnFindPath.Image = CType(resources.GetObject("spbtnFindPath.Image"), System.Drawing.Image)
        Me.spbtnFindPath.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.spbtnFindPath.Name = "spbtnFindPath"
        Me.spbtnFindPath.Size = New System.Drawing.Size(33, 22)
        Me.spbtnFindPath.Text = "寻路"
        '
        'spbtnClearPath
        '
        Me.spbtnClearPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.spbtnClearPath.Image = CType(resources.GetObject("spbtnClearPath.Image"), System.Drawing.Image)
        Me.spbtnClearPath.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.spbtnClearPath.Name = "spbtnClearPath"
        Me.spbtnClearPath.Size = New System.Drawing.Size(57, 22)
        Me.spbtnClearPath.Text = "清除路径"
        '
        'pn1
        '
        Me.pn1.BackColor = System.Drawing.Color.White
        Me.pn1.Location = New System.Drawing.Point(12, 40)
        Me.pn1.Name = "pn1"
        Me.pn1.Size = New System.Drawing.Size(356, 356)
        Me.pn1.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rb_End)
        Me.GroupBox1.Controls.Add(Me.rb_Start)
        Me.GroupBox1.Controls.Add(Me.rb_UnReach)
        Me.GroupBox1.Controls.Add(Me.rb_Reach)
        Me.GroupBox1.Location = New System.Drawing.Point(390, 48)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(113, 165)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "地形"
        '
        'rb_End
        '
        Me.rb_End.Appearance = System.Windows.Forms.Appearance.Button
        Me.rb_End.AutoSize = True
        Me.rb_End.BackColor = System.Drawing.Color.Gold
        Me.rb_End.Location = New System.Drawing.Point(16, 135)
        Me.rb_End.Name = "rb_End"
        Me.rb_End.Size = New System.Drawing.Size(51, 22)
        Me.rb_End.TabIndex = 3
        Me.rb_End.Text = "终止点"
        Me.rb_End.UseVisualStyleBackColor = False
        '
        'rb_Start
        '
        Me.rb_Start.Appearance = System.Windows.Forms.Appearance.Button
        Me.rb_Start.AutoSize = True
        Me.rb_Start.BackColor = System.Drawing.Color.Red
        Me.rb_Start.Location = New System.Drawing.Point(16, 107)
        Me.rb_Start.Name = "rb_Start"
        Me.rb_Start.Size = New System.Drawing.Size(51, 22)
        Me.rb_Start.TabIndex = 2
        Me.rb_Start.Text = "起始点"
        Me.rb_Start.UseVisualStyleBackColor = False
        '
        'rb_UnReach
        '
        Me.rb_UnReach.Appearance = System.Windows.Forms.Appearance.Button
        Me.rb_UnReach.BackColor = System.Drawing.Color.Blue
        Me.rb_UnReach.Location = New System.Drawing.Point(16, 58)
        Me.rb_UnReach.Name = "rb_UnReach"
        Me.rb_UnReach.Size = New System.Drawing.Size(75, 24)
        Me.rb_UnReach.TabIndex = 1
        Me.rb_UnReach.Text = "不可到达区"
        Me.rb_UnReach.UseVisualStyleBackColor = False
        '
        'rb_Reach
        '
        Me.rb_Reach.Appearance = System.Windows.Forms.Appearance.Button
        Me.rb_Reach.BackColor = System.Drawing.Color.GreenYellow
        Me.rb_Reach.Checked = True
        Me.rb_Reach.Location = New System.Drawing.Point(16, 30)
        Me.rb_Reach.Name = "rb_Reach"
        Me.rb_Reach.Size = New System.Drawing.Size(75, 24)
        Me.rb_Reach.TabIndex = 0
        Me.rb_Reach.TabStop = True
        Me.rb_Reach.Text = "可到达区"
        Me.rb_Reach.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chk1)
        Me.GroupBox2.Location = New System.Drawing.Point(390, 242)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(113, 50)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "规则"
        '
        'chk1
        '
        Me.chk1.AutoSize = True
        Me.chk1.Checked = True
        Me.chk1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk1.Location = New System.Drawing.Point(16, 20)
        Me.chk1.Name = "chk1"
        Me.chk1.Size = New System.Drawing.Size(84, 16)
        Me.chk1.TabIndex = 0
        Me.chk1.Text = "允许走斜线"
        Me.chk1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(537, 421)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.tStp1)
        Me.Controls.Add(Me.pn1)
        Me.Name = "Form1"
        Me.Text = "A*算法"
        Me.tStp1.ResumeLayout(False)
        Me.tStp1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tStp1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents sptxtSize As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents spbtnMadeMap As System.Windows.Forms.ToolStripButton
    Friend WithEvents spbtnFindPath As System.Windows.Forms.ToolStripButton
    Friend WithEvents pn1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_End As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Start As System.Windows.Forms.RadioButton
    Friend WithEvents rb_UnReach As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Reach As System.Windows.Forms.RadioButton
    Friend WithEvents spbtnClearPath As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chk1 As System.Windows.Forms.CheckBox

End Class
