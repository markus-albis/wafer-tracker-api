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

Partial Public Class Product
    Public Property ProductId As Integer
    Public Property ProductName As String
    Public Property ProductCode As String
    Public Property ShortName As String
    Public Property CustomerName As String
    Public Property Acronym As String
    Public Property Description As String

    Public Overridable Property ProcessGroups As ICollection(Of ProcessGroup) = New HashSet(Of ProcessGroup)
    Public Overridable Property Wafers As ICollection(Of Wafer) = New HashSet(Of Wafer)

End Class