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
using DroidTreeView.View;
using DroidTreeView.Model;
using static DroidTreeView_Sample.Holders.IconTreeItemHolder;
using DroidTreeView_Sample.Holders;

namespace DroidTreeView_Sample.Fragments
{
    [Register("droidtreeview_sample.fragments.SelectableTreeFragment")]
    public class SelectableTreeFragment : Fragment
    {
        private AndroidTreeView tView;
        private bool selectionModeEnabled = false;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.fragment_selectable_nodes, null, false);
            ViewGroup containerView = rootView.FindViewById<ViewGroup>(Resource.Id.container);

            rootView.FindViewById(Resource.Id.btn_toggleSelection).Click += (sender, e) => {
                selectionModeEnabled = !selectionModeEnabled;
                tView.SelectionModeEnabled = selectionModeEnabled;
            };

            rootView.FindViewById(Resource.Id.btn_selectAll).Click += (sender, e) => {
                if (!selectionModeEnabled)
                {
                    Toast.MakeText(Activity, "Enable selection mode first", ToastLength.Short).Show();
                }
                tView.SelectAll(true);
            };

            rootView.FindViewById(Resource.Id.btn_deselectAll).Click += (sender, e) => {
                if (!selectionModeEnabled)
                {
                    Toast.MakeText(Activity, "Enable selection mode first", ToastLength.Short).Show();
                }
                tView.DeselectAll();
            };

            rootView.FindViewById(Resource.Id.btn_checkSelection).Click += (sender, e) => {
                if (!selectionModeEnabled)
                {
                    Toast.MakeText(Activity, "Enable selection mode first", ToastLength.Short).Show();
                }else
                {
                    Toast.MakeText(Activity, tView.Selected.Count() + " selected", ToastLength.Short).Show();
                }
            };

            TreeNode root = TreeNode.InvokeRoot();

            TreeNode s1 = new TreeNode(new IconTreeItem(Resource.String.ic_sd_storage, "Storage1")).SetViewHolder(new ProfileHolder(Activity));
            TreeNode s2 = new TreeNode(new IconTreeItem(Resource.String.ic_sd_storage, "Storage2")).SetViewHolder(new ProfileHolder(Activity));
            s1.Selectable = false;
            s2.Selectable = false;

            TreeNode folder1 = new TreeNode(new IconTreeItem(Resource.String.ic_folder, "Folder 1")).SetViewHolder(new SelectableHeaderHolder(Activity));
            TreeNode folder2 = new TreeNode(new IconTreeItem(Resource.String.ic_folder, "Folder 2")).SetViewHolder(new SelectableHeaderHolder(Activity));
            TreeNode folder3 = new TreeNode(new IconTreeItem(Resource.String.ic_folder, "Folder 3")).SetViewHolder(new SelectableHeaderHolder(Activity));

            fillFolder(folder1);
            fillFolder(folder2);
            fillFolder(folder3);

            s1.AddChildren(folder1, folder2);
            s2.AddChildren(folder3);

            root.AddChildren(s1, s2);

            tView = new AndroidTreeView(Activity, root);
            tView.SetDefaultAnimation(true);
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

        private void fillFolder(TreeNode folder)
        {
            TreeNode file1 = new TreeNode("File1").SetViewHolder(new SelectableItemHolder(Activity));
            TreeNode file2 = new TreeNode("File2").SetViewHolder(new SelectableItemHolder(Activity));
            TreeNode file3 = new TreeNode("File3").SetViewHolder(new SelectableItemHolder(Activity));
            folder.AddChildren(file1, file2, file3);
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutString("tState", tView.SaveState);
        }
    }
}