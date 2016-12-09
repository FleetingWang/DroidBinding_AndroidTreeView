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

namespace DroidTreeView_Sample.Holders
{
    public class SelectableItemHolder : BaseNodeViewHolder
    {
        private TextView tvValue;
        private CheckBox nodeSelector;
        public SelectableItemHolder(Context context):base(context)
        {
        }

        public override View CreateNodeView(TreeNode node, Java.Lang.Object value)
        {
            var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            View view = inflater.Inflate(Resource.Layout.layout_selectable_item, null, false);

            tvValue = view.FindViewById<TextView>(Resource.Id.node_value);
            tvValue.Text = (string)value;

            nodeSelector = view.FindViewById<CheckBox>(Resource.Id.node_selector);
            nodeSelector.CheckedChange += (sender, e) => {
                node.Selected = e.IsChecked;
            };
            nodeSelector.Checked = node.Selected;

            if (node.IsLastChild)
            {
                view.FindViewById(Resource.Id.bot_line).Visibility = ViewStates.Invisible;
            }

            return view;
        }

        public override void ToggleSelectionMode(bool editModeEnabled)
        {
            nodeSelector.Visibility = editModeEnabled ? ViewStates.Visible : ViewStates.Gone;
            nodeSelector.Checked = MNode.Selected;
        }
    }
}