<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HealthProfile.ascx.cs" Inherits="UserControl_HealthProfile" %>


<%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />--%>
<%--<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />--%>
<%--<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css" />--%>

<%--<link href="../dist/css/styles-app.css" rel="stylesheet" />
<link href="../plugins/select2/select2.min.css" rel="stylesheet" />
<script src="../plugins/jQuery/jquery-2.2.3.min.js"></script>
<link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
<script src="../bootstrap/js/bootstrap.min.js"></script>--%>

<style>
    .HideControl {
        display: none;
    }
</style>
<script>
    $(document).ready(function () {
        $('.box-health-question').find('input').attr('disabled', true);
        $('.box-health-question').find('select').attr('disabled', true);
    });


    //function fnShowAdjDiv(a) {
    //    debugger;
    //    if ($(a).find('input[type="radio"]:checked').val() == 'No') {
    //        //$('#divHideWeightCause').addClass('HideControl');
    //        $(a).parent().parent().closest('div').find('.HideShowDiv').addClass('HideControl');
    //    }
    //    else {
    //        //$('#divHideWeightCause').removeClass('HideControl');
    //        $(a).parent().parent().closest('div').find('.HideShowDiv').removeClass('HideControl');
    //    }
    //}

    //function fnShowAdjDivDdl(a) {
    //    debugger;
    //    if ($(a).val() == 'No') {
    //        $(a).parent().parent().next('.HideShowDiv').addClass('HideControl');
    //    }
    //    else {
    //        $(a).parent().parent().next('.HideShowDiv').removeClass('HideControl');
    //    }
    //}
    //function fnShowAdjDivOnYesNo(a) {
    //    debugger;
    //    if ($(a).find('input[type="radio"]:checked').val() == 'No') {
    //        $(a).parent().parent().closest('div').find('.HideShowDivOnYes').addClass('HideControl');
    //        $(a).parent().parent().closest('div').find('.HideShowDivOnNo').removeClass('HideControl');
    //    }
    //    else {
    //        $(a).parent().parent().closest('div').find('.HideShowDivOnYes').removeClass('HideControl');
    //        $(a).parent().parent().closest('div').find('.HideShowDiv').addClass('HideControl');
    //        $(a).parent().parent().closest('div').find('.HideShowDivOnNo').addClass('HideControl');
    //    }
    //}
    //function fnShowAsset(a) {
    //    var count = 0;
    //    $(a).find('input[type="checkbox"]').each(function () {
    //        if ($(this).val() == "Life Insurance" && $(this).is(':checked') == true) {
    //            count++;
    //            return;
    //        }
    //        if ($(this).val() == "Health Insurance" && $(this).is(':checked') == true) {
    //            count++;
    //            return;
    //        }

    //    })
    //    if (count > 0) {
    //        $(a).parent().parent().closest('div').find('.HideShowDiv').removeClass('HideControl');
    //    }
    //    else {
    //        $(a).parent().parent().closest('div').find('.HideShowDiv').addClass('HideControl');
    //    }
    //}

    //function fnCollapsedBox(req) {
    //    debugger;
    //    //Find the box parent        
    //    var box = $(req).parents(".box").first();
    //    //Find the body and the footer
    //    var bf = box.find(".box-warning , .box-solid");
    //    if (!box.hasClass("collapsed-box")) {
    //        box.addClass("collapsed-box");
    //        bf.slideUp();
    //    } else {
    //        box.removeClass("collapsed-box");
    //        bf.slideDown();
    //    }
    //}
</script>
<%--<asp:UpdatePanel runat="server" ID="upHealtgDetails" UpdateMode="Conditional">
    <ContentTemplate>--%>
