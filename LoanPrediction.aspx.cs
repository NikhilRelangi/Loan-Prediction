using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;


public partial class LoanPrediction : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["conString"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private void GetAttributeValues()
    {
        try
        {
            DataTable dtresidency = new DataTable();
            DataTable dtmarital_status = new DataTable();
            DataTable dtemployment_status = new DataTable();
            DataTable dtloan_type = new DataTable();

            // getting attribute values for every attribute
            #region getvaluesfromdatabase
            cmd = new SqlCommand("Getatributevalues", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables.Count > 3)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtresidency = ds.Tables[0]; // attribute values for residency
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    dtmarital_status = ds.Tables[1]; // attribute values for marital status
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    dtemployment_status = ds.Tables[2];// attribute values for employment status
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    dtloan_type = ds.Tables[3];// attribute values for loan type
                }
            }
            #endregion

            //show attribute values
            #region show attribute values
            string attribute_values = "";

            // show residency values
            attribute_values += "</br>Attributes values for residency are:</br>";
            foreach (DataRow dataRow in dtresidency.Rows)
            {
                foreach (var item in dataRow.ItemArray)
                {
                    attribute_values += item + " ,";
                }
            }
            attribute_values = attribute_values.Substring(0, attribute_values.Length - 2);
            attribute_values += "<br/>";

            // showmaritalstatus values
            attribute_values += "</br>Attributes values for marital status are:</br>";
            foreach (DataRow dataRow in dtmarital_status.Rows)
            {
                foreach (var item in dataRow.ItemArray)
                {
                    attribute_values += item + " ,";
                }
            }
            attribute_values = attribute_values.Substring(0, attribute_values.Length - 2);
            attribute_values += "<br/>";


            // show employment status values
            attribute_values += "</br>Attributes values for employment status are:</br>";
            foreach (DataRow dataRow in dtemployment_status.Rows)
            {
                foreach (var item in dataRow.ItemArray)
                {
                    attribute_values += item + " ,";
                }
            }
            attribute_values = attribute_values.Substring(0, attribute_values.Length - 2);
            attribute_values += "<br/>";
            Label1.Text += attribute_values;

            // show loan type values
            attribute_values += "</br>Attributes values for loan type are:</br>";
            foreach (DataRow dataRow in dtloan_type.Rows)
            {
                foreach (var item in dataRow.ItemArray)
                {
                    attribute_values += item + " ,";
                }
            }
            attribute_values = attribute_values.Substring(0, attribute_values.Length - 2);
            attribute_values += "<br/>";
            Label1.Text += attribute_values;
            #endregion

            conn.Close();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message.ToString();
        }
    }
    private double PartialProbability(string loanDecision, string age, string residency, string marital_status, string employment_status, string credit_score, string dti, string loan_type, bool withSmoothing, int xClasses)
    {

        int totalApproved = 0;
        int totalDenied = 0;
        int totalCases = 0;
        int totalToUse = 0;

        // get total training cases for approved and denied loans
        #region get total count for aproved and denied loans
        cmd = new SqlCommand("GetTotalCount", conn);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        DataSet ds = new DataSet();
        da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        if (ds.Tables.Count > 1)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                totalApproved = Convert.ToInt16(ds.Tables[0].Rows[0][0]); //total training cases with loan approved
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                totalDenied = Convert.ToInt16(ds.Tables[1].Rows[0][0]); //total training cases with loan denied
            }

            totalCases = totalApproved + totalDenied; // total training cases
        }
        #endregion

       // Label1.Text += "</br>Total Approved: " + totalApproved.ToString() + "</br>" + "Total Denied: " + totalDenied.ToString() + "</br>" + "Total Cases: " + totalCases.ToString() + "</br>";

        if (loanDecision == "Approved") totalToUse = totalApproved; // for loan decision approved
        else if (loanDecision == "Denied") totalToUse = totalDenied; // for loan decision denied

        // probabilities for each attribute
        double pDecision = (totalToUse * 1.0) / (totalCases); // Prob approved or denied
        double pAge = 1.0; // probability for age
        double pResidency = 1.0; // probability for residency
        double pMaritalStatus = 1.0; // probability for marital status
        double pEmploymentStatus = 1.0; // probability for employment status
        double pCreditScore = 1.0; // probability for credit score
        double pDTI = 1.0; // probability for debt to income ratio
        double pLoanType = 1.0; // probability for loan type

        // calculating probabilities for each attribute
        if (age != "")
        {
            pAge = CalculateProbability(loanDecision, "age", age, totalToUse, withSmoothing, xClasses);
            //Label1.Text += "</br>" + "P(Age): " + Math.Round(pAge, 3) + "</br>";
        }
        if (residency != "")
        {
            pResidency = CalculateProbability(loanDecision, "residency", residency, totalToUse, withSmoothing, xClasses);
            //Label1.Text += "</br>" + "P(Residency): " + Math.Round(pResidency, 3) + "</br>";
        }
        if (marital_status != "")
        {
            pMaritalStatus = CalculateProbability(loanDecision, "marital_status", marital_status, totalToUse, withSmoothing, xClasses);
            //Label1.Text += "P(MaritalStatus): " + Math.Round(pMaritalStatus, 3) + "</br>";
        }
        if (employment_status != "")
        {
            pEmploymentStatus = CalculateProbability(loanDecision, "employment_status", employment_status, totalToUse, withSmoothing, xClasses);
            //Label1.Text += "P(EmploymentStatus): " + Math.Round(pEmploymentStatus, 3) + "</br>";
        }
        if (credit_score != "")
        {
            pCreditScore = CalculateProbability(loanDecision, "credit_score", credit_score, totalToUse, withSmoothing, xClasses);
            //Label1.Text += "</br>" + "P(CreditScore): " + Math.Round(pCreditScore, 3) + "</br>";
        }
        if (dti != "")
        {
            pDTI = CalculateProbability(loanDecision, "dti", dti, totalToUse, withSmoothing, xClasses);
            //Label1.Text += "</br>" + "P(DTI): " + Math.Round(pDTI, 3) + "</br>";
        }
        if (loan_type != "")
        {
            pLoanType = CalculateProbability(loanDecision, "loan_type", loan_type, totalToUse, withSmoothing, xClasses);
            //Label1.Text += "P(LoanType): " + Math.Round(pLoanType, 3) + "</br>"; ;
        }

        return Math.Exp(Math.Log(pDecision) + Math.Log(pAge) + Math.Log(pResidency) + Math.Log(pMaritalStatus) + Math.Log(pEmploymentStatus) + Math.Log(pCreditScore) + Math.Log(pDTI) + Math.Log(pLoanType));
        //return Math.Exp(Math.Log(pDecision) + Math.Log(pAge));

    }

    // calculate probability for each attribute
    private double CalculateProbability(string loanDecision, string attributeName, string attributeValue, int totalToUse, bool withSmoothing, int xClasses)
    {
        double probability = 0.0;
        cmd = new SqlCommand("GetAtributeValueCount", conn);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        SqlParameter param1 = new SqlParameter("@attributename", attributeName);
        param1.Direction = ParameterDirection.Input;
        param1.DbType = DbType.String;

        SqlParameter param2 = new SqlParameter("@attributevalue", attributeValue);
        param2.Direction = ParameterDirection.Input;
        param2.DbType = DbType.String;

        SqlParameter param3 = new SqlParameter("@loandecision", loanDecision);
        param3.Direction = ParameterDirection.Input;
        param3.DbType = DbType.String;

        cmd.Parameters.Add(param1);
        cmd.Parameters.Add(param2);
        cmd.Parameters.Add(param3);
        // DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (withSmoothing == false)
            {
                probability = (Convert.ToInt16(dt.Rows[0][0]) * 1.0) / totalToUse; //without using laplacian smoothing
            }
            else if (withSmoothing == true)
            {
                probability = (Convert.ToInt16(dt.Rows[0][0]) + 1) / ((totalToUse + xClasses) * 1.0); //with using laplacian smoothing
            }
        }
        return probability;
    }

    //calculating probabilities for loan decision approved and loan decision denied
    private int Classify(string age, string residency, string marital_status, string employment_status, string credit_score, string dti, string loan_type, bool withSmoothing, int xClasses)
    {
        // partial probability for loan decision: Approved
        double partProbApproved = PartialProbability("Approved", age, residency, marital_status, employment_status, credit_score, dti, loan_type, withSmoothing, xClasses);
        Label1.Text += "</br></br>Partial Probability - Approved: " + Math.Round(partProbApproved, 4) + "</br>";

        // partial probability for loan decision: Denied
        double partProbDenied = PartialProbability("Denied", age, residency, marital_status, employment_status, credit_score, dti, loan_type, withSmoothing, xClasses);
        Label1.Text += "</br>Partial Probability - Denied: " + Math.Round(partProbDenied, 4) + "</br>";

        double evidence = partProbApproved + partProbDenied; // evidence = partial probability approved + partial probability denied
        Label1.Text += "</br>Evidence - PPApproved + PPDenied: " + Math.Round(evidence, 4) + "</br>";

        double probApproved = partProbApproved / evidence; //probability loan decision: Approved
        Label1.Text += "</br>PApproved: " + Math.Round(probApproved, 4) + "</br>";

        double probDenied = partProbDenied / evidence; //probability loan decision: Denied
        Label1.Text += "</br>PDenied: " + Math.Round(probDenied, 4) + "</br>";

        if (probApproved > probDenied) return 0;
        else return 1;
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        try
        {
            Label1.Text = "";
        
            // setting up attributes: Optional
            //string[] attributes = new string[] { "age", "residency", "marital_status", "employed","credit_score","debt-to-income ratio", "loan_type", "loan_decision_type" };
            //string attribute_names = "Attributes are:" + "<br/>";

            //// show attribute names
            //for (int i = 0; i < attributes.Length; ++i)
            //{
            //    attribute_names += attributes[i] + ", ";
            //}
            //attribute_names = attribute_names.Substring(0, attribute_names.Length - 2);
            //attribute_names += "<br/>";
            //Label1.Text = attribute_names;

            // getting attributes values
            //GetAttributeValues();

            //user input test data
            string age = "";
            string residency = "";
            string marital_status = "";
            string employment_status = "";
            string credit_score = "";
            string dti = ""; // debt-to-income ratio
            string loan_type = "";

            bool withLaplacian = true; // add one smoothing

            Label1.Text = "Attributes Selected: </br>";

            if (ddlAge.SelectedIndex != 0)
            {
                age = ddlAge.SelectedItem.Text;
                Label1.Text += "</br> Age = " + age;
            }
            if (ddlResidency.SelectedIndex != 0)
            {
                residency = ddlResidency.SelectedItem.Text;
                Label1.Text += "</br> Residency = " + residency ;
            }
            if (ddlMaritalStatus.SelectedIndex != 0)
            {
                marital_status = ddlMaritalStatus.SelectedItem.Text;
                Label1.Text += "</br> Marital Status = " + marital_status;
            }
            if (ddlEmploymentStatus.SelectedIndex != 0)
            {
                employment_status = ddlEmploymentStatus.SelectedItem.Text;
                Label1.Text += "</br> Employment Status = " + employment_status;
            }
            if (ddlCreditScore.SelectedIndex != 0)
            {
                credit_score = ddlCreditScore.SelectedValue.ToString();
                Label1.Text += "</br> Credit Score = " + credit_score ;
            }
            if (ddlDTI.SelectedIndex != 0)
            {
                dti = ddlDTI.SelectedItem.Text;
                Label1.Text += "</br> Debt-to-income ratio = " + dti ;
            }
            if (ddlLoanType.SelectedIndex != 0)
            {
                loan_type = ddlLoanType.SelectedItem.Text;
                Label1.Text += "</br> Loan Type = " + loan_type ;
            }

            // show test data
            //Label1.Text += "</br> age = " + age + "<br/>";
            //Label1.Text += "</br> residency = " + residency + "<br/>";
            //Label1.Text += "</br> marital_status = " + marital_status + "<br/>";
            //Label1.Text += "</br> employment_status = " + employment_status + "<br/>";
            //Label1.Text += "</br> credit_score = " + credit_score + "<br/>";
            //Label1.Text += "</br> Debt-to-income ratio = " + dti + "<br/>";
            //Label1.Text += "</br> loan type = " + loan_type + "<br/>";


            if (age == "" && residency == "" && marital_status == "" && employment_status == "" && credit_score == "" && dti == "" && loan_type == "")
            {
                Label1.Text = "Please select atleast one attribute to predict loan decision.";
            }
            else
            {
                // classifying loan decision
                int c = Classify(age, residency, marital_status, employment_status, credit_score, dti, loan_type, withLaplacian, 1);
                if (c == 0)
                    Label1.Text += "</br>Loan decision is most likely approved";
                else if (c == 1)
                    Label1.Text += "</br>Loan decision is most likely denied";
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message.ToString();
        }
    }
}