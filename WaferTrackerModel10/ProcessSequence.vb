Imports System.ComponentModel.DataAnnotations

Namespace WaferTrackerModel

    Public Class ProcessSequence

        <Key>
        Public Property SequenceId As Integer
        Public Property GroupId As Integer
        Public Property Index As Integer
        Public Property LayerId As Integer

    End Class

End Namespace
