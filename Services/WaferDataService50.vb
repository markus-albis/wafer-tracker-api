Imports Dapper
Imports Dapper.Contrib
Imports Breeze.ContextProvider
Imports Schema50 = WaferTrackingLibrary.WaferDataModel50
Imports Breeze.ContextProvider.EF6
Imports WaferDataLibrary.WaferDataModel40
Imports Newtonsoft.Json.Linq
Imports System.Data.Entity
Imports Dapper.Contrib.Extensions
Imports WaferTrackerLibrary.WaferDataModel50

Namespace Services

    Public Class WaferDataService50

        Private conn As IDbConnection
        Private _waferId As Integer = 1

        Public Function GetChips(database As String) As IEnumerable(Of Chip)
            Dim _queryResult As IEnumerable(Of Chip)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Chip)("Select * FROM Chip")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetChipsByWafer(waferid As Integer, database As String) As IEnumerable(Of Chip)
            Dim _queryResult As IEnumerable(Of Chip)
            conn = GetConnection(database)
            conn.Open()
            Dim _query As String = String.Format("Select * FROM Chip WHERE WaferId = {0}", waferid)

            _queryResult = conn.Query(Of Chip)(_query)
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetChipsBySplitWafer(waferId As Integer, splitX As Integer, splitY As Integer, database As String) As IEnumerable(Of Chip)
            Dim _tmpResult As IEnumerable(Of Chip)
            Dim _tmpList As List(Of Chip)
            Dim _queryResult As IEnumerable(Of Chip)
            Dim _query As String
            conn = GetConnection(database)
            conn.Open()

            _query = String.Format("Select * FROM Chip WHERE WaferId = {0}", waferId)
            _tmpResult = conn.Query(Of Chip)(_query)

            'Regular case, no splits
            If splitX <= 1 And splitY <= 1 Then
                _queryResult = _tmpResult
                conn.Close()
                Return _queryResult
            End If

            'We have a wafer that needs to be split in x-direction
            If splitX > 1 Then
                _tmpList = New List(Of Chip)
                _query = String.Format("Select Device.DeviceId As ChipId, Device.ChipId As ChipNumber, Chip.ChipXPosition, Chip.ChipYPosition, Chip.ChipStatusCode, Chip.ChipTypeCode  FROM Device Inner Join Chip On Device.ChipId=Chip.ChipId  WHERE Chip.WaferId = {0} ORDER BY Device.DeviceId", waferId)
                _tmpResult = conn.Query(Of Chip)(_query)

                For Each c In _tmpResult
                    Dim chip As New Chip
                    chip.ChipId = c.ChipId
                    chip.ChipNumber = c.ChipId
                    chip.ChipXPosition = (c.ChipXPosition * splitX) - (c.ChipId Mod 2) + 1
                    chip.ChipYPosition = c.ChipYPosition
                    chip.ChipStatusId = c.ChipStatusId
                    chip.ChipTypeId = c.ChipTypeId
                    _tmpList.Add(chip)
                Next

                _queryResult = _tmpList
                conn.Close()
                Return _queryResult
            End If

            'We have a wafer that needs to be split in y-direction
            If splitY > 1 Then
                _tmpList = New List(Of Chip)
                _query = String.Format("Select Device.DeviceId As ChipId, Device.ChipId As ChipNumber, Chip.ChipXPosition, Chip.ChipYPosition, Chip.ChipStatusCode, Chip.ChipTypeCode  FROM Device Inner Join Chip On Device.ChipId=Chip.ChipId  WHERE Chip.WaferId = {0}", waferId)
                _tmpResult = conn.Query(Of Chip)(_query)

                For Each c In _tmpResult
                    Dim chip As New Chip
                    chip.ChipId = c.ChipId
                    chip.ChipNumber = (c.ChipNumber * splitY) - (c.ChipNumber Mod 2)
                    chip.ChipXPosition = (c.ChipXPosition * splitY) - (c.ChipId Mod 2)
                    chip.ChipYPosition = c.ChipYPosition
                    chip.ChipStatusId = c.ChipStatusId
                    chip.ChipTypeId = c.ChipTypeId
                    _tmpList.Add(chip)
                Next

                _queryResult = _tmpList
                conn.Close()
                Return _queryResult

            End If

        End Function

        Public Function GetChipStates(database As String) As IEnumerable(Of ChipStatus)
            Dim _queryResult As IEnumerable(Of ChipStatus)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of ChipStatus)("Select * FROM ChipStatus")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetChipTypes() As IEnumerable(Of ChipType)
            Dim _queryResult As IEnumerable(Of ChipType)
            conn.Open()
            _queryResult = conn.Query(Of ChipType)("Select * FROM ChipType")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetDevices(database As String) As IEnumerable(Of Device)
            Dim _queryResult As IEnumerable(Of Device)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Device)("Select * FROM Device")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetDevicesByWafer(waferid As Integer, database As String) As IEnumerable(Of Device)
            Dim _queryResult As IEnumerable(Of Device)
            conn = GetConnection(database)
            conn.Open()
            Dim _query As String = String.Format("SELECT Device.DeviceId, Device.ChipId, Device.PositionId, Device.DeviceTypeId, Device.DeviceStatusId FROM Chip INNER JOIN Device ON Chip.[ChipId] = Device.[ChipId] WHERE Chip.WaferId = {0};", waferid)

            _queryResult = conn.Query(Of Device)(_query)
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetDevicePositions(database As String) As IEnumerable(Of DevicePosition)
            Dim _queryResult As IEnumerable(Of DevicePosition)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of DevicePosition)("Select * FROM DevicePosition")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetDimensions(database As String) As IEnumerable(Of Dimension)
            Dim _queryResult As IEnumerable(Of Dimension)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Dimension)("Select * FROM Dimension")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetFailures(database As String, testid As Integer, sql As String) As IEnumerable(Of Failure)
            Dim _queryResult As IEnumerable(Of Failure)
            conn = GetConnection(database)
            conn.Open()

            Dim baseQuery As String = "Select Device.DeviceId, Chip.ChipId, Chip.ChipXPosition, Chip.ChipYPosition FROM (Chip INNER JOIN Device On Chip.ChipId = Device.ChipId) INNER JOIN (Test INNER JOIN TestResult On Test.TestId = TestResult.TestId) On Device.DeviceId = Test.DeviceId "

            Dim _query As String = String.Format("{0} Where Test.TestDefinitionId = {1} And {2}", baseQuery, testid, sql)
            _queryResult = conn.Query(Of Failure)(_query)
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetFilters(database As String) As IEnumerable(Of Filter)
            Dim _queryResult As IEnumerable(Of Filter)
            conn = GetConnection(database)
            conn.Open()
            Dim _query As String = String.Format("Select * FROM Filter")
            _queryResult = conn.Query(Of Filter)(_query)
            conn.Close()
            Return _queryResult
        End Function

        Public ReadOnly Property LocalMetadata As String
            Get
                Dim _contextProvider As EFContextProvider(Of WaferDataContext) = New EFContextProvider(Of WaferDataContext)()
                Dim _metadata As String
                Try
                    _metadata = _contextProvider.Metadata()
                    Return _metadata
                Catch ex As Exception
                    Console.WriteLine("Error fetching metadata: " + ex.Message + ex.InnerException.ToString)
                End Try
                Return _contextProvider.Metadata()
            End Get
        End Property

        Public Function GetProjects(database As String) As IEnumerable(Of Project)
            Dim _queryResult As IEnumerable(Of Project)
            conn = GetConnection(database)
            Try
                conn.Open()
                _queryResult = conn.Query(Of Project)("Select * FROM Project")
                conn.Close()
            Catch ex As OleDb.OleDbException
                _queryResult = Nothing
                Console.WriteLine(ex.Message, ex.InnerException.ToString)
            End Try
            Return _queryResult
        End Function

        Public Function GetLots(database As String) As IEnumerable(Of Lot)
            Dim _queryResult As IEnumerable(Of Lot)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Lot)("Select * FROM Lot")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetTestDefinitions(database As String) As IEnumerable(Of TestDefinition)
            Dim _queryResult As IEnumerable(Of TestDefinition)
            conn = GetConnection(database)
            conn.Open()
            Dim _query As String = String.Format("Select * FROM TestDefinition")
            _queryResult = conn.Query(Of TestDefinition)(_query)
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetTestsDone(database As String) As IEnumerable(Of TestsDone)
            Dim _queryResult As IEnumerable(Of TestsDone)
            conn = GetConnection(database)
            conn.Open()
            Dim _query As String = String.Format("Select * FROM TestsDone")
            _queryResult = conn.Query(Of TestsDone)(_query)
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetTests(database As String) As IEnumerable(Of Test)
            Dim _queryResult As IEnumerable(Of Test)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of Test)("Select * FROM Test")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetTestsByTestsDone(tdid As Integer, database As String) As IEnumerable(Of Test)
            Dim _queryResult As IEnumerable(Of Test)
            conn = GetConnection(database)
            conn.Open()
            Dim _query As String = String.Format("Select * FROM Test WHERE Test.TestsDoneId = {0};", tdid)
            _queryResult = conn.Query(Of Test)(_query)
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetTestLists(database As String) As IEnumerable(Of TestList)
            Dim _queryResult As IEnumerable(Of TestList)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of TestList)("Select * FROM TestList")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetTestPortDefs(database As String) As IEnumerable(Of TestPortDefs)
            Dim _queryResult As IEnumerable(Of TestPortDefs)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of TestPortDefs)("Select * FROM TestPortDefs")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetTestPortUses(database As String) As IEnumerable(Of TestPortUse)
            Dim _queryResult As IEnumerable(Of TestPortUse)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of TestPortUse)("Select * FROM TestPortUse")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetTestResults(database As String) As IEnumerable(Of TestResult)
            Dim _queryResult As IEnumerable(Of TestResult)
            conn = GetConnection(database)
            conn.Open()
            _queryResult = conn.Query(Of TestResult)("Select * FROM TestResult")
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetTestResultsByTestsDone(tdid As Integer, database As String) As IEnumerable(Of TestResult)
            Dim _queryResult As IEnumerable(Of TestResult)
            conn = GetConnection(database)
            conn.Open()
            Dim baseQuery As String = "SELECT TestResult.TestResultId, TestResult.TestId, TestResult.XValue, TestResult.YValue From Test INNER Join TestResult On Test.[TestId] = TestResult.[TestId]"

            Dim _query As String = String.Format("{0} WHERE Test.TestsDoneId = {1};", baseQuery, tdid)
            _queryResult = conn.Query(Of TestResult)(_query)
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetSystemStates(database As String) As IEnumerable(Of SystemStatus)
            Dim _queryResult As IEnumerable(Of SystemStatus)
            conn = GetConnection(database)
            conn.Open()
            Dim _query As String = "Select * FROM SystemStatus"
            _queryResult = conn.Query(Of SystemStatus)(_query)
            conn.Close()
            Return _queryResult
        End Function

        Public Function GetWafers(database As String) As IEnumerable(Of Wafer)
            Dim _queryResult As IEnumerable(Of Wafer)
            conn = GetConnection(database)
            conn.Open()
            'Dim _query As String = "Select WaferId, RunName, WaferType AS Type, Orientation, RowsPerWafer, ColumnsPerWafer, Description FROM Wafer"
            Dim _query As String = "Select * FROM Wafer"
            _queryResult = conn.Query(Of Wafer)(_query)
            conn.Close()
            Return _queryResult
        End Function


