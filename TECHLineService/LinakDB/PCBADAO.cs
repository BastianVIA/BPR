using LINTest.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LINTest.LinakDB;

public class PCBADAO : IPCBAService
{
    private string _linakDbConnectionString;

    public PCBADAO(IConfiguration configuration)
    {
        _linakDbConnectionString = configuration.GetConnectionString("LINAKDatabaseConnection");
    }

    public PCBAModel GetPCBA(string uid)
    {
        var pcbaToReturn = new PCBAModel();

        using var connection = new SqlConnection();
        connection.ConnectionString = _linakDbConnectionString;
            
        var query = "SELECT Uid, ItemNumber, ManufacturerNumber, Software, ProductionDateCode, Configuration FROM PCBAs p " +
                    "join Actuators a on p.Uid = a.PCBAUid " +
                    "join Orders o on a.WorkOrderNumber = o.WorkOrderNumber " +
                    "WHERE Uid = '" + uid +"'";
            
        var command = new SqlCommand(query, connection);
        connection.Open();
        using var dataReader = command.ExecuteReader();
        if (!dataReader.HasRows)
        {
            throw new KeyNotFoundException($"Could not find PCBA with UID: {uid}");
        }
            
        while (dataReader.Read())
        {
            pcbaToReturn.Uid = dataReader.GetInt32(0);
            pcbaToReturn.ItemNumber = dataReader.GetString(1);
            pcbaToReturn.ManufacturerNumber = dataReader.GetInt32(2);
            pcbaToReturn.Software = dataReader.GetString(3);
            pcbaToReturn.ProductionDateCode = dataReader.GetInt32(4);
            pcbaToReturn.ConfigNo = dataReader.GetString(5);
        }
        return pcbaToReturn;
    }
}