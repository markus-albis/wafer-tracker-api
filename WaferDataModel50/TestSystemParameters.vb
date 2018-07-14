Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace WaferDataModel50

    Public Class TestSystemParameters

        <Key>
        Public Property ParameterId As Integer
        Public Property EquipmentCode As String
        Public Property ParameterName As String
        Public Property ParameterValue As Double
        Public Property ParameterDimension As String

    End Class

End Namespace