<%--personal details--%>
<asp:Panel class="box box-health-question box-warning box-solid collapsed-box" runat="server" ID="Panel6">
    <div class="box-header with-border">
        <div class="col-md-12">
            <div class="col-md-9">
                <h3 class="box-title">section #. Personal details</h3>
            </div>
            <div class="col-md-3">
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse" onclick="fnCollapsedBox(this);">
                        <i class="fa fa-minus"></i>
                    </button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.box-header -->

    <div class="box-body" id="Div1" runat="server" ondblclick="fnCollapsedBox(this);">
        <div class="row"></div>
        <div class="col-md-12">
            <div class="col-md-12" runat="server" id="div3" visible="false">
                <asp:CheckBox ID="CheckBox1" AutoPostBack="false" runat="server" Text="Edit" />
            </div>
            <div style="clear: both;"></div>
            <div class="form-group">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exampleInputEmail1">Hazardous Occupation</label>
                        <asp:TextBox runat="server" ID="txtHazardousOccupation" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exampleInputEmail1">Agent Confidentiality Rpt</label>
                        <asp:TextBox runat="server" ID="txtAgentConfidentialityRpt" class="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exampleInputEmail1">NSAP Consent</label>
                        <asp:TextBox runat="server" ID="txtNsapConsent" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exampleInputEmail1">Nature of Duty</label>
                        <asp:TextBox runat="server" ID="txtNatureOfDuty" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exampleInputEmail1">OFAC</label>
                        <asp:TextBox runat="server" ID="txtOfac" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exampleInputEmail1">Moral Hazard Rpt </label>
                        <asp:TextBox runat="server" ID="txtMoralHazardRpt" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exampleInputEmail1">FATCA Category</label>
                        <asp:TextBox runat="server" ID="txtFatcaCategory" class="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div style="clear: both;"></div>
            <div class="col-md-12">
                1.21 Do you consume tobacco in any form? 
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <%--<asp:UpdatePanel runat="server" ID="upTobaco" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                    <h4>
                        <asp:RadioButtonList runat="server" ID="rdbIsConsumeTobacco" onchange="fnShowAdjDivOnYesNo(this);" ClientIDMode="Static" AutoPostBack="false" CssClass="flat-red" RepeatDirection="Horizontal">
                            <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="False" Text="No"></asp:ListItem>
                        </asp:RadioButtonList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" SetFocusOnError="true" runat="server" CssClass="reqerror" ErrorMessage="Required" ControlToValidate="rdbIsConsumeTobacco" Display="Dynamic" ValidationGroup="section1"></asp:RequiredFieldValidator>

                        <div class="col-md-12 HideControl HideShowDivOnYes" id="divToboacoList" runat="server">
                            <%--style="padding: 0px;"--%>
                            <asp:HiddenField runat="server" ID="hftabacoo" Value="false" />
                            <div class="col-lg-12 table-responsive">
                                <%--style="padding: 0px;"--%>
                                <table class="table table-bordered table-responsive">
                                    <tbody>
                                        <tr>
                                            <th style="width: 2%;">&nbsp;</th>
                                            <th>Name</th>
                                            <th>Quantity</th>

                                        </tr>
                                        <asp:Repeater runat="server" ID="rpTobaco" ClientIDMode="AutoID">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%--    <asp:CheckBox runat="server" ID="chktabacoo" CssClass="chktabacoo" AutoPostBack="true" OnCheckedChanged="chktabacoo_CheckedChanged"/></td>
                                                        --%>
                                                        <asp:CheckBox runat="server" ID="chkTabacoo" CssClass="chkTabacoo" /></td>

                                                    <td>
                                                        <asp:Literal runat="server" ID="lblTabacooName" Text='<%# Eval("DRUG_TYPE")%>'></asp:Literal></td>
                                                    <td>
                                                        <asp:TextBox runat="server" min="0" TextMode="Number" ClientIDMode="Static" ID="txtTabaccoQuantity" AutoPostBack="false" CssClass="form-control" PlaceHolder="Enter Quantity / Per Day"></asp:TextBox>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" Enabled="false" Visible="false" CssClass="reqerror" ErrorMessage="Required" ValidationGroup="section1" ControlToValidate="txttabaccoquantity" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </tbody>
                                </table>
                            </div>
                        </div>

                        <div class="col-md-4 HideControl HideShowDivOnNo" runat="server" id="divUsedTobaco">
                            <%--style="padding: 0px;"--%>
                            <div class="col-md-12">
                                <%--style="padding: 0px;"--%>
                                <label for="inputEmail3" class="col-sm-4 control-label">&nbsp;</label>
                                <asp:DropDownList runat="server" ID="ddlStillConsumeTobacoo" CssClass="form-control" onchange="fnShowAdjDivDdl(this);" AutoPostBack="false">
                                    <asp:ListItem Value="-1">Select</asp:ListItem>
                                    <asp:ListItem Value="true">Stopped</asp:ListItem>
                                    <asp:ListItem Value="false">Never Used</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator79" Enabled="false" Visible="false" SetFocusOnError="true" runat="server" InitialValue="0" CssClass="reqerror" ErrorMessage="Required" ControlToValidate="ddno" Display="Dynamic" Style="position: relative;" ValidationGroup="section1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-8 HideControl HideShowDiv" id="divStoppedOnMonthYear" runat="server">
                            <div class="col-md-12" style="text-align: right;">
                                <%--style="padding: 0px;"--%>
                                <label for="inputEmail3" class="col-sm-4 control-label" style="text-align: left;">Stopped on (month and year)</label>
                                <div class="col-sm-12">
                                    <asp:TextBox runat="server" ID="txtStopped" CssClass="form-control" Placeholder="Enter Stopped on (month and year)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator47" Enabled="false" Visible="false" SetFocusOnError="true" runat="server" CssClass="reqerror" ErrorMessage="Required" ControlToValidate="txtstopped" Display="Dynamic" Style="position: relative;" ValidationGroup="section1"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </h4>

                    <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                </div>

            </div>

            <div style="clear: both;"></div>
            <div class="col-md-12">
                1.22 Do you consume alcoholic drinks?  
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <%--<asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                    <h4>
                        <asp:RadioButtonList runat="server" ID="rdbIsAlcoholic" AutoPostBack="false" onchange="fnShowAdjDiv(this);" CssClass="flat-red" RepeatDirection="Horizontal">
                            <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="False" Text="No"></asp:ListItem>
                        </asp:RadioButtonList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator78" SetFocusOnError="true" runat="server" CssClass="reqerror" ErrorMessage="Required" ControlToValidate="rdbIsAlcoholic" Display="Dynamic" ValidationGroup="section1"></asp:RequiredFieldValidator>

                        <div class="col-md-12 HideShowDiv HideControl" runat="server" id="divAlcoholDetails">
                            <%--style="padding: 0px;" --%>
                            <asp:HiddenField runat="server" ID="hfalcohol" Value="false" />
                            <div class="col-lg-12 table-responsive">
                                <%--style="padding: 0px;" --%>
                                <table class="table table-bordered">
                                    <tbody>
                                        <tr>
                                            <th style="width: 2%;">&nbsp;</th>
                                            <th>Name</th>
                                            <th>Quantity</th>

                                        </tr>
                                        <asp:Repeater runat="server" ID="rpAlcohol" ClientIDMode="AutoID">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox runat="server" ID="chkAlcohol" AutoPostBack="false" OnCheckedChanged="chkalcohol_CheckedChanged" /></td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="lblAlcoholName" Text='<%# Eval("DRUG_TYPE")%>'></asp:Literal></td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtAlcoholQuantity" ClientIDMode="Static" AutoPostBack="true" CssClass="form-control" PlaceHolder="Enter Quantity / Per Week"></asp:TextBox>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator88" CssClass="reqerror" Enabled="false" Visible="false" ErrorMessage="Required" ValidationGroup="section1" ControlToValidate="txtalcoholquantity" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </td>


                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </h4>
                    <%--</ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rdbIsAlcoholic" />

                                </Triggers>
                            </asp:UpdatePanel>--%>
                </div>

            </div>

            <div style="clear: both;"></div>
            <div class="col-md-12">
                1.23 Do you consume Narcotics like Herion, Cocaine, Cannabis/Ganja,LSD etc. or any drug not prescribed by physician  
            </div>

            <div class="col-md-12">
                <%--style="padding: 0px;" --%>
                <div class="form-group">
                    <%--<asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                    <h4>
                        <div class="col-md-4">
                            <asp:RadioButtonList runat="server" ID="rdbHeroin" onchange="fnShowAdjDiv(this);" AutoPostBack="false" CssClass="flat-red" RepeatDirection="Horizontal">
                                <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                                <asp:ListItem Value="False" Text="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-8 HideShowDiv HideControl" runat="server" id="divHeroineDetails">
                            <%--style="padding: 0px;" --%>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label for="exampleInputEmail1">If Yes, <span style="font-size: 12px;">(Please mention Name and Quantity of drug consumed)</span></label>
                                    <asp:TextBox runat="server" ID="txtHeroineDetails" placeholder="Enter Quantity" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator18" CssClass="reqerror" Enabled="false" ErrorMessage="Required" ValidationGroup="section1" ControlToValidate="txtHeroineDetails" Display="Dynamic"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                        </div>
                    </h4>
                    <%--</ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rdbHeroin" />

                                </Triggers>
                            </asp:UpdatePanel>--%>
                </div>

            </div>

            <div style="clear: both;"></div>
            <div class="col-md-12">
                1.24 Do you indulge in hobbies/ sports(like aviation, diving,racing mountaineering, car/mortbike racing, etc.)  
            </div>

            <div class="col-md-12">
                <%--style="padding: 0px;" --%>
                <div class="form-group">
                    <%--<asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                    <h4>
                        <div class="col-md-4">

                            <asp:RadioButtonList runat="server" ID="rdbSport" AutoPostBack="false" onchange="fnShowAdjDiv(this);" CssClass="flat-red" RepeatDirection="Horizontal">
                                <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                                <asp:ListItem Value="False" Text="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>


                        <div class="col-md-8 HideShowDiv HideControl" runat="server" id="divSportDetails">
                            <%--style="padding: 0px;" --%>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label for="exampleInputEmail1">If Yes, <span style="font-size: 12px;">(Please provide details)</span></label>
                                    <asp:TextBox runat="server" ID="txtSportDetails" placeholder="Enter Details" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" CausesValidation="false" ID="RequiredFieldValidator17" CssClass="reqerror" Enabled="false" ErrorMessage="Required" ValidationGroup="section1" ControlToValidate="txtSportDetails" Display="Dynamic"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                        </div>
                    </h4>
                    <%--</ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rdbsport" />

                                </Triggers>
                            </asp:UpdatePanel>--%>
                </div>

            </div>

            <div style="clear: both;"></div>
            <div class="col-md-12">
                1.25 Have you ever been investigated, prosecuted or have been issued a charge sheet in respect of any criminal/civil offence in any court of law in india or abroad?  
            </div>

            <div class="col-md-12">
                <%--style="padding: 0px;" --%>
                <div class="form-group">
                    <%--<asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                    <h4>
                        <div class="col-md-4">
                            <asp:RadioButtonList runat="server" ID="rdbCriminal" AutoPostBack="false" onchange="fnShowAdjDiv(this);" CssClass="flat-red" RepeatDirection="Horizontal">
                                <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                                <asp:ListItem Value="False" Text="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>


                        <div class="col-md-8 HideShowDiv HideControl" runat="server" id="divCriminalDetails">
                            <%--style="padding: 0px;" --%>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label for="exampleInputEmail1">If Yes, <span style="font-size: 12px;">(Please provide details)</span></label>
                                    <asp:TextBox runat="server" ID="txtCriminalDetails" placeholder="Enter Details" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" CausesValidation="false" ID="RequiredFieldValidator9" CssClass="reqerror" Enabled="false" ErrorMessage="Required" ValidationGroup="section1" ControlToValidate="txtCriminalDetails" Display="Dynamic"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                        </div>
                    </h4>
                    <%--                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rdbcriminal" />

                                </Triggers>
                            </asp:UpdatePanel>--%>
                </div>

            </div>

            <div style="clear: both;"></div>
            <div class="col-md-12">
                1.26 Are you associated or have been associated with a political party or politican or holidung a senior position in ministry, government, state owned entrprise, judicial body in India or aborad or a family member or associate of any person in said capacity?  
            </div>

            <div class="col-md-12">
                <%--style="padding: 0px;" --%>
                <div class="form-group">
                    <%--<asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                    <h4>
                        <div class="col-md-4">
                            <asp:RadioButtonList runat="server" ID="rdbPolitician" onchange="fnShowAdjDiv(this);" AutoPostBack="false" CssClass="flat-red" RepeatDirection="Horizontal">
                                <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                                <asp:ListItem Value="False" Text="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>


                        <div class="col-md-8 HideShowDiv HideControl" runat="server" id="divPoliticalDetails">
                            <%--style="padding: 0px;" --%>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label for="exampleInputEmail1">If Yes, <span style="font-size: 12px;">(Please provide details)</span></label>
                                    <asp:TextBox runat="server" ID="txtPoliticalDetails" placeholder="Enter Details" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" CausesValidation="false" ID="RequiredFieldValidator10" CssClass="reqerror" ErrorMessage="Required" Enabled="false" ValidationGroup="section1" ControlToValidate="txtPoliticalDetails" Display="Dynamic"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                        </div>
                    </h4>
                    <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                </div>

            </div>

            <div style="clear: both;"></div>
            <div class="col-md-12">
                1.27 Please indicate which of the following assets or products you already own/have
                        <div class="form-group">
                            <%--<asp:UpdatePanel runat="server" ID="updatepanel" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                            <h4>
                                <div class="col-lg-12">
                                    <%--style="padding: 0px;" --%>

                                    <asp:CheckBoxList runat="server" ID="chkProduct" RepeatLayout="Flow" RepeatDirection="Horizontal" onchange="fnShowAsset(this);" AutoPostBack="false">
                                        <asp:ListItem Value="Two wheeler">Two wheeler  </asp:ListItem>
                                        <asp:ListItem Value="Small Car">Small Car  </asp:ListItem>
                                        <asp:ListItem Value="Sedan or SUV">Sedan or SUV  </asp:ListItem>
                                        <asp:ListItem Value="Land">Land  </asp:ListItem>
                                        <asp:ListItem Value="Own House">Own House  </asp:ListItem>
                                        <asp:ListItem Value="Bank Deposits">Bank Deposits  </asp:ListItem>
                                        <asp:ListItem Value="Mutual Funds">Mutual Funds  </asp:ListItem>
                                        <asp:ListItem Value="Equity/Shares">Equity/Shares  </asp:ListItem>
                                        <asp:ListItem Value="home Loan">home Loan  </asp:ListItem>
                                        <asp:ListItem Value="Car/Personal Loan">Car/Personal Loan  </asp:ListItem>
                                        <asp:ListItem Value="Credit Card">Credit Card  </asp:ListItem>
                                        <asp:ListItem Value="Life Insurance">Life Insurance  </asp:ListItem>
                                        <asp:ListItem Value="Health Insurance">Health Insurance  </asp:ListItem>
                                        <asp:ListItem Value="Pension Plan">Pension Plan  </asp:ListItem>
                                        <asp:ListItem Value="Debit Card">Debit Card  </asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                            </h4>
                            <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </div>
            </div>
            <div class="col-md-12">
                <%--style="padding: 0px;" --%>
                <div class="col-md-12 HideShowDiv HideControl" id="divLifeInsurance" runat="server">
                    1.28 You have tickedYes for Life or Health Insurance in the question above (1.27) or you have submitted any proposal/application for life/ health/ rider cover with any life/ health insurance company, please provide details of policies/ applications below.                            
                                            <div class="col-md-12 table-responsive">
                                                <%--style="padding: 0px;" --%>
                                                <asp:GridView runat="server" ID="gvInsurancePlan" AutoGenerateColumns="false" CssClass="table" HeaderStyle-CssClass="btn-primary">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Name Of The Insurer" DataField="NameOfTheInsurer" />
                                                        <asp:BoundField HeaderText="Policy No." DataField="PolicyNo" />
                                                        <asp:BoundField HeaderText="Sum Assured" DataField="SumAssured" />
                                                        <asp:BoundField HeaderText="Acceptance Terms" DataField="AcceptanceTerms" />
                                                        <asp:BoundField HeaderText="Current Status" DataField="CurrentStatus" />
                                                    </Columns>
                                                </asp:GridView>
                                                <%--<table class="table table-bordered">
                                        <tbody>
                                            <tr>
                                                <th style="width: 10%;">Name of the insurer</th>
                                                <th style="width: 10%;">Policy no.</th>
                                                <th style="width: 10%;">Sum assured</th>
                                                <th style="width: 25%;">Acceptance terms <span>(standard/ rated/ postponed/ declined/ accepted at extra premium)</span></th>
                                                <th style="width: 25%;">Current Status <span>(in force/ reduced cover/ lapse/ surrendered/ withdrawn)</span></th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:HiddenField runat="server" ID="hfsrno1" />
                                                    <asp:TextBox runat="server" ID="txtinsurer1" placeholder="Enter insurer" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtpolicyno1" placeholder="Enter policy no." CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtsumassured1" placeholder="Enter sum assured" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddacceptance1" CssClass="form-control select2">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="Standard">Standard</asp:ListItem>
                                                        <asp:ListItem Value="Rated">Rated</asp:ListItem>
                                                        <asp:ListItem Value="PostPhone">PostPhone</asp:ListItem>
                                                        <asp:ListItem Value="Decline">Decline</asp:ListItem>
                                                        <asp:ListItem Value="Decline">Accepted at extra premium</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddcurrentstatus1" CssClass="form-control select2">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="Inforce">Inforce</asp:ListItem>
                                                        <asp:ListItem Value="Reduced cover">Reduced cover</asp:ListItem>
                                                        <asp:ListItem Value="Lapsed">Lapsed</asp:ListItem>
                                                        <asp:ListItem Value="Surrendered">Surrendered</asp:ListItem>
                                                        <asp:ListItem Value="Withdrawn">Withdrawn</asp:ListItem>
                                                    </asp:DropDownList></td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:HiddenField runat="server" ID="hfsrno2" />
                                                    <asp:TextBox runat="server" ID="txtinsurer2" placeholder="Enter insurer" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtpolicyno2" placeholder="Enter policy no." CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtsumassured2" placeholder="Enter sum assured" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddacceptance2" CssClass="form-control select2">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="Standard">Standard</asp:ListItem>
                                                        <asp:ListItem Value="Rated">Rated</asp:ListItem>
                                                        <asp:ListItem Value="PostPhone">PostPhone</asp:ListItem>
                                                        <asp:ListItem Value="Decline">Decline</asp:ListItem>
                                                        <asp:ListItem Value="Decline">Accepted at extra premium</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddcurrentstatus2" CssClass="form-control select2">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="Inforce">Inforce</asp:ListItem>
                                                        <asp:ListItem Value="Reduced cover">Reduced cover</asp:ListItem>
                                                        <asp:ListItem Value="Lapsed">Lapsed</asp:ListItem>
                                                        <asp:ListItem Value="Surrendered">Surrendered</asp:ListItem>
                                                        <asp:ListItem Value="Withdrawn">Withdrawn</asp:ListItem>
                                                    </asp:DropDownList></td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:HiddenField runat="server" ID="hfsrno3" />
                                                    <asp:TextBox runat="server" ID="txtinsurer3" placeholder="Enter insurer" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtpolicyno3" placeholder="Enter policy no." CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtsumassured3" placeholder="Enter sum assured" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddacceptance3" CssClass="form-control select2">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="Standard">Standard</asp:ListItem>
                                                        <asp:ListItem Value="Rated">Rated</asp:ListItem>
                                                        <asp:ListItem Value="PostPhone">PostPhone</asp:ListItem>
                                                        <asp:ListItem Value="Decline">Decline</asp:ListItem>
                                                        <asp:ListItem Value="Decline">Accepted at extra premium</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddcurrentstatus3" CssClass="form-control select2">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="Inforce">Inforce</asp:ListItem>
                                                        <asp:ListItem Value="Reduced cover">Reduced cover</asp:ListItem>
                                                        <asp:ListItem Value="Lapsed">Lapsed</asp:ListItem>
                                                        <asp:ListItem Value="Surrendered">Surrendered</asp:ListItem>
                                                        <asp:ListItem Value="Withdrawn">Withdrawn</asp:ListItem>
                                                    </asp:DropDownList></td>

                                            </tr>
                                        </tbody>
                                    </table>--%>
                                            </div>

                </div>
            </div>
        </div>
    </div>
