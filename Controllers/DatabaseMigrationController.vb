
Imports System.Web.Http
Imports Newtonsoft.Json.Linq
Imports WaferTrackerLibrary.Services
Imports Schema10 = WaferTrackerLibrary.WaferTrackerModel


Public Class DatabaseMigrationController
    Inherits ApiController

    Private appPath As String
    ' Private contextProvider As EFContextProvider(Of WaferTrackerEntities) = New EFContextProvider(Of WaferTrackerEntities)
    Private context As WaferTrackerEntities = New WaferTrackerEntities
    Private dataService As WaferTrackerDataService

    Public Sub New()
        dataService = New WaferTrackerDataService
    End Sub

    <HttpPost>
    Public Sub MigrateTable(<FromBody> data As JObject)
        appPath = My.Settings.AppFolder
        Dim table As String = data("table")

        Select Case table
            Case "EpiType"
                MigrateEpiTypeTable()
            Case "EpiSupplier"
                MigrateEpiSupplierTable()
            Case "WaferType"
                MigrateWaferTypeTable()
            Case "WaferSize"
                MigrateWaferSizeTable()
            Case "WaferSupplier"
                MigrateWaferSupplierTable()
            Case "WaferStatus"
                MigrateWaferStatusTable()
            Case "WaferQuality"
                MigrateWaferQualityTable()
            Case "ProductFamiliy"
                MigrateProductFamiliyTable()
            Case "LotNumber"
                MigrateLotNumberTable()
            Case "Mask"
                MigrateMaskTable()
            Case "ProductName"
                MigrateProductNameTable()
            Case "Wafer"
                MigrateWaferTable()
            Case "Technician"
                MigrateTechnicianTable()
            Case "WaferLocation"
                MigrateWaferLocationTable()
            Case "ProcessGroup"
                'MigrateProcessGroupTable()
            Case "ProcessSequence"
                'MigrateProcessSequenceTable()
            Case "Layer"
                'MigrateLayerTable()
            Case "SPC_Parameter"
                'MigrateSPCParameterTable()
            Case "SPC_Acquisition"
                'MigrateSPCAcquisitionTable()
            Case "SPC_Data"
                'MigrateSPCDataTable()
            Case "SPC_Data_Type"
                'MigrateSPCDataTypeTable()
            Case "Line"
                'MigrateLineTable()     
        End Select

    End Sub


