using System.ComponentModel.DataAnnotations;

namespace Application.Features.StoreManager.Technologies.Models;
public class Technology : AuditableEntity
{
    // PK
    [Key]
    public string StoreId { get; set; } = string.Empty;

    // Telefon
    public string Phone { get; set; } = string.Empty;

    // Kasse IP
    public string CashDeskIp { get; set; } = string.Empty;

    // Kasse Name
    public string CashDeskName { get; set; } = string.Empty;

    // Terminal ID
    public string TerminalId { get; set; } = string.Empty;

    // Terminal IP
    public string TerminalIp { get; set; } = string.Empty;

    // Router IP
    public string RouterIp { get; set; } = string.Empty;

    // Router Ort
    public string RouterStoragePlace { get; set; } = string.Empty;

    // TK-Anlage Ort
    public string TkStoragePlace { get; set; } = string.Empty;

    // Anschlusskennung
    public string InternetConnectionId { get; set; } = string.Empty;

    // Zugangsnummer
    public string InternetAccessId { get; set; } = string.Empty;

    // Internet - Benutzername
    public string InternetUserName { get; set; } = string.Empty;

    // Internet - Passwort
    public string InternetPassword { get; set; } = string.Empty;

    // Internet - Kundennummer
    public string InternetCustomerId { get; set; } = string.Empty;

    // Bemerkungen
    public string Comments { get; set; } = string.Empty;

    // Fiskal S/N
    public string FiscalSN { get; set; } = string.Empty;

    // Fiskal Ort
    public string FiscalPlace { get; set; } = string.Empty;

    // Video System
    public string VideoSystem { get; set; } = string.Empty;

    // Schlüsselnummer
    public string KeyNumber { get; set; } = string.Empty;

    // EC Gerät
    public string EcDevice { get; set; } = string.Empty;

    // Switch Hersteller
    public Switch Switch { get; set; }

    // Switch Hersteller
    public string SwitchText { get; set; } = string.Empty;

    // Fritz Box IP
    public string FritzBoxIp { get; set; } = string.Empty;

    // Access Point
    public string AccessPoint { get; set; } = string.Empty;

    // Videro 1. IP
    public string VideoroIpFirst { get; set; } = string.Empty;

    // Videro 2. IP
    public string VideoroIpSecond { get; set; } = string.Empty;

    // Klima IP
    public string AirConditionerIp { get; set; } = string.Empty;

    // Store Ever IP
    public string StoreEverIp { get; set; } = string.Empty;

    // KFZ 1. IP
    public string KfzIpFirst { get; set; } = string.Empty;

    // KFZ 2. IP
    public string KfzIpSecond { get; set; } = string.Empty;

    // Router Typ
    public string Router { get; set; } = string.Empty;

    // Musikanalage
    public string MusicMaticIP { get; set; } = string.Empty;
}

// Switch Hersteller
public enum Switch
{
    NotSelected,
    Lancom,
    HP
}
