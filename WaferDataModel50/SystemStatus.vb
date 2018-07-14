Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace WaferDataModel50

    Public Class SystemStatus

        <Key>
        Public Property SystemStatusId As Integer
        Public Property CurrentWaferId As Integer
        Public Property CurrentChipId As Integer
        Public Property CurrentTestsDoneId As Integer
        Public Property CurrentTestDefinitionId As Integer
        Public Property CurrentDeviceId As Integer

    End Class

End Namespace
