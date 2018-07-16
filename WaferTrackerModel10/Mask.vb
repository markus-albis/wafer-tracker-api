Imports System.ComponentModel.DataAnnotations

Namespace WaferTrackerModel

    Public Class Mask

        <Key>
        Public Property MaskId As Integer
        Public Property MaskNameShortcut As String
        Public Property MaskName As String
        Public Property DeviceName As String
        Public Property Flat As String
        Public Property Description As String

    End Class

End Namespace
