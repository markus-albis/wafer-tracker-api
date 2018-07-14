Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace WaferDataModel50

    Public Class Test

        <Key>
        Public Property TestId As Integer
        Public Property DeviceId As Integer
        Public Property TestDefinitionId As Integer
        Public Property TestsDoneId As Integer
        Public Property TestTime As DateTime
        <ForeignKey("DeviceId")>
        Public Property Device As Device
        <ForeignKey("TestDefinitionId")>
        Public Property TestDefinition As TestDefinition


    End Class

End Namespace
