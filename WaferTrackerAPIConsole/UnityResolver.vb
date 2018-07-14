Imports Microsoft.Practices.Unity
Imports System.Collections.Generic
Imports System.Web.Http.Dependencies

Public Class UnityResolver
    Implements IDependencyResolver

    Protected _container As IUnityContainer

    Public Sub New(container As IUnityContainer)
        If container Is Nothing Then
            Throw New ArgumentNullException("Container")
        End If
        Me._container = container
    End Sub

    Public Function GetService(serviceType As Type) As Object Implements IDependencyResolver.GetService
        Try
            Return _container.Resolve(serviceType)
        Catch generatedExceptionName As ResolutionFailedException
            Return Nothing
        End Try
    End Function

    Public Function GetServices(serviceType As Type) As IEnumerable(Of Object) Implements IDependencyResolver.GetServices
        Try
            Return _container.ResolveAll(serviceType)
        Catch generatedExceptionName As ResolutionFailedException
            Return New List(Of Object)()
        End Try
    End Function

    Public Function BeginScope() As IDependencyScope Implements IDependencyResolver.BeginScope
        Dim child = _container.CreateChildContainer()
        Return New UnityResolver(child)
    End Function

    Public Sub Dispose() Implements IDependencyResolver.Dispose
        _container.Dispose()
    End Sub

End Class
