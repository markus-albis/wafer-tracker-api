Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace WaferDataModel50

    Public Class TestResult

        <Key>
        Public Property TestResultId As Integer
        Public Property TestId As Integer
        Public Property XValue As Double
        Public Property YValue As Double
        <ForeignKey("TestId")>
        Public Property Test As Test

    End Class

End Namespace
