using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class PCBAModel
{
    public string Uid { get; set; }
    public int ManufacturerNumber { get; set; }
}