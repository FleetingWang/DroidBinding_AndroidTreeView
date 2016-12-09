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
using DroidTreeView.Model;
using Java.Lang;
using static DroidTreeView.Model.TreeNode;
using Com.Github.Johnkil.Print;
using static DroidTreeView_Sample.Holders.IconTreeItemHolder;

namespace DroidTreeView_Sample.Holders
{
    public class ArrowExpandSelectableHeaderHolder : BaseNodeViewHolder
    {
        private TextView tvValue;
        private PrintView arrowView;
        private CheckBox nodeSelector;
        public ArrowExpandSelectableHeaderHolder(Context context):base(context)
        {
        }
        public override View CreateNodeView(TreeNode node, Java.Lang.Object value)
        {
            var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            View view = inflater.Inflate(Resource.Layout.layout_selectable_header, null, false);

            var iconTreeItem = value.JavaCast<IconTreeItem>();

            tvValue = view.FindViewById<TextView>(Resource.Id.node_value);
            tvValue.Text = iconTreeItem.Text;

            PrintView iconView = view.FindViewById<PrintView>(Resource.Id.icon);
            iconView.SetIconText(iconTreeItem.Icon);

            arrowView = view.FindViewById<PrintView>(Resource.Id.arrow_icon);
            arrowView.SetPadding(20, 10, 10, 10);
            if (node.IsLeaf)
            {
                arrowView.Visibility = ViewStates.Gone;
            }
            arrowView.Click += (sender, e) => {
                TView.ToggleNode(node);
            };

            nodeSelector = view.FindViewById<CheckBox>(Resource.Id.node_selector);
            nodeSelector.CheckedChange += (sender, e) => {
                node.Selected = e.IsChecked;
                foreach (var childNode in node.Children)
                {
                    TreeView.SelectNode(childNode, e.IsChecked);
                }
            };
            nodeSelector.Checked = node.Selected;

            return view;
        }

        public override void Toggle(bool active)
        {
            arrowView.SetIconText(active ? Resource.String.ic_keyboard_arrow_down : Resource.String.ic_keyboard_arrow_right);
        }

        public override void ToggleSelectionMode(bool editModeEnabled)
        {
            nodeSelector.Visibility = editModeEnabled ? ViewStates.Visible : ViewStates.Gone;
            nodeSelector.Checked = MNode.Selected;
        }
    }
}