Imports System.ComponentModel.DataAnnotations

Namespace WaferTrackerModel

    Public Class SPC_Parameter

        <Key>
        Public Property AcquisitionTypeId As Integer
        Public Property Variable As String
        Public Property Unit As String
        Public Property Description As String
        Public Property RemarkSPC As String
        Public Property Limit_lower As Double
        Public Property Limit_upper As Double

    End Class

End Namespace
