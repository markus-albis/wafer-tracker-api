Imports System.IO
Imports WaferDataLibrary.Models
Imports WaferDataLibrary.Common

Namespace Services

Public Class FileSystemService
		Implements IFileSystemService

        Public Function CopyFile(source As String, destination As String) As Boolean Implements IFileSystemService.CopyFile
            If System.IO.File.Exists(source) = True Then
                Try
                    System.IO.File.Copy(source, destination)
                    Debug.WriteLine("File copy ....")
                    Return True
                Catch
                    Return False
                End Try
            End If
            Debug.WriteLine("File copy failed (file does not exist): " & source & "," & destination)
            Return False
        End Function

        Public Function MoveFile(source As String, destination As String) As Boolean Implements IFileSystemService.MoveFile
            If System.IO.File.Exists(source) = True Then
                Try
                    Debug.WriteLine("File move ...")
                    System.IO.File.Move(source, destination)
                    Return True
                Catch ex As Exception
                    Debug.WriteLine("File move failed: " & ex.Message)
                    Return False
                End Try
            End If
            Debug.WriteLine("File move failed (file does not exist): " & source & "," & destination)
            Return False
        End Function

        Public Function DeleteFile(target As String) As Boolean Implements IFileSystemService.DeleteFile
            If System.IO.File.Exists(target) = True Then
                Try
                    System.IO.File.Delete(target)
                    Return True
                Catch
                    Debug.WriteLine("File delete failed " & target)
                    Return False
                End Try
            End If
            Debug.WriteLine("File delete failed (file does not exist) " & target)
            Return False
        End Function

        Public Function ReadTextFile(fileName As String, path As String) As String Implements IFileSystemService.ReadTextFile
            Dim content As String
            Dim filePath As String = path & "\" & fileName
            Try
                content = File.ReadAllText(filePath)
                Return content
            Catch ex As IOException
                Debug.WriteLine("Reading of file failed: " & ex.Message)
                Return ex.Message
            End Try
        End Function

        Public Function WriteTextFile(target As String, text As String) As Boolean Implements IFileSystemService.WriteTextFile
            Try
                File.WriteAllText(target, text)
                Return True
            Catch ex As IOException
                Debug.WriteLine("Writing CSV file failed: " & ex.Message)
                Return False
            End Try
        End Function

        Public Function GetDatabasePath(database As String) As String Implements IFileSystemService.GetDatabasePath
            Dim path As String
            Dim root As String = My.Settings.Access97Database
            Dim dB As String = database.Remove(database.LastIndexOf("."), 4)
            Dim dirs As IEnumerable(Of String) = Directory.EnumerateDirectories(root, dB, SearchOption.AllDirectories)
            path = (From dir In dirs Where dir.Substring(dir.LastIndexOf("\") + 1) = dB).FirstOrDefault.ToString
            Return path
        End Function

    End Class

End Namespace
