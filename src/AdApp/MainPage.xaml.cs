using MiracleGamesAd;
using MiracleGamesAd.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
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
            try
            {
                // ========= Step 1: Call OpenCmp (Core Interface) =========
                PopupCmpSettingOptions popupCmpSettingOptions = new PopupCmpSettingOptions();

                // false (Recommended): Popup only appears once after the user's first selection, compliant with GDPR
                // true: Popup appears every time the app starts, suitable for testing environments
                popupCmpSettingOptions.IgnoreExpiredCheck = false;

                var cmpResult = await ApplicationManager.OpenCmp(AppId, SecretKey, popupCmpSettingOptions);
                if (cmpResult.ReturnValue)
                {
                    // CMP popup displayed successfully
                    if (cmpResult.Tag is CmpResult cmpData)
                    {
                        if (cmpData.Success)
                        {
                            // User has made a selection, result data can be retrieved if needed 
                            ShowMessage($"This country needs CMP, CMP result={cmpData.Payload}");
                        }
                        // else: User did not make a selection, continue with the following process
                    }
                }
                else
                {
                    // Failed to display CMP popup, log the error if needed and continue initialization
                    ShowMessage($"The CMP pop-up window is not displayed for the following reasons: 1. CMP is not required in this country, and 2. The CMP pop-up does not appear on the second launch.");
                }

                // ========= Optional: Get Region CMP Requirement (Additional Interface, Used in Specific Scenarios) =========
                // bool isRequired = ApplicationManager.UserRegionCmpRequirement;

                // ========= Step 2: Call Initialize (Core Interface) =========
                var initResult = await ApplicationManager.Initialize(AppId, SecretKey);
                if (initResult.ReturnValue)
                {
                    ShowMessage($"Initialization complete:Token={ApplicationManager.AccessToken.Token}, ExpiresIn={ApplicationManager.AccessToken.ExpiresIn}");

                    // Initialization successful, advertising features are available

                    AdvertisingManager.SetupExitunitId(ExitScreenAdUnitId);
                    var ad = await AdvertisingManager.ShowAd(FullScreenAdUnitId, AdType.FullScreen);
                    if (ad.ReturnValue)
                    {
                    }
                }
                else
                {
                    ShowMessage("Initialization failed");
                    // Initialization failed, troubleshoot based on the error message
                    // Common reasons: network failure, VPN conflict, invalid AppKey/Secret, server exception
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async void OpenCmp()
        {
            try
            {
                bool isUserRegionCmpRequired = ApplicationManager.UserRegionCmpRequirement;
                ShowMessage($"CMP is required in this region, value={isUserRegionCmpRequired.ToString()}");

                //if (isUserRegionCmpRequired) // CMP is required for this region.
                {
                    PopupCmpSettingOptions popupCmpSettingOptions = new PopupCmpSettingOptions();
                    // false (Recommended): Popup only appears once after the user's first selection, compliant with GDPR
                    // true: Popup appears every time the app starts, suitable for testing environments
                    popupCmpSettingOptions.IgnoreExpiredCheck = true;

                    var cmpResult = await ApplicationManager.OpenCmp(AppId, SecretKey, popupCmpSettingOptions);
                    if (cmpResult.ReturnValue)
                    {
                        // CMP popup displayed successfully
                        if (cmpResult.Tag is CmpResult cmpData)
                        {
                            if (cmpData.Success)
                            {
                                // User has made a selection, result data can be retrieved if needed
                                string userChoiceData = cmpData.Payload;
                                ShowMessage($"This country needs CMP, CMP result={cmpData.Payload}");
                            }
                            // else: User did not make a selection, continue with the following process
                        }
                    }
                    else
                    {
                        ShowMessage($"The CMP pop-up window is not displayed for the following reasons: CMP is not required in this country");
                        // Failed to display CMP popup, log the error if needed and continue initialization
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        #region //ad example 
        public async void ShowFullScreenDefault()
        {
            try
            {
                ShowMessage($"Loading ad, UnitId = {FullScreenAdUnitId}");
                var result = await AdvertisingManager.ShowAd(FullScreenAdUnitId, AdType.FullScreen);
                if (result.ReturnValue)
                {
                    ShowMessage($"Ad display completed, UnitId = {FullScreenAdUnitId}"); 
                }
                else
                {
                    ShowMessage("The ad is not displaying. Please check whether the settings in the MG backend are correct.");
                }
            }
            catch (Exception)
            {
            }
        }

        public async void ShowFullScreenImage()
        {
            try
            {
                ShowMessage($"Loading ad, UnitId = {FullScreenAdUnitId}");
                /*MediaType supported types: web, video;
                  Generally, developers do not need to set this.
                  If no value is provided, a value is selected randomly based on the MG backend configuration.
                 */
                var result = await AdvertisingManager.ShowAd(FullScreenAdUnitId, AdType.FullScreen, new BannerAdSettingOptions { MediaType = "image" });
                if (result.ReturnValue)
                {
                    ShowMessage($"Ad display completed, UnitId = {FullScreenAdUnitId}");
                }
                else
                {
                    ShowMessage("The ad is not displaying. Please check whether the settings in the MG backend are correct.");
                }
            }
            catch (Exception)
            {
            }
        }

        public async void ShowFullScreenWeb()
        {
            try
            {
                ShowMessage($"Loading ad, UnitId = {FullScreenAdUnitId}");

                /*MediaType supported types: web, video;
                  Generally, developers do not need to set this.
                  If no value is provided, a value is selected randomly based on the MG backend configuration.
                 */
                var result = await AdvertisingManager.ShowAd(FullScreenAdUnitId, AdType.FullScreen, new BannerAdSettingOptions { MediaType = "web" });
                if (result.ReturnValue)
                {
                    ShowMessage($"Ad display completed, UnitId = {FullScreenAdUnitId}");
                }
                else
                {
                    ShowMessage("The ad is not displaying. Please check whether the settings in the MG backend are correct.");
                }
            }
            catch (Exception)
            {
            }
        }


        public async void ShowBannerAdDefault()
        {
            try
            {
                ShowMessage($"Loading ad, UnitId = {BannerAdUnitId}");
                var result = await AdvertisingManager.ShowAd(BannerAdUnitId, AdType.Banner);
                if (result.ReturnValue)
                {
                    ShowMessage($"Ad display completed, UnitId = {BannerAdUnitId}");
                }
                else
                {
                    ShowMessage("This ad is not displaying. Please check whether the settings in the MG backend are correct.");
                }
            }
            catch (Exception)
            {
            }
        }

        public async void CloseBannerAd()
        {
            var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                try
                {
                    AdvertisingManager.CloseAd(BannerAdUnitId);
                }
                catch (Exception)
                {
                }
            });
        }

        public async void ShowBannerAdImage()
        {
            try
            {
                ShowMessage($"Loading ad, UnitId = {BannerAdUnitId}");
                var result = await AdvertisingManager.ShowAd(BannerAdUnitId, AdType.Banner, new BannerAdSettingOptions { MediaType = "image" });
                if (result.ReturnValue)
                {
                    ShowMessage($"Ad display completed, UnitId = {BannerAdUnitId}");
                }
                else
                {
                    ShowMessage("This ad is not displaying. Please check whether the settings in the MG backend are correct.");
                }
            }
            catch (Exception)
            {
            } 
        }

        public async void ShowBannerAdWeb()
        {
            try
            {
                ShowMessage($"Loading ad, UnitId = {BannerAdUnitId}");
                var result = await AdvertisingManager.ShowAd(BannerAdUnitId, AdType.Banner, new BannerAdSettingOptions { MediaType = "web" });
                if (result.ReturnValue)
                {
                    ShowMessage($"Ad display completed, UnitId = {BannerAdUnitId}");
                }
                else
                {
                    ShowMessage("This ad is not displaying. Please check whether the settings in the MG backend are correct.");
                }
            }
            catch (Exception)
            {
            } 
        }
  

        public async void ShowInterstitialAdDefault()
        {
            try
            {
                ShowMessage($"Loading ad, UnitId = {InterstitialAdUnitId}");
                var result = await AdvertisingManager.ShowAd(InterstitialAdUnitId, AdType.Interstitial);
                if (result.ReturnValue)
                {
                    ShowMessage($"Ad display completed, UnitId = {InterstitialAdUnitId}");
                }
                else
                {
                    ShowMessage("This ad is not displaying. Please check whether the settings in the MG backend are correct.");
                }
            }
            catch (Exception)
            {
            } 
        }
        public async void ShowInterstitialAdImage()
        {
            try
            {
                ShowMessage($"Loading ad, UnitId = {InterstitialAdUnitId}");
                var result = await AdvertisingManager.ShowAd(InterstitialAdUnitId, AdType.Interstitial, new BannerAdSettingOptions { MediaType = "image" });
                if (result.ReturnValue)
                {
                    ShowMessage($"Ad display completed, UnitId = {InterstitialAdUnitId}");
                }
                else
                {
                    ShowMessage("This ad is not displaying. Please check whether the settings in the MG backend are correct.");
                }
            }
            catch (Exception)
            {
            } 
        }
        public async void ShowInterstitialAdWeb()
        {
            try
            {
                ShowMessage($"Loading ad, UnitId = {InterstitialAdUnitId}");
                var result = await AdvertisingManager.ShowAd(InterstitialAdUnitId, AdType.Interstitial, new BannerAdSettingOptions { MediaType = "web" });
                if (result.ReturnValue)
                {
                    ShowMessage($"Ad display completed, UnitId = {InterstitialAdUnitId}");
                }
                else
                {
                    ShowMessage("This ad is not displaying. Please check whether the settings in the MG backend are correct.");
                }
            }
            catch (Exception)
            {
            }
        }
        

        public async void ShowCoupletAdDefault()
        {
            try
            {
                ShowMessage($"Loading ad, UnitId = {CoupletAdUnitId}");
                var result = await AdvertisingManager.ShowAd(CoupletAdUnitId, AdType.Couplet);
                if (result.ReturnValue)
                {
                    ShowMessage($"Ad display completed, UnitId = {CoupletAdUnitId}");
                }
                else
                {
                    ShowMessage("This ad is not displaying. Please check whether the settings in the MG backend are correct.");
                }
            }
            catch (Exception)
            {
            }
        }
        public async void ShowCoupletAdImage()
        {
            try
            {
                ShowMessage($"Loading ad, UnitId = {CoupletAdUnitId}");
                var result = await AdvertisingManager.ShowAd(CoupletAdUnitId, AdType.Couplet, new BannerAdSettingOptions { MediaType = "image" });
                if (result.ReturnValue)
                {
                    ShowMessage($"Ad display completed, UnitId = {CoupletAdUnitId}");
                }
                else
                {
                    ShowMessage("This ad is not displaying. Please check whether the settings in the MG backend are correct.");
                }
            }
            catch (Exception)
            {
            } 
        }
        public async void ShowCoupletAdWeb()
        {
            try
            {
                ShowMessage($"Loading ad, UnitId = {CoupletAdUnitId}");
                var result = await AdvertisingManager.ShowAd(CoupletAdUnitId, AdType.Couplet, new BannerAdSettingOptions { MediaType = "web" });
                if (result.ReturnValue)
                {
                    ShowMessage($"Ad display completed, UnitId = {CoupletAdUnitId}");
                }
                else
                {
                    ShowMessage("This ad is not displaying. Please check whether the settings in the MG backend are correct.");
                }
            }
            catch (Exception)
            {
            } 
        }



        public async void ShowRewardAdDefault()
        {
            try
            {
                ShowMessage($"Loading ad, UnitId = {RewardAdUnitId}");

                var json = "{\"coin\":100}";
                var result = await AdvertisingManager.ShowAd(RewardAdUnitId, AdType.Reward,
                    new RewardAdSettingOptions
                    {
                        //MediaType = "video",//Supported types: web, video; Generally, developers do not need to configure this.;If no value is provided, a random selection is made based on the MG backend configuration.
                        Comment = WebUtility.UrlEncode(json),//Developer-Defined Parameters
                    });

                if (result.ReturnValue && result.Tag is RewardAdCompleteState completeState)
                {
                    if (completeState.IsCompleted)
                    {
                        ShowMessage($"Ad display completed, UnitId = {RewardAdUnitId}，When the user watches the video in its entirety, the reward logic is triggered.");

                        // When the user watches the video in its entirety, the reward logic is triggered.
                        var comment = WebUtility.UrlDecode(completeState.Comment);
                        // Claim Incentive Ad Rewards Through MG Services
                        AdvertisingManager.ReportAdRewardFulfillment(completeState.RewardId);
                    }
                    else
                    {
                        ShowMessage("If a user does not watch the entire video, no reward will be issued.");
                    }
                }
                else
                {
                    ShowMessage("This ad is not displaying. Please check whether the settings in the MG backend are correct.");
                }
            }
            catch (Exception)
            {
            }
        }

        public async void ShowRewardAdWeb()
        {
            try
            {
                ShowMessage($"Loading ad, UnitId = {RewardAdUnitId}");

                var json = "{\"coin\":100}";
                var result = await AdvertisingManager.ShowAd(RewardAdUnitId, AdType.Reward,
                    new RewardAdSettingOptions
                    {
                        MediaType = "web",
                        Comment = WebUtility.UrlEncode(json),//Developer-Defined Parameters
                    });

                if (result.ReturnValue && result.Tag is RewardAdCompleteState completeState)
                { 
                    if (completeState.IsCompleted)
                    {
                        ShowMessage($"Ad display completed, UnitId = {RewardAdUnitId}，When the user watches the video in its entirety, the reward logic is triggered.");

                        // When the user watches the video in its entirety, the reward logic is triggered.
                        var comment = WebUtility.UrlDecode(completeState.Comment);
                        // Claim Incentive Ad Rewards Through MG Services
                        AdvertisingManager.ReportAdRewardFulfillment(completeState.RewardId);
                    }
                    else
                    {
                        ShowMessage("If a user does not watch the entire video, no reward will be issued."); 
                    }
                }
                else
                {
                    ShowMessage("This ad is not displaying. Please check whether the settings in the MG backend are correct.");
                }
            }
            catch (Exception)
            {
            } 
        }

        public async void ShowRewardAdVideo()
        {
            try
            {
                ShowMessage($"Loading ad, UnitId = {RewardAdUnitId}");

                var json = "{\"coin\":100}";
                var result = await AdvertisingManager.ShowAd(RewardAdUnitId, AdType.Reward,
                    new RewardAdSettingOptions
                    {
                        MediaType = "video",
                        Comment = WebUtility.UrlEncode(json),//Developer-Defined Parameters
                    });

                if (result.ReturnValue && result.Tag is RewardAdCompleteState completeState)
                {
                    if (completeState.IsCompleted)
                    {
                        ShowMessage($"Ad display completed, UnitId = {RewardAdUnitId}，When the user watches the video in its entirety, the reward logic is triggered.");

                        // When the user watches the video in its entirety, the reward logic is triggered.
                        var comment = WebUtility.UrlDecode(completeState.Comment);
                        // Claim Incentive Ad Rewards Through MG Services
                        AdvertisingManager.ReportAdRewardFulfillment(completeState.RewardId);
                    }
                    else
                    {
                        ShowMessage("If a user does not watch the entire video, no reward will be issued.");
                    }
                }
                else
                {
                    ShowMessage("This ad is not displaying. Please check whether the settings in the MG backend are correct.");
                }
            }
            catch (Exception)
            {
            } 
        }

         

        public async void ShowFeedAdDefault()
        {
            try
            {
                ShowMessage($"Loading ad, UnitId = {FeedAdUnitId}");

                var adSettingOptions = new CustomAdSettingOptions
                {
                    Container = FeedContainer  // 开发者创建并维护的控件实例
                };
                var result = await AdvertisingManager.ShowAd(FeedAdUnitId, AdType.Custom, adSettingOptions);

                if (result.ReturnValue)
                {
                    ShowMessage($"Ad display completed, UnitId = {FeedAdUnitId}");
                }
                else
                {
                    ShowMessage("This ad is not displaying. Please check whether the settings in the MG backend are correct.");
                } 
            }
            catch (Exception)
            { 
            } 
        }

        public async void ShowEmbedAdDefault()
        {
            try
            {
                ShowMessage($"Loading ad, UnitId = {EmbeddedAdUnitId}");

                var adSettingOptions = new CustomAdSettingOptions
                {
                    Container = EmbedContainer  // 开发者创建并维护的控件实例
                };
                var result = await AdvertisingManager.ShowAd(EmbeddedAdUnitId, AdType.Custom, adSettingOptions);

                if (result.ReturnValue)
                {
                    ShowMessage($"Ad display completed, UnitId = {EmbeddedAdUnitId}");
                }
                else
                {
                    ShowMessage("This ad is not displaying. Please check whether the settings in the MG backend are correct.");
                }
            }
            catch (Exception)
            {
            } 
        }
        #endregion


        private async void ShowMessage(string msg)
        {
            try
            {
                StringBuilder sb = new StringBuilder(txtMessage.Text);
                sb.AppendLine(msg);

                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    txtMessage.Text = sb.ToString();
                });
            }
            catch (Exception)
            {
            }
        }

        #region //preload ad example
        private void EnableShowPreloadAdBtn(string adUnitid)
        {
            foreach (var item in gridPreloadAd.Children)
            {
                if (item is Button button)
                {
                    if (button.Tag.ToString().Contains($"SHOW_{adUnitid}"))
                    {
                        button.IsEnabled = true;
                        return;
                    }
                }
            }
        }

        private async void btnPreloadAd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btnPreload = (Button)sender;
                string btnTag = btnPreload.Tag.ToString();

                string adUnitId = btnTag.Split('_')[1];
                AdType adType = (AdType)Convert.ToInt32(btnTag.Split('_')[2]);

                ShowMessage($"Ad unit [{adUnitId}] [{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] Start Preloading...");

                var result = await AdvertisingManager.PreloadAd(adUnitId, adType);
                if (result.ReturnValue)
                {
                    EnableShowPreloadAdBtn(adUnitId);
                    ShowMessage($"Ad unit [{adUnitId}] [{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] materials are ready.");
                }
                else
                {
                    ShowMessage($"Ad unit [{adUnitId}] [{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] Preload Failed");
                } 
            }
            catch (Exception)
            {
                 
            }
        }

        private async void btnShowPreloadAd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string btnTag = btn.Tag.ToString();
                string adUnitId = btnTag.Split('_')[1];
                AdType adType = (AdType)Convert.ToInt32(btnTag.Split('_')[2]);

                var result = await AdvertisingManager.ShowPreloadAd(adUnitId, adType);
                if (result.ReturnValue)
                {
                    btn.IsEnabled = false;
                    ShowMessage($"Ad unit [{adUnitId}] [{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] Already Displayed.");
                }
                else
                {
                    ShowMessage($"Ad unit [{adUnitId}] [{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] Display Failed."); 
                } 
            }
            catch (Exception)
            { 
            }
        }

        public async void btnShowRewardedPreloadAd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var json = "{\"coin\":100}";
                var result = await AdvertisingManager.ShowPreloadAd(RewardAdUnitId, AdType.Reward,
                    new RewardAdSettingOptions
                    {
                        //MediaType = "video",//Supported types: web, video; Generally, developers do not need to configure this.;If no value is provided, a random selection is made based on the MG backend configuration.
                        Comment = WebUtility.UrlEncode(json),//Developer-Defined Parameters
                    });

                if (result.ReturnValue && result.Tag is RewardAdCompleteState completeState)
                {
                    if (completeState.IsCompleted)
                    {
                        ShowMessage($"Rewarded Ad Already Displayed, UnitId = {RewardAdUnitId}，When the user watches the video in its entirety, the reward logic is triggered.");

                        // When the user watches the video in its entirety, the reward logic is triggered.
                        var comment = WebUtility.UrlDecode(completeState.Comment);
                        // Claim Incentive Ad Rewards Through MG Services
                        AdvertisingManager.ReportAdRewardFulfillment(completeState.RewardId);
                    }
                    else
                    {
                        ShowMessage("If a user does not watch the rewarded video, no reward will be issued.");
                    }

                    Button btn = (Button)sender;
                    btn.IsEnabled = false;
                }
                else
                {
                    ShowMessage("This ad is not displaying. Please check whether the settings in the MG backend are correct.");
                } 
            }
            catch (Exception)
            {
            }
        }

        private async void btnShowFeedPreloadAd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string btnTag = btn.Tag.ToString();
                string adUnitId = btnTag.Split('_')[1];
                AdType adType = (AdType)Convert.ToInt32(btnTag.Split('_')[2]);

                var adSettingOptions = new CustomAdSettingOptions
                {
                    Container = FeedContainer  // Control instances created and maintained by developers
                };
                var result = await AdvertisingManager.ShowPreloadAd(FeedAdUnitId, AdType.Custom, adSettingOptions);
                if (result.ReturnValue)
                {
                    btn.IsEnabled = false;
                    ShowMessage($"Ad unit [{adUnitId}] [{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] Already Displayed.");
                }
                else
                {
                    ShowMessage($"Ad unit [{adUnitId}] [{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] Display Failed.");
                }
            }
            catch (Exception)
            {
            }
        }

        private async void btnShowEmbedPreloadAd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string btnTag = btn.Tag.ToString();
                string adUnitId = btnTag.Split('_')[1];
                AdType adType = (AdType)Convert.ToInt32(btnTag.Split('_')[2]);

                var adSettingOptions = new CustomAdSettingOptions
                {
                    Container = EmbedContainer  // Control instances created and maintained by developers
                };
                var result = await AdvertisingManager.ShowPreloadAd(FeedAdUnitId, AdType.Custom, adSettingOptions);
                if (result.ReturnValue)
                {
                    btn.IsEnabled = false;
                    ShowMessage($"Ad unit [{adUnitId}] [{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] Already Displayed.");
                }
                else
                {
                    ShowMessage($"Ad unit [{adUnitId}] [{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] Display Failed.");
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
