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
using AndroidTreeView.Model;
using Java.Lang;
using static AndroidTreeView.Model.TreeNode;
using static Binding_AndroidTreeView_Sample.Holders.IconTreeItemHolder;
using Com.Github.Johnkil.Print;

namespace Binding_AndroidTreeView_Sample.Holders
{
    public class ProfileHolder : BaseNodeViewHolder
    {

        public ProfileHolder(Context context) : base(context)
        {
        }

        public override View CreateNodeView(TreeNode node, Java.Lang.Object value)
        {
            var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            View view = inflater.Inflate(Resource.Layout.layout_profile_node, null, false);

            var tvValue = view.FindViewById<TextView>(Resource.Id.node_value);
            var iconTreeItem = value.JavaCast<IconTreeItem>();
            tvValue.Text = iconTreeItem.Text;

            PrintView iconView = view.FindViewById<PrintView>(Resource.Id.icon);
            iconView.SetIconText(iconTreeItem.Icon);

            return view;
        }

        public override int ContainerStyle => Resource.Style.TreeNodeStyleCustom;
    }
}