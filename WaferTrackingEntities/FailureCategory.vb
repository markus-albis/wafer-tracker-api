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

Partial Public Class FailureCategory
    Public Property FailureCategoryId As Integer
    Public Property Description As String
    Public Property Remark As String

    Public Overridable Property WaferStates As ICollection(Of WaferState) = New HashSet(Of WaferState)

End Class
