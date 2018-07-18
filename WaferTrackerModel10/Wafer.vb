Imports System.ComponentModel.DataAnnotations

Namespace WaferTrackerModel

    Public Class Wafer

        <Key>
        Public Property WaferId As Integer
        Public Property WaferIdentification As String
        Public Property WaferNo As String
        Public Property WaferLotNo As String
        Public Property EpiRunNumber As String
        Public Property LotIdRef As Integer
        Public Property MaskIdRef As Integer
        Public Property WaferSupplierIdRef As Integer
        Public Property WaferTypeIdRef As Integer
        Public Property WaferSizeId As Integer
        Public Property EpiSupplierIdRef As Integer
        Public Property EpiTypeIdRef As Integer
        Public Property EpiDeliveryDate As DateTime
        Public Property ProductNameIdRef As Integer
        Public Property ProductFamilyIdRef As Integer
        Public Property QualityIdRef As Integer
        Public Property Remark As String

    End Class

End Namespace
