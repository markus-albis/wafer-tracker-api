Imports System.ComponentModel.DataAnnotations

Namespace WaferTrackerModel

    Public Class Layer

        <Key>
        Public Property LayerId As Integer
        Public Property Description As String
        Public Property Acronym As String
        Public Property Duration As Integer
        Public Property LocationId As String

    End Class

End Namespace
