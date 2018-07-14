Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace WaferDataModel50

    Public Class Dimension

        <Key>
        Public Property DimensionId As Integer

        Public Property XDimension As String

        Public Property XTitle As String

        Public Property YDimension As String

        Public Property YTitle As String

    End Class

End Namespace
