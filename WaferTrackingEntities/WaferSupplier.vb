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

Partial Public Class WaferSupplier
    Public Property WaferSupplierId As Integer
    Public Property WaferSupplierShortcut As String
    Public Property WaferSupplierName As String

    Public Overridable Property Wafers As ICollection(Of Wafer) = New HashSet(Of Wafer)

End Class
