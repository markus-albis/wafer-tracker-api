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

Partial Public Class WaferHistory
    Public Property WaferHistoryId As Integer
    Public Property StartDate As Nullable(Of Date)
    Public Property StopDate As Nullable(Of Date)
    Public Property Remark As String
    Public Property Technician_TechnicianId As Nullable(Of Integer)
    Public Property WaferLocation_WaferLocationId As Nullable(Of Integer)
    Public Property Wafer_WaferId As Nullable(Of Integer)
    Public Property Process_ProcessId As Nullable(Of Integer)

    Public Overridable Property Process As Process
    Public Overridable Property Technician As Technician
    Public Overridable Property WaferLocation As WaferLocation
    Public Overridable Property Wafer As Wafer

End Class
