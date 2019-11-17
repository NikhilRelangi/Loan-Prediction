using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VariableElimination : System.Web.UI.Page
{
    string[] variables = new string[] { "loan_decision", "loan_type", "credit_score", "residency_status", "employment_status", "dbi_ratio"/*debt to income ratio*/, "age", "marital_status" };
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    private string[][] DAGLevels()
    {
        // creating DAG levels     
        string[][] DAG = new string[variables.Length][];// parent variable, node variable, status: removed or in DAG
        DAG[0] = new string[] { "", variables[0], "inDAG" }; // loan decision
        DAG[1] = new string[] { variables[0], variables[1], "inDAG" }; // loan type
        DAG[2] = new string[] { variables[0], variables[2], "inDAG" }; // credit score
        DAG[3] = new string[] { variables[0], variables[3], "inDAG" }; // residency ststus
        DAG[4] = new string[] { variables[0], variables[4], "inDAG" }; // employment status
        DAG[5] = new string[] { variables[4], variables[5], "inDAG" }; // debt-to-income ratio
        DAG[6] = new string[] { variables[4], variables[6], "inDAG" }; // age
        DAG[7] = new string[] { variables[6], variables[7], "inDAG" }; // marital status
        return DAG;
    }
    private void RemoveBarren(string[][] DAG)
    {
        // remove barren variables
        for (int k = 1; k < DAG.Length; k++)
        {
            for (int i = 1; i < DAG.Length; i++)
            {
                string evidence_variable = DAG[i][1].ToString();
                if (ddlQuery.SelectedValue.ToString() != evidence_variable)
                {
                    bool barren = true;
                    for (int j = 1; j < DAG.Length; j++)
                    {
                        string parent_variable = DAG[j][0].ToString();
                        if (parent_variable == evidence_variable && DAG[j][2] == "inDAG")
                        {
                            barren = false;
                        }
                    }
                    if (barren == true)
                    {
                        DAG[i][2] = "removed";
                    }
                }
            }
        }
    }
    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        try
        {
            string[][] DAG = DAGLevels();
            RemoveBarren(DAG);
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message.ToString();
        }
    }
}