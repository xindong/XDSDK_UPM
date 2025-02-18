//
//  UIApplication+XDG.m
//  Unity-iPhone
//
//  Created by Fattycat on 2025/2/18.
//

#import "UIApplication+XDG.h"

#import <objc/runtime.h>

@implementation UIApplication_XDG
// 利用runtime将UIApplication的@selector(openURL:)改为iOS 10的那个

+ (void)load {
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        // 获取原始方法和替换方法
        Method originalMethod = class_getInstanceMethod(self, @selector(openURL:));
        Method swizzledMethod = class_getInstanceMethod(self, @selector(swizzled_openURL:));
        if (!originalMethod || !swizzledMethod) {
            return;
        }

        // 交换方法实现
        method_exchangeImplementations(originalMethod, swizzledMethod);
    });
}

// 替换的方法实现
- (void)swizzled_openURL:(NSURL *)url {
    // 在这里可以添加自定义逻辑
    NSLog(@"Swizzled openURL: %@", url);
    
    // 调用新的 openURL:options:completionHandler: 方法
    [self openURL:url options:@{} completionHandler:nil];
}
@end
