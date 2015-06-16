#import "AlertView.h"
#import "StringHelper.h"

#define CallbackFunctionName @"CallbackFunction"

#define YES_BUTTON      0
#define NO_BUTTON       1
#define OTHER_BUTTON    2

static AlertViewDelegate* delegateObject = nil;

@implementation AlertViewDelegate
{
    NSInteger buttonsCount;
}

@synthesize callbackObjectName, callbackFunctionName;

- (void)showAlertViewWithTitle:(NSString *)title message:(NSString *)message okButtonTitle:(NSString *)okButtonTitle  {
    buttonsCount = 1;
    UIAlertView *alertView = [[UIAlertView alloc] initWithTitle:title message:message delegate:self cancelButtonTitle:okButtonTitle otherButtonTitles:nil];
    
    [alertView show];
}

- (void)showYesNoAlertViewWithTitle:(NSString *)title message:(NSString *)message okButtonTitle:(NSString *)okButtonTitle cancelButtonTitle:(NSString *)cancelButtonTitle {
    buttonsCount = 2;
    UIAlertView *alertView = [[UIAlertView alloc] initWithTitle:title message:message delegate:self cancelButtonTitle:cancelButtonTitle otherButtonTitles:okButtonTitle, nil];
    
    [alertView show];
}

- (void)showYesNoOtherAlertViewWithTitle:(NSString *)title message:(NSString *)message okButtonTitle:(NSString *)okButtonTitle cancelButtonTitle:(NSString *)cancelButtonTitle otherButtonTitle:(NSString *)otherButtonTitle {
    buttonsCount = 3;
    UIAlertView *alertView = [[UIAlertView alloc] initWithTitle:title message:message delegate:self cancelButtonTitle:okButtonTitle otherButtonTitles:cancelButtonTitle, otherButtonTitle, nil];
    
    [alertView show];
}

- (void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex {
    NSInteger button = [self returnValueForButtonIndex:buttonIndex];
    UnitySendMessage([callbackObjectName UTF8String], [callbackFunctionName UTF8String], [StringHelper charFromInteger:button]);
}

- (NSInteger)returnValueForButtonIndex:(NSInteger)buttonIndex {
    if (buttonsCount == 3) {
        if (buttonIndex == 0) {
            return YES_BUTTON;
        } else if (buttonIndex == 1) {
            return NO_BUTTON;
        } else {
            return OTHER_BUTTON;
        }
    } else if (buttonsCount == 2) {
        if (buttonIndex == 1) {
            return YES_BUTTON;
        } else {
            return NO_BUTTON;
        }
    } else if (buttonsCount == 1) {
        return YES_BUTTON;
    }
    
    return OTHER_BUTTON;
}

@end

extern "C" {
    
    void _ShowAlertView (char *title, char *message, char *okButtonTitle, char *callbackObjectName)
    {
        if (delegateObject == nil)
            delegateObject = [[AlertViewDelegate alloc] init];
        
        delegateObject.callbackFunctionName = CallbackFunctionName;
        NSString *nsCallbackObjectName = [StringHelper stringFromChar:callbackObjectName];
        delegateObject.callbackObjectName = nsCallbackObjectName;
        if (!nsCallbackObjectName || [nsCallbackObjectName isEqualToString:@""]) {
            NSLog(@"[Native Plugin] Callback object name is not set");
        }

        NSString *nsTitle = [StringHelper stringFromChar:title];
        NSString *nsMessage = [StringHelper stringFromChar:message];
        NSString *nsOkButtonTitle = [StringHelper stringFromChar:okButtonTitle];
        if (!nsOkButtonTitle || [nsOkButtonTitle isEqualToString:@""]) {
            nsOkButtonTitle = [StringHelper localizedOkButton];
        }
        [delegateObject showAlertViewWithTitle:nsTitle message:nsMessage okButtonTitle:nsOkButtonTitle];
    }
    
    void _ShowYesNoAlertView (char* title, char *message, char *okButtonTitle, char *cancelButtonTitle, char *callbackObjectName)
    {
        if (delegateObject == nil)
            delegateObject = [[AlertViewDelegate alloc] init];
        
        delegateObject.callbackFunctionName = CallbackFunctionName;
        NSString *nsCallbackObjectName = [StringHelper stringFromChar:callbackObjectName];
        delegateObject.callbackObjectName = nsCallbackObjectName;
        if (!nsCallbackObjectName || [nsCallbackObjectName isEqualToString:@""]) {
            NSLog(@"[Native Plugin] Callback object name is not set");
        }

        NSString *nsTitle = [StringHelper stringFromChar:title];
        NSString *nsMessage = [StringHelper stringFromChar:message];
        
        NSString *nsOkButtonTitle = [StringHelper stringFromChar:okButtonTitle];
        if (!nsOkButtonTitle || [nsOkButtonTitle isEqualToString:@""]) {
            nsOkButtonTitle = [StringHelper localizedOkButton];
        }
        
        NSString *nsCancelButtonTitle = [StringHelper stringFromChar:cancelButtonTitle];
        if (!nsCancelButtonTitle || [nsCancelButtonTitle isEqualToString:@""]) {
            nsCancelButtonTitle = [StringHelper localizedCancelButton];
        }
        
        [delegateObject showYesNoAlertViewWithTitle:nsTitle message:nsMessage okButtonTitle:nsOkButtonTitle cancelButtonTitle:nsCancelButtonTitle];
    }
    
    void _ShowYesNoOtherAlertView (char *title, char *message, char *okButtonTitle, char *cancelButtonTitle, char *otherButtonTitle, char *callbackObjectName)
    {
        if (delegateObject == nil)
            delegateObject = [[AlertViewDelegate alloc] init];
        
        delegateObject.callbackFunctionName = CallbackFunctionName;
        NSString *nsCallbackObjectName = [StringHelper stringFromChar:callbackObjectName];
        delegateObject.callbackObjectName = nsCallbackObjectName;
        if (!nsCallbackObjectName || [nsCallbackObjectName isEqualToString:@""]) {
            NSLog(@"[Native Plugin] Callback object name is not set");
        }

        NSString *nsTitle = [StringHelper stringFromChar:title];
        NSString *nsMessage = [StringHelper stringFromChar:message];
        
        NSString *nsOkButtonTitle = [StringHelper stringFromChar:okButtonTitle];
        if (!nsOkButtonTitle || [nsOkButtonTitle isEqualToString:@""]) {
            nsOkButtonTitle = [StringHelper localizedOkButton];
        }
        
        
        NSString *nsCancelButtonTitle = [StringHelper stringFromChar:cancelButtonTitle];
        if (!nsCancelButtonTitle || [nsCancelButtonTitle isEqualToString:@""]) {
            nsCancelButtonTitle = [StringHelper localizedCancelButton];
        }
        
        NSString *nsOtherButtonTitle = [StringHelper stringFromChar:otherButtonTitle];
        
        [delegateObject showYesNoOtherAlertViewWithTitle:nsTitle message:nsMessage okButtonTitle:nsOkButtonTitle cancelButtonTitle:nsCancelButtonTitle otherButtonTitle:nsOtherButtonTitle];
    }
    
}