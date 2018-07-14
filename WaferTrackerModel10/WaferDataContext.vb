Imports System.Data.Entity
Imports Schema10 = WaferTrackerLibrary.WaferTrackerModel

Namespace WaferTrackerModel10

    Friend Class WaferTrackerContext
        Inherits DbContext

        Shared Sub New()
            'Prevent attempt to initialize a database for this context
            Database.SetInitializer(Of WaferTrackerContext)(Nothing)
        End Sub

        Public Property EpiTypes As DbSet(Of Schema10.EpiType)

    End Class

End Namespace
