#import "StringHelper.h"

@implementation StringHelper

+ (const char *)charFromInteger:(NSInteger) value
{
    NSString *tmp = [NSString stringWithFormat:@"%d", value];
    return [tmp UTF8String];
}

+ (NSString *)stringFromChar:(const char*) string
{
    if (string)
        return [NSString stringWithUTF8String: string];
    else
        return @"";
}

+ (NSString *)localizedButtonForTitle:(NSString *) buttonTitle
{
    NSBundle *uiKitBundle = [NSBundle bundleWithIdentifier:@"com.apple.UIKit"];
    if (uiKitBundle == nil) {
        NSLog(@"[Native Plugin] Can't get localized string for '%@'", buttonTitle);
        return buttonTitle;
    }
    NSString *localizedButtonTitle = [uiKitBundle localizedStringForKey:buttonTitle value:nil table:nil];
    
    return localizedButtonTitle;
}

+ (NSString *)localizedOkButton {
    return [StringHelper localizedButtonForTitle:@"OK"];
}

+ (NSString *)localizedCancelButton {
    return [StringHelper localizedButtonForTitle:@"Cancel"];
}

+ (NSString *)localizedNoButton {
    return [StringHelper localizedButtonForTitle:@"No"];
}

@end