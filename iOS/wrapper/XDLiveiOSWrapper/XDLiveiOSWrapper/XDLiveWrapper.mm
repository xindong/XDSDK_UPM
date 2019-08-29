//
//  XDLiveWrapper.cpp
//  XDLiveiOSWrapper
//
//  Created by 孙毅 on 2018/10/22.
//  Copyright © 2018 xd. All rights reserved.
//

#include "XDLiveWrapper.h"
#include "XDLive/XDLive.h"


@interface XDLiveWrapper() <XDLiveDelegate>
@end


@implementation XDLiveWrapper

static XDLiveWrapper * instance;

+ (instancetype)defaultInstance{
    
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        instance = [[XDLiveWrapper alloc] init];
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


- (void)onXDLiveClosed {
//    NSLog(@"WRAPPER onXDLiveClosed");
        UnitySendMessage("XDLiveListener", "OnXDLiveClosed" , "");
}

- (void)onXDLiveOpen {
//    NSLog(@"WRAPPER onXDLiveOpen");
        UnitySendMessage("XDLiveListener", "OnXDLiveOpen" , "");
}

- (void)InvokeFuncCallback:(NSString *) resultString{
    UnitySendMessage("XDLiveListener", "InvokeFuncCallback", [resultString UTF8String]);
}


#if __cplusplus
extern "C" {
#endif
    void openXDLive(const char * appid){
        [XDLive setDelegate:[XDLiveWrapper defaultInstance]];
        [XDLive openXDLive:[NSString stringWithUTF8String:appid]];
    }
    
    void openXDLiveWithUri(const char * appid,const char * uri){
        [XDLive setDelegate:[XDLiveWrapper defaultInstance]];
        [XDLive openXDLive:[NSString stringWithUTF8String:appid] uri:[NSString stringWithUTF8String:uri]];
    }
    
    void closeXDLive() {
        [XDLive closeXDLive];
    }
    
    void invokeFunc(const char * unityCallbackID, const char * params) {
        NSString *unityCallbackIDString = [NSString stringWithUTF8String:unityCallbackID];
        NSString *paramsString = [NSString stringWithUTF8String:params];
        NSData *paramsData = [paramsString dataUsingEncoding:NSUTF8StringEncoding];
        NSError *err;
        NSDictionary *paramsDict = [NSJSONSerialization JSONObjectWithData:paramsData
                                                        options:NSJSONReadingMutableContainers
                                                              error:&err];
        [XDLive invokeFunc:paramsDict callback:^(NSDictionary *result) {
            NSError *error;
            NSMutableDictionary *mutableResult = [result mutableCopy];
            [mutableResult setObject:unityCallbackIDString forKey:@"unity_callback_id"];
            NSData *jsonData = [NSJSONSerialization dataWithJSONObject:mutableResult options:NSJSONWritingPrettyPrinted error:&error];
            NSString *resultString = [[NSString alloc]initWithData:jsonData encoding:NSUTF8StringEncoding];
            [[XDLiveWrapper defaultInstance] InvokeFuncCallback:resultString];
            
        }];
        
    }
#if __cplusplus
}
#endif




@end
