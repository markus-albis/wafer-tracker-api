'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class SPCParameter
    Public Property SPCParameterId As Integer
    Public Property Variable As String
    Public Property Unit As String
    Public Property Description As String
    Public Property RemarkSPC As String
    Public Property Limit_lower As Nullable(Of Double)
    Public Property Limit_upper As Nullable(Of Double)

    Public Overridable Property SPCAcquisitions As ICollection(Of SPCAcquisition) = New HashSet(Of SPCAcquisition)

End Class
