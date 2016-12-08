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
using Binding_AndroidTreeView_Sample.Holders;
using static Binding_AndroidTreeView_Sample.Holders.IconTreeItemHolder;

namespace Binding_AndroidTreeView_Sample.Fragments
{
    [Register("binding_androidtreeview_sample.fragments.CustomViewHolderFragment")]
    public class CustomViewHolderFragment : Fragment
    {
        private AndroidTreeView.View.AndroidTreeView tView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View rootView = inflater.Inflate(Resource.Layout.fragment_default, null, false);
            ViewGroup containerView = rootView.FindViewById<ViewGroup>(Resource.Id.container);
            rootView.FindViewById(Resource.Id.status_bar).Visibility = ViewStates.Gone;

            TreeNode root = TreeNode.InvokeRoot();

            TreeNode myProfile = new TreeNode(new IconTreeItem(Resource.String.ic_person, "My Profile")).SetViewHolder(new ProfileHolder(Activity));
            TreeNode bruce = new TreeNode(new IconTreeItem(Resource.String.ic_person, "Bruce Wayne")).SetViewHolder(new ProfileHolder(Activity));
            TreeNode clark = new TreeNode(new IconTreeItem(Resource.String.ic_person, "Clark Kent")).SetViewHolder(new ProfileHolder(Activity));
            TreeNode barry = new TreeNode(new IconTreeItem(Resource.String.ic_person, "Barry Allen")).SetViewHolder(new ProfileHolder(Activity));
            addProfileData(myProfile);
            addProfileData(clark);
            addProfileData(bruce);
            addProfileData(barry);
            root.AddChildren(myProfile, bruce, barry, clark);

            tView = new AndroidTreeView.View.AndroidTreeView(Activity, root);
            tView.SetDefaultAnimation(true);
            tView.SetDefaultContainerStyle(Resource.Style.TreeNodeStyleDivided, true);
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

        private void addProfileData(TreeNode profile)
        {
            TreeNode socialNetworks = new TreeNode(new IconTreeItem(Resource.String.ic_people, "Social")).SetViewHolder(new HeaderHolder(Activity));
            TreeNode places = new TreeNode(new IconTreeItem(Resource.String.ic_place, "Places")).SetViewHolder(new HeaderHolder(Activity));

            TreeNode facebook = new TreeNode(new SocialViewHolder.SocialItem(Resource.String.ic_post_facebook)).SetViewHolder(new SocialViewHolder(Activity));
            TreeNode linkedin = new TreeNode(new SocialViewHolder.SocialItem(Resource.String.ic_post_linkedin)).SetViewHolder(new SocialViewHolder(Activity));
            TreeNode google = new TreeNode(new SocialViewHolder.SocialItem(Resource.String.ic_post_gplus)).SetViewHolder(new SocialViewHolder(Activity));
            TreeNode twitter = new TreeNode(new SocialViewHolder.SocialItem(Resource.String.ic_post_twitter)).SetViewHolder(new SocialViewHolder(Activity));

            TreeNode lake = new TreeNode(new PlaceHolderHolder.PlaceItem("A rose garden")).SetViewHolder(new PlaceHolderHolder(Activity));
            TreeNode mountains = new TreeNode(new PlaceHolderHolder.PlaceItem("The white house")).SetViewHolder(new PlaceHolderHolder(Activity));

            places.AddChildren(lake, mountains);
            socialNetworks.AddChildren(facebook, google, twitter, linkedin);
            profile.AddChildren(socialNetworks, places);
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutString("tState", tView.SaveState);
        }
    }
}