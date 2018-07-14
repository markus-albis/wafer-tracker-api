Imports Owin
Imports System.Web.Http
Imports Microsoft.Owin.Cors
Imports System.Web.Http.Cors
Imports System.Web.Http.Dispatcher

Public Class Startup

    Public Sub Configuration(app As IAppBuilder)
        Dim webApiConfiguration = ConfigureWebApi()
        ' Use the extension method provided by the WebApi.Owin library:
        app.UseCors(CorsOptions.AllowAll)
        app.UseWebApi(webApiConfiguration)
    End Sub

    Private Function ConfigureWebApi() As HttpConfiguration

        Dim config = New HttpConfiguration()

        'Configure Cors
        Dim origins As String = My.Settings.Local1 + "," + My.Settings.Local2 + "," + My.Settings.Origin1
        Dim headers As String = "*"
        Dim methods As String = "*"
        Dim cors As New EnableCorsAttribute(origins, headers, methods)
        config.EnableCors(cors)
        config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always

        'Assembly resolver to find controllers in external library
        Dim assemblyResolver As New AssemblyResolver()
        config.Services.Replace(GetType(IAssembliesResolver), assemblyResolver)

        'Configure UnityContainer
        'Dim _container As New UnityContainer
        '_container.RegisterType(Of WaferDataService50)(New HierarchicalLifetimeManager())
        'config.DependencyResolver = New UnityResolver(_container)

        'config.Formatters.XmlFormatter.SupportedMediaTypes.Clear()		' return JSON for testing purposes
        config.Routes.MapHttpRoute("BreezeApi", "breeze/{controller}/{action}")
        config.Routes.MapHttpRoute("Action", "api/{controller}/{action}")
        config.Routes.MapHttpRoute("Default", "api/{controller}/{id}", defaults:=New With {Key .id = RouteParameter.Optional})
        Return config
    End Function

End Class
