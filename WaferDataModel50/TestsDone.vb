Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace WaferDataModel50

    Public Class TestsDone

        <Key>
        Public Property TestsDoneId As Integer
        Public Property WaferId As Integer
        Public Property TestDefinitionId As Integer
        Public Property TestComment As String
        Public Property TestDate As DateTime

    End Class

End Namespace
