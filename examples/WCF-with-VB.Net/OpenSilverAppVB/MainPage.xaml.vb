Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls

Partial Public Class MainPage
    Inherits Page
    Public Sub New()
        Me.InitializeComponent()
        Main()
    End Sub

    Public Async Sub Main()
        Await MyFunc()
    End Sub

    Public Async Function MyFunc() As Task(Of Integer)
        Dim proxy = New OpenSilverServiceProxy.ServiceProxy()

        Dim results = Await proxy.GetDataAsync(1)
        Console.WriteLine(results)
    End Function

End Class