using LabFusion.Network;

public class GetPlayersCommand : IFusionCommand
{
    public string Name => "getplayers";

    public async Task<object> ExecuteAsync(List<string> args)
    {
        var players = LobbyInfoManager.LobbyInfo.PlayerList.Players
            .Select(playerId => playerId.Username)
            .ToList();

        return await Task.FromResult(players);
    }
}
