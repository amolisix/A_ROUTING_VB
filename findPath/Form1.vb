Public Class Form1
    Const maxSizeX As Integer = 50   '��ͼ���ߴ�
    Const maxSizeY As Integer = 50

    Private intSizeX As Integer = 10 '��ͼ��ǰ�ߴ�
    Private intSizeY As Integer = 10

    Private myPen As New Pen(Color.Gray)
    Private myBrush As New SolidBrush(Color.White)

    Private unit(intSizeX - 1, intSizeY - 1) As _unit   '��ͼ���ݽṹ
    Private unit_start As _unit            '��ʼ��
    Private unit_end As _unit          '��ֹ��


    Private m_down As Boolean = False  '����Ƿ���

    Private arrOpen As New ArrayList '�����б���Ԫ��Ϊ��_unit
    Private arrClose As New ArrayList '�ر��б�   Ԫ��Ϊ��_unit
    Private arrDraw As New ArrayList '������ɵ�·���б�  Ԫ��Ϊ point

    Private posCurr As _unit '��ǰ��
    Private boolFindPath As Boolean = False '�Ƿ��Ѿ��ҵ��յ�
    Private step_G As Integer 'G ��ֵ����ֱ��Ϊ10,��б��Ϊ14

    Dim mySort As New myReverserClass '_unit �������



    Public Class myReverserClass  '_unit ������򣬸���Fֵ����
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


    Private Function checkSize() As Boolean  '�������ĵ�ͼ��ֵ�Ƿ�Ϸ�
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
            MessageBox.Show("��ͼ��С��������")
            Exit Sub
        End If

        madeMap()

        pn1.Refresh()
    End Sub

    Private Sub madeMap() '���ɵ�ͼ
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

    Private Sub DrawGrid(ByVal g As Graphics) '������
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

    Private Sub DrawUnit(ByVal g As Graphics) '�����з����ڵ���
        For i As Integer = 0 To intSizeX - 1
            For j As Integer = 0 To intSizeY - 1
                DrawOneUnit(g, unit(i, j))
            Next
        Next
    End Sub

    Private Sub DrawOneUnit(ByVal g As Graphics, ByVal one_unit As _unit) '��һ�������ڵ���
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
        myPen.Width = 2 '����߿�����߿��ߣ����
        madeMap()
    End Sub

    Private Sub pn1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pn1.MouseDown
        Dim g As Graphics = pn1.CreateGraphics '�������Ƶ���

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
        If m_down Then '�������Ƶ���
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

    Private Function getUnitRect(ByVal pt As Point) As Rectangle '���ݷ�������õ���pn1�ϵ�����
        Dim rect As Rectangle
        Dim douStepX As Double = pn1.Width / intSizeX
        Dim douStepY As Double = pn1.Height / intSizeY

        rect.X = CInt(pt.X * douStepX)
        rect.Y = CInt(pt.Y * douStepY)
        rect.Width = CInt(douStepX)
        rect.Height = CInt(douStepY)

        Return rect
    End Function

    Private Function getUnitCenPos(ByVal pt As Point) As Point '���ݷ�������õ��˷�����pn1�ϵ����ĵ�����
        Dim rePoint As Point
        Dim douStepX As Double = pn1.Width / intSizeX
        Dim douStepY As Double = pn1.Height / intSizeY
        rePoint.X = CInt(pt.X * douStepX + douStepX / 2)
        rePoint.Y = CInt(pt.Y * douStepY + douStepY / 2)

        Return rePoint
    End Function

    Private Function getUnit(ByVal pt As Point) As Point '�����������λ�ã�����˷�������
        Dim douStepX As Double = pn1.Width / intSizeX
        Dim douStepY As Double = pn1.Height / intSizeY

        Return New Point(CInt(Int(pt.X / douStepX)), CInt(Int(pt.Y / douStepY)))
    End Function


    Private Sub spbtnFindPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles spbtnFindPath.Click
        If unit(unit_start.pos.X, unit_start.pos.Y).sty <> _sty.sty_Start Then
            MessageBox.Show("�붨����ʼ��")
            Exit Sub
        End If
        If unit(unit_end.pos.X, unit_end.pos.Y).sty <> _sty.syt_End Then
            MessageBox.Show("�붨����ֹ��")
            Exit Sub
        End If



        fPath()


    End Sub


    Private Sub fPath() 'Ѱ·

        '���������Ҫ�õ����б�
        arrOpen.Clear()
        arrClose.Clear()
        arrDraw.Clear()

        boolFindPath = False

        arrOpen.Add(unit_start) '�����һ����ʼ��
        startFindPath()


    End Sub

    Private Sub startFindPath() '��ʼѰ·

        Do While arrOpen.Count > 0 '���open��δ�գ�����Ѱ·
            posCurr = CType(arrOpen(0), _unit) '��open�б�����ǰ���Ԫ����Ϊ��ǰ��


            arrOpen.RemoveAt(0) '��open�б�����ȥ��ǰ�񣬲�����close�б�
            arrClose.Add(posCurr)


            AddToOpen() '����ǰ����ϵĸ��ӷ���open��(����ͨ�������Ѿ���open���close�������)

            If boolFindPath Then '���Ѿ��ҵ�·��
                ' MessageBox.Show("find")
                AddToDraw()
                Dim g As Graphics = pn1.CreateGraphics
                drawPath(g)
                g.Dispose()
                Exit Sub
            End If

            arrOpen.Sort(mySort) '��������open��Fֵ��͵��ŵ���ǰ��ȥ
        Loop
        MessageBox.Show("no path") 'open���ѿգ���·��������
    End Sub

    Private Sub AddToDraw() '�ѵõ���·�����꣬��������

        Dim tempPoint As Point = unit_end.pos

        Do
            arrDraw.Add(tempPoint)
            tempPoint = unit(tempPoint.X, tempPoint.Y).parent_pos

        Loop Until tempPoint = unit_start.pos

    End Sub

    Private Sub drawPath(ByVal g As Graphics) '����·��
        Dim showPoint As Point
        Dim r_x As Integer = CInt(pn1.Width / intSizeX / 4)
        Dim r_y As Integer = CInt(pn1.Height / intSizeY / 4)

        For Each tempPoint As Point In arrDraw
            showPoint = getUnitCenPos(tempPoint)
            g.FillEllipse(Brushes.Fuchsia, New Rectangle(showPoint.X - r_x, showPoint.Y - r_y, r_x * 2, r_y * 2))
        Next
    End Sub

    Private Sub AddToOpen() '����ǰ���Աߵĸ��ӷ���open��(����ͨ�������Ѿ���open���close�������)
        Dim tempPoint As Point
        Dim boolUp As Boolean = False '��ǰ���Ϸ��ɷ�ͨ��
        Dim boolDown As Boolean = False '�·��ɷ�ͨ��
        Dim boolLeft As Boolean = False '�󷽿ɷ�ͨ��
        Dim boolRight As Boolean = False '�ҷ��ɷ�ͨ��

        step_G = 10 '��������Ϊֱ�ߣ�ͨ������10
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
            step_G = 14 'б��ͨ������14
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

    Private Function AddOnePos(ByVal tempPoint As Point) As Boolean '���˷������open��
        If unit(tempPoint.X, tempPoint.Y).sty = _sty.syt_End Then '��˷���Ϊ�յ�
            boolFindPath = True '�ҵ�·���ˣ�
            unit(unit_end.pos.X, unit_end.pos.Y).parent_pos = posCurr.pos '��ǰ������Ϊ�յ�ĸ���������
            Return False '������ɣ����� :)
        End If

        If unit(tempPoint.X, tempPoint.Y).sty = _sty.sty_unReach Then
            Return False '��·��ͨ
        End If

        If Not IsInOpenOrClose(tempPoint) Then '����˷�����open���close��
            unit(tempPoint.X, tempPoint.Y).parent_pos = posCurr.pos '��ǰ��Ϊ�˸�ĸ�����
            unit(tempPoint.X, tempPoint.Y).a_G = posCurr.a_G + step_G '����Gֵ
            unit(tempPoint.X, tempPoint.Y).a_H = (Math.Abs(unit_end.pos.X - tempPoint.X) + Math.Abs(unit_end.pos.Y - tempPoint.Y)) * 10 '����Hֵ
            unit(tempPoint.X, tempPoint.Y).a_F = unit(tempPoint.X, tempPoint.Y).a_G + unit(tempPoint.X, tempPoint.Y).a_H '����Fֵ

            arrOpen.Add(unit(tempPoint.X, tempPoint.Y)) '���˷������open��
        End If

        Return True '��·��ͨ��
    End Function

    Private Function IsInOpenOrClose(ByVal tempPoint As Point) As Boolean '�˷����Ƿ��Ѿ��� open���close����
        For Each po As _unit In arrOpen
            If tempPoint = po.pos Then '�˷�������open����
                If unit(tempPoint.X, tempPoint.Y).a_G > posCurr.a_G + step_G Then  '�Ƚϵ�ǰ·�������Gֵ��ԭ�������Gֵ���統ǰ�������滻�������õ�ǰ��Ϊ�˸�ĸ�����
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


    '�õ���ǰ������Χ���ӵ����꣬�����������Ϊ(-1,-1)
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
