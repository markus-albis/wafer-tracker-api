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

        Public Function GetEpiTypes() As IEnumerable(Of Schema10.EpiType)
            Dim _queryResult As IEnumerable(Of Schema10.EpiType)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Schema10.EpiType)("Select * FROM EpiType")
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
