using System;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using UIKit;

namespace InfiniteScrollingMauiVersion.iOS.CustomRenderers
{
    public class CustomGroupableItemsViewController<TItemsView> : GroupableItemsViewController<TItemsView>
     where TItemsView : GroupableItemsView
    {
        public CustomGroupableItemsViewController(TItemsView groupableItemsView, ItemsViewLayout layout)
            : base(groupableItemsView, layout)
        {
        }

        protected override UICollectionViewDelegateFlowLayout CreateDelegator()
        {
            return new CustomGroupableItemsViewDelegator<TItemsView, CustomGroupableItemsViewController<TItemsView>>(ItemsViewLayout, this);
        }
    }
}

