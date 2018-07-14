'Imports System.ComponentModel.DataAnnotations
Imports Dapper.Contrib.Extensions.KeyAttribute
'Imports System.ComponentModel.DataAnnotations.Schema

Namespace WaferDataModel50

    <Dapper.Contrib.Extensions.Table("TestList")>
    Public Class TestList

        <Dapper.Contrib.Extensions.Key>
        Public Property TestListId As Integer
        Public Property TestOrder As Integer
        Public Property TestDefinitionId As Integer
        Public Property Description As String

    End Class

End Namespace
