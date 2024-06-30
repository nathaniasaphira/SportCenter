using System;
using System.Collections.Generic;
namespace SportCenter.Models.Entities;

public class Member
{
    public static readonly string Table = "members";

    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Contact { get; set; }
}
