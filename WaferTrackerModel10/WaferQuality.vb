Imports System.ComponentModel.DataAnnotations

Namespace WaferTrackerModel

    Public Class WaferQuality

        <Key>
        Public Property QualityId As Integer
        Public Property Quality As String
        Public Property SortOrder As Integer

    End Class

End Namespace
