Imports WaferDataLibrary.Models

Public Interface IFileSystemService

    Function CopyFile(source As String, target As String) As Boolean
    Function MoveFile(source As String, target As String) As Boolean
    Function DeleteFile(target As String) As Boolean
    Function ReadTextFile(fileName As String, path As String) As String
    Function WriteTextFile(target As String, text As String) As Boolean
    Function GetDatabasePath(database As String) As String

End Interface
