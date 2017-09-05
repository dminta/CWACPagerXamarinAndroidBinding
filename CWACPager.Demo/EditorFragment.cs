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
using Android.Views;
using Android.Widget;

namespace CommonsWare.CWAC.Pager.Demo
{
    public class EditorFragment : Fragment
    {
        const string KeyTitle = "title";

        internal static EditorFragment NewInstance(string title)
        {
            EditorFragment frag = new EditorFragment();
            var args = new Bundle();

            args.PutString(KeyTitle, title);
            frag.Arguments = args;

            return frag;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View result = inflater.Inflate(Resource.Layout.editor, container, false);
            var editor = result.FindViewById<EditText>(Resource.Id.editor);

            editor.Hint = Title;

            return result;
        }

        public string Title => Arguments.GetString(KeyTitle);
    }
}                         
