Imports System.Data.Entity
Imports Schema10 = WaferTrackerLibrary.WaferTrackerModel

Namespace WaferTrackerModel10

    Friend Class WaferTrackerContext
        Inherits DbContext

        Shared Sub New()
            'Prevent attempt to initialize a database for this context
            Database.SetInitializer(Of WaferTrackerContext)(Nothing)
        End Sub

        Public Property EpiSupliers As DbSet(Of Schema10.EpiSupplier)
        Public Property EpiTypes As DbSet(Of Schema10.EpiType)
        Public Property Layers As DbSet(Of Schema10.Layer)
        Public Property Lines As DbSet(Of Schema10.Line)
        Public Property LotNumbers As DbSet(Of Schema10.LotNumber)
        Public Property Masks As DbSet(Of Schema10.Mask)
        Public Property ProcessGroups As DbSet(Of Schema10.ProcessGroup)
        Public Property ProcessSequences As DbSet(Of Schema10.ProcessSequence)
        Public Property ProductFamilies As DbSet(Of Schema10.ProductFamily)
        Public Property ProductNames As DbSet(Of Schema10.ProductName)

        Public Property SPC_Acquisitions As DbSet(Of Schema10.SPC_Acquisition)
        Public Property SPC_Data As DbSet(Of Schema10.SPC_Data)
        Public Property SPC_Data_Types As DbSet(Of Schema10.SPC_Data_Type)
        Public Property SPC_Parameters As DbSet(Of Schema10.SPC_Parameter)

        Public Property Technicians As DbSet(Of Schema10.Technician)

        Public Property Wafers As DbSet(Of Schema10.Wafer)
        Public Property WaferLocations As DbSet(Of Schema10.WaferLocation)
        Public Property WaferQualities As DbSet(Of Schema10.WaferQuality)
        Public Property WaferSizes As DbSet(Of Schema10.WaferSize)
        Public Property WaferStatus As DbSet(Of Schema10.WaferStatus)
        Public Property WaferSuppliers As DbSet(Of Schema10.WaferSupplier)
        Public Property WaferTypes As DbSet(Of Schema10.WaferType)

    End Class

End Namespace
