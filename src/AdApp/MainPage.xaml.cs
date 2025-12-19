using MiracleGamesAd;
using MiracleGamesAd.Models;
using System;
using System.Diagnostics;
using System.Net;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AdApp
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
        private const string FullScreenAdUnitId = "b871f83c5e8845f1b43325561bcdd6c7";     //开屏:1920 x 1080
        private const string ExitScreenAdUnitId = "5076eab6ae1042b6b92f73ea01981475";       //退屏:1920 x 1080
        private const string BannerAdUnitId = "cb7d9688a2d9499992febb6b642b3625";           //横幅:728 x 90
        private const string InterstitialAdUnitId = "2cb66a1301404561881a3f26b6ce5ba7";     //插屏:1024 x 768
        private const string CoupletAdUnitId = "b502f6e6281c43e4b28ea22503471039";          //对联:300 x 600
        private const string RewardAdUnitId = "2ae60936ba664fbfb7d92ce3a19c2915";           //激励广告:1024x768
        private const string FeedAdUnitId = "f152f6caf7a8440f8510bc31534baf4e";  //信息流，由开发者维护广告控件
        private const string EmbeddedAdUnitId = "4192966a9db343f48dd2f6308ea9ec30";         //嵌入式，由开发者维护广告控件

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ApplicationManager.OpenCmp(AppId, SecretKey);
            var result = await ApplicationManager.Initialize(AppId, SecretKey);
            if (result.ReturnValue)
            {
                AdvertisingManager.SetupExitunitId(ExitScreenAdUnitId);
                var ad = await AdvertisingManager.ShowAd(FullScreenAdUnitId, AdType.FullScreen);
                if (ad.ReturnValue)
                {
                }
            }
        }



        public async void OpenCmp()
        {
            await ApplicationManager.OpenCmp(AppId, SecretKey);
        }

        public async void ShowBannerAdDefault()
        {
            var bannerAd = await AdvertisingManager.ShowAd(BannerAdUnitId, AdType.Banner);
        }

        public async void ShowBannerAdImage()
        {
            var bannerAd = await AdvertisingManager.ShowAd(BannerAdUnitId, AdType.Banner, new BannerAdSettingOptions { MediaType = "image" });
        }

        public async void ShowBannerAdWeb()
        {
            var bannerAd = await AdvertisingManager.ShowAd(BannerAdUnitId, AdType.Banner, new BannerAdSettingOptions { MediaType = "web" });
        }

        public async void ShowInterstitialAdDefault()
        {
            var interstitialAd = await AdvertisingManager.ShowAd(InterstitialAdUnitId, AdType.Interstitial);
        }
        public async void ShowInterstitialAdImage()
        {
            var interstitialAd = await AdvertisingManager.ShowAd(InterstitialAdUnitId, AdType.Interstitial, new InterstitialAdSettingOptions { MediaType = "image"});
        }
        public async void ShowInterstitialAdWeb()
        {
            var interstitialAd = await AdvertisingManager.ShowAd(InterstitialAdUnitId, AdType.Interstitial, new InterstitialAdSettingOptions { MediaType = "web"});
        }
        public async void ShowInterstitialAdVideo()
        {
            var interstitialAd = await AdvertisingManager.ShowAd(InterstitialAdUnitId, AdType.Interstitial, new InterstitialAdSettingOptions { MediaType = "video"});
        }

        public async void ShowCoupletAdDefault()
        {
            var coupletAd = await AdvertisingManager.ShowAd(CoupletAdUnitId, AdType.Couplet);
        }
        public async void ShowCoupletAdImage()
        {
            var coupletAd = await AdvertisingManager.ShowAd(CoupletAdUnitId, AdType.Couplet, new CoupletAdSettingOptions { MediaType = "image"});
        }
        public async void ShowCoupletAdWeb()
        {
            var coupletAd = await AdvertisingManager.ShowAd(CoupletAdUnitId, AdType.Couplet, new CoupletAdSettingOptions { MediaType = "web"});
        }


        public async void ShowRewardAdDefault()
        {
            var json = "{\"coin\":100}";
            var rewardAd = await AdvertisingManager.ShowAd(RewardAdUnitId,
                AdType.Reward,
                new RewardAdSettingOptions
                {
                    MediaType = "video",//设置广告类型图片="image",网页="web",视频="video"
                    Comment = WebUtility.UrlEncode(json),//开发者自定义参数
                });
            if (rewardAd.Tag is RewardAdCompleteState completeState)
            {
                if (completeState.IsCompleted)
                {
                    //游戏逻辑发奖，然后报告核销
                    var comment = WebUtility.UrlDecode(completeState.Comment);
             
                }
            }

        }

        public async void ShowRewardAdWeb()
        {
            var rewardAd = await AdvertisingManager.ShowAd(RewardAdUnitId, AdType.Reward, new RewardAdSettingOptions {  MediaType = "web"} );
        }

        public async void ShowRewardAdVideo()
        {
            var rewardAd = await AdvertisingManager.ShowAd(RewardAdUnitId, AdType.Reward, new RewardAdSettingOptions {  MediaType = "video"} );
        }

        public async void ShowFeedAdDefault()
        {
            var feedAdSettingOptions = new FeedAdSettingOptions
            {
                Container = FeedContainer
            };
            var feed = await AdvertisingManager.ShowAd(FeedAdUnitId, AdType.Feed, feedAdSettingOptions);
        }

        public async void ShowEmbedAdDefault()
        {
            var embedAdSettingOptions = new EmbedAdSettingOptions
            {
                Container = EmbedContainer
            };
            var embed = await AdvertisingManager.ShowAd(EmbeddedAdUnitId, AdType.Embeded, embedAdSettingOptions);
        }
    }
}
