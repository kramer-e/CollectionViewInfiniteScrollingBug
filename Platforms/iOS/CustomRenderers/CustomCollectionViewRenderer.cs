using System;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;

namespace InfiniteScrollingMauiVersion.iOS.CustomRenderers
{
	public class CustomCollectionViewRenderer : CollectionViewRenderer
	{
        protected override GroupableItemsViewController<GroupableItemsView> CreateController(GroupableItemsView itemsView, ItemsViewLayout layout)
        {
            return new CustomGroupableItemsViewController<GroupableItemsView>(itemsView, layout);
        }
    }
}

