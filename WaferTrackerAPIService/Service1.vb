Imports Microsoft.Owin.Hosting

Public Class WaferDataAPIService

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things in motion so your service can do its work.
        Dim baseUri As String = My.Settings.BaseUri
        WebApp.Start(Of Startup)(baseUri)
    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.

    End Sub

End Class
