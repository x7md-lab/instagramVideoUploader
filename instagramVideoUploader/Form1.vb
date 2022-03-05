Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Web.Script.Serialization

Public Class Form1
    Dim cookies As New CookieContainer()
    Dim cookiesString As String
    Dim imagepost As Byte()
    Dim Widthimg As String
    Dim Heightimg As String
    Dim VideoPath As String
    Dim VideoInfo As String()
    Dim imagepost_mime_type As String

    Function timeNow() As String
        Dim now_ As String = DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1)).TotalMilliseconds
        Return now_.Split(".")(0)
    End Function

    Function Login(User As String, Pass As String)
        Dim postData As String = loginParam(User, Pass)
        Dim encoding As New UTF8Encoding
        Dim byteData As Byte() = encoding.GetBytes(postData)
        Dim postreq As HttpWebRequest = DirectCast(HttpWebRequest.Create("https://www.instagram.com/accounts/login/ajax/"), HttpWebRequest)
        postreq.Method = "POST"
        postreq.KeepAlive = True
        postreq.Accept = "*/*"
        postreq.Headers.Add("accept-encoding", "gzip, deflate, br")
        postreq.Headers.Add("accept-language", "en-US,en;q=0.9")
        postreq.Headers.Add("cache-control", "no-cache")
        postreq.Headers.Add("cookie", "ig_did=missing; ig_nrcb=1; csrftoken=missing; mid=missing")
        postreq.Headers.Add("x-csrftoken", "missing")
        postreq.Headers.Add("x-ig-app-id", "936619743392459")
        postreq.Headers.Add("x-requested-with", "XMLHttpRequest")
        postreq.Headers.Add("x-instagram-ajax", "1")
        postreq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36"
        postreq.ContentType = "application/x-www-form-urlencoded"
        postreq.Referer = "https://www.instagram.com/"
        postreq.ContentLength = byteData.Length
        Dim postreqstream As Stream = postreq.GetRequestStream()
        postreqstream.Write(byteData, 0, byteData.Length)
        postreqstream.Close()
        Dim postresponse As HttpWebResponse
        postresponse = DirectCast(postreq.GetResponse, HttpWebResponse)
        cookies.Add(postresponse.Cookies)
        Dim tt = postresponse.Headers("Set-Cookie")
        cookiesString = Regex.Match(tt, "ig_did=(.*?;|.*?$)").Value & " ig_nrcb=1; " & Regex.Match(tt, "csrftoken=(.*?;|.*?$)").Value & " " & Regex.Match(tt, "mid=(.*?;|.*?$)").Value & " " & Regex.Match(tt, "ds_user_id=(.*?;|.*?$)").Value & " " & Regex.Match(tt, "sessionid=(.*?;|.*?$)").Value
        Debug.WriteLine(cookiesString)
        Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
        Dim jsonReader As New JavaScriptSerializer()
        Dim thepage As String = postreqreader.ReadToEnd
        Return jsonReader.DeserializeObject(thepage)("authenticated")
    End Function

    Function loginPassWord(pass As String) As String
        Dim StrData As String = $"#PWD_INSTAGRAM_BROWSER:0:{timeNow()}:{pass}"
        Return StrData
    End Function
    Function loginParam(Username As String, Password As String) As String
        Dim param As String = $"username={Username}&enc_password={loginPassWord(Password)}&queryParams={{}}&optIntoOneTap=false&stopDeletionNonce=&trustedDeviceRecords={{}}"
        Return param
    End Function

    Private Sub LoginBtn_Click(sender As Object, e As EventArgs) Handles LoginBtn.Click
        If Login(UserBox.Text, PassBox.Text) Then
            Statuslb.Text = "Status: Logged In!"
        Else
            Statuslb.Text = "Status: Faild!"
        End If
    End Sub

    Private Sub togglePass_CheckedChanged(sender As Object, e As EventArgs) Handles togglePass.CheckedChanged
        If DirectCast(sender, CheckBox).Checked Then
            PassBox.PasswordChar = Nothing
        Else
            PassBox.PasswordChar = "●"
        End If
    End Sub

    Private Sub Filepicker_Click(sender As Object, e As EventArgs) Handles Filepicker.Click
        Dim filePicker As New OpenFileDialog()
        filePicker.Title = "Select Video"
        filePicker.Filter = "MP4|*.mp4"
        If filePicker.ShowDialog() = DialogResult.OK AndAlso filePicker.FileName IsNot Nothing Then
            DirectCast(sender, Button).Text = IO.Path.GetFileName(filePicker.FileName)
            VideoPath = filePicker.FileName
            VideoInfo = getVideoInfo(VideoPath)
            ' MsgBox(buildVideoHeader("", VideoInfo(0), VideoInfo(2), VideoInfo(1)))
            Dim byteData As Byte() = File.ReadAllBytes(VideoPath)
            Debug.WriteLine(byteData.Length.ToString)
        End If
    End Sub
    Function Ran() As String
        'Random value "******" 5 cat lenght
        Return New Random().Next(100000, 999999)
    End Function
    Function UploadVideo() As String
        Dim byteData As Byte() = File.ReadAllBytes(VideoPath)
        Using _web As New WebClient()
            Dim timestamp As String = timeNow()
            _web.Headers.Add("authority", "i.instagram.com")
            _web.Headers.Add("x-entity-name", "fb_uploader_" & timestamp)
            _web.Headers.Add("offset", "0")
            _web.Headers.Add("accept", "*/*")
            _web.Headers.Add("x-instagram-rupload-params", buildVideoHeader(timestamp, VideoInfo(0), VideoInfo(2), VideoInfo(1)))
            _web.Headers.Add("x-asbd-id", Ran())
            _web.Headers.Add("x-csrftoken", Regex.Match(cookiesString, "csrftoken=(.*?;|.*?$)").Groups(1).Value.Replace(";", ""))
            _web.Headers.Add("x-entity-length", byteData.Length.ToString)
            _web.Headers.Add("x-instagram-ajax", "1")
            _web.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36")
            _web.Headers.Add("x-ig-app-id", "936619743392459")
            _web.Headers.Add("cookie", cookiesString)
            Try
                ' get video by byte
                byteData = File.ReadAllBytes(VideoPath)
                Uploadlb.Invoke(Sub() Uploadlb.Text = "Upload Status: Uploading...")

                Dim repo As Byte() = _web.UploadData("https://i.instagram.com/rupload_igvideo/fb_uploader_" + timestamp, "POST",
                                       byteData)
                Dim StrRepo As String = New UTF8Encoding().GetString(repo)
                If (StrRepo.Contains("""status"":""ok""")) Then
                    Uploadlb.Invoke(Sub() Uploadlb.Text = "Upload Status: video processing...")
                    Debug.WriteLine(StrRepo)
                    uploud_img(cookiesString, timestamp, imagepost, Heightimg, Widthimg)
                End If
            Catch ex As WebException
                Dim rsponString As String = New IO.StreamReader(ex.Response.GetResponseStream).ReadToEnd
                Dim ExResponse = TryCast(ex.Response, HttpWebResponse)
                Debug.WriteLine(rsponString)
                Debug.WriteLine(ExResponse.StatusCode())
                MsgBox("Can't Upload", MsgBoxStyle.Critical)
                Uploadlb.Invoke(Sub() Uploadlb.Text = "Upload Status: Error...")
            End Try

            Return ""
        End Using
    End Function

    Function buildVideoHeader(id As String, duration_ms As String, vidH As String, vidW As String) As String
        ' some long Herader have value of video info 
        Return "{""client-passthrough"":""1"",""is_igtv_video"":true,""is_sidecar"":""0"",""is_unified_video"":""0"",""media_type"":2,""for_album"":false,""video_format"":"""",""upload_id"":""" + id + """,""upload_media_duration_ms"":" + duration_ms + ",""upload_media_height"":" + vidH + ",""upload_media_width"":" + vidW + ",""video_transform"":null,""video_edit_params"":{""crop_height"":" + vidH + ",""crop_width"":" + vidW + ",""crop_x1"":0,""crop_y1"":0,""mute"":false,""trim_end"":" + (Convert.ToSingle(duration_ms) / 1000).ToString() + ",""trim_start"":0}}"
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If imagepost IsNot Nothing Then
            If VideoPath IsNot Nothing Then
                Dim thread_ = New Threading.Thread(Sub()
                                                       UploadVideo()
                                                   End Sub)
                thread_.Start()
            Else
                MsgBox("Did you pick a video?", MsgBoxStyle.Exclamation)
            End If

        Else
            MsgBox("Did you pick a thumbnail", MsgBoxStyle.Exclamation)
        End If
    End Sub

    'get video info 
    Function getVideoInfo(path As String) As String()
        Dim ffProbe = New NReco.VideoInfo.FFProbe()
        Dim videoInfo = ffProbe.GetMediaInfo(path)
        Console.WriteLine(videoInfo.Duration.TotalSeconds)
        Return {videoInfo.Duration().TotalMilliseconds, videoInfo.Streams(0).Width, videoInfo.Streams(0).Height}
    End Function


#Region "Upload Image"
    Function uploud_img(acc_cookies As String, time_nowis As String, imgbyts As Byte(), imgHG As String, imgwd As String) As Boolean
        Dim Rcs As New Regex("csrftoken=(\w+)(;|$)")
        Dim csrftoken = Rcs.Match(acc_cookies).Groups(1).Value
        Try
            Dim en As New UTF8Encoding
            ' Dim postData As String = Encoding.Default.GetString(imgby)
            Dim tempcook As New CookieContainer

            Dim byteData As Byte() = imgbyts

            Dim httpPost = DirectCast(WebRequest.Create($"https://www.instagram.com/rupload_igphoto/fb_uploader_{time_nowis}"), HttpWebRequest)

            httpPost.Host = "i.instagram.com"
            httpPost.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36"
            httpPost.Accept = "*/*"
            httpPost.Method = "POST"
            httpPost.KeepAlive = True
            httpPost.ContentType = "image/jpeg"
            httpPost.ContentLength = byteData.Length
            httpPost.Headers.Add("Cookie", acc_cookies)
            httpPost.Headers.Add("X-Instagram-Rupload-Params: {""media_type"":2,""upload_id"":""" & time_nowis & """,""upload_media_height"":" & imgHG & ",""upload_media_width"":" & imgwd & "}")
            httpPost.Headers.Add("X-Entity-Name: fb_uploader_" & time_nowis)
            httpPost.Headers.Add("X-Entity-Length", byteData.Length.ToString)
            httpPost.Headers.Add("x-csrftoken", csrftoken)
            httpPost.Headers.Add("X-Instagram-AJAX", "1")
            httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
            httpPost.Headers.Add("x-ig-app-id", "936619743392459")
            httpPost.Headers.Add("Offset: 0")
            'Send Data
            Dim poststr As IO.Stream = httpPost.GetRequestStream()
            poststr.Write(byteData, 0, byteData.Length)
            poststr.Close()

            'Get Response
            Dim POST_Response As HttpWebResponse
            POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)

            Dim Post_Reader As New IO.StreamReader(POST_Response.GetResponseStream())
            Dim Response As String = Post_Reader.ReadToEnd
            Debug.WriteLine(Response)
            If share_it(acc_cookies, time_nowis) Then
                Return True
            End If
        Catch ex As WebException : End Try
        Return False
    End Function
    Function share_it(acc_cookies, id_time) As Boolean
        Dim Rcs As New Regex("csrftoken=(\w+)(;|$)")
        Dim csrftoken = Rcs.Match(acc_cookies).Groups(1).Value
        Try
            Dim en As New UTF8Encoding
            Dim postData As String = "source_type=library&caption=" &
                Destb.Text.Replace(vbCrLf, "%0D%0A").Replace(" ", "+") &
                "&upload_id=" + id_time + "&usertags=&custom_accessibility_caption=&disable_comments=0&&disable_comments=0&like_and_view_counts_disabled=0&igtv_ads_toggled_on=&igtv_share_preview_to_feed=1&is_unified_video=1&video_subtitles_enabled=0"
            Dim tempcook As New CookieContainer

            Dim byteData As Byte() = en.GetBytes(postData)


            Dim httpPost = DirectCast(WebRequest.Create($"https://i.instagram.com/api/v1/media/configure/"), HttpWebRequest)

            httpPost.Host = "i.instagram.com"
            httpPost.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.183 Safari/537.36"
            httpPost.Accept = "*/*"
            httpPost.Method = "POST"
            httpPost.KeepAlive = True
            httpPost.ContentType = "application/x-www-form-urlencoded"
            httpPost.ContentLength = byteData.Length
            httpPost.Headers.Add("Cookie", acc_cookies)
            httpPost.Headers.Add("x-csrftoken", csrftoken)
            httpPost.Headers.Add("x-asbd-id", "437806")
            httpPost.Headers.Add("x-ig-app-id", "936619743392459")
            httpPost.Headers.Add("X-Instagram-AJAX", "1")
            httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
            'Send Data
            Dim poststr As IO.Stream = httpPost.GetRequestStream()
            poststr.Write(byteData, 0, byteData.Length)
            poststr.Close()

            'Get Response
            Dim POST_Response As HttpWebResponse
            POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)

            Dim Post_Reader As New IO.StreamReader(POST_Response.GetResponseStream())
            Dim Response As String = Post_Reader.ReadToEnd
            Debug.WriteLine(Response)
            If Response.Contains("""status"":""ok""") Then
                Uploadlb.Invoke(Sub() Uploadlb.Text = "Upload Status: Done")
                Return True
            Else
                System.Threading.Thread.Sleep(10000)
                Debug.WriteLine(Response.Contains("""status"":""ok"""))
                share_it(acc_cookies, id_time)
            End If
        Catch ex As WebException
            Dim rsponString As String = New IO.StreamReader(ex.Response.GetResponseStream).ReadToEnd
            Dim ExResponse = TryCast(ex.Response, HttpWebResponse)
            If rsponString IsNot Nothing Then
                MsgBox(rsponString + "   " + ExResponse.StatusCode, MsgBoxStyle.Critical)
            End If
        End Try
        Return False
    End Function


#End Region
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim op As New OpenFileDialog() With {.Filter = "Image|*.jpg; *.png; *.jfif", .Multiselect = False, .Title = "Post Image"}
        If op.ShowDialog = DialogResult.OK Then
            Dim bmp As New Bitmap(op.FileName)
            If Not MimeMapping.GetMimeMapping(op.FileName).Contains("jpg") Then
                Dim ms = New IO.MemoryStream()
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                imagepost = ms.ToArray()
                Widthimg = bmp.Width
                Heightimg = bmp.Height
            Else
                Widthimg = bmp.Width
                Heightimg = bmp.Height
                imagepost = IO.File.ReadAllBytes(op.FileName)
                imagepost_mime_type = MimeMapping.GetMimeMapping(op.FileName)
            End If
            DirectCast(sender, Button).ForeColor = Color.Green
        End If
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Application.Exit()
    End Sub
End Class