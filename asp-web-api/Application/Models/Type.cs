using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Application.Models;

public class Type
{
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [JsonIgnore]
    public virtual List<Transaction> Transactions { get; set; }
}

