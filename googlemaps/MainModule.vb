Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Net
Imports System.Threading

Module MainModule
    Const Path = "D:\Map\maptile\googlemaps\roadmap\"
    Const Url = "http://gac-geo.googlecnapps.cn/maps/vt?lyrs=m&x={0}&y={1}&z={2}"
    Const FilePath = "D:\Map\maptile\googlemaps\roadmap\{0}\{1}\{2}.png"
    Sub Main()
        'GetFiles()
        DownloadFile()
        Console.ReadLine()
    End Sub

    Private Sub DownloadFile()
        Using db As New GoogleMapsContext
            Dim FileList = db.RoadMaps.ToList
            For Each file In FileList
                Dim MapUrl = String.Format(Url, file.X, file.Y, file.Z)
                Dim filename = String.Format(FilePath, file.Z, file.X, file.Y)
                'My.Computer.Network.DownloadFile(New Uri(MapUrl, UriKind.Absolute), filename, "", "", False, 100000, True)
                Dim map = DownloadImage(MapUrl)
                map.Save(filename, ImageFormat.Png)
                'Thread.Sleep(3000)
                Console.WriteLine(MapUrl)
            Next
        End Using
        Console.WriteLine("下载完成")
    End Sub

    Private Sub GetFiles()
        Dim FileList = My.Computer.FileSystem.GetFiles(Path, FileIO.SearchOption.SearchAllSubDirectories, "*.png")
        Dim db As New GoogleMapsContext
        For Each file In FileList
            Console.WriteLine(file)
            file = Replace(file, Path, "")
            file = Replace(file, ".png", "")
            Dim s = Split(file, "\")
            db.RoadMaps.Add(New RoadMap With {
            .Z = s(0),
            .X = s(1),
            .Y = s(2)
            })
        Next
        db.SaveChanges()
    End Sub

    Private Function DownloadImage(ByVal url As String) As Bitmap
        Dim bitmap As Bitmap = Nothing
        Dim stream As Stream = DownloadResource(url)

        If stream IsNot Nothing Then
            bitmap = New Bitmap(stream)
        End If

        Return bitmap
    End Function

    Private Function DownloadResource(ByVal url As String) As Stream
        Dim client As New WebClient()
        client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)")
        Dim data As Byte() = client.DownloadData(url)
        Dim stream As New MemoryStream(data)
        client.Dispose()

        Return stream
    End Function
End Module