Imports System.Data.OleDb
Imports System.IO
Imports System.Web.Http
Imports Microsoft.Office.Interop.Access.Dao
Imports Newtonsoft.Json.Linq
Imports Breeze.ContextProvider
Imports Breeze.ContextProvider.EF6
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
                'MigrateEpiSupplierTable()
            Case "WaferType"
                'MigrateWaferTypeTable()
            Case "WaferSize"
                'MigrateWaferSizeTable()
            Case "WaferSupplier"
                'MigrateWaferSupplierTable()
            Case "WaferState"
                'MigrateWaferStateTable()
            Case "ProductFamiliy"
                'MigrateProductFamiliyTable()
            Case "LotNumber"
                'MigrateLotNumberTable()
            Case "Mask"
                'MigrateMaskTable()
            Case "ProductName"
                'MigrateProductNameTable()
            Case "Wafer"
                'MigrateWaferTable()
            Case "WaferStatus"
                'MigrateWaferStatusTable()
            Case "Technician"
                'MigrateTechnicianTable()
            Case "WaferLocation"
                'MigrateWaferLocationTable()
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

    Public Sub MigrateEpiTypeTable()

        Dim _rows As IEnumerable(Of Schema10.EpiType)
        _rows = dataService.GetEpiTypes()

        For Each _row As Schema10.EpiType In _rows
            Dim newEntity = New EpiType
            newEntity.EpiTypeId = _row.EpiTypeId
            newEntity.EpiTypeShortcut = _row.EpiTypeNameShortcut
            newEntity.EPITypeName = _row.EpiTypeName
            context.EpiTypes.Add(newEntity)
        Next
        context.SaveChanges()

    End Sub


#End Region



End Class

