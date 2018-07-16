Imports System.ComponentModel.DataAnnotations

Namespace WaferTrackerModel

    Public Class ProcessGroup

        <Key>
        Public Property GroupId As Integer
        Public Property GroupDescription As String
        Public Property Options As String
        Public Property BaseMaterial As String
        Public Property TargetDuration As Integer

    End Class

End Namespace
