//
//  XDLiveWrapper.cpp
//  XDLiveiOSWrapper
//
//  Created by 孙毅 on 2018/10/22.
//  Copyright © 2018 xd. All rights reserved.
//

#include "XDTrafficControlWrapper.h"
#include <XDTrafficControl/XDTrafficControl.h>


@interface XDTrafficControlWrapper() <XDTrafficControlDelegate>
@end


@implementation XDTrafficControlWrapper

static XDTrafficControlWrapper * instance;

+ (instancetype)defaultInstance{
    
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        instance = [[XDTrafficControlWrapper alloc] init];
    });
    return instance;
}

#if defined(__cplusplus)
extern "C"{
#endif
    extern void UnitySendMessage(const char *, const char *, const char *);
#if defined(__cplusplus)
}
#endif

- (void)onQueuingFinished {
    UnitySendMessage("XDTrafficControlListener", "XDTrafficControlFinished" , "");
}

- (void)onQueuingFailed:(NSString *)errorMessage {
    UnitySendMessage("XDTrafficControlListener", "XDTrafficControlFailed" , errorMessage?errorMessage.UTF8String:"");
    
}

- (void)onQueuingCancelled {
    UnitySendMessage("XDTrafficControlListener", "XDTrafficControlCanceled" , "");
}


#if __cplusplus
extern "C" {
#endif
    void xdtcCheck(const char * appid){
        [XDTrafficControl setDelegate:[XDTrafficControlWrapper defaultInstance]];
        [XDTrafficControl check:[NSString stringWithUTF8String:appid]];
    }
#if __cplusplus
}
#endif




@end
