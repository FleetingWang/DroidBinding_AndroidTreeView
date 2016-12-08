using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Linq;
using Android.Support.V7.App;
using Binding_AndroidTreeView_Sample.Fragments;

namespace Binding_AndroidTreeView_Sample
{
    [Activity(
        Label = "AndroidTreeView", 
        MainLauncher = true,
        Theme = "@style/AppTheme",
        Icon = "@drawable/ic_launcher")]
    public class MainActivity : ActionBarActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Dictionary<string, Type> listDictionary = new Dictionary<string, Type> {
                { "Folder Structure Example", typeof(FolderStructureFragment) },
                { "Custom Holder Example", typeof(CustomViewHolderFragment) },
                { "Selectable Nodes", typeof(SelectableTreeFragment) },
                { "2d scrolling", typeof(TwoDScrollingArrowExpandFragment) },
                { "Expand with arrow only", typeof(TwoDScrollingFragment) },
            };
            var list = listDictionary.Keys.ToList();
            ListView listview = FindViewById<ListView>(Resource.Id.listview);
            SimpleArrayAdapter adapter = new SimpleArrayAdapter(this, list);
            listview.Adapter = adapter;
            listview.ItemClick += (sender, e) => {
                Type selectType = listDictionary.Values.ToArray()[e.Position];
                Intent i = new Intent(this, typeof(SingleFragmentActivity));
                i.PutExtra(SingleFragmentActivity.FRAGMENT_PARAM, FragmentJavaName(selectType));
                StartActivity(i);
            };
        }

        private class SimpleArrayAdapter: ArrayAdapter<string>
        {
            public SimpleArrayAdapter(Context context, List<String> objects)
                :base(context, Android.Resource.Layout.SimpleListItem1, objects)
            {
            }

            public override long GetItemId(int position)
            {
                return position;
            }
        }

        protected static string FragmentJavaName(Type fragmentType)
        {
            var namespaceText = fragmentType.Namespace ?? "";
            if (namespaceText.Length > 0)
                namespaceText = namespaceText.ToLowerInvariant() + ".";
            return namespaceText + fragmentType.Name;
        }
    }
}

