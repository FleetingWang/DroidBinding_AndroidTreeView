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
using Java.Lang;

namespace Binding_AndroidTreeView_Sample.Fragments
{
    [Register("binding_androidtreeview_sample.fragments.FolderStructureFragment")]
    public class FolderStructureFragment : Fragment
    {
        private TextView statusBar;
        private AndroidTreeView.View.AndroidTreeView tView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetHasOptionsMenu(true);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.fragment_default, null, false);
            ViewGroup containerView = rootView.FindViewById<ViewGroup>(Resource.Id.container);

            statusBar = rootView.FindViewById<TextView>(Resource.Id.status_bar);

            TreeNode root = TreeNode.InvokeRoot();
            TreeNode computerRoot = new TreeNode(new IconTreeItem(Resource.String.ic_laptop, "My Computer"));

            TreeNode myDocuments = new TreeNode(new IconTreeItem(Resource.String.ic_folder, "My Documents"));
            TreeNode downloads = new TreeNode(new IconTreeItem(Resource.String.ic_folder, "Downloads"));
            TreeNode file1 = new TreeNode(new IconTreeItem(Resource.String.ic_drive_file, "Folder 1"));
            TreeNode file2 = new TreeNode(new IconTreeItem(Resource.String.ic_drive_file, "Folder 2"));
            TreeNode file3 = new TreeNode(new IconTreeItem(Resource.String.ic_drive_file, "Folder 3"));
            TreeNode file4 = new TreeNode(new IconTreeItem(Resource.String.ic_drive_file, "Folder 4"));
            fillDownloadsFolder(downloads);
            downloads.AddChildren(file1, file2, file3, file4);

            TreeNode myMedia = new TreeNode(new IconTreeItem(Resource.String.ic_photo_library, "Photos"));
            TreeNode photo1 = new TreeNode(new IconTreeItem(Resource.String.ic_photo_library, "Folder 1"));
            TreeNode photo2 = new TreeNode(new IconTreeItem(Resource.String.ic_photo_library, "Folder 2"));
            TreeNode photo3 = new TreeNode(new IconTreeItem(Resource.String.ic_photo_library, "Folder 3"));
            myMedia.AddChildren(photo1, photo2, photo3);

            myDocuments.AddChild(downloads);
            computerRoot.AddChildren(myDocuments, myMedia);

            root.AddChildren(computerRoot);

            tView = new AndroidTreeView.View.AndroidTreeView(Activity, root);
            tView.SetDefaultAnimation(true);
            tView.SetDefaultContainerStyle(Resource.Style.TreeNodeStyleCustom);
            tView.SetDefaultViewHolder(Java.Lang.Class.FromType(typeof(IconTreeItemHolder)));
            tView.SetDefaultNodeClickListener(new CustomTreeNodeClickListener(statusBar));
            tView.SetDefaultNodeLongClickListener(new CustomTreeNodeLongClickListener(Activity));

            containerView.AddView(tView.View);

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

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.expandAll:
                    tView.ExpandAll();
                    break;
                case Resource.Id.collapseAll:
                    tView.CollapseAll();
                    break;
            }
            return true;
        }

        private int counter = 0;

        private void fillDownloadsFolder(TreeNode node)
        {
            TreeNode downloads = new TreeNode(new IconTreeItemHolder.IconTreeItem(Resource.String.ic_folder, "Downloads" + (counter++)));
            node.AddChild(downloads);
            if (counter < 5)
            {
                fillDownloadsFolder(downloads);
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutString("tState", tView.SaveState);
        }

        private class CustomTreeNodeClickListener : Java.Lang.Object, TreeNode.ITreeNodeClickListener
        {
            private TextView statusBar;
            public CustomTreeNodeClickListener(TextView statusBar)
            {
                this.statusBar = statusBar;
            }

            public void OnClick(TreeNode node, Java.Lang.Object value)
            {
                IconTreeItem item = value.JavaCast<IconTreeItem>();
                statusBar.Text = "Last clicked: " + item.Text;
            }
        }

        private class CustomTreeNodeLongClickListener : Java.Lang.Object, TreeNode.ITreeNodeLongClickListener
        {
            private Activity activity;
            public CustomTreeNodeLongClickListener(Activity activity)
            {
                this.activity = activity;
            }
            public bool OnLongClick(TreeNode node, Java.Lang.Object value)
            {
                IconTreeItem item = value.JavaCast<IconTreeItem>();
                Toast.MakeText(activity, "Long click: " + item.Text, ToastLength.Short).Show();
                return true;
            }
        }
    }
}