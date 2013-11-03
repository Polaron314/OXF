Imports Ionic.Zip
Imports System.IO

Public Class Info

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (My.Application.CommandLineArgs.Count > 0) Then
        End If
        RunFile("C:\Users\Server\Desktop\oxf\Minecraft.oxf")
    End Sub
    Public Sub RunFile(ByVal exeLoc As String)
        Randomize()
        Dim value As Integer = CInt(Int((100 * Rnd()) + 1))
        Dim location = Application.StartupPath + "\temp\" + value.ToString + "\"
        If (System.IO.Directory.Exists(location)) Then
            My.Computer.FileSystem.DeleteDirectory(location, FileIO.DeleteDirectoryOption.DeleteAllContents)
        End If
        System.IO.Directory.CreateDirectory(location)
        extractZip(exeLoc, location)
        Dim contents = My.Computer.FileSystem.ReadAllText(location + "main.main")
        Dim contentString = contents.Replace(", ", ",").Split(",")
        MsgBox(getParamInfo(contentString(0)) + getParamInfo(contentString(1)))
        Dim Applocation As String = location + getParamInfo(contentString(0))
        Dim command = contentString(1).Replace("%filepath%", location)
        Shell("cmd.exe /c" & command)
    End Sub
    Private Shared Sub extractZip(ByVal zipLocation As String, ByVal extractLoc As String)
        Dim ZipToUnpack As String = zipLocation
        Dim UnpackDirectory As String = extractLoc
        Using zip1 As ZipFile = ZipFile.Read(ZipToUnpack)
            Dim e As ZipEntry
            ' here, we extract every entry, but we could extract conditionally,
            ' based on entry name, size, date, checkbox status, etc.   
            For Each e In zip1
                e.Extract(UnpackDirectory, ExtractExistingFileAction.OverwriteSilently)
            Next
        End Using
    End Sub
    Private Function getParamInfo(ByVal param As String) As String
        Return param.Substring(param.LastIndexOf("="))
    End Function
End Class
