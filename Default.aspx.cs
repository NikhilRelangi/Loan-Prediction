using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data.Common;
using System.Data.OleDb;
using Microsoft.VisualBasic.FileIO;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["conString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {   
            //SqlConnection conn = new SqlConnection("Data Source=Danny\\SQLEXPRESS;Initial Catalog=Naive_Bayes;Integrated Security=SSPI");
            //conn.Open();
            //Label1.Text = "Connection Successful!!";
            //conn.Close();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    private DataTable GetDataTabletFromCSVFile(string csv_file_path)
    {
        DataTable csvData = new DataTable();
        try
        {
            using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
            {
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;
                string[] colFields = csvReader.ReadFields();
                foreach (string column in colFields)
                {
                    DataColumn datecolumn = new DataColumn(column);
                    datecolumn.AllowDBNull = true;
                    csvData.Columns.Add(datecolumn);
                }
                while (!csvReader.EndOfData)
                {
                    string[] fieldData = csvReader.ReadFields();
                    //Making empty value as null
                    for (int i = 0; i < fieldData.Length; i++)
                    {
                        if (fieldData[i] == "")
                        {
                            fieldData[i] = null;
                        }
                    }
                    csvData.Rows.Add(fieldData);
                }
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return csvData; 
}

    private void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable csvFileData)
    {
        conn.Open();
        using (SqlBulkCopy s = new SqlBulkCopy(conn))
            {
                s.DestinationTableName = "training_set";
                foreach (var column in csvFileData.Columns)
                    s.ColumnMappings.Add(column.ToString(), column.ToString());
                s.WriteToServer(csvFileData);
            }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack && FileUpload1.HasFile)
            {
                string path = string.Concat(Server.MapPath("~/excel/" + FileUpload1.FileName));
                FileUpload1.SaveAs(path);
                DataTable dt = GetDataTabletFromCSVFile(path);
                InsertDataIntoSQLServerUsingSQLBulkCopy(dt);
                //string excelConnectionString = string.Format("Provider=microsoft.jet.oledb.4.0;Data Source={0};Extended Properties=\"text;HDR=Yes;FMT=Delimited\";", Server.MapPath("~/excel/"));
                //OleDbConnection connection = new OleDbConnection();
                //connection.ConnectionString = excelConnectionString;
                //OleDbCommand command = new OleDbCommand("select * from ["+ FileUpload1.FileName + "]", connection);
                //connection.Open();
                //// Create DbDataReader to Data Worksheet
                //DbDataReader dr = command.ExecuteReader();
                //// SQL Server Connection String

                //// Bulk Copy to SQL Server 
                //conn.Open();
                //SqlBulkCopy bulkInsert = new SqlBulkCopy(conn);
                //bulkInsert.ColumnMappings.Add(1, "applicant_Id");
                //bulkInsert.DestinationTableName = "training_set";
                //bulkInsert.WriteToServer(dr);
                //conn.Close();
                //Label1.Text = "Ho Gaya";
            }
          
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

}
