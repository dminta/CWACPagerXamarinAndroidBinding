/***
  Copyright (c) 2013 CommonsWare, LLC
  Xamarin port (c) 2017 Dominik Minta
  Licensed under the Apache License, Version 2.0 (the "License"); you may not
  use this file except in compliance with the License. You may obtain a copy
  of the License at http://www.apache.org/licenses/LICENSE-2.0. Unless required
  by applicable law or agreed to in writing, software distributed under the
  License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS
  OF ANY KIND, either express or implied. See the License for the specific
  language governing permissions and limitations under the License.
  
  From _The Busy Coder's Guide to Android Development_
    http://commonsware.com/Android
 */

using Android.App;
using Android.OS;
using Android.Support.V4.View;
using Android.Views;
using System.Collections.Generic;

namespace CommonsWare.CWAC.Pager.Demo
{
    [Activity(Label="@string/app_name", MainLauncher=true)]
    public class MainActivity : Activity
    {
        ArrayPagerAdapter _adapter = null;
        ViewPager _pager = null;
        int _pageNumber = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
                                                           
            SetContentView(Resource.Layout.main);

            _pager = FindViewById<ViewPager>(Resource.Id.pager);
            _adapter = BuildAdapter();
            _pager.Adapter = _adapter;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actions, menu);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.add:
                    Add(true);
                    break;

                case Resource.Id.split:
                    Add(false);
                    break;

                case Resource.Id.remove:
                    Remove();
                    break;

                case Resource.Id.swap:
                    Swap();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        string BuildTag(int position)
        {
            return $"editor{_pageNumber++}";
        }

        string BuildTitle(int position)
        {
            return Java.Lang.String.Format(GetString(Resource.String.hint), position + 1);
        }

        ArrayPagerAdapter BuildAdapter()
        {
            List<IPageDescriptor> pages = new List<IPageDescriptor>();

            for (int i=0; i<10; i++)
            {
                pages.Add(new SimplePageDescriptor(BuildTag(i), BuildTitle(i)));
            }

            return new SamplePagerAdapter(FragmentManager, pages);
        }

        void Add(bool before)
        {
            int current = _pager.CurrentItem;

            SimplePageDescriptor desc = new SimplePageDescriptor(BuildTag(_adapter.Count), BuildTitle(_adapter.Count));

            if (before)
            {
                _adapter.Insert(desc, current);
            }
            else
            {
                if (current < _adapter.Count - 1)
                {
                    _adapter.Insert(desc, current + 1);
                }
                else
                {
                    _adapter.Add(desc);
                }
            }
        }

        void Remove()
        {
            if (_adapter.Count > 1)
            {
                _adapter.Remove(_pager.CurrentItem);
            }
        }

        void Swap()
        {
            int current = _pager.CurrentItem;

            if (current < _adapter.Count - 1)
            {
                _adapter.Move(current, current + 1);
            }
            else
            {
                _adapter.Move(current, current - 1);
            }
        }

        class SamplePagerAdapter : ArrayPagerAdapter
        {       
            public SamplePagerAdapter(FragmentManager fragmentManager,
                List<IPageDescriptor> descriptors)
                : base(fragmentManager, descriptors)
            {   
            }     

            protected override Java.Lang.Object CreateFragment(IPageDescriptor desc)
            {
                return EditorFragment.NewInstance(desc.Title);
            }
        }
    }
}
                                                