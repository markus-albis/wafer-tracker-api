Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace WaferDataModel50

    Public Class DevicePosition

        <Key>
        Public Property PositionId As Integer
        Public Property ProjectId As Integer
        Public Property DeviceNumber As Integer
        Public Property XPosition As Integer
        Public Property YPosition As Integer

    End Class

End Namespace
