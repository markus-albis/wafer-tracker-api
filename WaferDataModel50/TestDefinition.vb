Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace WaferDataModel50

    Public Class TestDefinition

        <Key>
        Public Property TestDefinitionId As Integer
        Public Property Description As String
        Public Property DimensionId As Integer
        Public Property LinkedTo As String
        Public Property EngineerName As String
        Public Property TestReleaseTime As DateTime
        Public Property Comment As String
        <ForeignKey("DimensionId")>
        Public Property Dimension As Dimension


    End Class

End Namespace
