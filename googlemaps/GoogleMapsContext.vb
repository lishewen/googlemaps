Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class GoogleMapsContext
    Inherits DbContext
    Public Sub New()
        Database.CreateIfNotExists()
    End Sub

    Public Property RoadMaps As DbSet(Of RoadMap)
End Class

Public Class RoadMap
    <Key>
    Public Property Id As Integer
    Public Property X As Integer
    Public Property Y As Integer
    Public Property Z As Integer
End Class