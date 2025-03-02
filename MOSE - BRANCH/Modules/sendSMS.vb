Imports System.Web
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Resources
Public Class sendSMS
    Private _objcmnbLayer As New clsCommon_BL
    Public Function sendSMSFn(ByVal phonenumber As String, ByVal smsMessage As String) As String
        Dim dt As DataTable
        Dim apikey As String = ""
        dt = _objcmnbLayer._fldDatatable("Select smsAPIKey from CompanyTb")
        If dt.Rows.Count > 0 Then
            apikey = dt(0)(0)
        End If
        If apikey = "" Then
            apikey = "FKDI0t216A0-tXEvyDbSsKLsriBJzcgO00LSZVJAZA"
        End If
        Dim numbers = phonenumber
        Dim strPost As String
        Dim sender = "TXTLCL"
        Dim url As String = "https://api.textlocal.in/send/?"

        strPost = url + "apikey=" + apikey _
        + "&numbers=" + numbers _
        + "&message=" + Web.HttpUtility.UrlEncode(smsMessage) '_
        '+ "&sender=" + sender

        Dim request As WebRequest = WebRequest.Create(strPost)
        request.Method = "POST"
        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(strPost)
        request.ContentType = "application/x-www-form-urlencoded"
        request.ContentLength = byteArray.Length
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(byteArray, 0, byteArray.Length)
        dataStream.Close()

        Dim response As WebResponse = request.GetResponse()
        dataStream = response.GetResponseStream()
        Dim reader As New StreamReader(dataStream)
        Dim responseFromServer As String = reader.ReadToEnd()
        Console.WriteLine(responseFromServer)
        Console.ReadLine()

        reader.Close()
        dataStream.Close()
        response.Close()
        '{"errors":[{"code":3,"message":"Invalid login details"}],"status":"failure"}
        If responseFromServer.Length > 0 Then
            Return responseFromServer
        Else
            Return CType(response, HttpWebResponse).StatusDescription
        End If
    End Function
End Class
