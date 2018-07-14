Imports System.ComponentModel.DataAnnotations

Namespace WaferDataModel50

    Public Class TestPortDefs

        <Key>
        Public Property TestPortDefsId As Integer
        Public Property TestDefinitionId As Integer
        Public Property PortId As String
        Public Property EquipmentCode As String
        Public Property Source As String
        Public Property Start As Double
        Public Property [Stop] As Double
        Public Property StepSize As Double
        Public Property Compliance As Double
        Public Property SweepMode As String
        Public Property SwitchIn As String
        Public Property SwitchOut As String

    End Class

End Namespace
