Module MainModule
    Const Path = "D:\Map\maptile\googlemaps\roadmap\"
    Sub Main()
        Dim FileList = My.Computer.FileSystem.GetFiles(Path, FileIO.SearchOption.SearchAllSubDirectories, "*.png")
        For Each file In FileList
            Console.WriteLine(file)
        Next
        Console.ReadLine()
    End Sub

End Module
