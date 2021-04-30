using System;
using System.Collections.Generic;
using Android.Speech;
using Android.Speech.Tts;
using Android;
using Android.App;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace AppTTS
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener , TextToSpeech.IOnInitListener
    {
        TextToSpeech tts;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            // menu 
            #region Menu Slinding

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            #endregion

            /// app
            tts = new TextToSpeech(this, this);
            //AudioManager media = (AudioManager)GetSystemService(AudioService) as AudioManager;
            //try
            //{
            //    media.SetStreamVolume(Android.Media.Stream.System, 10, Android.Media.VolumeNotificationFlags.PlaySound);
            //}
            //catch { }
            

        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {
                Android.App.AlertDialog.Builder message = new Android.App.AlertDialog.Builder(this);
                message.SetTitle("خروج");
                message.SetMessage("آیا از برنامه خارج میشود ؟");
                message.SetNegativeButton("خیر", delegate { return; });
                message.SetPositiveButton("بله", delegate { Finish(); });
                message.Create(); message.Show();
            }
            return base.OnKeyDown(keyCode, e);
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
          
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.navm_Run)
            {
                // Handle the camera action
            }
           

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        // من متد رو بازطراحی کردم از طریق virtual
        public virtual void OnInit(object sender, OperationResult status)
        {
            switch (status)
            {
                case OperationResult.Success:
                   
                    break;
                case OperationResult.Error:
                    View view = (View)sender;
                    Snackbar.Make(view, "Text To Speech Error", Snackbar.LengthLong).SetAction("TTS", (Android.Views.View.IOnClickListener)null).Show();
                    break;
                case OperationResult.Stopped:

                    break;
                default:
                    break;
            }
        }
        // link 
        public void OnInit([GeneratedEnum] OperationResult status)
        {
            OnInit(new object(), status);
        }
    }
}

