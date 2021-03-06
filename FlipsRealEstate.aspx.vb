Imports System.Xml
Imports net.webservicex.www



Partial Class FlipRealEstate
    Inherits System.Web.UI.Page
    Dim clickCount As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("ClickCount") Is Nothing Then
            clickCount = 0
        Else
            clickCount = CInt(Session("ClickCount"))
        End If

        If Not Me.IsPostBack Then
            'Create a new global weather object
            Dim proxy As New GlobalWeather
            'Create a new xml document object. We need xml implementation since the data is in xml format.
            Dim xmlDoc As New XmlDocument
            'Load all country/city names in to the xml document.
            xmlDoc.LoadXml(proxy.GetCitiesByCountry(""))
            'Query only country names and put them in to xml node list
            Dim xmlNodes As XmlNodeList = xmlDoc.GetElementsByTagName("Country")
            Dim countryList As New List(Of String)
            For Each node As XmlNode In xmlNodes
                If Not countryList.Contains(node.InnerText) Then
                    'Loop through the country names and put them in to a list object
                    countryList.Add(node.InnerText)
                End If
            Next
            countryList.Sort()

            'Bind the CountryDropDownList control.
            Me.CountryDropDownList.DataSource = countryList
            Me.CountryDropDownList.DataBind()
            ' change the selected index here.
            If clickCount = 0 Then
                Me.CountryDropDownList.SelectedIndex = 196
            Else
            End If
            Me.CountryDropDownList_SelectedIndexChanged(Me.CountryDropDownList, New EventArgs)
        End If

    End Sub
    Protected Sub CountryDropDownList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CountryDropDownList.SelectedIndexChanged

        If Me.CountryDropDownList.SelectedItem IsNot Nothing Then
            Dim cityList As New List(Of String)
            Dim proxy As New GlobalWeather
            Dim xmlDoc As New XmlDocument
            'Load the xml document with all city names for the selected country.
            xmlDoc.LoadXml(proxy.GetCitiesByCountry(Me.CountryDropDownList.SelectedValue))
            'Get all city names and put them in to xml node list.
            Dim xmlNodes As XmlNodeList = xmlDoc.GetElementsByTagName("City")
            For Each node As XmlNode In xmlNodes
                If Not cityList.Contains(node.InnerText) Then
                    'Loop through the city names and put them in to a list object
                    cityList.Add(node.InnerText)
                End If
            Next
            cityList.Sort()
            'Bind the CityDropDownList control.
            Me.CityDropDownList.DataSource = cityList
            Me.CityDropDownList.DataBind()
            If clickCount = 0 And Me.CountryDropDownList.SelectedIndex = 196 Then
                Me.CityDropDownList.SelectedIndex = 236
            End If
        Else
            Me.CityDropDownList.DataSource = Nothing
            Me.CityDropDownList.DataBind()
        End If
    End Sub
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If Me.CountryDropDownList.SelectedItem IsNot Nothing _
                AndAlso Me.CityDropDownList.SelectedItem IsNot Nothing Then
            Dim sb As New StringBuilder
            Dim proxy As New GlobalWeather
            'Get weather information for the selected country and city by using the "GetWeather" service method.
            Dim xmlStr As String = proxy.GetWeather(Me.CityDropDownList.SelectedValue, Me.CountryDropDownList.SelectedValue)
            Dim success As Boolean = False
            Try
                Dim xmlDoc As New XmlDocument
                'Load the xml string data into xml document
                xmlDoc.LoadXml(xmlStr)
                Dim xmlNode As XmlNode = xmlDoc.DocumentElement()
                For Each node As XmlNode In xmlNode.ChildNodes
                    If node.Name = "Status" Then
                        success = node.InnerText = "Success"
                    Else
                        'Loop through the nodes and put the information in the StringBuilder object.
                        sb.Append("<b>" & node.Name & ":</b> " & node.InnerText & "<br />")
                    End If
                Next
            Catch ex As Exception
                sb.Append("Data Not Found")
            End Try
            If success Then
                'If everything went well than show the information.
                Me.WeatherLabel.Text = sb.ToString
            Else
                Me.WeatherLabel.Text = "Data Not Found"
            End If
        End If

    End Sub
    Protected Sub NewWeatherButton_Click(sender As Object, e As System.EventArgs)
        clickCount += 1
        If Not Me.IsPostBack Then
            'Create a new global weather object
            Dim proxy As New GlobalWeather
            'Create a new xml document object. We need xml implementation since the data is in xml format.
            Dim xmlDoc As New XmlDocument
            'Load all country/city names in to the xml document.
            xmlDoc.LoadXml(proxy.GetCitiesByCountry(""))
            'Query only country names and put them in to xml node list
            Dim xmlNodes As XmlNodeList = xmlDoc.GetElementsByTagName("Country")
            Dim countryList As New List(Of String)
            For Each node As XmlNode In xmlNodes
                If Not countryList.Contains(node.InnerText) Then
                    'Loop through the country names and put them in to a list object
                    countryList.Add(node.InnerText)
                End If
            Next
            countryList.Sort()
            'Bind the CountryDropDownList control.
            Me.CountryDropDownList.DataSource = countryList
            Me.CountryDropDownList.DataBind()
            Me.CountryDropDownList_SelectedIndexChanged(Me.CountryDropDownList, New EventArgs)
        End If
    End Sub
End Class