</asp:Panel>
<%--health details--%>
<asp:Panel class="box box-health-question box-warning box-solid collapsed-box" runat="server" ID="pnlHealthDetails">
    <div class="box-header with-border">
        <div class="col-md-12">
            <div class="col-md-9">
                <h3 class="box-title">section #. Health details</h3>
            </div>
            <div class="col-md-3">
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse" onclick="fnCollapsedBox(this);">
                        <i class="fa fa-minus"></i>
                    </button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.box-header -->

    <div class="box-body" id="Section11" runat="server" ondblclick="fnCollapsedBox(this);">
        <div class="row"></div>
        <div class="col-md-12">
            <div class="col-md-12" runat="server" id="divEditSection" visible="false">
                <asp:CheckBox ID="cbEditSection" AutoPostBack="false" runat="server" Text="Edit" />
            </div>
            <%--<asp:UpdatePanel runat="server" ID="uplifeinsurance" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-md-12" id="hidelifeinsurance" runat="server">
                                <h4 class="box-title"><b>1.28 If you have ticked( <i class="fa fa-check"></i>) Yes for Life or Health Insurance in the question above (1.27) or you have submitted any proposal/application for life/ health/ rider cover with any life/ health insurance company, please provide details of policies/ applications below.</b></h4>

                                <div class="col-md-12 table-responsive" style="padding: 0px;">
                                    <table class="table table-bordered">
                                        <tbody>
                                            <tr>
                                                <th style="width: 10%;">Name of the insurer</th>
                                                <th style="width: 10%;">Policy no.</th>
                                                <th style="width: 10%;">Sum assured</th>
                                                <th style="width: 25%;">Acceptance terms <span>(standard/ rated/ postponed/ declined/ accepted at extra premium)</span></th>
                                                <th style="width: 25%;">Current Status <span>(in force/ reduced cover/ lapse/ surrendered/ withdrawn)</span></th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:HiddenField runat="server" ID="hfsrno1" />
                                                    <asp:TextBox runat="server" ID="txtinsurer1" placeholder="Enter insurer" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtpolicyno1" placeholder="Enter policy no." CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtsumassured1" placeholder="Enter sum assured" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddacceptance1" CssClass="form-control select2">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="Standard">Standard</asp:ListItem>
                                                        <asp:ListItem Value="Rated">Rated</asp:ListItem>
                                                        <asp:ListItem Value="PostPhone">PostPhone</asp:ListItem>
                                                        <asp:ListItem Value="Decline">Decline</asp:ListItem>
                                                        <asp:ListItem Value="Decline">Accepted at extra premium</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddcurrentstatus1" CssClass="form-control select2">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="Inforce">Inforce</asp:ListItem>
                                                        <asp:ListItem Value="Reduced cover">Reduced cover</asp:ListItem>
                                                        <asp:ListItem Value="Lapsed">Lapsed</asp:ListItem>
                                                        <asp:ListItem Value="Surrendered">Surrendered</asp:ListItem>
                                                        <asp:ListItem Value="Withdrawn">Withdrawn</asp:ListItem>
                                                    </asp:DropDownList></td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:HiddenField runat="server" ID="hfsrno2" />
                                                    <asp:TextBox runat="server" ID="txtinsurer2" placeholder="Enter insurer" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtpolicyno2" placeholder="Enter policy no." CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtsumassured2" placeholder="Enter sum assured" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddacceptance2" CssClass="form-control select2">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="Standard">Standard</asp:ListItem>
                                                        <asp:ListItem Value="Rated">Rated</asp:ListItem>
                                                        <asp:ListItem Value="PostPhone">PostPhone</asp:ListItem>
                                                        <asp:ListItem Value="Decline">Decline</asp:ListItem>
                                                        <asp:ListItem Value="Decline">Accepted at extra premium</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddcurrentstatus2" CssClass="form-control select2">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="Inforce">Inforce</asp:ListItem>
                                                        <asp:ListItem Value="Reduced cover">Reduced cover</asp:ListItem>
                                                        <asp:ListItem Value="Lapsed">Lapsed</asp:ListItem>
                                                        <asp:ListItem Value="Surrendered">Surrendered</asp:ListItem>
                                                        <asp:ListItem Value="Withdrawn">Withdrawn</asp:ListItem>
                                                    </asp:DropDownList></td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:HiddenField runat="server" ID="hfsrno3" />
                                                    <asp:TextBox runat="server" ID="txtinsurer3" placeholder="Enter insurer" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtpolicyno3" placeholder="Enter policy no." CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtsumassured3" placeholder="Enter sum assured" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddacceptance3" CssClass="form-control select2">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="Standard">Standard</asp:ListItem>
                                                        <asp:ListItem Value="Rated">Rated</asp:ListItem>
                                                        <asp:ListItem Value="PostPhone">PostPhone</asp:ListItem>
                                                        <asp:ListItem Value="Decline">Decline</asp:ListItem>
                                                        <asp:ListItem Value="Decline">Accepted at extra premium</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddcurrentstatus3" CssClass="form-control select2">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="Inforce">Inforce</asp:ListItem>
                                                        <asp:ListItem Value="Reduced cover">Reduced cover</asp:ListItem>
                                                        <asp:ListItem Value="Lapsed">Lapsed</asp:ListItem>
                                                        <asp:ListItem Value="Surrendered">Surrendered</asp:ListItem>
                                                        <asp:ListItem Value="Withdrawn">Withdrawn</asp:ListItem>
                                                    </asp:DropDownList></td>

                                            </tr>
                                        </tbody>
                                    </table>
                                </div>

                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chkproduct" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
            <div class="form-group">
                <asp:Panel runat="server" ID="QC2" class="col-md-12">
                    <%--style="padding: 0px;" --%>

                    <div class="col-md-3">
                        2.1 Height (Cms) 
                            <div class="form-group">
                                <h4>
                                    <label for="exampleInputEmail1">Height (Cms)  </label>
                                    <asp:TextBox runat="server" ID="txtHeight" class="form-control" placeholder="Enter Height"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator53" CssClass="reqerror" ErrorMessage="Required" ValidationGroup="section2" ControlToValidate="txtHeight" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </h4>
                            </div>

                    </div>

                    <div class="col-md-3">
                        2.2 Weight (Kgs)
                            <div class="form-group">
                                <h4>
                                    <label for="exampleInputEmail1">Weight (Kgs)  </label>
                                    <asp:TextBox runat="server" ID="txtWeight" class="form-control" placeholder="Enter Weight" onfocus="NG_DrawZone('Weight','C0','2','1142','2363','1258','1350')"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator54" CssClass="reqerror" ErrorMessage="Required" ValidationGroup="section2" ControlToValidate="txtWeight" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </h4>
                            </div>
                    </div>

                    <div class="col-md-6">
                        2.3 In past 6 months has your body weight changed more than 5Kgs?  
                            <div class="col-md-12">
                                <div class="form-group">
                                    <%--<asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                                        <ContentTemplate>--%>
                                    <h4>
                                        <div class="col-md-4">
                                            <asp:RadioButtonList runat="server" ID="rdbIsWeightChanged" onchange="fnShowAdjDiv(this);" AutoPostBack="false" CssClass="flat-red" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="Yes"></asp:ListItem>
                                                <asp:ListItem Value="No"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>


                                        <div class="col-md-8 HideControl HideShowDiv" id="divHideWeightCause" runat="server">
                                            <%--style="padding: 0px;" --%>
                                            <div class="col-md-12">
                                                <%--style="padding: 0px;" --%>
                                                <div class="form-group">
                                                    <label for="exampleInputEmail1">(Cause of Change)</label>
                                                    <asp:TextBox runat="server" ID="txtCauseOfWtChange" placeholder="Enter Cause of Change" class="form-control"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                    </h4>
                                    <%--</ContentTemplate>
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="rdbweight" />

                                        </Triggers>-->
                                    </asp:UpdatePanel>--%>
                                </div>

                            </div>
                    </div>
                    <%--<div class="col-md-3">
                            <h4 class="box-title"><b>2.4 If pregnant, enter approximate due date of delivery</b></h4>

                            <div class="form-group">


                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>

                                    <asp:TextBox runat="server" ID="txtDeliveryDate" class="form-control" placeholder="dd/mm/yyyy" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask></asp:TextBox>

                                </div>
                                <!-- /.input group -->
                            </div>
                            <!-- /.form group -->
                        </div>--%>
                    <div style="clear: both;"></div>
                    <div class="col-md-12">
                        2.5. Please(<i class="fa fa-check"></i>) Yes or No indicate if LA were ever been diagnosed with, treated for, or advised to seek treatment from any of the following condition? If Yes, details provided in 2.6
                            <div class="col-md-12 table-responsive">
                                <%--style="padding: 0px;" --%>
                                <table class="table table-bordered">
                                    <tbody>
                                        <asp:Repeater runat="server" ID="rpHealthQuestion" ClientIDMode="AutoID" OnItemDataBound="rpHealthQuestion_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Literal runat="server" ID="lblquestion" Text='<%# Eval("ENTITY_DESC") %>'></asp:Literal>
                                                        <asp:Literal runat="server" ID="Literal1" Visible="false" Text='<%# Eval("COL_01") %>'></asp:Literal></td>
                                                    <td>
                                                        <h4>
                                                            <asp:DropDownList CssClass="form-control" Style="width: 100px;" runat="server" ID="ddlQuestionHealthStatus">
                                                                <asp:ListItem Value="NA">Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" CssClass="reqerror HideControl" InitialValue="NA" ErrorMessage="Required" ValidationGroup="btnsubmit" ControlToValidate="ddlQuestionHealthStatus" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </h4>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>

                        <asp:Button runat="server" ID="btnsubmit" Text="Submit" ValidationGroup="btnsubmit" OnClientClick="showloading()" CssClass="btn btn-danger pull-right HideControl" OnClick="btnsubmit_Click" />
                    </div>
                    <div style="clear: both"></div>
                    <%--<asp:UpdatePanel runat="server" ID="upillness" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                    <asp:HiddenField runat="server" ID="hfcheckclick" />
                    <div class="col-lg-12" runat="server" id="hidelife" visible="false">
                        2.6 Details of illness/ injury/ treatment.If any of the response to the question above (Q 2.5) is Yes Please provide details below: 
                                    <div class="col-md-12 table-responsive">
                                        <%--style="padding: 0px;" --%>
                                        <table class="table table-bordered">
                                            <tbody>
                                                <tr>
                                                    <th>Q.No</th>
                                                    <th>Name of the illness/ injury suffered/ surgery undergone</th>
                                                    <th>Date of first diagnosis</th>
                                                    <th>Treatment details(exact details of medication)</th>
                                                    <th>Current Status</th>

                                                </tr>

                                                <asp:Repeater runat="server" ID="rplifeinsurance">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <%-- <td style="text-align: center;">
                                                                        <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />
                                                                        <asp:Label runat="server" ID="lblrow" Text='<%# Eval("RowNumber") %>'></asp:Label>
                                                                    </td>--%>

                                                            <td style="text-align: center;">
                                                                <asp:Literal runat="server" ID="lblHDT_code" Text='<%# Eval("HDT_code") %>'></asp:Literal>
                                                            </td>
                                                            <td>

                                                                <asp:TextBox runat="server" ID="txtillness1" CssClass="form-control" placeholder="Enter Name of illness"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtdatediagnosis1" CssClass="form-control" placeholder="DD-MM-YYYY"></asp:TextBox>
                                                            </td>

                                                            <td>
                                                                <asp:TextBox runat="server" ID="txttreatment1" CssClass="form-control" placeholder="Enter Treatment"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtstatus1" CssClass="form-control" placeholder="Enter status"></asp:TextBox>
                                                            </td>


                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>


                                            </tbody>
                                        </table>
                                    </div>

                    </div>
                    <%--</ContentTemplate>
                            <%--<Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnsubmit" />
                            </Triggers>-->
                        </asp:UpdatePanel>--%>
                    <div style="clear: both"></div>
                    <%--<div class="col-lg-12" style="padding: 0px;">
                            <div class="col-lg-10">
                                <h4 class="box-title"><b>2.7 Has any of your immediate family member (father/mother/sister/brother), whether living or deceased, ever been diagnosed with Diabetes, Cancer, stroke, Heart, Heart ailment or any neurological disorder before age 65?  </b></h4>

                            </div>
                            <div class="col-lg-2">
                                <asp:DropDownList runat="server" CssClass="form-control" ID="dddecidefamily" Style="margin-top: 8px;" AutoPostBack="false" onchange="fnShowAdjDivDdl(this);">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="True">Yes</asp:ListItem>
                                    <asp:ListItem Value="False">No</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator56" CssClass="reqerror" ErrorMessage="Required" InitialValue="0" ValidationGroup="section2" ControlToValidate="dddecidefamily" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>

                            </div>

                        </div>--%>
                </asp:Panel>
                <div class="row" style="width: 199px; margin: 0 auto; margin-top: 20px; margin-bottom: 15px;">

                    <div class="col-lg-12">

                        <asp:LinkButton runat="server" ID="lnkupdate2" ValidationGroup="section2" CssClass="btn btn-block btn-success btn-flat HideControl" Text="Save & Proceed" OnClick="lnkupdate2_Click"></asp:LinkButton>

                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Panel>

