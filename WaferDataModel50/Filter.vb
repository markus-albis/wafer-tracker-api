Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace WaferDataModel50

    Public Class Filter

        <Key>
        Public Property FilterId As Integer
        Public Property TestDefinitionId As Integer
        Public Property SQL As String
        <ForeignKey("TestDefinitionId")>
        Public Property TestDefinition As TestDefinition

    End Class

End Namespace
