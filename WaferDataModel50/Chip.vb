Imports System.ComponentModel.DataAnnotations

Namespace WaferDataModel50

    Public Class Chip

        <Key>
        Public Property ChipId As Integer
        Public Property ChipNumber As Integer
        Public Property ChipXPosition As Integer
        Public Property ChipYPosition As Integer
        Public Property ChipStatusId As Integer
        Public Property ChipTypeId As Integer
        Public Property WaferId As Integer

    End Class

End Namespace
