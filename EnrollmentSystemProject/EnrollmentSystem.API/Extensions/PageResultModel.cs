using System.Collections;

namespace EnrollmentSystem.API.Extensions;

public class PageResultModel
{
    public IEnumerable Items { get; }
    public long Count { get; }

    public PageResultModel(IQueryable items, long count)
    {
        Items = items;
        Count = count;
    }

    public PageResultModel(IList items, long count)
    {
        Items = items;
        Count = count;
    }
}