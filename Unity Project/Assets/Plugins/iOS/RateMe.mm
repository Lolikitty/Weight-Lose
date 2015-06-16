#import "RateMe.h"
#import "StringHelper.h"

static RateMeDelegate* delegateObject = nil;

@implementation RateMeDelegate
{
    BOOL alreadyShowed;
}

@synthesize appID;

- (NSString *)keyForCurrentVersion
{
    NSString *base = @"Do Not Show Rate Me Dialog For Version ";
    NSString *appVersion = [[[NSBundle mainBundle] infoDictionary] objectForKey:@"CFBundleShortVersionString"];
    NSString *result = [base stringByAppendingString:appVersion];
    
    return result;
}

- (BOOL)shouldShowRateMeDialog
{
    return ![[NSUserDefaults standardUserDefaults] boolForKey:[self keyForCurrentVersion]];
}

- (void)showRateMeWithTitle:(NSString *)title message:(NSString *)message rateButtonTitle:(NSString *)rateButtonTitle laterButtonTitle:(NSString *)laterButtonTitle neverButtonTitle:(NSString *)neverButtonTitle
{
    UIAlertView *alertView = [[UIAlertView alloc] initWithTitle:title message:message delegate:self cancelButtonTitle:rateButtonTitle otherButtonTitles:neverButtonTitle, laterButtonTitle, nil];
    
    [alertView show];
}

- (NSString *)templateReviewURL
{
    if (floor(NSFoundationVersionNumber) > NSFoundationVersionNumber_iOS_7_1) {
        // iOS 8
        return @"itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?id=APP_ID&onlyLatestVersion=true&pageNumber=0&sortOrdering=1&type=Purple+Software";
        
    } else if (floor(NSFoundationVersionNumber) > NSFoundationVersionNumber_iOS_6_1) {
        // iOS 7
        return @"itms-apps://itunes.apple.com/app/idAPP_ID";
    } else {
        // iOS 6
        return @"itms-apps://ax.itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id=APP_ID";
    }
}

- (void)goToAppStoreRatingPage
{
    NSString *templateReviewURL = [self templateReviewURL];
    NSString *urlString = [templateReviewURL stringByReplacingOccurrencesOfString:@"APP_ID" withString:appID];
    NSLog(@"[Native Plugin] Processing to the App Store. URL: %@", urlString);
    NSURL *url = [NSURL URLWithString:urlString];
    [[UIApplication sharedApplication] openURL: url];
}

- (void)doNotShowDialogInFuture
{
    [[NSUserDefaults standardUserDefaults] setBool:YES forKey:[self keyForCurrentVersion]];
}

- (void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex {
    if (buttonIndex == 0) {
        // Rate app button pressed
        [self goToAppStoreRatingPage];
        [self doNotShowDialogInFuture];
    } else if (buttonIndex == 1) {
        // Never show dialog again button pressed
        [self doNotShowDialogInFuture];
    } else {
        // Later button pressed
    }
}

@end

extern "C" {
    void _ShowRateMe (char *appID, char *title, char *message, char *rateButtonTitle, char *laterButtonTitle, char *neverButtonTitle)
    {
        if (delegateObject == nil)
            delegateObject = [[RateMeDelegate alloc] init];
        
        if (![delegateObject shouldShowRateMeDialog]) {
            NSLog(@"[Native Plugin] You have already rated the app or decided to never show the dialog again. Rate Me dialog will not appear");
            return;
        }
        
        NSString *nsAppID = [StringHelper stringFromChar:appID];
        if (nsAppID == nil || [nsAppID isEqualToString:@""]) {
            NSLog(@"[Native Plugin] ShowRateMe failed. Set AppID before showing 'Rate Me' dialog");
            return;
        }
        
        delegateObject.appID = nsAppID;
        
        NSString *nsTitle = [StringHelper stringFromChar:title];
        NSString *nsMessage = [StringHelper stringFromChar:message];
        if (nsMessage == nil || [nsMessage isEqualToString:@""]) {
            nsMessage = @"If you enjoy our game, would you mind taking a moment to rate it? It won't take more than a minute. Thanks for your support!";
        }
        
        NSString *nsRateButtonTitle = [StringHelper stringFromChar:rateButtonTitle];
        if (nsRateButtonTitle == nil || [nsRateButtonTitle isEqualToString:@""]) {
            nsRateButtonTitle = @"Rate us";
        }
        
        NSString *nsLaterButtonTitle = [StringHelper stringFromChar:laterButtonTitle];
        if (nsLaterButtonTitle == nil || [nsLaterButtonTitle isEqualToString:@""]) {
            nsLaterButtonTitle = @"Remind me later";
        }
        
        NSString *nsNeverButtonTitle = [StringHelper stringFromChar:neverButtonTitle];
        if (nsNeverButtonTitle == nil || [nsNeverButtonTitle isEqualToString:@""]) {
            nsNeverButtonTitle = @"No, thanks";
        }
        
        [delegateObject showRateMeWithTitle:nsTitle message:nsMessage rateButtonTitle:nsRateButtonTitle laterButtonTitle:nsLaterButtonTitle neverButtonTitle:nsNeverButtonTitle];
    }
}