Imports System.Web.Http
Imports System.Web.Http.Cors
Imports WaferTrackerLibrary.Services
Imports System.IO
Imports WaferTrackingLibrary.Models
Imports Newtonsoft.Json.Linq

Namespace Controllers

Public Class FileSystemController
		Inherits ApiController

		Private _fileSystemService As New FileSystemService

		Public Sub New(fileSystemService As IFileSystemService)
			_fileSystemService = fileSystemService
		End Sub

		Public Sub New()
			_fileSystemService = New FileSystemService
		End Sub

        <HttpGet>
        Public Function DatabasePath(database As String) As String
            Return _fileSystemService.GetDatabasePath(database)
        End Function

        <HttpPost>
        Public Function ReadTextFile(<FromBody> data As JObject) As String
            Dim _fileName As String = data("filename")
            Dim _path As String = data("path")
            Dim _filePath As String = _path + "\" + _fileName
            Dim _content As String
            If File.Exists(_filePath) Then
                Using stream As New StreamReader(_filePath, False)
                    Try
                        _content = stream.ReadToEnd()
                    Catch ex As IOException
                        _content = ex.Message
                    End Try
                End Using
            Else
                _content = "File not found"
            End If
            Return _content
        End Function

        <HttpPost>
        Public Async Sub WriteMapToFile(<FromBody> data As JObject)
            Dim _filename As String = data("filename")
            Dim _path As String = data("path")
            Dim _map As String = data("map")
            If Directory.Exists(_path) Then
                Using stream As New StreamWriter(_path + "\" + _filename, False)
                    Await stream.WriteAsync(_map)
                End Using
            Else
                Directory.CreateDirectory(_path)
                Using stream As New StreamWriter(_path + "\" + _filename, False)
                    Await stream.WriteAsync(_map)
                End Using
            End If
        End Sub

        <HttpPost>
        Public Async Sub WriteImageToFile(<FromBody> data As JObject)
            Dim _filename As String = data("filename")
            Dim _path As String = data("path")
            Dim _modified As String = data("image").ToString.Replace("data:image/png;base64,", String.Empty)
            Dim _bytes As Byte() = Convert.FromBase64String(_modified)

            If Directory.Exists(_path) Then
                File.WriteAllBytes(_path + "\" + _filename, _bytes)
            Else
                Directory.CreateDirectory(_path)
                File.WriteAllBytes(_path + "\" + _filename, _bytes)
            End If

        End Sub

        <HttpPost>
        Public Sub CopyFile(<FromBody> data As JObject)
            Dim _source As String = data("source")
            Dim _target As String = data("target")
            Dim appPath As String = My.Settings.AppFolder
            _fileSystemService.CopyFile(appPath + _source, appPath + _target)
        End Sub

        <HttpPost>
        Public Function MoveFile(<FromBody> data As JObject) As Boolean
            Dim _source As String = data("source")
            Dim _destination As String = data("target")
            Dim appPath As String = My.Settings.AppFolder
            Return _fileSystemService.MoveFile(appPath + _source, _destination)
        End Function

        <HttpPost>
        Public Function DeleteFile(<FromBody> data As JObject) As Boolean
            Dim _target As String = data("target")
            Dim appPath As String = My.Settings.AppFolder
            Return _fileSystemService.DeleteFile(appPath + _target)
        End Function

    End Class

End Namespace
