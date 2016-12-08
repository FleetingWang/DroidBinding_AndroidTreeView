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
using Android.Support.V7.App;

namespace Binding_AndroidTreeView_Sample
{
    [Activity(Label = "SingleFragmentActivity", Theme = "@style/AppTheme")]
    public class SingleFragmentActivity : ActionBarActivity
    {
        public const String FRAGMENT_PARAM = "fragment";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_single_fragment);

            string fragmentName = Intent.GetStringExtra(FRAGMENT_PARAM);
            if (!string.IsNullOrEmpty(fragmentName))
            {
                Fragment f = Fragment.Instantiate(this, fragmentName);
                f.Arguments = Intent.Extras;
                FragmentManager.BeginTransaction().Replace(Resource.Id.fragment, f, fragmentName).Commit();
            }
            // Create your application here
        }
    }
}