Imports System.ComponentModel.DataAnnotations

Namespace WaferDataModel50

    Public Class Failure

        <Key>
        Public Property DeviceId As Integer
        Public Property ChipId As Integer
        Public Property ChipXPosition As Integer
        Public Property ChipYPosition As Integer

    End Class

End Namespace
