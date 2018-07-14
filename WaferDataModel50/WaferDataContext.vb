Imports System.Data.Entity
Imports Schema50

Namespace WaferDataModel50

    Friend Class WaferDataContext
        Inherits DbContext

        Shared Sub New()
            'Prevent attempt to initialize a database for this context
            Database.SetInitializer(Of WaferDataContext)(Nothing)
        End Sub

        Public Property Chips As DbSet(Of Chip)
        Public Property ChipStates As DbSet(Of ChipStatus)
        Public Property ChipTypes As DbSet(Of ChipType)
        Public Property Devices As DbSet(Of Device)
        Public Property DevicePositions As DbSet(Of DevicePosition)
        Public Property DeviceStates As DbSet(Of DeviceStatus)
        Public Property DeviceTypes As DbSet(Of DeviceType)
        Public Property Dimensions As DbSet(Of Dimension)
        Public Property Failures As DbSet(Of Failure)
        Public Property Filters As DbSet(Of Filter)
        Public Property Lots As DbSet(Of Lot)
        Public Property Projects As DbSet(Of Project)
        Public Property StatusContexts As DbSet(Of StatusContext)
        Public Property SystemStates As DbSet(Of SystemStatus)
        Public Property Tests As DbSet(Of Test)
        Public Property TestDefinitions As DbSet(Of TestDefinition)
        Public Property TestLists As DbSet(Of TestList)
        Public Property TestPortDefs As DbSet(Of TestPortDefs)
        Public Property TestPortUses As DbSet(Of TestPortUse)
        Public Property TestResults As DbSet(Of TestResult)
        Public Property TestsDone As DbSet(Of TestsDone)
        Public Property TestSystems As DbSet(Of TestSystem)
        Public Property TestSystemParameters As DbSet(Of TestSystemParameters)
        Public Property VisualTests As DbSet(Of VisualTest)
        Public Property VisualTestDefinitions As DbSet(Of VisualTestDefinition)
        Public Property Wafers As DbSet(Of Wafer)

    End Class

End Namespace
