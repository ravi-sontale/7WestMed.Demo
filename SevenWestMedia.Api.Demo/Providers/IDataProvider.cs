using System.Collections.Generic;

namespace SevenWestMedia.Api.Demo.Providers
{
    public interface IDataProvider<T>
    {
        List<T> Provide();
    }
}