<%--female details--%>
<asp:Panel class="box box-health-question box-warning box-solid collapsed-box" runat="server" ID="Panel1">
    <div class="box-header with-border">
        <div class="col-md-12">
            <div class="col-md-9">
                <h3 class="box-title">section #. Female Details</h3>
            </div>
            <div class="col-md-3">
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse" onclick="fnCollapsedBox(this);">
                        <i class="fa fa-minus"></i>
                    </button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.box-header -->

    <div class="box-body" id="Section2" runat="server" ondblclick="fnCollapsedBox(this);">
        <div class="row">
        </div>
        <div class="form-group">
            <div class="col-md-4">
                <div class="form-group">
                    <label for="exampleInputEmail1">Spouse Name</label>
                    <asp:TextBox runat="server" ID="txtSpouseName" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label for="exampleInputEmail1">Spouse Occupation</label>
                    <asp:TextBox runat="server" ID="txtSpouseOccupation" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label for="exampleInputEmail1">Spouse Annual Income</label>
                    <asp:TextBox runat="server" ID="txtSpouseAnnualIncome" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label for="exampleInputEmail1">Is Pregnent</label>
                    <asp:TextBox runat="server" ID="txtisPregnent" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <h4 class="box-title"><b>Approximate due date of delivery</b></h4>

                <div class="form-group">


                    <div class="input-group">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>

                        <asp:TextBox runat="server" ID="txtDeliveryDate" class="form-control" placeholder="dd/mm/yyyy" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask></asp:TextBox>

                    </div>
                    <!-- /.input group -->
                </div>
                <!-- /.form group -->
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label for="exampleInputEmail1">Details of any complication or miscarrage or .. at deliverty</label>
                    <asp:TextBox runat="server" ID="txtComplication" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label for="exampleInputEmail1">
                        Have you had or have any gynaecological problem or been adviced to 
                                    have mammogram boipsy or operation of the breats,pelvis or any other gynaecological tests?</label>
                    <asp:TextBox runat="server" ID="txtGynaecological" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>

