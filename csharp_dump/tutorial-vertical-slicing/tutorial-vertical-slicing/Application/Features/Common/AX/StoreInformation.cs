namespace Application.Features.Common.AX;
public class StoreInformation
{
    // Filialnummer
    public string StoreId { get; set; } = string.Empty;

    // Name
    public string Name { get; set; } = string.Empty;

    // Center
    public string Center { get; set; } = string.Empty;

    // Street
    public string Street { get; set; } = string.Empty;

    // PLZ
    public string Zip { get; set; } = string.Empty;

    // Stadt
    public string City { get; set; } = string.Empty;

    // Land
    public string CountryLong { get; set; } = string.Empty;

    // Land Kurzform
    public string CountryShort { get; set; } = string.Empty;

    // Bundesland
    public string State { get; set; } = string.Empty;

    // Bundesland kurz
    public string StateShort { get; set; } = string.Empty;

    // Verkaufsbezirk
    public string SalesState { get; set; } = string.Empty;

    // Verkaufsbezirk Nummer
    public string SalesStateNumber { get; set; } = string.Empty;

    // Filialleiter
    public string StoreManager { get; set; } = string.Empty;

    // Verkaufsbezirk Nummer
    public string StoreManagerId { get; set; } = string.Empty;

    // Regionalleiter
    public string RegionalManager { get; set; } = string.Empty;

    // Regionalleiter Nummer
    public string RegionalManagerId { get; set; } = string.Empty;

    // Bezirksleiter
    public string DistrictManager { get; set; } = string.Empty;

    // Bezirksleiter Nummer
    public string DistrictManagerId { get; set; } = string.Empty;

    // Sortiment
    public string StoreType { get; set; } = string.Empty;

    // Status
    public string Status { get; set; } = string.Empty;

    // Beschreibung
    public string Description { get; set; } = string.Empty;
}
