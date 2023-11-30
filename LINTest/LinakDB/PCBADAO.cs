using LINTest.Models;
using Microsoft.Data.SqlClient;

namespace LINTest.LinakDB;

public class PCBADAO : IPCBAService
{
    string linakDbConnectionString = "Server=localhost;Database=LINAK-DB;Trusted_Connection=True;TrustServerCertificate=True;";

    public PCBAModel GetPCBA(string uid)
    {
        PCBAModel pcbaToReturn = new PCBAModel();
        
        using (SqlConnection connection = new SqlConnection())
        {
            connection.ConnectionString = linakDbConnectionString;
            
            string query = "SELECT * FROM PCBAs WHERE Uid = '" + uid + "'";
            
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            if (!dataReader.HasRows)
            {
                //throw exception
            }
            
            while (dataReader.Read())
            {
                pcbaToReturn.Uid = dataReader.GetInt32(0);
                pcbaToReturn.ItemNumber = dataReader.GetString(1);
                pcbaToReturn.ManufacturerNumber = dataReader.GetInt32(2);
                pcbaToReturn.Software = dataReader.GetString(3);
                pcbaToReturn.ProductionDateCode = dataReader.GetInt32(4);
            }
            
            dataReader.Close();
            connection.Close();
            
            return pcbaToReturn;
        }
    }
}