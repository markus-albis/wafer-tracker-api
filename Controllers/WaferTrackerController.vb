Imports System.Web.Http
Imports System.Web.Http.Cors
Imports Breeze.ContextProvider
Imports Breeze.ContextProvider.EF6
Imports Breeze.WebApi2
Imports Newtonsoft.Json.Linq

Namespace Controllers

	<BreezeController>
	Public Class WaferTrackerController
		   Inherits ApiController

        Private _contextProvider As EFContextProvider(Of WaferTrackerEntities) = New EFContextProvider(Of WaferTrackerEntities)

        ' ~/breeze/WaferTracker/Metadata
        <HttpGet>
        Public Function Metadata() As String
            Return _contextProvider.Metadata
        End Function

        ' ~/breeze/WaferTracker/EpiSuppliers
        <HttpGet>
        Public Function EpiSuppliers() As IQueryable
            Return _contextProvider.Context.EPIsuppliers
        End Function

        ' ~/breeze/WaferTracker/EpiTypes
        <HttpGet>
        Public Function EpiTypes() As IQueryable
            Return _contextProvider.Context.EpiTypes
        End Function

        ' ~/breeze/WaferTracker/Masks
        <HttpGet>
        Public Function Masks() As IQueryable
            Return _contextProvider.Context.Masks
        End Function

        ' ~/breeze/WaferTracker/ProductNames
        <HttpGet>
        Public Function Products() As IQueryable
            Return _contextProvider.Context.Products
        End Function

        ' ~/breeze/WaferTracker/Technicians
        <HttpGet>
        Public Function Technicians() As IQueryable
            Return _contextProvider.Context.Technicians
        End Function

        ' ~/breeze/WaferTracker/WaferSuppliers
        <HttpGet>
        Public Function WaferSuppliers() As IQueryable
            Return _contextProvider.Context.WaferSuppliers
        End Function

        <HttpPost>
        Public Function SaveChanges(saveBundle As JObject) As SaveResult
            Return _contextProvider.SaveChanges(saveBundle)
        End Function

    End Class

End Namespace