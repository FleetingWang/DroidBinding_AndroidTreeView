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
using Com.Github.Johnkil.Print;
using Binding_AndroidTreeView_Sample.Extensions;

namespace Binding_AndroidTreeView_Sample.Holders
{
    public class IconTreeItemHolder: BaseNodeViewHolder
    {
        private PrintView arrowView;

        public IconTreeItemHolder(Context context):base(context)
        {
        }

        public override View CreateNodeView(TreeNode node, Java.Lang.Object value)
        {
            var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            View view = inflater.Inflate(Resource.Layout.layout_icon_node, null, false);

            var iconTreeItem = value.JavaCast<IconTreeItem>();

            var tvValue = view.FindViewById<TextView>(Resource.Id.node_value);
            tvValue.Text = iconTreeItem.Text;

            PrintView iconView = view.FindViewById<PrintView>(Resource.Id.icon);
            iconView.SetIconText(iconTreeItem.Icon);

            arrowView = view.FindViewById<PrintView>(Resource.Id.arrow_icon);

            view.FindViewById(Resource.Id.btn_addFolder).Click += (sender, e) => {
                TreeNode newFolder = new TreeNode(new IconTreeItem(Resource.String.ic_folder, "New Folder"));
                TreeView.AddNode(node, newFolder);
            };

            view.FindViewById(Resource.Id.btn_delete).Click += (sender, e) => {
                TreeView.RemoveNode(node);
            };

            //if My computer
            if (node.Level == 1)
            {
                view.FindViewById(Resource.Id.btn_delete).Visibility = ViewStates.Gone;
            }

            return view;
        }

        public override void Toggle(bool active)
        {
            arrowView.SetIconText(active ? Resource.String.ic_keyboard_arrow_down : Resource.String.ic_keyboard_arrow_right);
        }

        public class IconTreeItem : Java.Lang.Object
        {
            public int Icon { get; private set; }
            public string Text { get; private set; }

            public IconTreeItem(int icon, string text)
            {
                this.Icon = icon;
                this.Text = text;
            }
        }
    }
}