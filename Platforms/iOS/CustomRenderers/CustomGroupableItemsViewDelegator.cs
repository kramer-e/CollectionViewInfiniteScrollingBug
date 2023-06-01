using System;
using CoreGraphics;
using Foundation;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using UIKit;

namespace InfiniteScrollingMauiVersion.iOS.CustomRenderers
{
    public class CustomGroupableItemsViewDelegator<TItemsView, TViewController> : GroupableItemsViewDelegator<TItemsView, TViewController>
        where TItemsView : GroupableItemsView
        where TViewController : GroupableItemsViewController<TItemsView>
    {
        public CustomGroupableItemsViewDelegator(ItemsViewLayout itemsViewLayout, TViewController itemsViewController)
            : base(itemsViewLayout, itemsViewController)
        {
        }

        public override void Scrolled(UIScrollView scrollView)
        {
            List<NSIndexPath> indexPathsForVisibleItems =
                ViewController.CollectionView.IndexPathsForVisibleItems.OrderBy(x => x.Row).ToList();

            if (indexPathsForVisibleItems.Count == 0)
                return;

            TItemsView itemsView = ViewController.ItemsView;
            IItemsViewSource source = ViewController.ItemsSource;

            UIEdgeInsets contentInset = scrollView.ContentInset;
            nfloat contentOffsetX = scrollView.ContentOffset.X + contentInset.Left;
            nfloat contentOffsetY = scrollView.ContentOffset.Y + contentInset.Top;

            UICollectionView collectionView = ViewController.CollectionView;
            CGPoint centerPoint = new CGPoint(collectionView.Center.X + collectionView.ContentOffset.X,
                collectionView.Center.Y + collectionView.ContentOffset.Y);

            NSIndexPath centerIndexPath = collectionView.IndexPathForItemAtPoint(centerPoint);

            NSIndexPath firstVisibleItem = indexPathsForVisibleItems.First();
            NSIndexPath centerItem = centerIndexPath ?? firstVisibleItem;
            NSIndexPath lastVisibleItem = indexPathsForVisibleItems.Last();
            // Changed code
            int firstVisibleItemIndex = GetItemIndexWithGrouping(firstVisibleItem, source);
            int centerItemIndex = GetItemIndexWithGrouping(centerItem, source);
            int lastVisibleItemIndex = GetItemIndexWithGrouping(lastVisibleItem, source);

            ItemsViewScrolledEventArgs itemsViewScrolledEventArgs = new ItemsViewScrolledEventArgs
            {
                HorizontalDelta = contentOffsetX - PreviousHorizontalOffset,
                VerticalDelta = contentOffsetY - PreviousVerticalOffset,
                HorizontalOffset = contentOffsetX,
                VerticalOffset = contentOffsetY,
                FirstVisibleItemIndex = firstVisibleItemIndex,
                CenterItemIndex = centerItemIndex,
                LastVisibleItemIndex = lastVisibleItemIndex
            };

            itemsView.SendScrolled(itemsViewScrolledEventArgs);

            PreviousHorizontalOffset = (float)contentOffsetX;
            PreviousVerticalOffset = (float)contentOffsetY;

            switch (itemsView.RemainingItemsThreshold)
            {
                case -1:
                    return;
                case 0:
                    if (lastVisibleItemIndex == source.ItemCount - 1)
                        itemsView.SendRemainingItemsThresholdReached();
                    break;
                default:
                    if (source.ItemCount - 1 - lastVisibleItemIndex <= itemsView.RemainingItemsThreshold)
                        itemsView.SendRemainingItemsThresholdReached();
                    break;
            }
        }
        // Added code
        private static int GetItemIndexWithGrouping(NSIndexPath indexPath, IItemsViewSource itemSource)
        {
            int index = (int)indexPath.Item;

            if (indexPath.Section > 0)
            {
                for (int i = 0; i < indexPath.Section; i++)
                {
                    index += itemSource.ItemCountInGroup(i);
                }
            }

            return index;
        }
    }
}

