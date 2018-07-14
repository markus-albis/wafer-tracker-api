Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace WaferDataModel50

    Public Class Device

        <Key>
        Public Property DeviceId As Integer
        Public Property ChipId As Integer
        Public Property PositionId As Integer
        Public Property DeviceTypeId As Integer
        Public Property DeviceStatusId As Integer
        <ForeignKey("ChipId")>
        Public Property Chip As Chip

    End Class

End Namespace
