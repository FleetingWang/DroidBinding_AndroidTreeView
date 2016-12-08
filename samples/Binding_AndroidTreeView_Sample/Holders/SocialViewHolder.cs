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

namespace Binding_AndroidTreeView_Sample.Holders
{
    public class SocialViewHolder : BaseNodeViewHolder
    {
        private static string[] NAMES = new string[]{"Bruce Wayne", "Clark Kent", "Barry Allen", "Hal Jordan"};

        public SocialViewHolder(Context context) : base(context)
        {
        }

        public override View CreateNodeView(TreeNode node, Java.Lang.Object value)
        {
            var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            View view = inflater.Inflate(Resource.Layout.layout_social_node, null, false);

            var socialItem = value.JavaCast<SocialItem>();

            PrintView iconView = view.FindViewById<PrintView>(Resource.Id.icon);
            iconView.SetIconText(socialItem.Icon);

            TextView connectionsLabel = view.FindViewById<TextView>(Resource.Id.connections);
            var rdm = new Random();
            connectionsLabel.Text = rdm.Next(150) + " connections";

            TextView userNameLabel = view.FindViewById<TextView>(Resource.Id.username);
            userNameLabel.Text = NAMES[rdm.Next(4)];

            TextView sizeText = view.FindViewById<TextView>(Resource.Id.size);
            sizeText.Text = rdm.Next(10) + " items";

            return view;
        }

        public class SocialItem : Java.Lang.Object
        {
            public int Icon { get; private set; }

            public SocialItem(int icon)
            {
                this.Icon = icon;
            }
            // rest will be hardcoded
        }
    }
}