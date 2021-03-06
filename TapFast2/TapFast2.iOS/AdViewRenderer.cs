﻿using CoreGraphics;
using Google.MobileAds;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using TapFast2.Controls;

[assembly: ExportRenderer(typeof(TapFast2.Controls.AdView), typeof(TapFast2.iOS.AdViewRenderer))]
namespace TapFast2.iOS
{
    public class AdViewRenderer : ViewRenderer<Controls.AdView, BannerView>
    {
        string bannerId = "ca-app-pub-5416107198862625/4573203797";
        BannerView adView;
        BannerView CreateNativeControl()
        {
            if (adView != null)
                return adView;


            // Setup your BannerView, review AdSizeCons class for more Ad sizes. 
            adView = new BannerView(size: AdSizeCons.SmartBannerPortrait,
                                           origin: new CGPoint(0, UIScreen.MainScreen.Bounds.Size.Height - AdSizeCons.Banner.Size.Height))
            {
                AdUnitID = bannerId,
                RootViewController = GetVisibleViewController()
            };

            // Wire AdReceived event to know when the Ad is ready to be displayed
            adView.AdReceived += (object sender, EventArgs e) =>
            {
                //ad has come in
            };


            adView.LoadRequest(GetRequest());
            return adView;
        }

        Request GetRequest()
        {
            var request = Request.GetDefaultRequest();
            // Requests test ads on devices you specify. Your test device ID is printed to the console when
            // an ad request is made. GADBannerView automatically returns test ads when running on a
            // simulator. After you get your device ID, add it here
            request.TestDevices = new [] { Request.SimulatorId.ToString () };
            return request;
        }

        /// 
        /// Gets the visible view controller.
        /// 
        /// The visible view controller.
        UIViewController GetVisibleViewController()
        {
            var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            if (rootController.PresentedViewController == null)
                return rootController;

            if (rootController.PresentedViewController is UINavigationController)
            {
                return ((UINavigationController)rootController.PresentedViewController).VisibleViewController;
            }

            if (rootController.PresentedViewController is UITabBarController)
            {
                return ((UITabBarController)rootController.PresentedViewController).SelectedViewController;
            }

            return rootController.PresentedViewController;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                CreateNativeControl();
                SetNativeControl(adView);


            }
        }

        //protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs e)
        //{
        //    base.OnElementChanged(e);
           
        //}
    }
}
