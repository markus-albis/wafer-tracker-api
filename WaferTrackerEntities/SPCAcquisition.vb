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

Partial Public Class SPCAcquisition
    Public Property SPCAqcuisitionId As Integer
    Public Property SPCParameterId As Nullable(Of Integer)
    Public Property DateSPC As Nullable(Of Date)
    Public Property OperatorIdRef As Nullable(Of Integer)
    Public Property RemarkSPC As String
    Public Property NoteSPC As String
    Public Property SPCParameter_SPCParameterId As Integer
    Public Property Wafer_WaferID As Integer
    Public Property Line_LineId As Integer

    Public Overridable Property Line As Line
    Public Overridable Property SPCDatas As ICollection(Of SPCData) = New HashSet(Of SPCData)
    Public Overridable Property SPCParameter As SPCParameter
    Public Overridable Property Wafer As Wafer

End Class