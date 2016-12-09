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

namespace DroidTreeView_Sample.Holders
{
    public class PlaceHolderHolder : BaseNodeViewHolder
    {
        public PlaceHolderHolder(Context context) : base(context)
        {
        }

        public override View CreateNodeView(TreeNode node, Java.Lang.Object value)
        {
            var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            View view = inflater.Inflate(Resource.Layout.layout_place_node, null, false);

            var placeItem = value.JavaCast<PlaceItem>();

            var placeName = view.FindViewById<TextView>(Resource.Id.place_name);
            placeName.Text = placeItem.Name;

            Random r = new Random();
            bool like = r.Next(2) > 0;

            PrintView likeView = view.FindViewById<PrintView>(Resource.Id.like);
            likeView.SetIconText(like ? Resource.String.ic_thumbs_up: Resource.String.ic_thumbs_down);
            return view;
        }

        public class PlaceItem : Java.Lang.Object
        {
            public string Name { get; private set; }

            public PlaceItem(string name)
            {
                this.Name = name;
            }
            // rest will be hardcoded
        }
    }
}