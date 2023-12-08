using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using UWSaralObjects;
using UWSaralDecision;
public partial class UserControl_HealthProfile : System.Web.UI.UserControl
{
    DataSet _dsPrevPol = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //#region fill master data
                //fill health related issues 
                string strHealthQuestion = "GetHealthQuestion"
                        , strApplicationNo = "OT1035402"
                        , strGender = "Male"
                        , strRelation = "FillRelation"
                        , strTablename = "T5663"
                        , strGetDrug = "GetDrugDetails";

                string strAppType = "offline"
                    , LAPushErrorMsg = string.Empty;
                int intLAPushErrorCode = 0;
                UWSaralDecision.BussLayer objBusinessLayer = new UWSaralDecision.BussLayer();
                ChangeValue objChangeValue = new ChangeValue();

                Commfun objCommfun = new Commfun();
                DataSet ds = new DataSet();
                string strConsentRespons = string.Empty;
                if (Request.QueryString["qsAppNo"] != null)
                {
                    strApplicationNo = Request.QueryString["qsAppNo"];
                    //strApplicationNo = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsAppNo"]);
                }
                if (!Page.IsPostBack)
                {
                    objCommfun.FetchClientInfoOnApplciationNo(ref ds, strApplicationNo, "LA");
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        strGender = Convert.ToString(ds.Tables[0].Rows[0]["GENDER"]);
                    }
                    ds.Clear();
                    ////health questions
                    //objCommfun.FillHealthQuetions(ref ds, strHealthQuestion, strApplicationNo, strGender);
                    //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    //{
                    //    rpHealthQuestion.DataSource = ds;
                    //}
                    //else
                    //{
                    ////    rpHealthQuestion.DataSource = null;
                    //}
                    //rpHealthQuestion.DataBind();

                    objCommfun.FillHealthQuetions(ref ds, strRelation, strTablename, strGender);
                    gvFamilyHealth.DataSource = null;
                    gvFamilyHealth.DataBind();
                    ds.Clear();
                    //tobaco and alcohol 
                    objCommfun.FillHealthQuetions(ref ds, strGetDrug, strApplicationNo, strGender);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        DataTable dt1 = new DataTable();
                        dt1.Columns.Add("DRUG_TYPE");
                        dt = ds.Tables[0];
                        DataRow[] rowtable = dt.Select("SNO>=5 AND SNO<=6", "SNO asc");

                        foreach (DataRow row in rowtable)
                        {
                            dt1.Rows.Add(row[3]);
                        }

