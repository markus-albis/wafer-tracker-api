Imports System.ComponentModel.DataAnnotations

Namespace WaferTrackerModel

    Public Class WaferLocation

        <Key>
        Public Property LocationId As Integer
        Public Property WaferSize As String
        Public Property LocationName As String
        Public Property SortOrder As Integer
        Public Property Waferloc_Remark As String

    End Class

End Namespace
