﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

Partial Public Class WaferTrackerEntities
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=WaferTrackerEntities")
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        Throw New UnintentionalCodeFirstException()
    End Sub

    Public Overridable Property EpiSuppliers() As DbSet(Of EpiSupplier)
    Public Overridable Property EpiTypes() As DbSet(Of EpiType)
    Public Overridable Property FailureCategories() As DbSet(Of FailureCategory)
    Public Overridable Property Layers() As DbSet(Of Layer)
    Public Overridable Property Lines() As DbSet(Of Line)
    Public Overridable Property Lots() As DbSet(Of Lot)
    Public Overridable Property Masks() As DbSet(Of Mask)
    Public Overridable Property ProcessGroups() As DbSet(Of ProcessGroup)
    Public Overridable Property ProcessSequences() As DbSet(Of ProcessSequence)
    Public Overridable Property ProductFamilies() As DbSet(Of ProductFamily)
    Public Overridable Property ProductNames() As DbSet(Of ProductName)
    Public Overridable Property SPCAcquisitions() As DbSet(Of SPCAcquisition)
    Public Overridable Property SPCDatas() As DbSet(Of SPCData)
    Public Overridable Property SPCDataTypes() As DbSet(Of SPCDataType)
    Public Overridable Property SPCParameters() As DbSet(Of SPCParameter)
    Public Overridable Property Technicians() As DbSet(Of Technician)
    Public Overridable Property Wafers() As DbSet(Of Wafer)
    Public Overridable Property WaferHistories() As DbSet(Of WaferHistory)
    Public Overridable Property WaferLocations() As DbSet(Of WaferLocation)
    Public Overridable Property WaferSizes() As DbSet(Of WaferSize)
    Public Overridable Property WaferStates() As DbSet(Of WaferState)
    Public Overridable Property WaferSuppliers() As DbSet(Of WaferSupplier)
    Public Overridable Property WaferTypes() As DbSet(Of WaferType)

End Class
