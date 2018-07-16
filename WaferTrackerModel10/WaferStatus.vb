Imports System.ComponentModel.DataAnnotations

Namespace WaferTrackerModel

    Public Class WaferStatus

        <Key>
        Public Property WaferStatusId As Integer
        Public Property WaferIdRef As Integer
        Public Property StartDate As DateTime
        Public Property StopDate As DateTime
        Public Property WaferLocationIdRef As Integer
        Public Property LayerId As Integer
        Public Property Number As Integer
        Public Property TechnicianRef As Integer
        Public Property Text As String
        Public Property Remark As String

    End Class

End Namespace
