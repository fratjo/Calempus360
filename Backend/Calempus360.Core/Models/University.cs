namespace Calempus360.Core.Models;

public class University(
    string           name,
    string           code,
    string           phone,
    string           address,
    Guid?            id = null,
    DateTime?        createdAt = null,
    DateTime?        updatedAt = null,
    List<Site>?      sites = null,
    List<Equipment>? equipments = null)
{
    public Guid?            Id         { get; private set; } = id;
    public string           Name       { get; private set; } = name;
    public string           Code       { get; private set; } = code;
    public string           Phone      { get; private set; } = phone;
    public string           Address    { get; private set; } = address;
    public DateTime?        CreatedAt  { get; private set; } = createdAt;
    public DateTime?        UpdatedAt  { get; private set; } = updatedAt;
    public List<Site>?      Sites      { get; private set; } = sites;
    public List<Equipment>? Equipments { get; private set; } = equipments;
}