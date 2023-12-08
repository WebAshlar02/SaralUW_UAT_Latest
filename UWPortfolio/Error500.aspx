<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Error500.aspx.cs" Inherits="Error500" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />

    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=8, IE=9, IE=10" />

    <title>BOE Portfolio</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />
    <!-- Font Awesome -->

</head>
<body>
    <form id="form1" runat="server" role="form">
        <!-- Content Wrapper. Contains page content -->
        <div class="content-wrapper" style="margin: 0px;">
            <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />
            <link rel="stylesheet" href="dist/css/AdminLTE.min.css" />

            <!-- Main content -->
            <section class="content">

                <div class="error-page" style="text-align: center;">
                    <h2 class="headline text-red">500</h2>

                    <div class="error-content">
                        <h3><i class="fa fa-warning text-red"></i>Oops! Something went wrong.</h3>

                        <p>
                            You weren't really clicking around any more, So we logged you out for your protection. To get back in, just login in again
                        </p>

                        <img src="dist/img/session2.png" />
                    </div>
                </div>
                <!-- /.error-page -->

            </section>
            <!-- /.content -->
        </div>
        <!-- /.content-wrapper -->
    </form>
</body>
</html>
