Imports System.ComponentModel.DataAnnotations

Namespace WaferDataModel50

    Public Class Wafer

        <Key>
        Public Property WaferId As Integer
        Public Property RunName As String
        Public Property WaferType As String
        Public Property Orientation As String
        Public Property RowsPerWafer As Integer
        Public Property ColumnsPerWafer As Integer
        Public Property TestXOffset As Integer
        Public Property TestYOffset As Integer
        Public Property Description As String
        Public Property WaferRadius As Integer

    End Class

End Namespace
