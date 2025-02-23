using System.Collections;

namespace EnrollmentSystem.Domain.BaseEntities;

public class PageResultDomain
{
    public IEnumerable Items { get; }
    public long Count { get; }

    public PageResultDomain(IQueryable items, long count)
    {
        Items = items;
        Count = count;
    }

    public PageResultDomain(IList items, long count)
    {
        Items = items;
        Count = count;
    }
}