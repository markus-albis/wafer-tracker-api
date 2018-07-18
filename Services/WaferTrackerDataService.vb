Imports Dapper
Imports Schema10 = WaferTrackerLibrary.WaferTrackerModel

Namespace Services

    Public Class WaferTrackerDataService

        Private conn As IDbConnection
        Private _waferId As Integer = 1
        Private database As String

        Public Sub New()
            database = My.Settings.Access97Database
        End Sub

        Public Function GetEpiSuppliers() As IEnumerable(Of Schema10.EpiSupplier)
            Dim _queryResult As IEnumerable(Of Schema10.EpiSupplier)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Schema10.EpiSupplier)("Select * FROM EpiSupplier")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetEpiTypes() As IEnumerable(Of Schema10.EpiType)
            Dim _queryResult As IEnumerable(Of Schema10.EpiType)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Schema10.EpiType)("Select * FROM EpiType")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetLotNumbers() As IEnumerable(Of Schema10.LotNumber)
            Dim _queryResult As IEnumerable(Of Schema10.LotNumber)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Schema10.LotNumber)("Select * FROM LotNumber")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetMasks() As IEnumerable(Of Schema10.Mask)
            Dim _queryResult As IEnumerable(Of Schema10.Mask)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Schema10.Mask)("Select * FROM Mask")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetProductFamilies() As IEnumerable(Of Schema10.ProductFamily)
            Dim _queryResult As IEnumerable(Of Schema10.ProductFamily)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Schema10.ProductFamily)("Select * FROM ProductFamily")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetProductNames() As IEnumerable(Of Schema10.ProductName)
            Dim _queryResult As IEnumerable(Of Schema10.ProductName)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Schema10.ProductName)("Select * FROM ProductName")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetTechnicians() As IEnumerable(Of Schema10.Technician)
            Dim _queryResult As IEnumerable(Of Schema10.Technician)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Schema10.Technician)("Select * FROM Technician")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetWafers() As IEnumerable(Of Schema10.Wafer)
            Dim _queryResult As IEnumerable(Of Schema10.Wafer)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Schema10.Wafer)("Select * FROM Wafer")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetWaferLocations() As IEnumerable(Of Schema10.WaferLocation)
            Dim _queryResult As IEnumerable(Of Schema10.WaferLocation)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Schema10.WaferLocation)("Select * FROM WaferLocation")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetWaferQualities() As IEnumerable(Of Schema10.WaferQuality)
            Dim _queryResult As IEnumerable(Of Schema10.WaferQuality)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Schema10.WaferQuality)("Select * FROM WaferQuality")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetWaferSizes() As IEnumerable(Of Schema10.WaferSize)
            Dim _queryResult As IEnumerable(Of Schema10.WaferSize)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Schema10.WaferSize)("Select * FROM WaferSize")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetWaferStatus() As IEnumerable(Of Schema10.WaferStatus)
            Dim _queryResult As IEnumerable(Of Schema10.WaferStatus)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Schema10.WaferStatus)("Select * FROM WaferStatus")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetWaferSuppliers() As IEnumerable(Of Schema10.WaferSupplier)
            Dim _queryResult As IEnumerable(Of Schema10.WaferSupplier)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Schema10.WaferSupplier)("Select * FROM WaferSupplier")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetWaferTypes() As IEnumerable(Of Schema10.WaferType)
            Dim _queryResult As IEnumerable(Of Schema10.WaferType)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Schema10.WaferType)("Select * FROM WaferType")
            conn.Close()
            Return _queryResult
        End Function


#Region "Helper Methods"

        Private Function GetConnection(db As String) As IDbConnection
            Dim _cnxString As String = String.Empty
            If db.Contains(".mdx") Then
                _cnxString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & db & ";"
            End If
            Dim _connection As IDbConnection = New OleDb.OleDbConnection(_cnxString)
            Return _connection
        End Function

#End Region

    End Class

End Namespace
