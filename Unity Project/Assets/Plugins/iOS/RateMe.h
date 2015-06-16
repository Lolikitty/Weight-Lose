#ifdef __OBJC__
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#endif

@interface RateMeDelegate : NSObject

- (BOOL)shouldShowRateMeDialog;
- (void)showRateMeWithTitle:(NSString *)title message:(NSString *)message rateButtonTitle:(NSString *)rateButtonTitle laterButtonTitle:(NSString *)laterButtonTitle neverButtonTitle:(NSString *)neverButtonTitle;

@property (nonatomic, strong) NSString *appID;

@end