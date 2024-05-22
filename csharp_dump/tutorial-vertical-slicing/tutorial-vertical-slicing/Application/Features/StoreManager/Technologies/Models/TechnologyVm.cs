namespace Application.Features.StoreManager.Technologies.Models;
public class TechnologyVm
{
    public string StoreId { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string CashDeskIp { get; set; } = string.Empty;
    public string CashDeskName { get; set; } = string.Empty;
    public string TerminalId { get; set; } = string.Empty;
    public string TerminalIp { get; set; } = string.Empty;
    public string RouterIp { get; set; } = string.Empty;
    public string RouterStoragePlace { get; set; } = string.Empty;
    public string TkStoragePlace { get; set; } = string.Empty;
    public string InternetConnectionId { get; set; } = string.Empty;
    public string InternetAccessId { get; set; } = string.Empty;
    public string InternetUserName { get; set; } = string.Empty;
    public string InternetPassword { get; set; } = string.Empty;
    public string InternetCustomerId { get; set; } = string.Empty;
    public string Comments { get; set; } = string.Empty;
    public string FiscalSN { get; set; } = string.Empty;
    public string FiscalPlace { get; set; } = string.Empty;
    public string VideoSystem { get; set; } = string.Empty;
    public string KeyNumber { get; set; } = string.Empty;
    public string EcDevice { get; set; } = string.Empty;
    public Switch Switch { get; set; }
    public string SwitchText { get; set; } = string.Empty;
    public string FritzBoxIp { get; set; } = string.Empty;
    public string AccessPoint { get; set; } = string.Empty;
    public string VideoroIpFirst { get; set; } = string.Empty;
    public string VideoroIpSecond { get; set; } = string.Empty;
    public string AirConditionerIp { get; set; } = string.Empty;
    public string StoreEverIp { get; set; } = string.Empty;
    public string KfzIpFirst { get; set; } = string.Empty;
    public string KfzIpSecond { get; set; } = string.Empty;
    public string Router { get; set; } = string.Empty;
    public string MusicMaticIP { get; set; } = string.Empty;


    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is Technology))
        {
            return false;
        }

        Technology technology = (Technology)obj;

        return StoreId == technology.StoreId &&
               Phone == technology.Phone &&
               CashDeskIp == technology.CashDeskIp &&
               CashDeskName == technology.CashDeskName &&
               TerminalId == technology.TerminalId &&
               TerminalIp == technology.TerminalIp &&
               RouterIp == technology.RouterIp &&
               RouterStoragePlace == technology.RouterStoragePlace &&
               TkStoragePlace == technology.TkStoragePlace &&
               InternetConnectionId == technology.InternetConnectionId &&
               InternetAccessId == technology.InternetAccessId &&
               InternetUserName == technology.InternetUserName &&
               InternetPassword == technology.InternetPassword &&
               InternetCustomerId == technology.InternetCustomerId &&
               Comments == technology.Comments &&
               FiscalSN == technology.FiscalSN &&
               FiscalPlace == technology.FiscalPlace &&
               VideoSystem == technology.VideoSystem &&
               KeyNumber == technology.KeyNumber &&
               EcDevice == technology.EcDevice &&
               Switch == technology.Switch &&
               SwitchText == technology.SwitchText &&
               FritzBoxIp == technology.FritzBoxIp &&
               AccessPoint == technology.AccessPoint &&
               VideoroIpFirst == technology.VideoroIpFirst &&
               VideoroIpSecond == technology.VideoroIpSecond &&
               AirConditionerIp == technology.AirConditionerIp &&
               StoreEverIp == technology.StoreEverIp &&
               KfzIpFirst == technology.KfzIpFirst &&
               KfzIpSecond == technology.KfzIpSecond &&
               Router == technology.Router &&
               MusicMaticIP == technology.MusicMaticIP;
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}
