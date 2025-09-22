using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;

namespace YarpGateway.API.Providers;

public class InMemoryConfig : IProxyConfig
{
    private CancellationTokenSource _cts = new();

    public InMemoryConfig(IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters)
    {
        Routes = routes;
        Clusters = clusters;
        ChangeToken = new CancellationChangeToken(_cts.Token);
    }

    public IReadOnlyList<RouteConfig> Routes { get; }
    public IReadOnlyList<ClusterConfig> Clusters { get; }
    public IChangeToken ChangeToken { get; private set; }

    public void SignalChange()
    {
        var previousCts = _cts;
        _cts = new CancellationTokenSource();
        ChangeToken = new CancellationChangeToken(_cts.Token);
        previousCts.Cancel();
    }
}
