
Imports System.Web.Http
Imports System.Web
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports WaferTrackerLibrary.Services
Imports Schema10 = WaferTrackerLibrary.WaferTrackerModel


Public Class DatabaseMigrationController
    Inherits ApiController

    Private appPath As String
    ' Private contextProvider As EFContextProvider(Of WaferTrackerEntities) = New EFContextProvider(Of WaferTrackerEntities)
    Private context As WaferTrackerEntities
    Private dataService As WaferTrackerDataService

    Public Sub New()
        dataService = New WaferTrackerDataService
        context = New WaferTrackerEntities
    End Sub

    <HttpPost>
    Public Sub CleanUp(<FromBody> data As JObject)

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
                FinalizeWaferTable()
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

    Private Sub FinalizeWaferTable()
        Dim _rows As IEnumerable(Of Schema10.Wafer)
        _rows = dataService.GetWafers()

        For Each _row As Schema10.Wafer In _rows
            If _row.QualityIdRef = 4 Then           'Finally we  map the legacy status "Undefined" to either "Stock", "On-Hold" or "WIP"
                Dim wafer = (From w In context.Wafers Where w.WaferIdentification = _row.WaferIdentification).FirstOrDefault
                Dim waferHistory = (From h In context.WaferHistories Where h.Wafer_WaferId = wafer.WaferId Order By h.StartDate Descending).ToList
                'Check the wafer location for the latest date in the list
                If waferHistory.Count > 0 Then
                    If waferHistory(0).WaferLocation.LocationName.Contains("Stock") Then
                        wafer.WaferState = (From ws In context.WaferStates Where ws.WaferStateId = 1).FirstOrDefault  'Stock
                    ElseIf waferHistory(0).WaferLocation.LocationName.Contains("On Hold") Then
                        wafer.WaferState = (From ws In context.WaferStates Where ws.WaferStateId = 3).FirstOrDefault  'On Hold
                    Else
                        wafer.WaferState = (From ws In context.WaferStates Where ws.WaferStateId = 2).FirstOrDefault  'WIP
                    End If
                End If
            End If
        Next
        context.SaveChanges()       'Save final update to wafer table
    End Sub

    Private Sub MigrateEpiSupplierTable()
        Dim _rows As IEnumerable(Of Schema10.EpiSupplier)
        _rows = dataService.GetEpiSuppliers
        Dim keyMappings As New Dictionary(Of Integer, Integer)
        Dim oldKey As Integer
        Dim newKey As Integer = -1

        For Each _row As Schema10.EpiSupplier In _rows
            Dim newEntity = New EpiSupplier
            oldKey = _row.EpiSupplierId
            newEntity.EPISupplierShortcut = _row.EpiSupplierShortcut
            newEntity.EPISupplierName = _row.EpiSupplierShortcut
            context.EpiSuppliers.Add(newEntity)
            context.SaveChanges()
            newKey = newEntity.EPISupplierId
            keyMappings.Add(oldKey, newKey)
        Next
        SaveKeyMapping(keyMappings, "episupplier")
    End Sub

    Private Sub MigrateEpiTypeTable()
        Dim _rows As IEnumerable(Of Schema10.EpiType)
        _rows = dataService.GetEpiTypes()
        Dim keyMappings As New Dictionary(Of Integer, Integer)
        Dim oldKey As Integer
        Dim newKey As Integer = -1

        For Each _row As Schema10.EpiType In _rows
            Dim newEntity = New EpiType
            oldKey = _row.EpiTypeId
            newEntity.EpiTypeShortcut = _row.EpiTypeNameShortcut
            newEntity.EpiTypeName = _row.EpiTypeName
            context.EpiTypes.Add(newEntity)
            context.SaveChanges()
            newKey = newEntity.EpiTypeId
            keyMappings.Add(oldKey, newKey)
        Next
        SaveKeyMapping(keyMappings, "epitype")

    End Sub

    Private Sub MigrateLotNumberTable()
        Dim _rows As IEnumerable(Of Schema10.LotNumber)
        _rows = dataService.GetLotNumbers()
        Dim keyMappings As New Dictionary(Of Integer, Integer)
        Dim oldKey As Integer
        Dim newKey As Integer = -1

        For Each _row As Schema10.LotNumber In _rows
            Dim newEntity = New Lot
            oldKey = _row.LotId
            newEntity.LotNumber = _row.LotNo
            newEntity.Remark = _row.Remark
            context.Lots.Add(newEntity)
            context.SaveChanges()
            newKey = newEntity.LotId
            keyMappings.Add(oldKey, newKey)
        Next
        SaveKeyMapping(keyMappings, "lotnumber")
    End Sub

    Private Sub MigrateMaskTable()
        Dim _rows As IEnumerable(Of Schema10.Mask)
        _rows = dataService.GetMasks()
        Dim keyMappings As New Dictionary(Of Integer, Integer)
        Dim oldKey As Integer
        Dim newKey As Integer = -1

        For Each _row As Schema10.Mask In _rows
            Dim newEntity = New Mask
            oldKey = _row.MaskId
            newEntity.MaskName = _row.MaskName
            newEntity.Shortcut = _row.MaskNameShortcut
            newEntity.DeviceName = _row.DeviceName
            newEntity.Flat = _row.Flat
            newEntity.Description = _row.Description
            context.Masks.Add(newEntity)
            context.SaveChanges()
            newKey = newEntity.MaskId
            keyMappings.Add(oldKey, newKey)
        Next
        SaveKeyMapping(keyMappings, "mask")
    End Sub

    Private Sub MigrateProductFamiliyTable()
        Dim _rows As IEnumerable(Of Schema10.ProductFamily)
        _rows = dataService.GetProductFamilies()
        Dim keyMappings As New Dictionary(Of Integer, Integer)
        Dim oldKey As Integer
        Dim newKey As Integer = -1

        For Each _row As Schema10.ProductFamily In _rows
            Dim newEntity = New ProductGroup
            oldKey = _row.ProductFamilyId
            newEntity.Acronym = _row.ProductFamilyName
            newEntity.Description = _row.Description
            context.ProductGroups.Add(newEntity)
            context.SaveChanges()
            newKey = newEntity.ProductGroupId
            keyMappings.Add(oldKey, newKey)
        Next
        SaveKeyMapping(keyMappings, "productfamily")
    End Sub

    Private Sub MigrateProductNameTable()
        Dim _rows As IEnumerable(Of Schema10.ProductName)
        _rows = dataService.GetProductNames()
        Dim keyMappings As New Dictionary(Of Integer, Integer)
        Dim oldKey As Integer
        Dim newKey As Integer = -1

        For Each _row As Schema10.ProductName In _rows
            Dim newEntity = New Product
            oldKey = _row.ProductNameId
            newEntity.ProductName = _row.ProductNameLong
            newEntity.ProductCode = _row.ProductCode
            newEntity.ShortName = _row.ProductNameShort
            newEntity.CustomerName = _row.ProductNameCustomer
            newEntity.Acronym = _row.ProductNameAcronym
            newEntity.Description = _row.Description
            context.Products.Add(newEntity)
            context.SaveChanges()
            newKey = newEntity.ProductId
            keyMappings.Add(oldKey, newKey)
        Next
        SaveKeyMapping(keyMappings, "productname")
    End Sub

    Private Sub MigrateTechnicianTable()
        Dim _rows As IEnumerable(Of Schema10.Technician)
        _rows = dataService.GetTechnicians()
        Dim keyMappings As New Dictionary(Of Integer, Integer)
        Dim oldKey As Integer
        Dim newKey As Integer = -1

        For Each _row As Schema10.Technician In _rows
            Dim newEntity = New Technician
            oldKey = _row.AdressId
            newEntity.FirstName = _row.FName
            newEntity.LastName = _row.Name
            newEntity.ShortCut = _row.ShortCutName
            newEntity.Site = _row.Site
            context.Technicians.Add(newEntity)
            context.SaveChanges()
            newKey = newEntity.TechnicianId
            keyMappings.Add(oldKey, newKey)
        Next
        SaveKeyMapping(keyMappings, "technician")
    End Sub

    Private Sub MigrateWaferTable()
        Dim _rows As IEnumerable(Of Schema10.Wafer)
        _rows = dataService.GetWafers()
        Dim FailureCategories As IEnumerable(Of FailureCategory)        'New Failure Categories 
        FailureCategories = context.FailureCategories
        Dim WaferStates As IEnumerable(Of WaferState)                   'New Wafer States
        WaferStates = context.WaferStates
        Dim keyMappings As New Dictionary(Of Integer, Integer)
        Dim mappedKey As Integer
        Dim oldKey As Integer
        Dim newKey As Integer = -1

        Dim EpiSupplierKeyMap As New Dictionary(Of Integer, Integer)
        Dim EpiTypeKeyMap As New Dictionary(Of Integer, Integer)
        Dim MaskKeyMap As New Dictionary(Of Integer, Integer)
        Dim LotKeyMap As New Dictionary(Of Integer, Integer)
        Dim ProductKeyMap As New Dictionary(Of Integer, Integer)
        Dim ProductGroupKeyMap As New Dictionary(Of Integer, Integer)
        Dim WaferSizeKeyMap As New Dictionary(Of Integer, Integer)
        Dim WaferSupplierKeyMap As New Dictionary(Of Integer, Integer)
        Dim WaferTypeKeyMap As New Dictionary(Of Integer, Integer)

        EpiSupplierKeyMap = GetKeyMapping("episupplier")
        epiTypeKeyMap = GetKeyMapping("epitype")
        maskKeyMap = GetKeyMapping("mask")
        LotKeyMap = GetKeyMapping("lotnumber")
        ProductKeyMap = GetKeyMapping("productname")
        ProductGroupKeyMap = GetKeyMapping("productfamily")
        WaferSizeKeyMap = GetKeyMapping("wafersize")
        WaferSupplierKeyMap = GetKeyMapping("wafersupplier")
        waferTypeKeyMap = GetKeyMapping("wafertype")

        For Each _row As Schema10.Wafer In _rows
            Dim newEntity = New Wafer
            oldKey = _row.WaferId
            newEntity.WaferIdentification = _row.WaferIdentification
            newEntity.WaferLotNo = _row.WaferLotNo
            newEntity.WaferNo = _row.WaferNo
            newEntity.EPIRunNumber = _row.EpiRunNumber
            newEntity.EPIDeliveryDate = _row.EpiDeliveryDate
            newEntity.Remark = _row.Remark

            If WaferSupplierKeyMap.ContainsKey(_row.WaferSupplierIdRef) Then
                mappedKey = WaferSupplierKeyMap(_row.WaferSupplierIdRef)
                newEntity.WaferSupplier = (From wsu In context.WaferSuppliers Where wsu.WaferSupplierId = mappedKey).FirstOrDefault
            Else
                newEntity.WaferSupplier = Nothing
            End If

            If EpiSupplierKeyMap.ContainsKey(_row.EpiSupplierIdRef) Then
                mappedKey = EpiSupplierKeyMap(_row.EpiSupplierIdRef)
                newEntity.EpiSupplier = (From esu In context.EpiSuppliers Where esu.EPISupplierId = mappedKey).FirstOrDefault
            Else
                newEntity.EpiSupplier = Nothing
            End If

            If EpiTypeKeyMap.ContainsKey(_row.EpiTypeIdRef) Then
                mappedKey = EpiTypeKeyMap(_row.EpiTypeIdRef)
                newEntity.EpiType = (From ety In context.EpiTypes Where ety.EpiTypeId = mappedKey).FirstOrDefault
            Else
                newEntity.EpiType = Nothing
            End If

            If WaferTypeKeyMap.ContainsKey(_row.WaferTypeIdRef) Then
                mappedKey = WaferTypeKeyMap(_row.WaferTypeIdRef)
                newEntity.WaferType = (From wty In context.WaferTypes Where wty.WaferTypeId = mappedKey).FirstOrDefault
            Else
                newEntity.WaferType = Nothing
            End If

            If WaferSizeKeyMap.ContainsKey(_row.WaferSizeId) Then
                mappedKey = WaferSizeKeyMap(_row.WaferSizeId)
                newEntity.WaferSize = (From wsi In context.WaferSizes Where wsi.WaferSizeId = mappedKey).FirstOrDefault
            Else
                newEntity.WaferSize = Nothing
            End If

            If MaskKeyMap.ContainsKey(_row.MaskIdRef) Then
                mappedKey = MaskKeyMap(_row.MaskIdRef)
                newEntity.Mask = (From mas In context.Masks Where mas.MaskId = mappedKey).FirstOrDefault
            Else
                newEntity.Mask = Nothing
            End If

            If LotKeyMap.ContainsKey(_row.LotIdRef) Then
                mappedKey = LotKeyMap(_row.LotIdRef)
                newEntity.Lot = (From lot In context.Lots Where lot.LotId = mappedKey).FirstOrDefault
            Else
                newEntity.Lot = Nothing
            End If

            If ProductKeyMap.ContainsKey(_row.ProductNameIdRef) Then
                mappedKey = ProductKeyMap(_row.ProductNameIdRef)
                newEntity.Product = (From pro In context.Products Where pro.ProductId = mappedKey).FirstOrDefault
            Else
                newEntity.Product = Nothing
            End If

            If ProductGroupKeyMap.ContainsKey(_row.ProductFamilyIdRef) Then
                mappedKey = ProductGroupKeyMap(_row.ProductFamilyIdRef)
                newEntity.ProductGroup = (From pgr In context.ProductGroups Where pgr.ProductGroupId = mappedKey).FirstOrDefault
            Else
                newEntity.ProductGroup = Nothing
            End If

            Select Case _row.QualityIdRef
                Case 1  'Legacy: Excellent
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 4).FirstOrDefault 'Passed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 1).FirstOrDefault  ' No Failure
                Case 2  'Legacy: Passed
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 4).FirstOrDefault  'Passed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 1).FirstOrDefault  ' No Failure
                Case 3  'Legacy: Failed
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 5).FirstOrDefault  'Failed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 16).FirstOrDefault  ' Undefined
                Case 4  'Legacy: Undefined
                    'WaferState will be defined after the table wafer history becomes available
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 1).FirstOrDefault  ' No Failure                                                                            '
                Case 5  'Legacy: Equipment
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 5).FirstOrDefault  'Failed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 2).FirstOrDefault  ' Equipment
                Case 6  'Legacy: Operator
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 5).FirstOrDefault  'Failed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 3).FirstOrDefault  ' Operator
                Case 7  'Legacy: Early Fracture
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 5).FirstOrDefault  'Failed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 4).FirstOrDefault  ' Fracture
                Case 8  'Legacy: Fracture
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 5).FirstOrDefault  'Failed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 4).FirstOrDefault  ' Fracture
                Case 9  'Legacy: Passivation
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 5).FirstOrDefault  'Failed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 5).FirstOrDefault  ' Passivation
                Case 10 'Legacy: AR-Coating
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 5).FirstOrDefault  'Failed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 6).FirstOrDefault  ' AR-Coating
                Case 11 'Legacy: Pad Adhesion
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 5).FirstOrDefault  'Failed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 7).FirstOrDefault  ' Pad Adhesion
                Case 12 'Legacy: Epitaxy
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 5).FirstOrDefault  'Failed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 8).FirstOrDefault  ' Epitaxy
                Case 13 'Legacy: Other
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 5).FirstOrDefault  'Failed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 9).FirstOrDefault  ' Other
                Case 14 'Legacy: Management
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 5).FirstOrDefault  'Failed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 10).FirstOrDefault  ' Management
                Case 15 'Lap Process
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 5).FirstOrDefault  'Failed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 11).FirstOrDefault  ' Lap Process
                Case 16 'Legacy: Wet Lens Process
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 5).FirstOrDefault  'Failed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 12).FirstOrDefault  ' Wet Lens Process
                Case 17 'Legacy: Dry Lens Process
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 5).FirstOrDefault  'Failed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 13).FirstOrDefault  ' Dry Lens process
                Case 18 'Legacy: Wafer Bond/Debond
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 5).FirstOrDefault  'Failed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 14).FirstOrDefault  ' Wafer Bond/Debond
                Case 19 'Subcontractor
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 5).FirstOrDefault  'Failed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 15).FirstOrDefault  ' Subcontractor
                Case Else
                    newEntity.WaferState = (From ws In WaferStates Where ws.WaferStateId = 5).FirstOrDefault  'Failed
                    newEntity.FailureCategory = (From fc In FailureCategories Where fc.FailureCategoryId = 16).FirstOrDefault  ' Undefined
            End Select

            context.Wafers.Add(newEntity)
            context.SaveChanges()
            newKey = newEntity.WaferId
            keyMappings.Add(oldKey, newKey)
        Next
        SaveKeyMapping(keyMappings, "wafer")
    End Sub

    Private Sub MigrateWaferLocationTable()
        Dim _rows As IEnumerable(Of Schema10.WaferLocation)
        _rows = dataService.GetWaferLocations()
        Dim keyMappings As New Dictionary(Of Integer, Integer)
        Dim oldKey As Integer
        Dim newKey As Integer = -1

        For Each _row As Schema10.WaferLocation In _rows
            Dim newEntity = New WaferLocation
            oldKey = _row.LocationId
            newEntity.LocationName = _row.LocationName
            newEntity.SortOrder = _row.SortOrder
            newEntity.Remark = _row.Waferloc_Remark
            context.WaferLocations.Add(newEntity)
            context.SaveChanges()
            newKey = newEntity.WaferLocationId
            keyMappings.Add(oldKey, newKey)
        Next
        SaveKeyMapping(keyMappings, "waferlocation")
    End Sub

    Private Sub MigrateWaferSizeTable()
        Dim _rows As IEnumerable(Of Schema10.WaferSize)
        _rows = dataService.GetWaferSizes()
        Dim keyMappings As New Dictionary(Of Integer, Integer)
        Dim oldKey As Integer
        Dim newKey As Integer = -1

        For Each _row As Schema10.WaferSize In _rows
            Dim newEntity = New WaferSize
            oldKey = _row.WaferSizeId
            newEntity.Description = _row.WaferSize
            context.WaferSizes.Add(newEntity)
            context.SaveChanges()
            newKey = newEntity.WaferSizeId
            keyMappings.Add(oldKey, newKey)
        Next
        SaveKeyMapping(keyMappings, "wafersize")
    End Sub

    Private Sub MigrateWaferStatusTable()
        Dim _rows As IEnumerable(Of Schema10.WaferStatus)
        _rows = dataService.GetWaferStatus()
        Dim mappedKey As Integer


        Dim TechnicianKeyMap As New Dictionary(Of Integer, Integer)
        Dim WaferLocationKeyMap As New Dictionary(Of Integer, Integer)
        Dim WaferKeyMap As New Dictionary(Of Integer, Integer)

        TechnicianKeyMap = GetKeyMapping("technician")
        WaferLocationKeyMap = GetKeyMapping("waferlocation")
        WaferKeyMap = GetKeyMapping("wafer")

        For Each _row As Schema10.WaferStatus In _rows
            Dim newEntity = New WaferHistory
            newEntity.WaferHistoryId = _row.WaferStatusId
            newEntity.StartDate = _row.StartDate
            newEntity.StopDate = _row.StopDate
            newEntity.Remark = _row.Remark

            If TechnicianKeyMap.ContainsKey(_row.TechnicianRef) Then
                mappedKey = TechnicianKeyMap(_row.TechnicianRef)
                newEntity.Technician = (From tec In context.Technicians Where tec.TechnicianId = mappedKey).FirstOrDefault
            Else
                newEntity.Technician = Nothing
            End If

            If WaferLocationKeyMap.ContainsKey(_row.WaferLocationIdRef) Then
                mappedKey = WaferLocationKeyMap(_row.WaferLocationIdRef)
                newEntity.WaferLocation = (From wlo In context.WaferLocations Where wlo.WaferLocationId = mappedKey).FirstOrDefault
            Else
                newEntity.WaferLocation = Nothing
            End If

            If WaferKeyMap.ContainsKey(_row.WaferIdRef) Then
                mappedKey = WaferKeyMap(_row.WaferIdRef)
                newEntity.Wafer = (From waf In context.Wafers Where waf.WaferId = mappedKey).FirstOrDefault
            Else
                newEntity.Wafer = Nothing
            End If

            newEntity.Process = Nothing

            context.WaferHistories.Add(newEntity)
            context.SaveChanges()
        Next

    End Sub

    Private Sub MigrateWaferSupplierTable()
        Dim _rows As IEnumerable(Of Schema10.WaferSupplier)
        _rows = dataService.GetWaferSuppliers()
        Dim keyMappings As New Dictionary(Of Integer, Integer)
        Dim oldKey As Integer
        Dim newKey As Integer = -1

        For Each _row As Schema10.WaferSupplier In _rows
            Dim newEntity = New WaferSupplier
            oldKey = _row.WaferSupplierId
            newEntity.WaferSupplierShortcut = _row.WaferSupplierShortcut
            newEntity.WaferSupplierName = _row.WaferSupplierName
            context.WaferSuppliers.Add(newEntity)
            context.SaveChanges()
            newKey = newEntity.WaferSupplierId
            keyMappings.Add(oldKey, newKey)
        Next
        SaveKeyMapping(keyMappings, "wafersupplier")
    End Sub

    Private Sub MigrateWaferTypeTable()
        Dim _rows As IEnumerable(Of Schema10.WaferType)
        _rows = dataService.GetWaferTypes()
        Dim keyMappings As New Dictionary(Of Integer, Integer)
        Dim oldKey As Integer
        Dim newKey As Integer = -1

        For Each _row As Schema10.WaferType In _rows
            Dim newEntity = New WaferType
            oldKey = _row.WaferTypeId
            newEntity.WaferTypeShortcut = _row.WaferTypeNameShortcut
            newEntity.WaferTypeName = _row.WaferTypeName
            context.WaferTypes.Add(newEntity)
            context.SaveChanges()
            newKey = newEntity.WaferTypeId
            keyMappings.Add(oldKey, newKey)
        Next
        SaveKeyMapping(keyMappings, "wafertype")
    End Sub


#End Region

#Region "Utilities"

    Private Sub SaveKeyMapping(keyMap As Dictionary(Of Integer, Integer), table As String)

        Dim mapPath As String
        Dim tmpFolder As String = "C:\tmp\KeyMaps"
        mapPath = tmpFolder + "\" + table + ".json"
        'Use NewtonSoft JSON to serialize the dictionary to JSON
        Dim jsonMap As String = JsonConvert.SerializeObject(keyMap, Formatting.Indented)
        'Use FileWriter to save the keyMap for later use
        File.WriteAllText(mapPath, jsonMap)

    End Sub

    Private Function GetKeyMapping(table As String) As Dictionary(Of Integer, Integer)

        Dim jsonMap As String
        Dim mapPath As String
        Dim tmpFolder As String = "C:\tmp\KeyMaps"
        mapPath = tmpFolder + "\" + table + ".json"
        If File.Exists(mapPath) Then
            jsonMap = File.ReadAllText(mapPath)
        End If
        Dim keyMap As New Dictionary(Of Integer, Integer)
        keyMap = JsonConvert.DeserializeObject(Of Dictionary(Of Integer, Integer))(jsonMap)
        Return keyMap

    End Function


#End Region



End Class

