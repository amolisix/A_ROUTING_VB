Public Class Form1
    Const maxSizeX As Integer = 50   '地图最大尺寸
    Const maxSizeY As Integer = 50

    Private intSizeX As Integer = 10 '地图当前尺寸
    Private intSizeY As Integer = 10

    Private myPen As New Pen(Color.Gray)
    Private myBrush As New SolidBrush(Color.White)

    Private unit(intSizeX - 1, intSizeY - 1) As _unit   '地图数据结构
    Private unit_start As _unit            '起始点
    Private unit_end As _unit          '终止点


    Private m_down As Boolean = False  '鼠标是否按下

    Private arrOpen As New ArrayList '开启列表　　元素为　_unit
    Private arrClose As New ArrayList '关闭列表   元素为　_unit
    Private arrDraw As New ArrayList '最后生成的路径列表  元素为 point

    Private posCurr As _unit '当前格
    Private boolFindPath As Boolean = False '是否已经找到终点
    Private step_G As Integer 'G 数值　走直线为10,走斜线为14

    Dim mySort As New myReverserClass '_unit 排序规则



    Public Class myReverserClass  '_unit 排序规则，根据F值来排
        Implements IComparer
        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
           Implements IComparer.Compare

            If CType(x, _unit).a_F = CType(y, _unit).a_F Then
                Return 0
            ElseIf CType(x, _unit).a_F > CType(y, _unit).a_F Then
                Return 1
            Else
                Return -1
            End If
        End Function 'IComparer.Compare

    End Class


    Private Function checkSize() As Boolean  '检查输入的地图数值是否合法
        Dim arrStr() As String

        arrStr = sptxtSize.Text.Split(","c)
        If arrStr.Length <> 2 Then
            Return False
        Else
            Try
                If CInt(arrStr(0)) > 2 And CInt(arrStr(0)) <= maxSizeX And CInt(arrStr(1)) > 2 And CInt(arrStr(1)) <= maxSizeY Then
                    intSizeX = CInt(arrStr(0))
                    intSizeY = CInt(arrStr(1))
                    Return True
                End If

            Catch ex As Exception

            End Try

            Return False
        End If

    End Function


    Private Sub spbtnMadeMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles spbtnMadeMap.Click
        If Not checkSize() Then
            MessageBox.Show("地图大小参数错误！")
            Exit Sub
        End If

        madeMap()

        pn1.Refresh()
    End Sub

    Private Sub madeMap() '生成地图
        arrDraw.Clear()
        ReDim unit(intSizeX - 1, intSizeY - 1)

        For i As Integer = 0 To intSizeX - 1
            For j As Integer = 0 To intSizeY - 1
                unit(i, j).a_F = 0
                unit(i, j).a_G = 0
                unit(i, j).a_H = 0
                unit(i, j).pos = New Point(i, j)
                unit(i, j).sty = _sty.sty_Reach
            Next
        Next

    End Sub

    Private Sub DrawGrid(ByVal g As Graphics) '画网格
        Dim douStepX As Double = pn1.Width / intSizeX
        Dim douStepY As Double = pn1.Height / intSizeY
        myPen.Color = Color.Gray

        For i As Integer = 0 To intSizeX - 1
            g.DrawLine(myPen, CInt(i * douStepX), 0, CInt(i * douStepX), pn1.Height)
        Next
        g.DrawLine(myPen, pn1.Width, 0, pn1.Width, pn1.Height)


        For j As Integer = 0 To intSizeY - 1
            g.DrawLine(myPen, 0, CInt(j * douStepY), pn1.Width, CInt(j * douStepY))
        Next
        g.DrawLine(myPen, 0, pn1.Height, pn1.Width, pn1.Height)
    End Sub

    Private Sub DrawUnit(ByVal g As Graphics) '画所有方格内地形
        For i As Integer = 0 To intSizeX - 1
            For j As Integer = 0 To intSizeY - 1
                DrawOneUnit(g, unit(i, j))
            Next
        Next
    End Sub

    Private Sub DrawOneUnit(ByVal g As Graphics, ByVal one_unit As _unit) '画一个方格内地形
        Select Case one_unit.sty
            Case _sty.sty_Reach
                myBrush.Color = Color.GreenYellow
            Case _sty.sty_unReach
                myBrush.Color = Color.Blue
            Case _sty.sty_Start
                myBrush.Color = Color.Red
            Case _sty.syt_End
                myBrush.Color = Color.Gold
        End Select
        g.FillRectangle(myBrush, getUnitRect(one_unit.pos))
    End Sub



    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        myPen.Width = 2 '方格边框（网格边框线）宽度
        madeMap()
    End Sub

    Private Sub pn1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pn1.MouseDown
        Dim g As Graphics = pn1.CreateGraphics '用鼠标绘制地形

        Dim tempUnit As _unit
        tempUnit.pos = getUnit(New Point(e.X, e.Y))

        If e.Button = Windows.Forms.MouseButtons.Left Then
            m_down = True
            If rb_Reach.Checked Or rb_UnReach.Checked Then
                If rb_Reach.Checked Then
                    tempUnit.sty = _sty.sty_Reach
                Else
                    tempUnit.sty = _sty.sty_unReach
                End If
            Else
                If rb_Start.Checked Then
                    If unit(unit_start.pos.X, unit_start.pos.Y).sty = _sty.sty_Start Then
                        unit(unit_start.pos.X, unit_start.pos.Y).sty = _sty.sty_Reach
                        DrawOneUnit(g, unit(unit_start.pos.X, unit_start.pos.Y))
                    End If

                    tempUnit.sty = _sty.sty_Start
                    unit_start.pos = tempUnit.pos
                Else
                    If unit(unit_end.pos.X, unit_end.pos.Y).sty = _sty.syt_End Then
                        unit(unit_end.pos.X, unit_end.pos.Y).sty = _sty.sty_Reach
                        DrawOneUnit(g, unit(unit_end.pos.X, unit_end.pos.Y))
                    End If
                    tempUnit.sty = _sty.syt_End
                    unit_end.pos = tempUnit.pos
                End If
            End If

            unit(tempUnit.pos.X, tempUnit.pos.Y).sty = tempUnit.sty


            DrawOneUnit(g, tempUnit)
            DrawGrid(g)
            g.Dispose()

        End If
    End Sub

    Private Sub pn1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pn1.MouseMove
        If m_down Then '用鼠标绘制地形
            If e.X >= pn1.Width Or e.Y >= pn1.Height Or e.X <= 0 Or e.Y <= 0 Then
                Exit Sub
            End If
            If e.Button = Windows.Forms.MouseButtons.Left Then
                If rb_Reach.Checked Or rb_UnReach.Checked Then
                    Dim tempUnit As _unit
                    tempUnit.pos = getUnit(New Point(e.X, e.Y))
                    If rb_Reach.Checked Then
                        tempUnit.sty = _sty.sty_Reach
                    Else
                        tempUnit.sty = _sty.sty_unReach
                    End If
                    unit(tempUnit.pos.X, tempUnit.pos.Y).sty = tempUnit.sty

                    Dim g As Graphics = pn1.CreateGraphics
                    DrawOneUnit(g, tempUnit)
                    DrawGrid(g)
                    g.Dispose()
                End If
            End If
        End If
    End Sub

    Private Sub pn1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pn1.MouseUp
        m_down = False


    End Sub

    Private Sub pn1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pn1.Paint
        Dim g As Graphics = e.Graphics

        DrawUnit(g)
        DrawGrid(g)

        drawPath(g)
    End Sub

    Private Function getUnitRect(ByVal pt As Point) As Rectangle '根据方格坐标得到在pn1上的区域
        Dim rect As Rectangle
        Dim douStepX As Double = pn1.Width / intSizeX
        Dim douStepY As Double = pn1.Height / intSizeY

        rect.X = CInt(pt.X * douStepX)
        rect.Y = CInt(pt.Y * douStepY)
        rect.Width = CInt(douStepX)
        rect.Height = CInt(douStepY)

        Return rect
    End Function

    Private Function getUnitCenPos(ByVal pt As Point) As Point '根据方格坐标得到此方格在pn1上的中心点坐标
        Dim rePoint As Point
        Dim douStepX As Double = pn1.Width / intSizeX
        Dim douStepY As Double = pn1.Height / intSizeY
        rePoint.X = CInt(pt.X * douStepX + douStepX / 2)
        rePoint.Y = CInt(pt.Y * douStepY + douStepY / 2)

        Return rePoint
    End Function

    Private Function getUnit(ByVal pt As Point) As Point '根据鼠标点击的位置，计算此方格坐标
        Dim douStepX As Double = pn1.Width / intSizeX
        Dim douStepY As Double = pn1.Height / intSizeY

        Return New Point(CInt(Int(pt.X / douStepX)), CInt(Int(pt.Y / douStepY)))
    End Function


    Private Sub spbtnFindPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles spbtnFindPath.Click
        If unit(unit_start.pos.X, unit_start.pos.Y).sty <> _sty.sty_Start Then
            MessageBox.Show("请定义起始点")
            Exit Sub
        End If
        If unit(unit_end.pos.X, unit_end.pos.Y).sty <> _sty.syt_End Then
            MessageBox.Show("请定义终止点")
            Exit Sub
        End If



        fPath()


    End Sub


    Private Sub fPath() '寻路

        '清空所有需要用到的列表
        arrOpen.Clear()
        arrClose.Clear()
        arrDraw.Clear()

        boolFindPath = False

        arrOpen.Add(unit_start) '加入第一个起始点
        startFindPath()


    End Sub

    Private Sub startFindPath() '开始寻路

        Do While arrOpen.Count > 0 '如果open表未空，继续寻路
            posCurr = CType(arrOpen(0), _unit) '将open列表中最前面的元素设为当前格


            arrOpen.RemoveAt(0) '从open列表中移去当前格，并放入close列表
            arrClose.Add(posCurr)


            AddToOpen() '将当前格边上的格子放入open表(不可通过，或已经在open表和close表的例外)

            If boolFindPath Then '如已经找到路径
                ' MessageBox.Show("find")
                AddToDraw()
                Dim g As Graphics = pn1.CreateGraphics
                drawPath(g)
                g.Dispose()
                Exit Sub
            End If

            arrOpen.Sort(mySort) '重新排列open表，F值最低的排到最前面去
        Loop
        MessageBox.Show("no path") 'open表已空，无路径　：（
    End Sub

    Private Sub AddToDraw() '把得到的路径坐标，保存起来

        Dim tempPoint As Point = unit_end.pos

        Do
            arrDraw.Add(tempPoint)
            tempPoint = unit(tempPoint.X, tempPoint.Y).parent_pos

        Loop Until tempPoint = unit_start.pos

    End Sub

    Private Sub drawPath(ByVal g As Graphics) '绘制路径
        Dim showPoint As Point
        Dim r_x As Integer = CInt(pn1.Width / intSizeX / 4)
        Dim r_y As Integer = CInt(pn1.Height / intSizeY / 4)

        For Each tempPoint As Point In arrDraw
            showPoint = getUnitCenPos(tempPoint)
            g.FillEllipse(Brushes.Fuchsia, New Rectangle(showPoint.X - r_x, showPoint.Y - r_y, r_x * 2, r_y * 2))
        Next
    End Sub

    Private Sub AddToOpen() '将当前格旁边的格子放入open表(不可通过，或已经在open表和close表的例外)
        Dim tempPoint As Point
        Dim boolUp As Boolean = False '当前格上方可否通过
        Dim boolDown As Boolean = False '下方可否通过
        Dim boolLeft As Boolean = False '左方可否通过
        Dim boolRight As Boolean = False '右方可否通过

        step_G = 10 '上下左右为直线，通过消耗10
        tempPoint = getUpPos(posCurr.pos)
        If tempPoint <> New Point(-1, -1) Then
            boolUp = AddOnePos(tempPoint)
        End If

        tempPoint = getDownPos(posCurr.pos)
        If tempPoint <> New Point(-1, -1) Then
            boolDown = AddOnePos(tempPoint)
        End If

        tempPoint = getLeftPos(posCurr.pos)
        If tempPoint <> New Point(-1, -1) Then
            boolLeft = AddOnePos(tempPoint)
        End If

        tempPoint = getRightPos(posCurr.pos)
        If tempPoint <> New Point(-1, -1) Then
            boolRight = AddOnePos(tempPoint)
        End If

        If chk1.Checked Then
            step_G = 14 '斜线通过消耗14
            If boolUp And boolRight Then
                tempPoint = getUpRightPos(posCurr.pos)
                AddOnePos(tempPoint)
            End If

            If boolDown And boolRight Then
                tempPoint = getDownRightPos(posCurr.pos)
                AddOnePos(tempPoint)
            End If

            If boolDown And boolLeft Then
                tempPoint = getDownLeftPos(posCurr.pos)
                AddOnePos(tempPoint)
            End If

            If boolUp And boolLeft Then
                tempPoint = getUpLeftPos(posCurr.pos)
                AddOnePos(tempPoint)
            End If
        End If
    End Sub

    Private Function AddOnePos(ByVal tempPoint As Point) As Boolean '将此方格加入open表？
        If unit(tempPoint.X, tempPoint.Y).sty = _sty.syt_End Then '如此方格为终点
            boolFindPath = True '找到路径了！
            unit(unit_end.pos.X, unit_end.pos.Y).parent_pos = posCurr.pos '当前格坐标为终点的父方格坐标
            Return False '工作完成，撤退 :)
        End If

        If unit(tempPoint.X, tempPoint.Y).sty = _sty.sty_unReach Then
            Return False '此路不通
        End If

        If Not IsInOpenOrClose(tempPoint) Then '如果此方格不在open表或close表
            unit(tempPoint.X, tempPoint.Y).parent_pos = posCurr.pos '当前格为此格的父方格
            unit(tempPoint.X, tempPoint.Y).a_G = posCurr.a_G + step_G '计算G值
            unit(tempPoint.X, tempPoint.Y).a_H = (Math.Abs(unit_end.pos.X - tempPoint.X) + Math.Abs(unit_end.pos.Y - tempPoint.Y)) * 10 '计算H值
            unit(tempPoint.X, tempPoint.Y).a_F = unit(tempPoint.X, tempPoint.Y).a_G + unit(tempPoint.X, tempPoint.Y).a_H '计算F值

            arrOpen.Add(unit(tempPoint.X, tempPoint.Y)) '将此方格加入open表
        End If

        Return True '此路可通过
    End Function

    Private Function IsInOpenOrClose(ByVal tempPoint As Point) As Boolean '此方格是否已经在 open表或close表中
        For Each po As _unit In arrOpen
            If tempPoint = po.pos Then '此方格已在open表中
                If unit(tempPoint.X, tempPoint.Y).a_G > posCurr.a_G + step_G Then  '比较当前路径算出的G值和原来保存的G值，如当前更低折替换，并设置当前格为此格的父方格
                    unit(tempPoint.X, tempPoint.Y).parent_pos = posCurr.pos
                    unit(tempPoint.X, tempPoint.Y).a_G = posCurr.a_G + step_G
                    unit(tempPoint.X, tempPoint.Y).a_F = unit(tempPoint.X, tempPoint.Y).a_G + unit(tempPoint.X, tempPoint.Y).a_H
                End If
                Return True
            End If
        Next

        For Each po As _unit In arrClose
            If tempPoint = po.pos Then
                Return True
            End If
        Next

        Return False
    End Function


    '得到当前格子周围格子的坐标，如果出界坐标为(-1,-1)
    Private Function getUpPos(ByVal poCurr As Point) As Point
        If poCurr.Y = 0 Then
            poCurr.X = -1
            poCurr.Y = -1
        Else
            poCurr.Y = poCurr.Y - 1
        End If
        Return poCurr
    End Function

    Private Function getDownPos(ByVal poCurr As Point) As Point
        If poCurr.Y = intSizeY - 1 Then
            poCurr.X = -1
            poCurr.Y = -1
        Else
            poCurr.Y = poCurr.Y + 1
        End If
        Return poCurr
    End Function

    Private Function getLeftPos(ByVal poCurr As Point) As Point
        If poCurr.X = 0 Then
            poCurr.X = -1
            poCurr.Y = -1
        Else
            poCurr.X = poCurr.X - 1
        End If
        Return poCurr
    End Function

    Private Function getRightPos(ByVal poCurr As Point) As Point
        If poCurr.X = intSizeX - 1 Then
            poCurr.X = -1
            poCurr.Y = -1
        Else
            poCurr.X = poCurr.X + 1
        End If
        Return poCurr
    End Function

    Private Function getUpRightPos(ByVal poCurr As Point) As Point
        poCurr.X = poCurr.X + 1
        poCurr.Y = poCurr.Y - 1
        Return poCurr
    End Function

    Private Function getDownRightPos(ByVal poCurr As Point) As Point
        poCurr.X = poCurr.X + 1
        poCurr.Y = poCurr.Y + 1
        Return poCurr
    End Function

    Private Function getDownLeftPos(ByVal poCurr As Point) As Point
        poCurr.X = poCurr.X - 1
        poCurr.Y = poCurr.Y + 1
        Return poCurr
    End Function

    Private Function getUpLeftPos(ByVal poCurr As Point) As Point
        poCurr.X = poCurr.X - 1
        poCurr.Y = poCurr.Y - 1
        Return poCurr
    End Function

    Private Sub spbtnClearPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles spbtnClearPath.Click
        arrDraw.Clear()
        pn1.Refresh()
    End Sub
End Class
