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

Partial Public Class EpiType
    Public Property EpiTypeId As Integer
    Public Property EpiTypeShortcut As String
    Public Property EpiTypeName As String

    Public Overridable Property Wafers As ICollection(Of Wafer) = New HashSet(Of Wafer)

End Class