#Region "Table Migration Methods"

    Public Sub MigrateEpiSupplierTable()
        Dim _rows As IEnumerable(Of Schema10.EpiSupplier)
        _rows = dataService.GetEpiSuppliers

        For Each _row As Schema10.EpiSupplier In _rows
            Dim newEntity = New EpiSupplier
            newEntity.EPISupplierId = _row.EpiSupplierId
            newEntity.EPISupplierShortcut = _row.EpiSupplierShortcut
            newEntity.EPISupplierName = _row.EpiSupplierShortcut
            context.EpiSuppliers.Add(newEntity)
        Next
        context.SaveChanges()
    End Sub

    Public Sub MigrateEpiTypeTable()
        Dim _rows As IEnumerable(Of Schema10.EpiType)
        _rows = dataService.GetEpiTypes()

        For Each _row As Schema10.EpiType In _rows
            Dim newEntity = New EpiType
            newEntity.EpiTypeId = _row.EpiTypeId
            newEntity.EpiTypeShortcut = _row.EpiTypeNameShortcut
            newEntity.EpiTypeName = _row.EpiTypeName
            context.EpiTypes.Add(newEntity)
        Next
        context.SaveChanges()
    End Sub

    Public Sub MigrateLotNumberTable()
        Dim _rows As IEnumerable(Of Schema10.LotNumber)
        _rows = dataService.GetLotNumbers()

        For Each _row As Schema10.LotNumber In _rows
            Dim newEntity = New Lot
            newEntity.LotId = _row.LotId
            newEntity.LotNumber = _row.LotNo
            newEntity.Remark = _row.Remark
            context.Lots.Add(newEntity)
        Next
        context.SaveChanges()
    End Sub

    Public Sub MigrateMaskTable()
        Dim _rows As IEnumerable(Of Schema10.Mask)
        _rows = dataService.GetMasks()

        For Each _row As Schema10.Mask In _rows
            Dim newEntity = New Mask
            newEntity.MaskId = _row.MaskId
            newEntity.MaskName = _row.MaskName
            newEntity.Shortcut = _row.MaskNameShortcut
            newEntity.DeviceName = _row.DeviceName
            newEntity.Flat = _row.Flat
            newEntity.Description = _row.Description
            context.Masks.Add(newEntity)
        Next
        context.SaveChanges()
    End Sub

    Public Sub MigrateProductFamiliyTable()
        Dim _rows As IEnumerable(Of Schema10.ProductFamily)
        _rows = dataService.GetProductFamilies()

        For Each _row As Schema10.ProductFamily In _rows
            Dim newEntity = New ProductGroup
            newEntity.ProductGroupId = _row.ProductFamilyId
            newEntity.Acronym = _row.ProductFamilyName
            newEntity.Description = _row.Description
            context.ProductGroups.Add(newEntity)
        Next
        context.SaveChanges()
    End Sub

    Public Sub MigrateProductNameTable()
        Dim _rows As IEnumerable(Of Schema10.ProductName)
        _rows = dataService.GetProductNames()

        For Each _row As Schema10.ProductName In _rows
            Dim newEntity = New Product
            newEntity.ProductId = _row.ProductNameId
            newEntity.ProductName = _row.ProductNameLong
            newEntity.ProductCode = _row.ProductCode
            newEntity.ShortName = _row.ProductNameShort
            newEntity.CustomerName = _row.ProductNameCustomer
            newEntity.Acronym = _row.ProductNameAcronym
            newEntity.Description = _row.Description
            context.Products.Add(newEntity)
        Next
        context.SaveChanges()
    End Sub

    Public Sub MigrateTechnicianTable()
        Dim _rows As IEnumerable(Of Schema10.Technician)
        _rows = dataService.GetTechnicians()

        For Each _row As Schema10.Technician In _rows
            Dim newEntity = New Technician
            newEntity.TechnicianId = _row.AddressId
            newEntity.FirstName = _row.FName
            newEntity.LastName = _row.Name
            newEntity.ShortCut = _row.ShortCutName
            newEntity.Site = _row.Site
            context.Technicians.Add(newEntity)
        Next
        context.SaveChanges()
    End Sub

    Public Sub MigrateWaferTable()
        Dim _rows As IEnumerable(Of Schema10.Wafer)
        _rows = dataService.GetWafers()

        For Each _row As Schema10.Wafer In _rows
            Dim newEntity = New Wafer
            newEntity.WaferID = _row.WaferId
            newEntity.WaferIdentification = _row.WaferIdentification
            newEntity.WaferLotNo = _row.WaferLotNo
            newEntity.WaferNo = _row.WaferNo
            newEntity.EPIRunNumber = _row.EpiRunNumber
            newEntity.EPIDeliveryDate = _row.EpiDeliveryDate
            newEntity.Remark = _row.Remark
            newEntity.WaferSupplier_WaferSupplierId = _row.WaferSupplierIdRef
            newEntity.EpiSupplier_EPISupplierId = _row.EpiSupplierIdRef
            newEntity.WaferType_WaferTypeId = _row.WaferTypeIdRef
            newEntity.WaferSize_WaferSizeId = _row.WaferSizeIdRef
            newEntity.Mask_MaskId = _row.MaskIdRef
            newEntity.Product_ProductId = _row.ProductNameId
            newEntity.ProductGroup_ProductGroupId = _row.ProductFamiliyId
            newEntity.WaferState_WaferStateId = _row.QualityIdRef
            newEntity.Lot_LotId = _row.LotIdRef
            newEntity.EpiType_EpiTypeId = _row.EpiTypeIdRef
            context.Wafers.Add(newEntity)
        Next
        context.SaveChanges()
    End Sub

    Public Sub MigrateWaferLocationTable()
        Dim _rows As IEnumerable(Of Schema10.WaferLocation)
        _rows = dataService.GetWaferLocations()

        For Each _row As Schema10.WaferLocation In _rows
            Dim newEntity = New WaferLocation
            newEntity.WaferLocationId = _row.LocationId
            newEntity.LocationName = _row.LocationName
            newEntity.SortOrder = _row.SortOrder
            newEntity.Remark = _row.Waferloc_Remark
            context.WaferLocations.Add(newEntity)
        Next
        context.SaveChanges()
    End Sub

    Public Sub MigrateWaferQualityTable()
        Dim _rows As IEnumerable(Of Schema10.WaferQuality)
        _rows = dataService.GetWaferQualities()

        For Each _row As Schema10.WaferQuality In _rows
            Dim newEntity = New WaferState
            newEntity.WaferStateId = _row.QualityId

            'Map legacy wafer qualities to new wafer states
            Select Case _row.Quality
                Case "Undefined"
                    newEntity.Description = "Undefined"
                    newEntity.SortOrder = 20
                    newEntity.FailureCategory_FailureCategoryId = 1
                Case "Passed"
                    newEntity.Description = "Passed"
                    newEntity.SortOrder = 10
                    newEntity.FailureCategory_FailureCategoryId = 1
                Case Else
                    newEntity.Description = "Failed"
                    newEntity.SortOrder = 30
            End Select

            'Map previous wafer qualities to new  failure categories (if required)
            If _row.Quality <> "Undefined" Or _row.Quality <> "Passed" Then
                Select Case _row.Quality
                    Case "Equipment"
                        newEntity.FailureCategory_FailureCategoryId = 2
                    Case "Operator"
                        newEntity.FailureCategory_FailureCategoryId = 3
                    Case "Early Fracture"
                        newEntity.FailureCategory_FailureCategoryId = 4
                    Case "Fracture"
                        newEntity.FailureCategory_FailureCategoryId = 5
                    Case "Passivation"
                        newEntity.FailureCategory_FailureCategoryId = 6
                    Case "AR-Coating"
                        newEntity.FailureCategory_FailureCategoryId = 7
                    Case "Pad Adhesion"
                        newEntity.FailureCategory_FailureCategoryId = 8
                    Case "Epitaxy"
                        newEntity.FailureCategory_FailureCategoryId = 9
                    Case "Other"
                        newEntity.FailureCategory_FailureCategoryId = 10
                    Case "Management"
                        newEntity.FailureCategory_FailureCategoryId = 11
                    Case "Lap Process"
                        newEntity.FailureCategory_FailureCategoryId = 12
                    Case "Wet Lens Process"
                        newEntity.FailureCategory_FailureCategoryId = 13
                    Case "Dry Lens Process"
                        newEntity.FailureCategory_FailureCategoryId = 14
                    Case "Wafer Bond/Debond"
                        newEntity.FailureCategory_FailureCategoryId = 15
                    Case "Subcontractor"
                        newEntity.FailureCategory_FailureCategoryId = 16
                End Select
            End If
            context.WaferStates.Add(newEntity)
        Next
        context.SaveChanges()
    End Sub

    Public Sub MigrateWaferSizeTable()
        Dim _rows As IEnumerable(Of Schema10.WaferSize)
        _rows = dataService.GetWaferSizes()

        For Each _row As Schema10.WaferSize In _rows
            Dim newEntity = New WaferSize
            newEntity.WaferSizeId = _row.WaferSizeId
            newEntity.WaferSize1 = _row.WaferSize
            context.WaferSizes.Add(newEntity)
        Next
        context.SaveChanges()
    End Sub

    Public Sub MigrateWaferStatusTable()
        Dim _rows As IEnumerable(Of Schema10.WaferStatus)
        _rows = dataService.GetWaferStatus()

        For Each _row As Schema10.WaferStatus In _rows
            Dim newEntity = New WaferHistory
            newEntity.WaferHistoryId = _row.WaferStatusId
            newEntity.StartDate = _row.StartDate
            newEntity.StopDate = _row.StopDate
            newEntity.Remark = _row.Remark
            newEntity.Technician_TechnicianId = _row.TechnicianRef
            newEntity.WaferLocation_WaferLocationId = _row.WaferLocationIdRef
            newEntity.Wafer_WaferID = _row.WaferIdRef
            newEntity.Process_ProcessId = _row.LayerId
            context.WaferHistories.Add(newEntity)
        Next
        context.SaveChanges()
    End Sub

    Public Sub MigrateWaferSupplierTable()
        Dim _rows As IEnumerable(Of Schema10.WaferSupplier)
        _rows = dataService.GetWaferTypes()

        For Each _row As Schema10.WaferSupplier In _rows
            Dim newEntity = New WaferSupplier
            newEntity.WaferSupplierId = _row.WaferSupplierId
            newEntity.WaferSupplierShortcut = _row.WaferSupplierShortcut
            newEntity.WaferSupplierName = _row.WaferSupplierName
            context.WaferSuppliers.Add(newEntity)
        Next
        context.SaveChanges()
    End Sub

    Public Sub MigrateWaferTypeTable()
        Dim _rows As IEnumerable(Of Schema10.WaferType)
        _rows = dataService.GetWaferTypes()

        For Each _row As Schema10.WaferType In _rows
            Dim newEntity = New WaferType
            newEntity.WaferTypeId = _row.WaferTypeId
            newEntity.WaferTypeShortcut = _row.WaferTypeNameShortcut
            newEntity.WaferTypeName = _row.WaferTypeName
            context.WaferTypes.Add(newEntity)
        Next
        context.SaveChanges()
    End Sub


#End Region



End Class

