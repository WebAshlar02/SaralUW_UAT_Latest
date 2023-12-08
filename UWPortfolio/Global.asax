<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_BeginRequest(object sender, EventArgs e)
    {
        //HttpContext.Current.Response.AddHeader("X-FRAME-OPTIONS", "DENY");
    }

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        //Exception ex = Server.GetLastError();        
        //UWSaralDecision.CommFun objCommFun = new UWSaralDecision.CommFun();
        //var strPageName= HttpContext.Current.Request.Url.Host;
        //objCommFun.SaveErrorLogs(string.Empty, "Failed", "UWSaralDecision", "Application Error", strPageName, "E-Error", "", "", ex.ToString());
        //Platform.Utilities.LoggerFramework.Logger.Error("Application_Error \\ PageName: " + strPageName +" Error Message "+ ex.Message);
        //Response.Redirect("ErrorPage.aspx",true);
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        //added by suraj for VAPT
        if (Request.IsSecureConnection == true)
            Response.Cookies["ASP.NET_SessionID"].Secure = true;
        //end VAPT
    }

    void Session_End(object sender, EventArgs e)
    {

        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
