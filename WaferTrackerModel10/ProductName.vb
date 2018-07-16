Imports System.ComponentModel.DataAnnotations

Namespace WaferTrackerModel

    Public Class ProductName

        <Key>
        Public Property ProductNameId As Integer
        Public Property ProductNameLong As String
        Public Property ProductNameShort As String
        Public Property ProductNameCustomer As String
        Public Property ProductNameAcronym As String
        Public Property ProductCode As String
        Public Property GroupId As String
        Public Property Description As String

    End Class

End Namespace
