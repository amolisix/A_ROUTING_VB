Module ModDb
    Friend Structure _unit
        Public pos As Point '此方格坐标，起点(0,0)定义在最左上角
        Public a_F As Integer   'F=G+H
        Public a_G As Integer '从起始点到此点的实际消耗
        Public a_H As Integer '从此点到终点的估计消耗
        Public sty As _sty '地形
        Public parent_pos As Point '父方格坐标
    End Structure

    Friend Enum _sty
        sty_Reach  '可到达
        sty_unReach '不可到达
        sty_Start '起点
        syt_End '终点
    End Enum

End Module
