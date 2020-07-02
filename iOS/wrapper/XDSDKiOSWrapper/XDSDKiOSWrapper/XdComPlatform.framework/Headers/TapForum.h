//
//  TapForum.h
//  XdComPlatform
//
//  Created by JiangJiahao on 2020/6/19.
//  Copyright © 2020 X.D. Network Inc. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

typedef enum{
    TapForumOrientationDefault = -1,
    TapForumOrientationLandscape = 0,
    TapForumOrientationPortrait = 1,

} TapForumOrientation;

@protocol TapForumDelegate <NSObject>

-(void) onForumAppear;
-(void) onForumDisappear;

@end

@interface TapForumConfig: NSObject

@property (nonatomic, copy) NSString *appid;
@property (nonatomic) TapForumOrientation orientation;
@property (nonatomic, copy) NSString *uri;
@property (nonatomic, copy) NSLocale *locale;
@property (nonatomic, copy) NSString *site;

- (instancetype)initWithAppid:(NSString *)appid
                   orientation:(TapForumOrientation)orientation
                           uri:(NSString *)uri
                        locale:(NSLocale *)locale
                          site:(NSString *)site;
@end

@interface TapForum : NSObject

+ (void)setDelegate:(id <TapForumDelegate>)delegate;

/**
 *  @brief 打开TapTap论坛
 *  @param appid 游戏论坛ID
 */
+ (void)openTapTapForum:(NSString *)appid;

+ (void)openTapTapForum:(NSString *)appid uri:(NSString *)uri;

+ (void)openTapTapForum:(NSString *)appid uri:(NSString *)uri locale:(NSLocale *)locale;

+ (void)openTapTapForum:(NSString *)appid
            orientation:(TapForumOrientation)orientation
                    uri:(NSString *)uri
                 locale:(NSLocale *)locale;

+ (void)openTapTapForumWithConfig:(TapForumConfig *)config;

@end

NS_ASSUME_NONNULL_END
