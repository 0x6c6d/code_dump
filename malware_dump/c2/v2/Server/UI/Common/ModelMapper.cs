using Backend.Features.Client.Models;
using Backend.Features.Settings.Models;
using Riok.Mapperly.Abstractions;
using UI.Models;

namespace UI.Common;

[Mapper]
public static partial class ModelMapper
{
    public static partial ClientModel ClientViewModelToClient(ClientViewModel model);
    
    public static partial List<ClientViewModel> ClientsToClientViewModels(List<ClientModel> model);
}