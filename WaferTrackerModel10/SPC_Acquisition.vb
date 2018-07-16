Imports System.ComponentModel.DataAnnotations

Namespace WaferTrackerModel

    Public Class SPC_Acquisition

        <Key>
        Public Property AcquisitionId As Integer
        Public Property WaferIdRefId As Integer
        Public Property AcquisitionTypeIdRef As Integer
        Public Property LineIdRef As Integer
        Public Property DateSPC As DateTime
        Public Property OperatorIdRef As Integer
        Public Property RemarkSPC As String
        Public Property NoteSPC As String


    End Class

End Namespace
