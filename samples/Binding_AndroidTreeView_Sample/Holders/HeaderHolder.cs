using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using static AndroidTreeView.Model.TreeNode;
using Com.Github.Johnkil.Print;
using AndroidTreeView.Model;
using static Binding_AndroidTreeView_Sample.Holders.IconTreeItemHolder;

namespace Binding_AndroidTreeView_Sample.Holders
{
    public class HeaderHolder : BaseNodeViewHolder
    {
        private PrintView arrowView;

        public HeaderHolder(Context context) : base(context)
        {
        }

        public override View CreateNodeView(TreeNode node, Java.Lang.Object value)
        {
            var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            View view = inflater.Inflate(Resource.Layout.layout_header_node, null, false);

            var iconTreeItem = value.JavaCast<IconTreeItem>();

            var tvValue = view.FindViewById<TextView>(Resource.Id.node_value);
            tvValue.Text = iconTreeItem.Text;

            PrintView iconView = view.FindViewById<PrintView>(Resource.Id.icon);
            iconView.SetIconText(iconTreeItem.Icon);

            arrowView = view.FindViewById<PrintView>(Resource.Id.arrow_icon);

            if (node.IsLeaf)
            {
                arrowView.Visibility = ViewStates.Invisible;
            }

            return view;
        }

        public override void Toggle(bool active)
        {
            arrowView.SetIconText(active ? Resource.String.ic_keyboard_arrow_down : Resource.String.ic_keyboard_arrow_right);
        }
    }
}