<%--family details of LA--%>
<asp:Panel class="box box-health-question box-warning box-solid collapsed-box" runat="server" ID="Panel3">
    <div class="box-header with-border">
        <div class="col-md-12">
            <div class="col-md-9">
                <h3 class="box-title">section #. Family Details</h3>
            </div>
            <div class="col-md-3">
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse" onclick="fnCollapsedBox(this);">
                        <i class="fa fa-minus"></i>
                    </button>

                </div>
            </div>
        </div>
    </div>
    <!-- /.box-header -->
    <div class="box-body" id="Section3" runat="server" ondblclick="fnCollapsedBox(this);">
        <div class="row">
        </div>
        <div class="col-lg-12 ">
            <%--HideControl HideShowDiv--%>
            <%--<asp:UpdatePanel runat="server" ID="UpdatePanel10" UpdateMode="Conditional">
                                <ContentTemplate>--%>
            <div class="col-lg-12 table-responsive" runat="server" id="divHideFamily">
                <%--style="padding: 0px;" --%>
                <asp:GridView ID="gvFamilyHealth" HeaderStyle-CssClass="btn-primary" runat="server" ShowFooter="false" Style="border-top: 0px;" CssClass="table table-bordered"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                FamilyMember
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblRelationShip" Text='<%# Eval("RELATIONSHIP")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Diagnosis (Disease /Ailment)
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDiagnosis" Text='<%# Eval("DIAGNOSIS")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Age at on set (Diagnosis)
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAgeAtOnSet" Text='<%# Eval("AGE_AT_ON_SET")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Living/Deceased (Currently)
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblLivingDeceased" Text='<%# Eval("LIVING_DECESED")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Cause Of Death
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCauseOfDeath" Text='<%# Eval("CAUSE_OF_DEATH")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div>
            </div>
            <%--</ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Gridview1" />
                                    <asp:AsyncPostBackTrigger ControlID="dddecidefamily" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
        </div>
    </div>