#Region "Named Breeze Table Updates"

        Public Function UpdateChipTable(saveMap As Dictionary(Of Type, List(Of EntityInfo)), saveOptions As SaveOptions) As SaveResult
            Dim _database As String = saveOptions.Tag
            Dim _entityErrors As List(Of EntityError) = New List(Of EntityError)
            Dim _query As String = String.Empty
            Dim keyMappings As New List(Of KeyMapping)

            conn = GetConnection(_database)
            conn.Open()

            For Each _entity In saveMap(GetType(Chip))                     'We should have only device entries
                Dim entry As Chip = CType(_entity.Entity, Chip)
                Dim _updateQuery As String = String.Format("UPDATE Chip SET ChipStatusId = {0} WHERE ChipId = {1}", entry.ChipStatusId, entry.ChipId)
                Try
                    conn.Execute(_updateQuery)
                Catch ex As OleDb.OleDbException
                    Dim _entityError As New EntityError With {.EntityTypeName = entry.GetType.FullName, .ErrorMessage = ex.Message, .ErrorName = ex.ErrorCode}
                    _entityErrors.Add(_entityError)
                End Try
            Next
            conn.Close()
            Return ToSaveResult(_entityErrors, Nothing, saveMap) 'We have to return a SaveResult to the client, no Keymappings required
        End Function

        Public Function UpdateDeviceTable(saveMap As Dictionary(Of Type, List(Of EntityInfo)), saveOptions As SaveOptions) As SaveResult
            Dim _database As String = saveOptions.Tag
            Dim _entityErrors As List(Of EntityError) = New List(Of EntityError)
            Dim _query As String = String.Empty
            Dim keyMappings As New List(Of KeyMapping)

            conn = GetConnection(_database)
            conn.Open()

            For Each _entity In saveMap(GetType(Device))                     'We should have only device entries
                Dim entry As Device = CType(_entity.Entity, Device)
                Dim _updateQuery As String = String.Format("UPDATE Device SET DeviceStatusId = {0} WHERE DeviceId = {1}", entry.DeviceStatusId, entry.DeviceId)
                Try
                    conn.Execute(_updateQuery)
                Catch ex As OleDb.OleDbException
                    Dim _entityError As New EntityError With {.EntityTypeName = entry.GetType.FullName, .ErrorMessage = ex.Message, .ErrorName = ex.ErrorCode}
                    _entityErrors.Add(_entityError)
                End Try

            Next
            conn.Close()
            Return ToSaveResult(_entityErrors, Nothing, saveMap) 'We have to return a SaveResult to the client, no Keymappings required
        End Function

        Public Function UpdateSystemStatusTable(saveMap As Dictionary(Of Type, List(Of EntityInfo)), saveOptions As SaveOptions) As SaveResult
            Dim _database As String = saveOptions.Tag
            Dim _entityErrors As List(Of EntityError) = New List(Of EntityError)
            Dim _query As String = String.Empty
            Dim keyMappings As New List(Of KeyMapping)

            conn = GetConnection(_database)
            conn.Open()
            For Each _entity In saveMap(GetType(SystemStatus))                     'We should have only device entries
                Dim entry As SystemStatus = CType(_entity.Entity, SystemStatus)
                Dim _updateQuery As String = String.Format("UPDATE SystemStatus SET CurrentWaferId = {0}, CurrentChipId = {1}, CurrentDeviceId = {2} WHERE SystemStatusId = 1", entry.CurrentWaferId, entry.CurrentChipId, entry.CurrentDeviceId)
                Try
                    conn.Execute(_updateQuery)
                Catch ex As OleDb.OleDbException
                    Dim _entityError As New EntityError With {.EntityTypeName = entry.GetType.FullName, .ErrorMessage = ex.Message, .ErrorName = ex.ErrorCode}
                    _entityErrors.Add(_entityError)
                End Try
            Next
            conn.Close()
            Return ToSaveResult(_entityErrors, Nothing, saveMap) 'We have to return a SaveResult to the client, no Keymappings required
        End Function

        Public Function UpdateFilterTable(saveMap As Dictionary(Of Type, List(Of EntityInfo)), saveOptions As SaveOptions) As SaveResult
            Dim _database As String = saveOptions.Tag
            Dim _entityErrors As List(Of EntityError) = New List(Of EntityError)
            Dim _query As String = String.Empty
            Dim keyMappings As New List(Of KeyMapping)

            '    'Open the database connection
            conn = GetConnection(_database)
            conn.Open()

            '    'Modify, delete or insert entities
            For Each _entity In saveMap(GetType(Filter))                     'We should have only filter entries
                Dim entry As Filter = CType(_entity.Entity, Filter)
                'Modify
                If _entity.EntityState = Breeze.ContextProvider.EntityState.Modified Then
                    Dim _updateQuery As String = String.Format("UPDATE Filter SET [SQL] = '{0}' WHERE FilterId = {1}", entry.SQL, entry.FilterId)
                    Try
                        conn.Execute(_updateQuery)
                    Catch ex As OleDb.OleDbException
                        Dim _entityError As New EntityError With {.EntityTypeName = entry.GetType.FullName, .ErrorMessage = ex.Message, .ErrorName = ex.ErrorCode}
                        _entityErrors.Add(_entityError)
                    End Try
                End If
                'Delete
                If _entity.EntityState = Breeze.ContextProvider.EntityState.Deleted Then
                    Dim _deleteQuery As String = String.Format("DELETE FROM Filter WHERE FilterId = {1}", entry.FilterId)
                    Try
                        conn.Execute(_deleteQuery)
                    Catch ex As OleDb.OleDbException
                        Dim _entityError As New EntityError With {.EntityTypeName = entry.GetType.FullName, .ErrorMessage = ex.Message, .ErrorName = ex.ErrorCode}
                        _entityErrors.Add(_entityError)
                    End Try
                End If
                'Insert
                If _entity.EntityState = Breeze.ContextProvider.EntityState.Added Then
                    Dim keyMapping As New KeyMapping With {.EntityTypeName = entry.GetType.FullName, .TempValue = entry.FilterId}
                    Dim _insertQuery As String = String.Format("INSERT INTO Filter (FilterId, TestDefinitionId, SQL) VALUES ({0}, {1}, '{2}')", entry.FilterId, entry.TestDefinitionId, entry.SQL)
                    Try
                        conn.Query(_insertQuery)
                        _query = String.Format("SELECT @@IDENTITY")
                        Dim newkeyValue As Integer = conn.ExecuteScalar(_query)
                        keyMapping.RealValue = newkeyValue
                        keyMappings.Add(keyMapping)
                        entry.FilterId = newkeyValue    'We replace the temporary key with the real key before we return the entities back to the client
                    Catch ex As OleDb.OleDbException
                        Dim _entityError As New EntityError With {.EntityTypeName = entry.GetType.FullName, .ErrorMessage = ex.Message, .ErrorName = ex.ErrorCode}
                        _entityErrors.Add(_entityError)
                    End Try
                End If
            Next

            'Close database connection
            conn.Close()
            'Return a SaveResult
            Dim saveResult As SaveResult
            If keyMappings.Count > 0 Then
                saveResult = ToSaveResult(_entityErrors, keyMappings, saveMap)
            Else
                saveResult = ToSaveResult(_entityErrors, Nothing, saveMap)     'No keyMappings
            End If
            'Return a SaveResult
            Return saveResult
        End Function

        Public Function UpdateTestsDoneTable(saveMap As Dictionary(Of Type, List(Of EntityInfo)), saveOptions As SaveOptions) As SaveResult
            Dim _database As String = saveOptions.Tag
            Dim _entityErrors As List(Of EntityError) = New List(Of EntityError)
            Dim _query As String = String.Empty
            Dim keyMappings As New List(Of KeyMapping)

            'Open the database connection
            conn = GetConnection(_database)
            conn.Open()

            'Modify, delete or insert entities
            For Each _entity In saveMap(GetType(TestsDone))                     'We should have only testsdone entries
                Dim entry As TestsDone = CType(_entity.Entity, TestsDone)
                'Modify
                If _entity.EntityState = Breeze.ContextProvider.EntityState.Modified Then
                    'To do
                End If
                'Delete
                If _entity.EntityState = Breeze.ContextProvider.EntityState.Deleted Then
                    'To do
                End If
                'Insert
                If _entity.EntityState = Breeze.ContextProvider.EntityState.Added Then
                    Dim keyMapping As New KeyMapping With {.EntityTypeName = entry.GetType.FullName, .TempValue = entry.TestsDoneId}
                    _query = String.Format("INSERT INTO TestsDone (WaferId, TestDefinitionId, TestComment, TestDate) VALUES ({0}, {1}, '{2}', #{3}#)", entry.WaferId, entry.TestDefinitionId, entry.TestComment, entry.TestDate)
                    Try
                        conn.Query(_query)
                        _query = String.Format("SELECT @@IDENTITY")
                        Dim newkeyValue As Integer = conn.ExecuteScalar(_query)
                        keyMapping.RealValue = newkeyValue
                        keyMappings.Add(keyMapping)
                        entry.TestsDoneId = newkeyValue    'We replace the temporary key with the real key before we return the entities back to the client
                    Catch ex As OleDb.OleDbException
                        Dim _entityError As New EntityError With {.EntityTypeName = entry.GetType.FullName, .ErrorMessage = ex.Message, .ErrorName = ex.ErrorCode}
                        _entityErrors.Add(_entityError)
                    End Try
                End If
            Next

            'Close database connection
            conn.Close()
            'Return a SaveResult
            Dim saveResult As SaveResult
            If keyMappings.Count > 0 Then
                saveResult = ToSaveResult(_entityErrors, keyMappings, saveMap)
            Else
                saveResult = ToSaveResult(_entityErrors, Nothing, saveMap)     'No keyMappings
            End If
            'Return a SaveResult
            Return saveResult
        End Function

        Public Function UpdateTestListTable(saveMap As Dictionary(Of Type, List(Of EntityInfo)), saveOptions As SaveOptions) As SaveResult
            Dim _database As String = saveOptions.Tag
            Dim _entityErrors As List(Of EntityError) = New List(Of EntityError)
            Dim _query As String = String.Empty
            Dim keyMappings As New List(Of KeyMapping)

            'Open the database connection
            conn = GetConnection(_database)
            conn.Open()

            'Modify, delete or insert entities
            For Each _entity In saveMap(GetType(TestList))                     'We should have only testlist entries
                Dim _entry As TestList = CType(_entity.Entity, TestList)
                'Modify
                If _entity.EntityState = Breeze.ContextProvider.EntityState.Modified Then
                    _query = String.Format("UPDATE TestList SET TestOrder = {0} WHERE TestListId = {1}", _entry.TestOrder, _entry.TestListId)
                    Try
                        conn.Execute(_query)
                    Catch ex As OleDb.OleDbException
                        Dim _entityError As New EntityError With {.EntityTypeName = _entry.GetType.FullName, .ErrorMessage = ex.Message, .ErrorName = ex.ErrorCode}
                        _entityErrors.Add(_entityError)
                    End Try
                End If
                'Delete
                If _entity.EntityState = Breeze.ContextProvider.EntityState.Deleted Then
                    '_query = String.Format("DELETE FROM TestList WHERE TestListId = {0}", _entry.TestListId)
                    Try
                        Dim _isSuccess As Boolean = conn.Delete(_entry)
                    Catch ex As OleDb.OleDbException
                        Dim _entityError As New EntityError With {.EntityTypeName = _entry.GetType.FullName, .ErrorMessage = ex.Message, .ErrorName = ex.ErrorCode}
                        _entityErrors.Add(_entityError)
                    End Try

                End If
                'Insert
                If _entity.EntityState = Breeze.ContextProvider.EntityState.Added Then
                    Dim _keyMapping As New KeyMapping With {.EntityTypeName = _entry.GetType.FullName, .TempValue = _entry.TestListId}
                    _query = String.Format("INSERT INTO TestList (Description, TestDefinitionId, TestOrder) VALUES ('{0}', {1}, {2})", _entry.Description, _entry.TestDefinitionId, _entry.TestOrder)
                    Try
                        conn.Query(_query)
                        _query = String.Format("SELECT @@IDENTITY")
                        Dim newkeyValue As Integer = conn.ExecuteScalar(_query)
                        _keyMapping.RealValue = newkeyValue
                        keyMappings.Add(_keyMapping)
                        _entry.TestListId = newkeyValue    'We replace the temporary key with the real key before we return the entities back to the client
                    Catch ex As OleDb.OleDbException
                        Dim _entityError As New EntityError With {.EntityTypeName = _entry.GetType.FullName, .ErrorMessage = ex.Message, .ErrorName = ex.ErrorCode}
                        _entityErrors.Add(_entityError)
                    End Try
                End If
            Next
            'Close database connection
            conn.Close()
            'Return a SaveResult
            Dim saveResult As SaveResult
            If keyMappings.Count > 0 Then
                saveResult = ToSaveResult(_entityErrors, keyMappings, saveMap)
            Else
                saveResult = ToSaveResult(_entityErrors, Nothing, saveMap)     'No keyMappings
            End If
            Return saveResult

        End Function

        Public Function UpdateTestResultsTable(saveMap As Dictionary(Of Type, List(Of EntityInfo)), saveOptions As SaveOptions) As SaveResult

            Dim _database As String = saveOptions.Tag
            Dim entityErrors As List(Of EntityError) = New List(Of EntityError)
            Dim _query As String = String.Empty
            Dim keyMappings As New List(Of KeyMapping)

            'Open the database connection
            conn = GetConnection(_database)
            conn.Open()

            'Modify, delete or insert test entities
            For Each entity1 In saveMap(GetType(Test))                     'We filter for test entries
                Dim entry1 As Test = CType(entity1.Entity, Test)

                'Modify test as well as matching test results
                If entity1.EntityState = Breeze.ContextProvider.EntityState.Modified Then
                    'To do
                End If

                'Delete test results and matching test
                If entity1.EntityState = Breeze.ContextProvider.EntityState.Deleted Then
                    'To do
                End If

                'Insert test as well as the matching test results
                If entity1.EntityState = Breeze.ContextProvider.EntityState.Added Then
                    Dim newkeyValue1 As Integer     ' Holds new key of test entities
                    Dim newkeyValue2 As Integer     ' Holds new key of testresult entities
                    Dim keyMapping1 As New KeyMapping With {.EntityTypeName = entry1.GetType.FullName, .TempValue = entry1.TestId}
                    _query = String.Format("INSERT INTO Test (DeviceId, TestDefinitionId, TestsDoneId, TestTime) VALUES ('{0}', {1}, {2}, #{3}#)", entry1.DeviceId, entry1.TestDefinitionId, entry1.TestsDoneId, entry1.TestTime)
                    Try
                        conn.Query(_query)
                        _query = String.Format("SELECT @@IDENTITY")
                        newkeyValue1 = conn.ExecuteScalar(_query)
                        keyMapping1.RealValue = newkeyValue1
                        keyMappings.Add(keyMapping1)
                    Catch ex As OleDb.OleDbException
                        Dim _entityError As New EntityError With {.EntityTypeName = entry1.GetType.FullName, .ErrorMessage = ex.Message, .ErrorName = ex.ErrorCode}
                        entityErrors.Add(_entityError)
                    End Try
                    'Get the matching testresult entities (nice linq example)
                    Dim entities2 = From tr In saveMap(GetType(TestResult)) Where CType(tr.Entity, TestResult).TestId = entry1.TestId
                    For Each entity2 In entities2
                        Dim entry2 As TestResult = CType(entity2.Entity, TestResult)
                        Dim keyMapping2 As New KeyMapping With {.EntityTypeName = entry2.GetType.FullName, .TempValue = entry2.TestResultId}
                        entry2.TestId = keyMapping1.RealValue      'We got the key after saving the test and we need this key to store the matching test results
                        _query = String.Format("INSERT INTO TestResult (TestId, XValue, YValue) VALUES ({0}, {1}, {2})", entry2.TestId, entry2.XValue, entry2.YValue)
                        Try
                            conn.Query(_query)
                            _query = String.Format("SELECT @@IDENTITY")
                            newkeyValue2 = conn.ExecuteScalar(_query)
                            keyMapping2.RealValue = newkeyValue2
                            keyMappings.Add(keyMapping2)
                        Catch ex As OleDb.OleDbException
                            Dim _entityError As New EntityError With {.EntityTypeName = entry2.GetType.FullName, .ErrorMessage = ex.Message, .ErrorName = ex.ErrorCode}
                            entityErrors.Add(_entityError)
                        End Try
                    Next
                    entry1.TestId = newkeyValue1        'We replace the temporary key with the real key before we return the entities back to the client
                End If
            Next
            'Close database connection
            conn.Close()
            'Return a SaveResult
            Dim saveResult As SaveResult
            If keyMappings.Count > 0 Then
                saveResult = ToSaveResult(entityErrors, keyMappings, saveMap)
            Else
                saveResult = ToSaveResult(entityErrors, Nothing, saveMap)     'No keyMappings
            End If
            Return saveResult
        End Function

#End Region

#Region "Helper Methods"

        Public Function ToSaveResult(entityErrors As List(Of EntityError), keyMappings As List(Of KeyMapping), saveMap As Dictionary(Of Type, List(Of EntityInfo))) As SaveResult
            If entityErrors IsNot Nothing And entityErrors.Count <> 0 Then
                Return New SaveResult() With {.Errors = entityErrors.Cast(Of Object)().ToList()}
            Else
                Dim entities = saveMap.SelectMany(Function(ei) ei.Value.Select(Function(entityInfo) entityInfo.Entity)).ToList()
                If keyMappings Is Nothing Then
                    keyMappings = New List(Of KeyMapping)
                End If
                Return New SaveResult() With {.Entities = entities, .KeyMappings = keyMappings}
            End If
        End Function

        Private Function GetConnection(db As String) As IDbConnection
            Dim _cnxString As String = String.Empty
            If db.Contains(".mdb") Then
                _cnxString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & db & ";"
            End If
            If db.Contains(".accdb") Then
                _cnxString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" & db & ";"
            End If
            Dim _connection As IDbConnection = New OleDb.OleDbConnection(_cnxString)
            Return _connection
        End Function

#End Region

    End Class

End Namespace
