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

Partial Public Class Line
    Public Property LineId As Integer
    Public Property LineName As String
    Public Property RemarkLine As String

    Public Overridable Property SPCAcquisitions As ICollection(Of SPCAcquisition) = New HashSet(Of SPCAcquisition)

End Class