</asp:Panel>
<%--personal habit of LA--%>
<asp:Panel class="box box-health-question box-warning box-solid collapsed-box" runat="server" ID="Panel4">
    <div class="box-header with-border">
        <div class="col-md-12">
            <div class="col-md-9">
                <h3 class="box-title">section #. Personal Habit Details</h3>
            </div>
            <div class="col-md-3">
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse" onclick="fnCollapsedBox(this);">
                        <i class="fa fa-minus"></i>
                    </button>

                </div>
            </div>
        </div>
    </div>
    <!-- /.box-header -->

    <div class="box-body" id="Section4" runat="server" ondblclick="fnCollapsedBox(this);">
        <div class="row">
        </div>
        <div class="form-group">
            <div class="col-md-4">
                <div class="form-group">
                    <label for="exampleInputEmail1">Health Details</label>
                    <asp:TextBox runat="server" ID="txtLaHealthDetails" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label for="exampleInputEmail1">Life Style Details</label>
                    <asp:TextBox runat="server" ID="txtLaLifeStyleDetails" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>
<%--simultanious appln and previous--%>
<asp:Panel class="box box-health-question box-warning box-solid collapsed-box" runat="server" ID="Panel5">
    <div class="box-header with-border">
        <div class="col-md-12">
            <div class="col-md-9">
                <h3 class="box-title">section #. Previous Policy Details</h3>
            </div>
            <div class="col-md-3">
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse" onclick="fnCollapsedBox(this);">
                        <i class="fa fa-minus"></i>
                    </button>

                </div>
            </div>
        </div>
    </div>
    <!-- /.box-header -->

    <div class="box-body" id="Section5" runat="server" ondblclick="fnCollapsedBox(this);">
        <div class="row">
        </div>
        <div class="col-lg-12 ">
            <%--HideControl HideShowDiv--%>
            <%--<asp:UpdatePanel runat="server" ID="UpdatePanel10" UpdateMode="Conditional">
                                <ContentTemplate>--%>
            <div class="col-lg-12 table-responsive" runat="server" id="div6">
                <%--style="padding: 0px;" --%>
                <asp:GridView ID="gvSimultaniousDetails" HeaderStyle-CssClass="btn-primary" runat="server" ShowFooter="false" Style="border-top: 0px;" CssClass="table table-bordered"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Application Number
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblApplnNo" Text='<%# Eval("ApplnNo")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Policy Number
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblApplnNo" Text='<%# Eval("PolicyNo")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Status
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" Text='<%# Eval("ApplnNoStatus")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div>
            </div>
            <%--</ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Gridview1" />
                                    <asp:AsyncPostBackTrigger ControlID="dddecidefamily" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
        </div>
    </div>
