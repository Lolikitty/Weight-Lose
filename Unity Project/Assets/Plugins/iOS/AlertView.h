#ifdef __OBJC__
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#endif

@interface AlertViewDelegate : NSObject

- (void)showAlertViewWithTitle:(NSString *)title message:(NSString *)message okButtonTitle:(NSString *)okButtonTitle;
- (void)showYesNoAlertViewWithTitle:(NSString *)title message:(NSString *)message okButtonTitle:(NSString *)okButtonTitle cancelButtonTitle:(NSString *)cancelButtonTitle;
- (void)showYesNoOtherAlertViewWithTitle:(NSString *)title message:(NSString *)message okButtonTitle:(NSString *)okButtonTitle cancelButtonTitle:(NSString *)cancelButtonTitle otherButtonTitle:(NSString *)otherButtonTitle;

@property (nonatomic, strong) NSString *callbackFunctionName;
@property (nonatomic, strong) NSString *callbackObjectName;

@end