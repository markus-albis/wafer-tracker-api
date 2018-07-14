Imports System.IO
Imports System.Reflection
Imports System.Web.Http.Dispatcher

Public Class AssemblyResolver
    Inherits DefaultAssembliesResolver

    Public Overrides Function GetAssemblies() As ICollection(Of Assembly)
        Dim baseAssemblies As ICollection(Of Assembly) = MyBase.GetAssemblies()
        Dim assemblies As New List(Of Assembly)(baseAssemblies)
        Dim _path As String = System.Reflection.Assembly.GetEntryAssembly().Location
        Dim _parent As DirectoryInfo = Directory.GetParent(_path)
        _path = Path.Combine(_parent.FullName, "WaferDataLibrary.dll")
        Dim controllersAssembly = Assembly.LoadFrom(_path)
        assemblies.Add(controllersAssembly)
        Return assemblies
    End Function

End Class
