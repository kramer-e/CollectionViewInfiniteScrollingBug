using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InfiniteScrollingMauiVersion;

public class GroupedItems : List<string>
{
    public GroupedItems(IList<string> items) : base(items)
    {
        Name = $"Group {this.First()} - {this.Last()}";
    }

    public string Name { get; private set; }
}