</asp:Panel>
<%--LA DETAILS--%>
<asp:Panel class="box box-warning box-solid collapsed-box HideControl" runat="server" ID="Panel7">
    <div class="box-header with-border">
        <div class="col-md-12">
            <div class="col-md-9">
                <h3 class="box-title">section #. LA DETAIS</h3>
            </div>
        </div>
        <div class="col-md-3">
            <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse" onclick="fnCollapsedBox(this);">
                    <i class="fa fa-minus"></i>
                </button>
            </div>
        </div>
    </div>
    <!-- /.box-header -->

    <div class="box-body" id="Div4" runat="server">
        <div class="row">
        </div>
        <div class="col-md-3">
            2.1 Height (Cms) 
                            <div class="form-group">
                                <h4>
                                    <label for="exampleInputEmail1">Height (Cms)  </label>
                                    <asp:TextBox runat="server" ID="TextBox1" class="form-control" placeholder="Enter Height"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="reqerror" ErrorMessage="Required" ValidationGroup="section2" ControlToValidate="txtHeight" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </h4>
                            </div>

        </div>

    </div>
</asp:Panel>
<%--PROPOSAR DETAILS--%>
<asp:Panel class="box box-warning box-solid collapsed-box" runat="server" ID="pnlProposarDetails">
    <div class="box-header with-border">
        <div class="col-md-12">
            <div class="col-md-9">
                <h3 class="box-title">section #. Proposar Details</h3>
            </div>
            <div class="col-md-3">
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse" onclick="fnCollapsedBox(this);">
                        <i class="fa fa-minus"></i>
                    </button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.box-header -->

    <div class="box-body" id="Div5" runat="server">
        <div class="row">
        </div>
        <div style="clear: both;"></div>
        <div class="col-md-12">
            1.11 Have you ever been investigated, prosecuted or have been issued a charge sheet in respect of any criminal/civil offence in any court of law in india or abroad?  
        </div>

        <div class="col-md-12">
            <%--style="padding: 0px;" --%>
            <div class="form-group">
                <%--<asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                <h4>
                    <div class="col-md-4">
                        <asp:RadioButtonList runat="server" ID="rdoProposarIsCriminal" AutoPostBack="false" onchange="fnShowAdjDiv(this);" CssClass="flat-red" RepeatDirection="Horizontal">
                            <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="False" Text="No"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>


                    <div class="col-md-8 HideShowDiv HideControl" runat="server" id="divIsProposarCriminal">
                        <%--style="padding: 0px;" --%>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="exampleInputEmail1">If Yes, <span style="font-size: 12px;">(Please provide details)</span></label>
                                <asp:TextBox runat="server" ID="txtProposarIsCriminal" placeholder="Enter Details" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" CausesValidation="false" ID="RequiredFieldValidator3" CssClass="reqerror" Enabled="false" ErrorMessage="Required" ValidationGroup="section1" ControlToValidate="txtCriminalDetails" Display="Dynamic"></asp:RequiredFieldValidator>

                            </div>
                        </div>
                    </div>
                </h4>
                <%--                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rdbcriminal" />

                                </Triggers>
                            </asp:UpdatePanel>--%>
            </div>

        </div>

        <div style="clear: both;"></div>
        <div class="col-md-12">
            1.26 Are you associated or have been associated with a political party or politican or holidung a senior position in ministry, government, state owned entrprise, judicial body in India or aborad or a family member or associate of any person in said capacity?  
        </div>
        <h4>
            <div class="col-md-12">
                <%--style="padding: 0px;" --%>
                <div class="form-group">
                    <%--<asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                    <h4>
                        <div class="col-md-4">
                            <asp:RadioButtonList runat="server" ID="rdoProposarIsPolitician" onchange="fnShowAdjDiv(this);" AutoPostBack="false" CssClass="flat-red" RepeatDirection="Horizontal">
                                <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                                <asp:ListItem Value="False" Text="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>


                        <div class="col-md-8 HideShowDiv HideControl" runat="server" id="dibProposalIsPolitician">
                            <%--style="padding: 0px;" --%>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label for="exampleInputEmail1">If Yes, <span style="font-size: 12px;">(Please provide details)</span></label>
                                    <asp:TextBox runat="server" ID="txtProposarIsPolitician" placeholder="Enter Details" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" CausesValidation="false" ID="RequiredFieldValidator2" CssClass="reqerror" ErrorMessage="Required" Enabled="false" ValidationGroup="section1" ControlToValidate="txtPoliticalDetails" Display="Dynamic"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                        </div>
                    </h4>
                    <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                </div>

            </div>
        </h4>
    </div>
</asp:Panel>
<%--template--%>
<asp:Panel class="box box-warning box-solid collapsed-box HideControl" runat="server" ID="pnlTemplate">
    <div class="box-header with-border">
        <div class="col-md-12">
            <div class="col-md-9">
                <h3 class="box-title">section #. Female Details</h3>
            </div>
            <div class="col-md-3">
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse" onclick="fnCollapsedBox(this);">
                        <i class="fa fa-minus"></i>
                    </button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.box-header -->

    <div class="box-body" id="Div2" runat="server">
        <div class="row">
        </div>
    </div>
</asp:Panel>
<%--</ContentTemplate>
    <%--<Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkupdate1" />
        <asp:AsyncPostBackTrigger ControlID="chksection2" />
    </Triggers>-->
</asp:UpdatePanel>--%>
