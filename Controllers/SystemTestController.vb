Imports System.Text
Imports System.Web.Http

Public Class SystemTestController
    Inherits ApiController

    <HttpGet>
    Public Function Test() As String

        Dim _name As String = My.Application.Info.Title
        Dim _major As String = My.Application.Info.Version.Major   'System.Reflection.Assembly.GetEntryAssembly.GetName.Version.Major
        Dim _minor As String = My.Application.Info.Version.Minor   'System.Reflection.Assembly.GetEntryAssembly.GetName.Version.Minor
        Dim _build As String = My.Application.Info.Version.Build   'System.Reflection.Assembly.GetExecutingAssembly.GetName.Version.Build

        Dim _out As New StringBuilder
        _out.Append(_name + "  ")
        _out.AppendFormat("Version: {0}.{1} ", _major, _minor)
        _out.AppendFormat("Build: {0}", _build)

        Return _out.ToString
    End Function


End Class
