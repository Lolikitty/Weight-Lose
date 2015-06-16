#ifdef __OBJC__
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#endif

@interface StringHelper : NSObject

+ (const char *)charFromInteger:(NSInteger) value;
+ (NSString *)stringFromChar:(const char*) string;

+ (NSString *)localizedCancelButton;
+ (NSString *)localizedOkButton;
+ (NSString *)localizedNoButton;

@end