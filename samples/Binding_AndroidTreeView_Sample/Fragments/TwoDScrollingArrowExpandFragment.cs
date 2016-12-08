using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidTreeView.Model;
using static Binding_AndroidTreeView_Sample.Holders.IconTreeItemHolder;
using Binding_AndroidTreeView_Sample.Holders;

namespace Binding_AndroidTreeView_Sample.Fragments
{
    [Register("binding_androidtreeview_sample.fragments.TwoDScrollingArrowExpandFragment")]
    public class TwoDScrollingArrowExpandFragment : Fragment
    {
        private const string NAME = "Very long name for folder";
        private AndroidTreeView.View.AndroidTreeView tView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.fragment_selectable_nodes, null, false);
            rootView.FindViewById(Resource.Id.status).Visibility = ViewStates.Gone;
            ViewGroup containerView = rootView.FindViewById<ViewGroup>(Resource.Id.container);

            TreeNode root = TreeNode.InvokeRoot();

            TreeNode s1 = new TreeNode(new IconTreeItem(Resource.String.ic_folder, "Folder with very long name ")).SetViewHolder(new ArrowExpandSelectableHeaderHolder(Activity));
            TreeNode s2 = new TreeNode(new IconTreeItem(Resource.String.ic_folder, "Another folder with very long name")).SetViewHolder(new ArrowExpandSelectableHeaderHolder(Activity));

            fillFolder(s1);
            fillFolder(s2);

            root.AddChildren(s1, s2);

            tView = new AndroidTreeView.View.AndroidTreeView(Activity, root);
            tView.SetDefaultAnimation(true);
            tView.SetUse2dScroll(true);
            tView.SetDefaultContainerStyle(Resource.Style.TreeNodeStyleCustom);
            tView.SetDefaultViewHolder(Java.Lang.Class.FromType(typeof(ArrowExpandSelectableHeaderHolder)));
            tView.SetDefaultNodeClickListener(new CustomTreeNodeClickListener(Activity));
            containerView.AddView(tView.View);

            tView.ExpandAll();

            if (savedInstanceState != null)
            {
                string state = savedInstanceState.GetString("tState");
                if (!string.IsNullOrEmpty(state))
                {
                    tView.RestoreState(state);
                }
            }

            return rootView;
        }

        private void fillFolder(TreeNode folder)
        {
            TreeNode currentNode = folder;
            for (int i = 0; i < 4; i++)
            {
                TreeNode file = new TreeNode(new IconTreeItem(Resource.String.ic_folder, NAME + " " + i));
                currentNode.AddChild(file);
                currentNode = file;
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutString("tState", tView.SaveState);
        }

        private class CustomTreeNodeClickListener : Java.Lang.Object, TreeNode.ITreeNodeClickListener
        {
            private Activity activity;
            public CustomTreeNodeClickListener(Activity activity)
            {
                this.activity = activity;
            }

            public void OnClick(TreeNode node, Java.Lang.Object value)
            {
                IconTreeItem item = value.JavaCast<IconTreeItem>();
                Toast.MakeText(activity, item.Text, ToastLength.Short).Show();
            }
        }
    }
}