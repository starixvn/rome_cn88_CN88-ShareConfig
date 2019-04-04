<%@ Page Language="C#" AutoEventWireup="false" %>
<%
    string hostName = HttpContext.Current.Request.Headers["Host"];
    string appPath = HttpContext.Current.Request.ApplicationPath;
    string url = string.Format("//{0}{1}", hostName, appPath);

    if (appPath.Length > 1)
    {
        url += "/";
    }

    string baseUrl = url;
    var domains = ConfigurationManager.AppSettings;

    foreach (string key in domains.AllKeys)
    {
        if (domains[key].Contains(baseUrl))
        {
            baseUrl = key;
            break;
        }
    }

    Response.Redirect(baseUrl, true);
%>