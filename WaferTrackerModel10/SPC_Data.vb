Imports System.ComponentModel.DataAnnotations

Namespace WaferTrackerModel

    Public Class SPC_Data

        <Key>
        Public Property AcquisitionDataId As Integer
        Public Property AcquisitionIdRef As Integer
        Public Property Value As Double
        Public Property Position As Double
        Public Property DataTypeIdRef As Integer

    End Class

End Namespace