                        rpTobaco.DataSource = dt1;
                        rpTobaco.DataBind();
                        dt1.Clear();
                        DataRow[] rowtable1 = dt.Select("SNO>=1 AND SNO<=4", "SNO asc");
                        foreach (DataRow row in rowtable1)
                        {
                            dt1.Rows.Add(row[3]);
                        }
                        rpAlcohol.DataSource = dt1;
                        rpAlcohol.DataBind();
                        dt1.Clear();
                    }
                    else
                    {
                        rpTobaco.DataSource = null;
                        rpAlcohol.DataSource = null;
                    }
                    ds.Clear();
                    //#endregion

                    //#region map data
                    HealthDeatils objHealthDeatils = new HealthDeatils();
                    objHealthDeatils.FillHealthUserInput(ref ds, strApplicationNo);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        FillHealthDetailsUserInput(ds);
                    }
                    ds.Clear();
                    objHealthDeatils.FillBmiUserInput(ref ds, strApplicationNo);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        FillBMIDetailsUserInput(ds);
                    }
                    ds.Clear();
                    objHealthDeatils.FillHealthQuestionAnswerUserInput(ref ds, strApplicationNo);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        FillHealthQuestionAnswerUserInput(ds);
                    }
                    ds.Clear();
                    objHealthDeatils.FillCriminalPoliticalDetails(ref ds, strApplicationNo, "LA");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        FillCriminalPoliticalDetails(ds);
                    }
                    ds.Clear();
                    objHealthDeatils.FillFamilyInfo(ref ds, strApplicationNo);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        FillFamilyStatus(ds);
                    }
                    //else
                    //{
                    //    if (dddecidefamily.Items.FindByValue("False") != null)
                    //    {
                    //        dddecidefamily.Items.FindByValue("False").Selected = true;
                    //    }
                    //}
                    ds.Clear();
                    objHealthDeatils.FillAssetInfo(ref ds, strApplicationNo);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        FillAssetInfo(ds);
                    }
                    ds.Clear();

                    objHealthDeatils.FillSimulatniousApplication(ref ds, strApplicationNo);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        FillSimultaniousDetails(ds);
                    }
                    ds.Clear();

                    //ofac service call                        
                    objBusinessLayer.OnlineApplicationLAServiceDetails_PUSH(strApplicationNo, strAppType, objChangeValue, ref ds,ref _dsPrevPol, "OFAC"
                        , ref intLAPushErrorCode, ref LAPushErrorMsg, ref strConsentRespons);
                    if (intLAPushErrorCode == 0)
                    {
                        txtOfac.Text = "Verified";
                    }
                    //#endregion 
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

    }
    protected void dddecidefamily_TextChanged(object sender, EventArgs e)
    {

    }
    protected void rdbweight_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void chksection2_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {

    }
    protected void Gridview1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void Gridview1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void lnkupdate2_Click(object sender, EventArgs e)
    {

    }
    protected void rdbtobacco_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void chktabacoo_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void txttabaccoquantity_TextChanged(object sender, EventArgs e)
    {

    }
    protected void chkalcohol_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void txtalcoholquantity_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddno_TextChanged(object sender, EventArgs e)
    {

    }
    protected void rdbHeroin_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btncheckbox_Click(object sender, EventArgs e)
    {

    }
    protected void rdbassociated_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void rdbcriminal_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void rdbalcoholic_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void chkproduct_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void rdbsport_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void FillHealthDetailsUserInput(DataSet ds)
    {
        //int intCountTobaco = 0;
        //int intCountAlcohol = 0;
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //tobaco
                foreach (RepeaterItem item in rpTobaco.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        CheckBox chkDoConsume = (CheckBox)item.FindControl("chkTabacoo");
                        Literal ltTobacoName = (Literal)item.FindControl("lblTabacooName");
                        TextBox txtTobacoQuantity = (TextBox)item.FindControl("txtTabaccoQuantity");
                        if (ltTobacoName.Text.Equals(Convert.ToString(ds.Tables[0].Rows[i]["HABIT_NAME"])) && Convert.ToBoolean(ds.Tables[0].Rows[i]["IS_SMOKER"]))
                        {
                            //intCountTobaco++;
                            chkDoConsume.Checked = true;
                            txtTobacoQuantity.Text = Convert.ToString(ds.Tables[0].Rows[i]["HABIT_QUANTITY"]);
                            //divToboacoList.Attributes["class"] = divToboacoList.Attributes["class"].Replace("HideControl", string.Empty);
                        }
                    }
                }
                //alcohol
                foreach (RepeaterItem item in rpAlcohol.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        CheckBox chkDoConsume = (CheckBox)item.FindControl("chkAlcohol");
                        Literal ltTobacoName = (Literal)item.FindControl("lblAlcoholName");
                        TextBox txtTobacoQuantity = (TextBox)item.FindControl("txtAlcoholQuantity");
                        if (ltTobacoName.Text.Equals(Convert.ToString(ds.Tables[0].Rows[i]["HABIT_NAME"])) && Convert.ToBoolean(ds.Tables[0].Rows[i]["IS_ALCOHOLIC"]))
                        {
                            //intCountAlcohol++;
                            chkDoConsume.Checked = true;
                            txtTobacoQuantity.Text = Convert.ToString(ds.Tables[0].Rows[i]["HABIT_QUANTITY"]);
                            divAlcoholDetails.Attributes["class"] = divAlcoholDetails.Attributes["class"].Replace("HideControl", string.Empty);
                        }
                    }
                }
            }

            //if (intCountAlcohol > 0)
            //{
            //    if (rdbIsAlcoholic.Items.FindByValue("True") != null)
            //    {
            //        rdbIsAlcoholic.Items.FindByValue("True").Selected = true;
            //    }
            //}
            //else
            //{
            //    if (rdbIsAlcoholic.Items.FindByValue("False") != null)
            //    {
            //        rdbIsAlcoholic.Items.FindByValue("False").Selected = true;
            //    }
            //}

            //if (intCountTobaco > 0)
            //{
            //    if (rdbIsConsumeTobacco.Items.FindByValue("True") != null)
            //    {
            //        rdbIsConsumeTobacco.Items.FindByValue("True").Selected = true;
            //    }
            //}
            //else
            //{
            //    if (rdbIsConsumeTobacco.Items.FindByValue("False") != null)
            //    {
            //        rdbIsConsumeTobacco.Items.FindByValue("False").Selected = true;
            //    }
            //}

            //alcohol
            if (rdbIsAlcoholic.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["IS_ALCOHOLIC"])) != null)
            {
                rdbIsAlcoholic.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["IS_ALCOHOLIC"])).Selected = true;
            }

            //NARCOTIC
            if (rdbHeroin.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["IS_NARCOTIC"])) != null)
            {
                rdbHeroin.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["IS_NARCOTIC"])).Selected = true;
            }
            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_NARCOTIC"]))
            {
                divHeroineDetails.Attributes["class"] = divHeroineDetails.Attributes["class"].Replace("HideControl", string.Empty);
                txtHeroineDetails.Text = Convert.ToString(ds.Tables[0].Rows[0]["NARCOTIC_TYPE"]);
            }
            //hobbies
            if (rdbSport.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["IS_ADVENTUROUS"])) != null)
            {
                rdbSport.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["IS_ADVENTUROUS"])).Selected = true;
            }
            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_ADVENTUROUS"]))
            {
                divSportDetails.Attributes["class"] = divHeroineDetails.Attributes["class"].Replace("HideControl", string.Empty);
                txtSportDetails.Text = Convert.ToString(ds.Tables[0].Rows[0]["HOBBIES"]);
            }

            ////tobacoo never used
            if (Convert.ToString(ds.Tables[0].Rows[0]["IS_SMOKER"]).ToLower().Equals("true"))
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["STOPPED_MONTH"])))
                {
                    if (ddlStillConsumeTobacoo.Items.FindByValue("true") != null)
                    {
                        ddlStillConsumeTobacoo.Items.FindByValue("true").Selected = true;
                    }
                    divStoppedOnMonthYear.Attributes["class"] = divStoppedOnMonthYear.Attributes["class"].Replace("HideControl", string.Empty);
                    txtStopped.Text = Convert.ToString(ds.Tables[0].Rows[0]["STOPPED_MONTH"]);
                }
                else
                {
                    if (ddlStillConsumeTobacoo.Items.FindByValue("false") != null)
                    {
                        ddlStillConsumeTobacoo.Items.FindByValue("false").Selected = true;
                    }
                }
                divUsedTobaco.Attributes["class"] = divUsedTobaco.Attributes["class"].Replace("HideControl", string.Empty);
                if (rdbIsConsumeTobacco.Items.FindByValue("False") != null)
                {
                    rdbIsConsumeTobacco.Items.FindByValue("False").Selected = true;
                }
            }
            else
            {
                divToboacoList.Attributes["class"] = divToboacoList.Attributes["class"].Replace("HideControl", string.Empty);
                if (rdbIsConsumeTobacco.Items.FindByValue("True") != null)
                {
                    rdbIsConsumeTobacco.Items.FindByValue("True").Selected = true;
                }
            }
            txtLaLifeStyleDetails.Text = Convert.ToString(ds.Tables[0].Rows[0]["LIFE_STYLE"]);
            txtLaHealthDetails.Text = Convert.ToString(ds.Tables[0].Rows[0]["HEALTH_DETAILS"]);

            //if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["STOPPED_MONTH"])))
            //{
            //    if (ddlStillConsumeTobacoo.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["TOBACCO_USED"])) != null)
            //    {
            //        ddlStillConsumeTobacoo.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["TOBACCO_USED"])).Selected = true;
            //    }
            //    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["TOBACCO_USED"]))
            //    {
            //        divStoppedOnMonthYear.Attributes["class"] = divStoppedOnMonthYear.Attributes["class"].Replace("HideControl", string.Empty);
            //    }
            //} 
        }
    }
    private void FillBMIDetailsUserInput(DataSet ds)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtWeight.Text = Convert.ToString(ds.Tables[0].Rows[0]["WEIGHT"]);
            txtHeight.Text = Convert.ToString(ds.Tables[0].Rows[0]["HEIGHT"]);
            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_WEIGHT_CHANGED"]))
            {
                if (rdbIsWeightChanged.Items.FindByValue("Yes") != null)
                {
                    txtCauseOfWtChange.Text = Convert.ToString(ds.Tables[0].Rows[0]["CAUSE_OF_CHANGE"]);
                    rdbIsWeightChanged.Items.FindByValue("Yes").Selected = true;
                    divHideWeightCause.Attributes["class"] = divHideWeightCause.Attributes["class"].Replace("HideControl", string.Empty);
                }
            }
            else
            {
                if (rdbIsWeightChanged.Items.FindByValue("No") != null)
                {
                    rdbIsWeightChanged.Items.FindByValue("No").Selected = true;
                }
            }
        }
        if (ds.Tables.Count == 2)
        {
            txtDeliveryDate.Text = Convert.ToString(ds.Tables[1].Rows[0]["EXPECTED_DELIVERY"]);
            txtSpouseName.Text = Convert.ToString(ds.Tables[1].Rows[0]["SPOUSE_NAME"]);
            txtSpouseOccupation.Text = Convert.ToString(ds.Tables[1].Rows[0]["SPOUSE_OCCUPATION"]);
            txtSpouseAnnualIncome.Text = Convert.ToString(ds.Tables[1].Rows[0]["SPOUSE_ANNUAL_INCOME"]);
            txtGynaecological.Text = Convert.ToString(ds.Tables[1].Rows[0]["GYNAECOLOGICAL_PROBLEM"]);
            txtisPregnent.Text = Convert.ToString(ds.Tables[1].Rows[0]["IS_PREGNANT"]);
            txtComplication.Text = Convert.ToString(ds.Tables[1].Rows[0]["COMPLICATION_DETAILS"]);
        }

    }
    private void FillHealthQuestionAnswerUserInput(DataSet ds)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpHealthQuestion.DataSource = ds.Tables[0];
        }
        rpHealthQuestion.DataBind();
    }
    protected void rpHealthQuestion_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList objHealthQuestion = (DropDownList)e.Item.FindControl("ddlQuestionHealthStatus");
            string strValue = (string)((DataRowView)e.Item.DataItem)["RESPONSE"];
            if (objHealthQuestion.Items.FindByValue(strValue) != null)
            {
                objHealthQuestion.Items.FindByValue(strValue).Selected = true;
            }
        }
    }
    private void FillCriminalPoliticalDetails(DataSet ds)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_CRIMINAL"]))
            {
                txtCriminalDetails.Text = Convert.ToString(ds.Tables[0].Rows[0]["CRIMINAL_DETAILS"]);
                divCriminalDetails.Attributes["class"] = divCriminalDetails.Attributes["class"].Replace("HideControl", string.Empty);
            }
            if (rdbCriminal.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["IS_CRIMINAL"])) != null)
            { rdbCriminal.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["IS_CRIMINAL"])).Selected = true; }
            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_POLITICIAN"]))
            {
                txtPoliticalDetails.Text = Convert.ToString(ds.Tables[0].Rows[0]["POLITICAL_DETAILS"]);
                divPoliticalDetails.Attributes["class"] = divPoliticalDetails.Attributes["class"].Replace("HideControl", string.Empty);
            }
            if (rdbPolitician.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["IS_POLITICIAN"])) != null)
            {
                rdbPolitician.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["IS_POLITICIAN"])).Selected = true;
            }

            txtMoralHazardRpt.Text = Convert.ToString(ds.Tables[0].Rows[0]["MORAL_HAZARD"]);
            txtNatureOfDuty.Text = Convert.ToString(ds.Tables[0].Rows[0]["NATURE_OF_DUTY"]);
            txtNsapConsent.Text = Convert.ToString(ds.Tables[0].Rows[0]["IS_NSAP"]);
            txtHazardousOccupation.Text = Convert.ToString(ds.Tables[0].Rows[0]["HAZARDOUS_OCCUPATION"]);
        }
        if (ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
        {
            if (Convert.ToBoolean(ds.Tables[1].Rows[0]["IS_CRIMINAL"]))
            {
                txtProposarIsCriminal.Text = Convert.ToString(ds.Tables[1].Rows[0]["CRIMINAL_DETAILS"]);
                divIsProposarCriminal.Attributes["class"] = divCriminalDetails.Attributes["class"].Replace("HideControl", string.Empty);
            }

            if (rdoProposarIsCriminal.Items.FindByValue(Convert.ToString(ds.Tables[1].Rows[0]["IS_CRIMINAL"])) != null)
            { rdoProposarIsCriminal.Items.FindByValue(Convert.ToString(ds.Tables[1].Rows[0]["IS_CRIMINAL"])).Selected = true; }

            if (Convert.ToBoolean(ds.Tables[1].Rows[0]["IS_POLITICIAN"]))
            {
                txtProposarIsPolitician.Text = Convert.ToString(ds.Tables[1].Rows[0]["POLITICAL_DETAILS"]);
                dibProposalIsPolitician.Attributes["class"] = divPoliticalDetails.Attributes["class"].Replace("HideControl", string.Empty);
            }

            if (rdoProposarIsPolitician.Items.FindByValue(Convert.ToString(ds.Tables[1].Rows[0]["IS_POLITICIAN"])) != null)
            {
                rdoProposarIsPolitician.Items.FindByValue(Convert.ToString(ds.Tables[1].Rows[0]["IS_POLITICIAN"])).Selected = true;
            }
        }
    }

    private void FillFamilyStatus(DataSet ds)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvFamilyHealth.DataSource = ds;
            //if (dddecidefamily.Items.FindByValue("True") != null)
            //{
            //    dddecidefamily.Items.FindByValue("True").Selected = true;
            //}
        }
        gvFamilyHealth.DataBind();
    }

    private void FillAssetInfo(DataSet ds)
    {
        //DataSet ds1 = new DataSet();
        if (ds.Tables[0].Rows.Count > 0)
        {
            string xmlString = ds.Tables[0].Rows[0][0].ToString();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);

            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("item");
            foreach (XmlNode node in nodes)
            {
                string value1 = node.InnerText;
                if (chkProduct.Items.FindByValue(value1) != null)
                {
                    chkProduct.Items.FindByValue(value1).Selected = true;
                }
                if (chkProduct.Items.FindByValue("Life Insurance") != null && chkProduct.Items.FindByValue("Life Insurance").Selected)
                {
                    divLifeInsurance.Attributes["class"] = divLifeInsurance.Attributes["class"].Replace("HideControl", string.Empty);
                }

                else if (chkProduct.Items.FindByValue("Health Insurance") != null && chkProduct.Items.FindByValue("Health Insurance").Selected)
                {
                    divLifeInsurance.Attributes["class"] = divLifeInsurance.Attributes["class"].Replace("HideControl", string.Empty);
                }
            }
        }
        if (ds.Tables.Count >= 2)
        {
            gvInsurancePlan.DataSource = ds.Tables[1];
            gvInsurancePlan.DataBind();
        }
    }

    private void FillSimultaniousDetails(DataSet ds)
    {
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            gvSimultaniousDetails.DataSource = ds;
            gvSimultaniousDetails.DataBind();
        }
    }
}