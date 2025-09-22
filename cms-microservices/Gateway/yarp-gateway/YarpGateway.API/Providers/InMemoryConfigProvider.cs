using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;

namespace YarpGateway.API.Providers;

public class InMemoryConfigProvider : IProxyConfigProvider
{
    private InMemoryConfig _config;

    public InMemoryConfigProvider()
    {
        _config = new InMemoryConfig(BuildRoutes(), BuildClusters());
    }

    public IProxyConfig GetConfig() => _config;

    public void Update()
    {
        _config = new InMemoryConfig(BuildRoutes(), BuildClusters());
        _config.SignalChange();
    }

    private IReadOnlyList<RouteConfig> BuildRoutes() => new[]
    {
        new RouteConfig
        {
            RouteId = "contentRoute",
            ClusterId = "contentCluster",
            Match = new RouteMatch { Path = "/content/{**catch-all}" }
        },
        new RouteConfig
        {
            RouteId = "userRoute",
            ClusterId = "userCluster",
            Match = new RouteMatch { Path = "/user/{**catch-all}" }
        }
    };

    private IReadOnlyList<ClusterConfig> BuildClusters() => new[]
    {
        new ClusterConfig
        {
            ClusterId = "contentCluster",
            Destinations = new Dictionary<string, DestinationConfig>
            {
                { "content", new DestinationConfig { Address = "http://localhost:44271/" } }
            }
        },
        new ClusterConfig
        {
            ClusterId = "userCluster",
            Destinations = new Dictionary<string, DestinationConfig>
            {
                { "user", new DestinationConfig { Address = "http://localhost:44272/" } }
            }
        }
    };
}
