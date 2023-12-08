<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UWExam.aspx.cs" Inherits="Appcode_UWExam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            //submit button by default hide on popupLoad
            $('#btnSubmitDesign').hide();

            var id = '#dialog';

            //Get the screen height and width
            var maskHeight = $(document).height();
            var maskWidth = $(window).width();

            //Set heigth and width to mask to fill up the whole screen
            $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

            //transition effect
            $('#mask').fadeIn(500);
            $('#mask').fadeTo("slow", 0.9);

            //Get the window height and width
            var winH = $(window).height();
            var winW = $(window).width();

            //Set the popup window to center
            $(id).css('top', winH / 2 - $(id).height() / 2);
            $(id).css('left', winW / 2 - $(id).width() / 2);

            //transition effect
            $(id).fadeIn(2000);

            //Submit button Show on RadioClick
            $('input[type = "radio"]').click(function () {
                if ($(this).attr('id') == 'rbtnAnsOpt') {
                    $('#btnSubmitDesign').hide();
                }
                else
                    $('#btnSubmitDesign').show();
            })

            //if close button is clicked
            $('.window .close').click(function (e) {
                //Cancel the link behavior
                e.preventDefault();

                $('#mask').hide();
                $('.window').hide();
            });

            //if mask is clicked
            $('#mask').click(function () {
                $(this).hide();
                $('.window').hide();
            });

        });
    </script>
    <style type="text/css">
        #mask {
            position: absolute;
            left: 0;
            top: 0;
            z-index: 9000;
            background-color: #000;
            display: none;
        }

        #boxes .window {
            position: absolute;
            left: 0;
            top: 0;
            width: 440px;
            height: 200px;
            display: none;
            z-index: 9999;
            padding: 20px;
            border-radius: 15px;
            text-align: center;
        }

        #boxes #dialog {
            width: 750px;
            height: 300px;
            padding: 10px;
            background-color: #ffffff;
            font-family: 'Segoe UI Light', sans-serif;
            font-size: 15pt;
        }

        #popupfoot {
            font-size: 16pt;
            position: absolute;
            bottom: 0px;
            width: 250px;
            left: 250px;
        }

        #lblQueDesign {
            text-align: left;
        }

        #btnSubmitDesign {
            text-align: center;
        }
        #boxes {
            height: 27px;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <div id="boxes">
            <div id="dialog" class="window">
                <div id="lblQueDesign">
                    <asp:Label runat="server" ID="lblQue" Text=""></asp:Label><br />
                    <br />

                    <asp:Label runat="server" ID="lblAnsOpt" CssClass="lblQue" Text="Ans: "></asp:Label>
                    <div>
                        <asp:RadioButtonList runat="server" ID="rbtnAnsOpt" RepeatDirection="Horizontal" ItemType="radio"></asp:RadioButtonList><br />
                    </div>

                    <br />
                    <div id="btnSubmitDesign">
                        <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" />
                    </div>
                    <asp:Label runat="server" ID="lblError" Text=""></asp:Label><br />
                    <br />
                    <asp:Button runat="server"  ID="btnClose" Text="Close" OnClick="btnClose_Click"/>
                    <%--<asp:Button runat="server"  ID="btnClose" Text="Close" OnClientClick="Login.aspx"/>--%>
                    <%--<input runat="server" type="button" id="btnClose" name="Close" style="display: none" />--%>
                </div>
                <%--Your Content Goes Here--%>
                <%--<div id="popupfoot"><a href="#" class="close agree">I agree</a> | <a class="agree" style="color: red;" href="#">I do not agree</a> </div>--%>
            </div>
            <div id="mask"></div>
        </div>
    </form>
</body>
</html>
