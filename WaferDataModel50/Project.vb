Imports System.ComponentModel.DataAnnotations

Namespace WaferDataModel50

    Public Class Project

        <Key>
        Public Property ProjectId As Integer
        Public Property ProductName As String
        Public Property ProductCode As String
        Public Property ProductType As String
        Public Property ProductRevision As String
        Public Property ChipXIndex As Integer
        Public Property ChipYIndex As Integer
        Public Property TestXIndex As Integer
        Public Property TestYIndex As Integer
        Public Property TestXSize As Integer
        Public Property TestYSize As Integer
        Public Property DevicePerChip As Integer
        Public Property SplitXIndex As Integer
        Public Property SplitYIndex As Integer
        Public Property DBVersion As String

    End Class

End Namespace
