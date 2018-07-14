Imports System.ComponentModel.DataAnnotations

Namespace WaferDataModel50

    Public Class TestPortUse

        <Key>
        Public Property TestPortUseId As Integer
        Public Property TestDefinitionId As Integer
        Public Property Sweep As String
        Public Property [Step] As String
        Public Property Y As String
        Public Property HoldTime As Double
        Public Property StepDelayTime As Double
        Public Property IntegrationTime As Integer
        Public Property Scalefactor As Double
        Public Property Offset As Double


    End Class

End Namespace
