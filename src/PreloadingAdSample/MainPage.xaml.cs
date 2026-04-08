using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MiracleGamesAd;
using MiracleGamesAd.Models;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls.Primitives;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AdSample
{
   
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;


           
        }

        private const string AppId = "69316b6861328938223cc124";
        private const string SecretKey = "MIGTAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBHkwdwIBAQQgZgULOuiIDYZyGiUyYdGr3odHVN6ebZ1uDwXx7PXiHh2gCgYIKoZIzj0DAQehRANCAASf1FWCfsSn/tXFVRt04C7JkpRG12KSC3wnaJRWb5QWin9dsBk1OR31BCsELMYtWsFhA7e6Q6Fi4Mi6+ub24O5a";
        private const string FullScreenAdUnitId = "b871f83c5e8845f1b43325561bcdd6c7";             //Splash Ad:1920 x 1080
        private const string ExitScreenAdUnitId = "5076eab6ae1042b6b92f73ea01981475";                 //Exit Ad:1920 x 1080
        private const string BannerAdUnitId = "cb7d9688a2d9499992febb6b642b3625";               //Banner Ad:728 x 90
        private const string InterstitialAdUnitId = "2cb66a1301404561881a3f26b6ce5ba7";           //Interstitial Ad:1024 x 768
        private const string CoupletAdUnitId = "b502f6e6281c43e4b28ea22503471039";              //Couple Ad:300 x 600
        private const string RewardAdUnitId = "2ae60936ba664fbfb7d92ce3a19c2915";           //Rewarded Ad:1024x768
        private const string FeedAdUnitId = "f152f6caf7a8440f8510bc31534baf4e";                      //Feed，Developers need to maintain the advertising control.
        private const string EmbeddedAdUnitId = "4192966a9db343f48dd2f6308ea9ec30";         //Embedded，Developers need to maintain the advertising control.

        //private const string AppId = "692e5d6a207c9dd383ba56f7";
        //private const string SecretKey = "MIGTAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBHkwdwIBAQQg9pm4A6JkgQr7Xx5UUmX/NT+ZKM+ZF/2btAIBsdrJF76gCgYIKoZIzj0DAQehRANCAARrjJmtngZzxhRAa0Wn99ZN7QGf9ozmvghuvaicqFmA3j35XDkfXBgIqMTABogfpd+1LrAADeXkgOPzqw6b12my";

        //private const string FullScreenAdUnitId = "9ad41e3410084523a4f2a13ca65df395";
        ////Splash Ad:1920 x 1080
        //private const string ExitScreenAdUnitId = "a0e4a92613674feab0e3eaa36d1c17b8";
        ////Exit Ad:1920 x 1080
        //private const string BannerAdUnitId = "948ba7ccdfa34a0b8e2f96a66244c494";
        ////Banner Ad:728 x 90
        //private const string InterstitialAdUnitId = "10494292d2d9431691c3bebf0f35815c";
        ////Interstitial Ad:1024 x 768
        //private const string CoupletAdUnitId = "ef5f566b0eb14132987efbd57d6af60f";
        ////Couple Ad:300 x 600
        //private const string RewardAdUnitId = "a9bd7d57faef4f8cb016979284c86102";
        ////Rewarded Ad:1024x768
        //private const string FeedAdUnitId = "e13d9a6a4dbd42c2bd50561773dbda40";
        ////Feed，Developers need to maintain the advertising control.
        //private const string EmbeddedAdUnitId = "eb60d1e936c044adb696d9fa8147d590";
        ////Embedded，Developers need to maintain the advertising control

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ApplicationManager.OpenCmp(AppId, SecretKey);
            var result = await ApplicationManager.Initialize(AppId, SecretKey);
            if (result.ReturnValue)
            {
                AdvertisingManager.SetupExitunitId(ExitScreenAdUnitId);

            }
        }

        public async void PreloadFullScreenAd()
        {
            var fiAd = await AdvertisingManager.PreloadAd(FullScreenAdUnitId,AdType.FullScreen);
            if (fiAd.ReturnValue)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("全屏广告预加载完成", "成功");
                await messageDialog.ShowAsync();
            }
            else
            {
                var messageDialog1 = new Windows.UI.Popups.MessageDialog("全屏广告预加载失败", "失败");
                await messageDialog1.ShowAsync();
            }
        }
        public async void ShowPreloadFullScreenAd()
        {
            var fiAd = await AdvertisingManager.ShowPreloadAd(FullScreenAdUnitId, AdType.FullScreen);
            if (fiAd.ReturnValue)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("全屏广告展示完成", "成功");
                await messageDialog.ShowAsync();
            }
            else
            {
                var messageDialog1 = new Windows.UI.Popups.MessageDialog("全屏广告展示失败", "失败");
                await messageDialog1.ShowAsync();
            }
        }
        public async void PreloadBannerAd()
        {
            var bannerAd = await AdvertisingManager.PreloadAd(BannerAdUnitId, AdType.Banner);
            if (bannerAd.ReturnValue)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("横幅广告预加载完成", "成功");
                await messageDialog.ShowAsync();
            }
            else
            {
                var messageDialog1 = new Windows.UI.Popups.MessageDialog("横幅广告预加载失败", "失败");
                await messageDialog1.ShowAsync();
            }
        }
        public async void ShowPreloadBannerAd()
        {
            var bannerAd = await AdvertisingManager.ShowPreloadAd(BannerAdUnitId, AdType.Banner);
            if (bannerAd.ReturnValue)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("横幅广告展示完成", "成功");
                await messageDialog.ShowAsync();
            }
            else
            {
                var messageDialog1 = new Windows.UI.Popups.MessageDialog("横幅广告展示失败", "失败");
                await messageDialog1.ShowAsync();
            }
        }
        public async void PreloadInterstitialAd()
        {
            //广告位ID
            //

            var interstitialAd = await AdvertisingManager.PreloadAd(InterstitialAdUnitId, AdType.Interstitial);
            if (interstitialAd.ReturnValue)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("插屏广告预加载完成", "成功");
                await messageDialog.ShowAsync();
            }
            else
            {
                var messageDialog1 = new Windows.UI.Popups.MessageDialog("插屏广告预加载失败", "失败");
                await messageDialog1.ShowAsync();
            }
        }
        public async void ShowPreloadInterstitialAd()
        {
            
            var interstitialAd = await AdvertisingManager.ShowPreloadAd(InterstitialAdUnitId, AdType.Interstitial);
            if (interstitialAd.ReturnValue)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("插屏广告展示完成", "成功");
                await messageDialog.ShowAsync();
            }
            else
            {
                var messageDialog1 = new Windows.UI.Popups.MessageDialog("插屏广告展示失败", "失败");
                await messageDialog1.ShowAsync();
            }
        }
        public async void PreloadCoupletAd()
        {
            var coupletAd = await AdvertisingManager.PreloadAd(CoupletAdUnitId, AdType.Couplet);
            if (coupletAd.ReturnValue)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("对联广告预加载完成", "成功");
                await messageDialog.ShowAsync();
            }
            else
            {
                var messageDialog1 = new Windows.UI.Popups.MessageDialog("对联广告预加载失败", "失败");
                await messageDialog1.ShowAsync();
            }
        }
        public async void ShowPreloadCoupletAd()
        {
            var coupletAd = await AdvertisingManager.ShowPreloadAd(CoupletAdUnitId, AdType.Couplet);
            if (coupletAd.ReturnValue)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("对联广告展示完成", "成功");
                await messageDialog.ShowAsync();
            }
            else
            {
                var messageDialog1 = new Windows.UI.Popups.MessageDialog("对联广告展示失败", "失败");
                await messageDialog1.ShowAsync();
            }
        }
        public async void PreloadRewardAd()
        {
            var rewardAd = await AdvertisingManager.PreloadAd(RewardAdUnitId, AdType.Reward );
            if (rewardAd.ReturnValue)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("激励广告预加载完成", "成功");
                await messageDialog.ShowAsync();
            }
            else
            {
                var messageDialog1 = new Windows.UI.Popups.MessageDialog("激励广告预加载失败", "失败");
                await messageDialog1.ShowAsync();
            }
        }
        public async void ShowPreloadRewardAd()
        {
            var json = "{\"coin\":100}";
            var rewardAd = await AdvertisingManager.ShowPreloadAd(RewardAdUnitId,
                AdType.Reward,
                new RewardAdSettingOptions
                {
                    MediaType = "web",
                    Comment = WebUtility.UrlEncode(json),
                    
                });
            if (rewardAd.ReturnValue)
            {
                if (rewardAd.Tag is RewardAdCompleteState completeState)
                {
                    if (completeState.IsCompleted)
                    {
                        //游戏逻辑发奖，然后报告核销
                        var comment = WebUtility.UrlDecode(completeState.Comment);
                     

                        var messageDialog = new Windows.UI.Popups.MessageDialog("激励广告展示完成奖励已发放", "成功");
                        await messageDialog.ShowAsync();
                        return;
                    }

                    var messageDialog1 = new Windows.UI.Popups.MessageDialog("激励广告未展示，奖励未发放", "失败");
                    await messageDialog1.ShowAsync();
                }
            }
            else
            {

                var messageDialog2 = new Windows.UI.Popups.MessageDialog("激励广告未展示", "失败");
                await messageDialog2.ShowAsync();
            }
        }

        //public async void PreloadFeedAdDefault()
        //{
        //    var ret = await AdvertisingManager.PreloadAd(FeedAdUnitId, AdType.Feed);

        //    if (ret.ReturnValue)
        //    {
        //        var messageDialog = new Windows.UI.Popups.MessageDialog("信息流广告预加载完成", "成功");
        //        await messageDialog.ShowAsync();
        //    }
        //    else
        //    {
        //        var messageDialog1 = new Windows.UI.Popups.MessageDialog("信息流广告预加载失败", "失败");
        //        await messageDialog1.ShowAsync();
        //    }
        //}

        //public async void ShowPreloadFeedAdDefault()
        //{

        //    var feedAdSettingOptions = new FeedAdSettingOptions
        //    {
        //        Container = FeedContainer
        //    };
        //    var ret = await AdvertisingManager.ShowPreloadAd(FeedAdUnitId, AdType.Feed, feedAdSettingOptions);
        //    if (ret.ReturnValue)
        //    {

        //    }
        //}

        //public async void PreloadEmbedAdDefault()
        //{

        //    var ret = await AdvertisingManager.PreloadAd(EmbeddedAdUnitId, AdType.Embeded);
        //    if (ret.ReturnValue)
        //    {
        //        var messageDialog = new Windows.UI.Popups.MessageDialog("嵌入式广告预加载完成", "成功");
        //        await messageDialog.ShowAsync();
        //    }
        //    else
        //    {
        //        var messageDialog1 = new Windows.UI.Popups.MessageDialog("嵌入式广告预加载失败", "失败");
        //        await messageDialog1.ShowAsync();
        //    }
        //}
        //public async void ShowPreloadEmbedAdDefault()
        //{
        //    var embedAdSettingOptions = new EmbedAdSettingOptions
        //    {
        //        Container = EmbedContainer
        //    };
        //    var ret = await AdvertisingManager.ShowPreloadAd(EmbeddedAdUnitId, AdType.Embeded, embedAdSettingOptions);
        //    if (ret.ReturnValue)
        //    {

        //    }
        //}

        public async void ShowFullScreenAd()
        {
            var fiAd = await AdvertisingManager.ShowAd(FullScreenAdUnitId,AdType.FullScreen);
            if (fiAd.ReturnValue)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("全屏插播广告展示完成", "成功");
                await messageDialog.ShowAsync();
            }
            else
            {
                var messageDialog1 = new Windows.UI.Popups.MessageDialog("全屏插播广告展示失败", "失败");
                await messageDialog1.ShowAsync();
            }
        }
        public async void ShowBannerAd()
        {
            var bannerAd = await AdvertisingManager.ShowAd(BannerAdUnitId,AdType.Banner);
            if (bannerAd.ReturnValue)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("横幅广告展示完成", "成功");
                await messageDialog.ShowAsync();
            }
            else
            {
                var messageDialog1 = new Windows.UI.Popups.MessageDialog("横幅广告展示失败", "失败");
                await messageDialog1.ShowAsync();
            }
        }
        public async void ShowInterstitialAd()
        {

            var interstitialAd = await AdvertisingManager.ShowAd(InterstitialAdUnitId, AdType.Interstitial);
            if (interstitialAd.ReturnValue)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("插屏广告展示完成", "成功");
                await messageDialog.ShowAsync();
            }
            else
            {
                var messageDialog1 = new Windows.UI.Popups.MessageDialog("插屏广告展示失败", "失败");
                await messageDialog1.ShowAsync();
            }
        }
        public async void ShowCoupletAd()
        {
            var coupletAd = await AdvertisingManager.ShowAd(CoupletAdUnitId, AdType.Couplet);
            if (coupletAd.ReturnValue)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("对联广告展示完成", "成功");
                await messageDialog.ShowAsync();
            }
            else
            {
                var messageDialog1 = new Windows.UI.Popups.MessageDialog("对联广告展示失败", "失败");
                await messageDialog1.ShowAsync();
            }
        }
        public async void ShowRewardAd()
        {
            var json = "{\"coin\":100}";
            var rewardAd = await AdvertisingManager.ShowAd(RewardAdUnitId,
                AdType.Reward,
                new RewardAdSettingOptions
                {
                    MediaType = "web",
                    Comment = WebUtility.UrlEncode(json)
                });
            if (rewardAd.ReturnValue)
            {
                if (rewardAd.Tag is RewardAdCompleteState completeState)
                {
                    if (completeState.IsCompleted)
                    {
                        //游戏逻辑发奖，然后报告核销
                        var comment = WebUtility.UrlDecode(completeState.Comment);
             

                        var messageDialog = new Windows.UI.Popups.MessageDialog("激励广告展示完成奖励已发放", "成功");
                        await messageDialog.ShowAsync();
                        return;
                    }

                    var messageDialog1 = new Windows.UI.Popups.MessageDialog("激励广告未展示，奖励未发放", "失败");
                    await messageDialog1.ShowAsync();
                }
            }
            else
            {

                var messageDialog2 = new Windows.UI.Popups.MessageDialog("激励广告未展示", "失败");
                await messageDialog2.ShowAsync();
            }
        }
    }
   
}
